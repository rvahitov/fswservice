using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using FileWatcherService.Configuration;
using FileWatcherService.Utils;
using Topshelf;
using Topshelf.Logging;
using Directory = FileWatcherService.Configuration.Directory;

namespace FileWatcherService
{
    public class Service : ServiceControl
    {
        private FileWatcherServiceSection _configuration;
        private MailService _mailService;
        private List<Regex> _patterns;
        private string _serviceName;
        private IDisposable _subscriber;
        private List<FileSystemWatcher> _watchers;

        public bool Start(HostControl hostControl)
        {
            _configuration = FileWatcherServiceSection.Instance;
            _serviceName = _configuration.StopService.Name;
            _mailService = new MailService(_configuration.SendMail);


            _watchers = new List<FileSystemWatcher>();
            var observable = GenerateWatchers();

            GeneratePatterns();

            _subscriber = observable.ObserveOn(NewThreadScheduler.Default)
                .Where(evp => WatcherEventFilter(evp.EventArgs))
                .Subscribe(evp => OnFileSystemEvent(evp.EventArgs));
            foreach (var fileSystemWatcher in _watchers)
            {
                fileSystemWatcher.EnableRaisingEvents = true;
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (_subscriber != null)
            {
                _subscriber.Dispose();
            }
            if (_mailService != null)
            {
                _mailService.Dispose();
            }
            foreach (var fileSystemWatcher in _watchers)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Dispose();
            }

            return true;
        }

        private void GeneratePatterns()
        {
            _patterns = new List<Regex>();
            foreach (FilePattern filePattern in _configuration.Patterns)
            {
                try
                {
                    var regex = filePattern.Regex
                        ? new Regex(filePattern.Value, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)
                        : FilePatternToRegex.Convert(filePattern.Value);
                    _patterns.Add(regex);
                }
                catch (Exception e)
                {
                    var message = string.Format("GeneratePatterns Error: Patern = {0}, IsRegex = {1}", filePattern.Value,
                        filePattern.Regex);
                    HostLogger.Current.Get("Service").Error(message, e);
                }
            }
        }

        private IObservable<EventPattern<FileSystemEventArgs>> GenerateWatchers()
        {
            var observables = new List<IObservable<EventPattern<FileSystemEventArgs>>>();
            foreach (Directory directory in _configuration.Watch)
            {
                if (!System.IO.Directory.Exists(directory.Path)) continue;
                var watcher = new FileSystemWatcher(directory.Path)
                {
                    IncludeSubdirectories = directory.IncludeSubDirs,
                    EnableRaisingEvents = false
                };
                _watchers.Add(watcher);
                foreach (var listenerType in directory.GetListenerTypes())
                {
                    IObservable<EventPattern<FileSystemEventArgs>> observable;
                    switch (listenerType)
                    {
                        case ListenerType.Created:
                            observable =
                                Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                                    ev => watcher.Created += ev, ev => watcher.Created -= ev);
                            observables.Add(observable);
                            break;
                        case ListenerType.Changed:
                            observable = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                                ev => watcher.Changed += ev, ev => watcher.Changed -= ev);
                            observables.Add(observable);
                            break;
                        case ListenerType.Deleted:
                            observable = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                                ev => watcher.Deleted += ev, ev => watcher.Deleted -= ev);
                            observables.Add(observable);
                            break;
                        case ListenerType.Renamed:
                            var renamedObservable = Observable.FromEventPattern<RenamedEventHandler, RenamedEventArgs>(
                                ev => watcher.Renamed += ev, ev => watcher.Renamed -= ev);
                            observables.Add(
                                renamedObservable.Select(
                                    evpat => new EventPattern<FileSystemEventArgs>(evpat.Sender, evpat.EventArgs)));
                            break;
                    }
                }
            }

            var mergedObservables = observables.Merge();
            return mergedObservables;
        }

        private bool WatcherEventFilter(FileSystemEventArgs args)
        {
            if (_patterns.Count == 0) return true;
            var fileName = Path.GetFileName(args.Name);
            return _patterns.Any(rx => rx.IsMatch(fileName ?? ""));
        }

        private void OnFileSystemEvent(FileSystemEventArgs args)
        {
            var message = string.Format("FileSystemEvent: Type = {0}, FilePath = {1}", args.ChangeType, args.FullPath);
            HostLogger.Current.Get("Service").Info(message);
            ServiceControllerStatus? serviceStatus = null;
            try
            {
                var serviceController = new ServiceController(_serviceName);
                serviceStatus = serviceController.Status;
                if (serviceStatus == ServiceControllerStatus.Running)
                    serviceController.Stop();
            }
            catch (Exception e)
            {
                message = string.Format("OnFileSystemEvent: error on stoping service {0}", _serviceName);
                HostLogger.Current.Get("Service").Error(message, e);
            }
            try
            {
                _mailService.SendMessage(args,_serviceName,serviceStatus==null?"uknown":serviceStatus.Value.ToString());
            }
            catch (Exception e)
            {
                message = "Sending mail failed";
                HostLogger.Current.Get("Service").Error(message, e);
            }
        }
    }
}