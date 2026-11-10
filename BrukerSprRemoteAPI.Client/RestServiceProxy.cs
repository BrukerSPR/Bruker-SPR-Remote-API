namespace BrukerSprRemoteAPI.Client
{
#if NET8_0_OR_GREATER

    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Transparent REST proxy which forwards every call of <see cref="IBrukerSprRemoteService"/>
    /// to the corresponding http endpoint of the remote service. No WCF is involved.
    /// </summary>
    public class RestServiceProxy : DispatchProxy
    {
        private HttpClient? httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public static IBrukerSprRemoteService Create(HttpClient httpClient)
        {
            object proxy = Create<IBrukerSprRemoteService, RestServiceProxy>();
            ((RestServiceProxy)proxy).httpClient = httpClient;
            return (IBrukerSprRemoteService)proxy;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            string requestUri = BuildRequestUri(targetMethod, args);

            if (this.httpClient?.GetAsync(requestUri).GetAwaiter().GetResult() is not { } response)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (targetMethod is not { } ||
                targetMethod.ReturnType == typeof(void))
            {
                return null;
            }

            return JsonSerializer.Deserialize(json, targetMethod.ReturnType, JsonOptions);
        }

        /// <summary>
        /// The remote service publishes every operation as a http GET using the method name as
        /// relative path and the parameter names as query string (same as WebGet does).
        /// </summary>
        private static string BuildRequestUri(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod is not { })
            {
                return string.Empty;
            }

            string uri = targetMethod.Name;

            ParameterInfo[] parameters = targetMethod.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Name is not { } name ||
                    args is not { } ||
                    FormatValue(args[i]) is not { } arg)
                {
                    continue;
                }

                uri += i == 0 ? "?" : "&";
                uri += $"{Uri.EscapeDataString(name)}={Uri.EscapeDataString(arg)}";
            }

            return uri;
        }

        private static string? FormatValue(object? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is bool boolValue)
            {
                return boolValue ? "true" : "false";
            }

            if (value is string[] stringArray)
            {
                return string.Join(",", stringArray);
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }
    }
#endif
}
