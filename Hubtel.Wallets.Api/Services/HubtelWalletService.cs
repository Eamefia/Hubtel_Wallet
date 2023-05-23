using Hubtel.Wallets.Api.Helpers;
using Hubtel.Wallets.Api.Models;
using Hubtel.Wallets.Api.Services.Dtos;
using Hubtel.Wallets.Api.Services.Interface;
using Hubtel.Wallets.Api.Services.Mappings;
using Hubtel.Wallets.Api.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Services
{
    public class HubtelWalletService : IHubtelWallet
    {
        private readonly HubtelWalletContext _hubtelContext;

        public HubtelWalletService(HubtelWalletContext context)
        {
            _hubtelContext = context;
        }



        public HubtelWalletDetailsResponse CreateWallet(HubtelWalletDto hubtelWalletDetails)
        {
            if (hubtelWalletDetails.Type == Constants.WalletTypeAsCard)
            {
                hubtelWalletDetails.AccountNumber = hubtelWalletDetails.AccountNumber.PadRight(6).Substring(0, 6);
            }

            int  allWalletCount = _hubtelContext.HubtelWalletDetails.Count(c => c.Owner.Equals(hubtelWalletDetails.Owner));

            bool isDuplicateWallet = _hubtelContext.HubtelWalletDetails.Any(c => c.AccountScheme.Equals(hubtelWalletDetails.AccountScheme)

            && c.AccountNumber.Equals(hubtelWalletDetails.AccountNumber));

            

            var hubtelWallet = HubtelWalletDtoMappers.MapHubtelWalletDtoToHubtelWalletDetailsModel(hubtelWalletDetails);

            var response = new HubtelWalletDetailsResponse();


            if (!isDuplicateWallet && allWalletCount < 5) {
               
                _hubtelContext.HubtelWalletDetails.Add(hubtelWallet);
                _hubtelContext.SaveChanges();
                response.hubtelWallet = hubtelWallet;
                response.StatusMessage = Messaging.SuccessfulMessageType;
                response.Code = StatusCodes.Status200OK;
                response.Error = false;
            }
            else if (allWalletCount >= 5) {
                response.hubtelWallet = hubtelWallet;
                response.StatusMessage = Messaging.walletLimit;
                response.Code = StatusCodes.Status400BadRequest;
                response.Error = true;
            }
            else
            {
                response.hubtelWallet = hubtelWallet;
                response.StatusMessage = Messaging.duplicateWallet;
                response.Code = StatusCodes.Status400BadRequest;
                response.Error = true;
            }

            return response;
            
        }



        public HubtelWalletDetailsResponse DeleteWallet(int Id)
        {
            var walletDetails = _hubtelContext.HubtelWalletDetails.FirstOrDefault(c => c.Id.Equals(Id));

            var res = new HubtelWalletDetailsResponse();

            if (walletDetails != null)
            {
                _hubtelContext.Remove(walletDetails);
                _hubtelContext.SaveChangesAsync();

                res.StatusMessage = Messaging.DeletedResource;
                res.Code = StatusCodes.Status200OK;
                res.Error = false;
                res.hubtelWallet = walletDetails;
            }
            else
            {
                res.StatusMessage = Messaging.NoData;
                res.Code = StatusCodes.Status400BadRequest;
                res.Error = true;
                res.hubtelWallet = walletDetails;
            }


            return res; 
        }



        public IEnumerable<HubtelWalletDetailsResponse> GetAllOwnerWallet(string owner)
        {
            IEnumerable<HubtelWalletDetailsModel> allWallet =  _hubtelContext.HubtelWalletDetails.Where(c => c.Owner.Equals(owner));

            List<HubtelWalletDetailsResponse>  allWalletResponse = new List<HubtelWalletDetailsResponse>();

            HubtelWalletDetailsResponse res = new HubtelWalletDetailsResponse();

            if (!allWallet.Any())
            {
                res.hubtelWallet = null;
                res.StatusMessage = Messaging.OwnerNoData;
                res.Code = StatusCodes.Status404NotFound;
                res.Error = true;
                allWalletResponse.Add(res);


                return allWalletResponse;
            }

            foreach (var wallet in allWallet) {
                
                
                res.hubtelWallet = wallet;  
                res.StatusMessage = Messaging.SuccessfulMessageType; 
                res.Code = StatusCodes.Status200OK;
                res.Error = false;

                allWalletResponse.Add(res);
            }


            return allWalletResponse;
        }

        public IEnumerable<HubtelWalletDetailsResponse> GetAllWallet()
        {
            IEnumerable<HubtelWalletDetailsModel> allWallet = _hubtelContext.HubtelWalletDetails.ToList();

            List<HubtelWalletDetailsResponse> allWalletResponse = new List<HubtelWalletDetailsResponse>();

            foreach (var wallet in allWallet)
            {
                HubtelWalletDetailsResponse res = new HubtelWalletDetailsResponse();
                res.hubtelWallet = wallet;
                res.StatusMessage = Messaging.SuccessfulMessageType;
                res.Code = StatusCodes.Status200OK;
                res.Error = false;
                allWalletResponse.Add(res);
            }

            return allWalletResponse;
        }



        public HubtelWalletDetailsResponse GetWallet(int Id)
        {
            HubtelWalletDetailsModel wallet =  _hubtelContext.HubtelWalletDetails.SingleOrDefault(c => c.Id.Equals(Id));

            var response = new HubtelWalletDetailsResponse();
            if (wallet != null)
            {
                response.hubtelWallet = wallet;
                response.StatusMessage = Messaging.SuccessfulMessageType;
                response.Code = StatusCodes.Status200OK;
                response.Error = false;
            }
            else
            {
                response.hubtelWallet = null;
                response.StatusMessage = Messaging.NoData;
                response.Code = StatusCodes.Status404NotFound;
                response.Error = true;
            }

            return response;
        }
    }
}
