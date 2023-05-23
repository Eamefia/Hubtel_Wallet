using Hubtel.Wallets.Api.Helpers;
using Hubtel.Wallets.Api.Models;
using System;

namespace Hubtel.Wallets.Api.Services.Response
{
    public class HubtelWalletDetailsResponse
    {
        public object hubtelWallet { get; set; }
        public string StatusMessage { get; set; }
        public bool Error { get; set; }
        public int  Code { get; set; }
    }
}
