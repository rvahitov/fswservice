using System.IO;

namespace FileWatcherService.Utils.TemplateValueProviders
{
    public class TemplateValueSource
    {
        public TemplateValueSource(FileSystemEventArgs fileSystemEventArgs, string serviceToStopName, string serviceToStopStatus)
        {
            FileSystemEventArgs = fileSystemEventArgs;
            ServiceToStopName = serviceToStopName;
            ServiceToStopStatus = serviceToStopStatus;
        }

        public string ServiceToStopName { get; private set; }
        public string ServiceToStopStatus { get; private set; }
        public FileSystemEventArgs FileSystemEventArgs { get; private set; }
    }
}