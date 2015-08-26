using System;
using System.IO;

namespace FileWatcherService.Configuration
{
    partial class FilePathValidatorCallbackValidatorClass
    {
        public static void Validate(object value)
        {
            if(value == null) throw new ArgumentNullException("value");
            var stringValue = value as string;
            if(stringValue == null) throw new ArgumentException("Value must be type of string","value");
            try
            {
                var fullPath = Path.GetFullPath(stringValue);
            }
            catch
            {
                throw new ArgumentException("Value is not valid directory path");
            }
        }
    }
}