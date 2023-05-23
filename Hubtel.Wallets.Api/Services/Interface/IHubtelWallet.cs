using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Hubtel.Wallets.Api.Models;
using Hubtel.Wallets.Api.Services.Dtos;
using Hubtel.Wallets.Api.Services.Response;

namespace Hubtel.Wallets.Api.Services.Interface
{
    public interface IHubtelWallet
    {
        IEnumerable<HubtelWalletDetailsResponse> GetAllOwnerWallet(string owner);
        IEnumerable<HubtelWalletDetailsResponse> GetAllWallet();
        HubtelWalletDetailsResponse CreateWallet(HubtelWalletDto walletDetails);
        HubtelWalletDetailsResponse GetWallet(int Id);
        HubtelWalletDetailsResponse DeleteWallet(int Id);
    }
}
