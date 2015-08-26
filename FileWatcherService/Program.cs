using Topshelf;

namespace FileWatcherService
{
    static class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Service>();
                x.RunAsLocalSystem();
            });
        }
    }
}
