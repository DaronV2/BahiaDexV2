// <copyright file="Http.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BahiaDex.ApiClient.Common
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Net.Sockets;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The HTTP methods.
    /// </summary>
    public class Http
    {
        private static readonly SocketsHttpHandler SharedHandler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            PooledConnectionIdleTimeout = TimeSpan.FromSeconds(30),
            MaxConnectionsPerServer = 20,
        };

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };

        /// <summary>
        /// The method to post data.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="param">The parametes.</param>
        /// <returns>The Json.</returns>
        internal static async Task<string> HttpPost(string url, object param)
        {
            try
            {
                // Préparation de la requête
                using HttpClient client = CreateClient();

                // Envoi
                JsonContent content = JsonContent.Create(JsonConvert.SerializeObject(param));
                using HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            catch (Exception)
            {
                return "ERROR";
            }
        }

        internal static async Task<string> HttpGetStrict(string url, string? bearerToken)
        {
            using HttpClient client = CreateClient(bearerToken);
            using HttpResponseMessage response = await client.GetAsync(url);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"GET {url} a échoué avec {(int)response.StatusCode} {response.ReasonPhrase}. Réponse : {jsonResponse}",
                    null,
                    response.StatusCode);
            }

            return jsonResponse;
        }

        /// <summary>
        /// The method to get data.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The json.</returns>
        internal static async Task<string> HttpGet(string url)
            => await HttpGet(url, null);

        internal static async Task<string> HttpGet(string url, string? bearerToken)
        {
            try
            {
                // Préparation de la requête
                using HttpClient client = CreateClient(bearerToken);

                // Envoi
                using HttpResponseMessage response = await client.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            catch (Exception e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
        }

        /// <summary>
        /// Put method.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">Parameters.</param>
        /// <returns>JSON.</returns>
        internal static async Task<string> HttpPut(string url, object param)
        {
            for (int attempt = 0; attempt < 3; attempt++)
            {
                try
                {
                    // Préparation de la requête
                    using HttpClient client = CreateClient();

                    // Envoi
                    JsonContent content = JsonContent.Create(param, options: JsonSerializerOptions);
                    using HttpResponseMessage response = await client.PutAsync(url, content);
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return $"ERROR: {(int)response.StatusCode} {response.ReasonPhrase} - {jsonResponse}";
                    }

                    return jsonResponse;
                }
                catch (Exception ex) when (IsTransient(ex) && attempt < 2)
                {
                    await Task.Delay((attempt + 1) * 300);
                }
            }

            return "ERROR";
        }

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns>JSON.</returns>
        internal static async Task<bool> HttpDelete(string url)
        {
            try
            {
                // Préparation de la requête
                using HttpClient client = CreateClient();

                // Envoi
                using HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static HttpClient CreateClient(string? bearerToken = null)
        {
            HttpClient client = new(SharedHandler, disposeHandler: false)
            {
                BaseAddress = AndonLinks.BaseUri,
                DefaultRequestVersion = HttpVersion.Version11,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Timeout = TimeSpan.FromMinutes(5),
            };

            string token = bearerToken ?? AndonLinks.GetAuthToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        private static bool IsTransient(Exception ex)
            => ex is SocketException
            || ex is IOException
            || ex is TaskCanceledException
            || ex is HttpRequestException
            || ex.InnerException is SocketException
            || ex.InnerException is IOException;
    }
}
