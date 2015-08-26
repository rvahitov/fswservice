using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileWatcherService.Utils.TemplateValueProviders
{
    public class TemplateValueProviders : KeyedCollection<string, ITemplateValueProvider>
    {
        private static readonly Lazy<TemplateValueProviders> _singletonInstance =
            new Lazy<TemplateValueProviders>(CreateFromThisAssembly);

        protected TemplateValueProviders()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public static TemplateValueProviders Instance
        {
            get { return _singletonInstance.Value; }
        }

        protected override string GetKeyForItem(ITemplateValueProvider item)
        {
            return item.TemplateKey;
        }

        private static TemplateValueProviders CreateFromThisAssembly()
        {
            var _templateProviderInterfaceType = typeof (ITemplateValueProvider);
            var assembly = _templateProviderInterfaceType.Assembly;
            var collection = new TemplateValueProviders();

            var providers = from t in assembly.DefinedTypes
                            where !t.IsInterface && _templateProviderInterfaceType.IsAssignableFrom(t)
                select (ITemplateValueProvider) Activator.CreateInstance(t);

            foreach (var provider in providers)
            {
                collection.Add(provider);
            }

            return collection;
        }

        public bool TryGet(string key, out ITemplateValueProvider provider)
        {
            provider = null;
            if (!Contains(key)) return false;
            provider = this[key];
            return true;
        }
    }
}