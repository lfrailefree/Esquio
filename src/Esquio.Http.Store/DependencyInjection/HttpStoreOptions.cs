﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace Esquio.Http.Store.DependencyInjection
{
    /// <summary>
    /// The http store options
    /// </summary>
    public sealed class HttpStoreOptions
    {
        internal bool CacheEnabled = false;
        internal TimeSpan? AbsoluteExpirationRelativeToNow = null;
        internal TimeSpan? SlidingExpiration = null;
        /// <summary>
        /// Configure if cache is enabled on distributed store.
        /// </summary>
        /// <param name="enabled">If True distributed store use default IDistributedStore configured on container. Else, cache is not enabled.</param>
        /// <param name="absoluteExpirationRelativeToNow">The absolute expiration time.</param>
        /// <param name="slidingExpiration">The sliding expiration time.</param>
        /// <returns>The same configuration to be chained.</returns>
        public HttpStoreOptions UseCache(bool enabled = false, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null)
        {
            CacheEnabled = enabled;
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            SlidingExpiration = slidingExpiration;

            return this;
        }

        internal Uri BaseAddress = null;
        /// <summary>
        /// Configure distributed store base address.
        /// </summary>
        /// <param name="uri">The distributed store base address to be used.</param>
        /// <returns>The same configuration to be chained.</returns>
        public HttpStoreOptions UseBaseAddress(Uri uri)
        {
            BaseAddress = uri;
            return this;
        }

        /// <summary>
        /// Configure Esquio base address.
        /// </summary>
        /// <param name="uri">The Esquio base address to be used.</param>
        /// <returns>The same configuration to be chained.</returns>
        public HttpStoreOptions UseBaseAddress(string uri)
        {
            BaseAddress = new Uri(uri);
            return this;
        }


        internal string ApiKey = null;
        /// <summary>
        /// Configure Esquio api key.
        /// </summary>
        /// <param name="apiKey">The Esquio store api key to be used.</param>
        /// <returns>The same configuration to be chained.</returns>
        public HttpStoreOptions UseApiKey(string apiKey)
        {
            ApiKey = apiKey;
            return this;
        }


        internal TimeSpan Timeout = TimeSpan.FromSeconds(100); 
        /// <summary>
        /// Configure Esquio api key.
        /// </summary>
        /// <param name="timeout">The maximiun time than distributed store wait for server response.</param>
        /// <returns>The same configuration to be chained.</returns>
        public HttpStoreOptions SetTimeout(TimeSpan timeout)
        {
            Timeout = timeout;
            return this;
        }

        internal Action<IHttpClientBuilder> EsquioHttpClientConfigurer;
        /// <summary>
        /// Allow to configure the HttpClient used internally on Esquio. You can setup your retry policies, inner handlers etc
        /// here or register a new client with name "ESQUIO" and perform your custom setup.
        /// </summary>
        /// <param name="esquioHttpClientConfigurer">The action used to configure the internal HTTP Client used </param>
        /// <returns>The same configuration to be chained</returns>
        public HttpStoreOptions ConfigureHttpClient(Action<IHttpClientBuilder> esquioHttpClientConfigurer)
        {
            EsquioHttpClientConfigurer = esquioHttpClientConfigurer;
            return this;
        }
    }
}
