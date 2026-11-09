// <copyright file="BasLinks.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BahiaDex.ApiClient.Common
{
    using Microsoft.Extensions.Configuration;
    // using UamLibraryClientApi.Data;

    /// <summary>
    /// The ANDON links.
    /// </summary>
    public class AndonLinks
    {

        /// <summary>
        /// Gets or sets the base URI for the BAS API.
        /// </summary>
        internal static Uri? BaseUri { get; set; }

        /// <summary>
        /// Gets the token provider function.
        /// </summary>
        internal static Func<string>? AuthTokenProvider { get; private set; }

        /// <summary>
        /// Explicit mappings for each known API type. Compare by type name to avoid direct dependency on definitions.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>The uri.</returns>
        public static Uri ObjectUri<T>()
        {
            string typeName = typeof(T).Name;

            return new(BaseUri ?? new(string.Empty), $"{typeName}/");
        }

        /// <summary>
        /// Sets the token provider function.
        /// </summary>
        /// <param name="tokenProvider">The token provider function.</param>
        public static void SetAuthTokenProvider(Func<string> tokenProvider)
        {
            AuthTokenProvider = tokenProvider;
        }

        /// <summary>
        /// Initialize the BaseUri from configuration.
        /// </summary>
        /// <param name="configuration">The config string.</param>
        public static void Initialize(IConfiguration configuration)
        {
            BaseUri = new(configuration["Api:Uri:And"] ?? string.Empty);
        }

        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <returns>The JWT token as a string.</returns>
        internal static string GetAuthToken()
            => AuthTokenProvider?.Invoke() ?? string.Empty;

        /// <summary>
        /// Get by identifier.
        /// </summary>
        /// <param name="url">The uri.</param>
        /// <param name="id">The id.</param>
        /// <returns>The link.</returns>
        internal static string GetById(Uri url, Guid? id) => $"{url}{(id == null ? string.Empty : id)}";

        /// <summary>
        /// Detele by identifier.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="id">The id.</param>
        /// <returns>The link.</returns>
        internal static string DeleteById(Uri url, Guid? id) => $"{url}{(id == null ? string.Empty : id)}";

        /// <summary>
        /// Update list link.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The link.</returns>
        internal static string UpdateList(Uri url) => $"{url}list";

        /// <summary>
        /// Update link.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The link.</returns>
        internal static string Update(Uri url) => $"{url}";
    }
}
