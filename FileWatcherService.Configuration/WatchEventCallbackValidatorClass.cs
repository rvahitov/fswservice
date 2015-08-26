using System;
using System.Collections.Generic;
using System.Linq;

namespace FileWatcherService.Configuration
{
    partial class WatchEventCallbackValidatorClass
    {
        public static readonly Dictionary<string, ListenerType> ListenerMaps =
            new Dictionary<string, ListenerType>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"created",ListenerType.Created},
                {"deleted", ListenerType.Deleted},
                {"changed",ListenerType.Changed},
                {"renamed",ListenerType.Renamed}
            };
        public static void Validate(object value)
        {
            if (value == null) throw new ArgumentNullException("value");
            var stringValue = value as string;
            if (stringValue == null) throw new ArgumentException("Value must be type of string", "value");
            if(string.IsNullOrWhiteSpace(stringValue)) throw new Exception("Value of Event attribute can't be empty");
            var parts = stringValue.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
            foreach (var part in parts)
            {
                if (!ListenerMaps.ContainsKey(part))
                {
                    throw new Exception(string.Format("'{0}' is illegal value for watch event type",part));
                }
            }
        }

    }
}