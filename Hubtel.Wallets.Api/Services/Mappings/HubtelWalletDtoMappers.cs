using Hubtel.Wallets.Api.Models;
using Hubtel.Wallets.Api.Services.Dtos;
using System;

namespace Hubtel.Wallets.Api.Services.Mappings
{
    public static partial class HubtelWalletDtoMappers
    {
        public static HubtelWalletDetailsModel MapHubtelWalletDtoToHubtelWalletDetailsModel(this HubtelWalletDto dto) {
            var walletDetails = new HubtelWalletDetailsModel
            {
                Name= dto.Name,
                Type= dto.Type,
                AccountNumber= dto.AccountNumber,
                AccountScheme= dto.AccountScheme,
                Owner= dto.Owner,
                CreatedAt = DateTime.Now
            };
            return walletDetails;   
        }
    }
}
