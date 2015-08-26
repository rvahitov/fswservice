using System;
using System.Collections.Generic;
using System.Linq;

namespace FileWatcherService.Configuration
{
    partial class Directory
    {
        public ListenerType[] GetListenerTypes()
        {
            var types = Event.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            var result = types.Select(t=>t.Trim()).Where(t => WatchEventCallbackValidatorClass.ListenerMaps.ContainsKey(t))
                .Select(t => WatchEventCallbackValidatorClass.ListenerMaps[t])
                .ToArray();

            return result;
        }
    }

    partial class WatchCollection : IEnumerable<Directory>
    {
        IEnumerator<Directory> IEnumerable<Directory>.GetEnumerator()
        {
            return BaseGetAllKeys().Select(key => this[key]).GetEnumerator();
        }
    }
}