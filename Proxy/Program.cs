using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace ProxyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ISearchFeature searcher = new MetadataSearch();
            ScanDoc("./some.pdf", new string[]{ "dog", "cat"}, searcher);

            // ... refactor
            searcher = new ProxyMetadataSearch(Thread.CurrentPrincipal, true, TimeSpan.FromMinutes(5));
            ScanDoc("./some.pdf", new string[]{ "dog", "cat"}, searcher);
        }

        private static void ScanDoc(string path, string[] keywordList, ISearchFeature searcher)
        {
            //...
            searcher.SearchInsideOf(path, keywordList);
            //...
        }
    }

    internal interface ISearchFeature
    {
        void SearchInsideOf(string path, string[] keywordList);
    }

    public class MetadataSearch : ISearchFeature
    {
        public void SearchInsideOf(string path, string[] keywordList)
        {
            ///... scanning metadata
        }
    }

    public class ProxyMetadataSearch : ISearchFeature
    {
        IPrincipal principal;
        private bool doCheckLicenseOnline;
        private TimeSpan retentionPolicy;

        IDictionary<string, DateTime> cache = new Dictionary<string, DateTime>();

        public ProxyMetadataSearch(IPrincipal principal, bool doCheckLicenseOnline, TimeSpan retentionPolicy)
        {
            this.principal = principal;
            this.doCheckLicenseOnline = doCheckLicenseOnline;
            this.retentionPolicy = retentionPolicy;
        }

        public void SearchInsideOf(string path, string[] keywordList)
        {
            var isAuthorized = principal.IsInRole("DocReviewer");
            if (!isAuthorized)
            {
                return;
            }

            if(cache.ContainsKey(path) && cache[path] < DateTime.UtcNow - retentionPolicy)
            {
                // use cached values
            }
            
            //...
        }
    }
}
