using System;
using System.IO;

namespace FileWatcherService.Utils.TemplateValueProviders
{
    public interface ITemplateValueProvider
    {
        string TemplateKey { get; }
        string GetTemplateValue(TemplateValueSource valueSource);
    }

    internal class EventValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "Event"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return valueSource.FileSystemEventArgs.ChangeType.ToString();
        }
    }

    internal class FileNameValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "FileName"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return Path.GetFileName(valueSource.FileSystemEventArgs.FullPath);
        }
    }

    internal class FullPathNameValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "FullPath"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return valueSource.FileSystemEventArgs.FullPath;
        }
    }

    internal class OldNameValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "OldName"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            var renamedArgs = valueSource.FileSystemEventArgs as RenamedEventArgs;
            return renamedArgs == null ? valueSource.FileSystemEventArgs.Name : renamedArgs.OldName;
        }
    }

    internal class OldFullPathValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "OldFullPath"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            var renamedArgs = valueSource.FileSystemEventArgs as RenamedEventArgs;
            return renamedArgs == null ? valueSource.FileSystemEventArgs.FullPath : renamedArgs.OldFullPath;
        }
    }

    internal class OccurredValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "Occurred"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return DateTime.Now.ToString("G");
        }
    }

    internal class MachineNameValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "MachineName"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return Environment.MachineName;
        }
    }

    internal class ServiceNameValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "ServiceName"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return valueSource.ServiceToStopName;
        }
    }

    internal class ServiceStatusValueProvider : ITemplateValueProvider
    {
        public string TemplateKey
        {
            get { return "ServiceStatus"; }
        }

        public string GetTemplateValue(TemplateValueSource valueSource)
        {
            return valueSource.ServiceToStopStatus;
        }
    }
}