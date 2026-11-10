namespace BrukerSprRemoteAPI.Client
{
#if NET8_0_OR_GREATER
    internal class BrukerSprRemoteAPIclient_net8
    {
        /// <summary>
        /// Default address of the REST (json) endpoint of the remote service.
        /// </summary>
        public const string DefaultAddress = "http://localhost:9001/bruker-spr/json/";

        private readonly HttpClient httpClient;

        /// <summary>
        /// Proxy implementing the service contract and performing the REST calls.
        /// </summary>
        public IBrukerSprRemoteService Channel { get; }

        public RestChannelFactory ChannelFactory { get; }

        public BrukerSprRemoteAPIclient_net8(string address)
        {
            if (!address.EndsWith('/'))
            {
                address += "/";
            }

            httpClient = new HttpClient { BaseAddress = new Uri(address) };
            Channel = RestServiceProxy.Create(httpClient);
            ChannelFactory = new RestChannelFactory(httpClient);
        }

        public BrukerSprRemoteAPIclient_net8()
        : this(DefaultAddress)
        {
        }

        public void Close()
        {
            httpClient.Dispose();
        }
    }
#endif
}
