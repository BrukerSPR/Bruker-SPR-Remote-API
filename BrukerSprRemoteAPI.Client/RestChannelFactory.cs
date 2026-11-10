namespace BrukerSprRemoteAPI.Client
{
#if NET8_0_OR_GREATER

    using System.Net.Http;

    public class RestChannelFactory(HttpClient httpClient)
    {
        private readonly HttpClient httpClient = httpClient;

        public void Close()
        {
            httpClient.Dispose();
        }
    }
#endif
}
