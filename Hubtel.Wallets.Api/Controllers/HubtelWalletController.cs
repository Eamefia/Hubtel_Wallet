using Hubtel.Wallets.Api.Helpers;
using Hubtel.Wallets.Api.Models;
using Hubtel.Wallets.Api.Services;
using Hubtel.Wallets.Api.Services.Dtos;
using Hubtel.Wallets.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Hubtel.Wallets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubtelWalletController : ControllerBase
    {
        private readonly IHubtelWallet _hubtelWalletService;
        public HubtelWalletController(IHubtelWallet hubtelWalletService)
        {
            this._hubtelWalletService = hubtelWalletService;
        }

        //This endpoint is use to create wallet, it calls the CreateWallet wallet method from the service
        //This endpoint will have a url which looks like baseUrl/api/hubtelwallet
        [HttpPost]
        public ActionResult Create(HubtelWalletDto walletDetailsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                ApiResponseObj response = new ApiResponseObj();
            try
            {   
               var createdWallet = _hubtelWalletService.CreateWallet(walletDetailsModel);
                
                    response.Error = createdWallet.Error;
                    response.Message = createdWallet.StatusMessage;
                    response.Data = createdWallet.hubtelWallet;
                    response.Code = createdWallet.Code;
                
               

                return StatusCode(response.Code, response);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = Messaging.ExceptionMessaging;
                response.Data = null;
                response.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(response.Code, response);
            }

        }

        //This endpoint is use to get all wallets of a single user using the wallet owner's number
        //it calls the GetAllOwnerWallet wallet method from the service
        //This endpoint will have a url which looks like baseUrl/api/hubtelwallet/{owner}/wallets
        [HttpGet("{owner}/wallets")    ]
        public ActionResult GetAllWallet(string owner) {

            if (owner == null)
            {
                return BadRequest();
            }
            ApiResponseObj response = new ApiResponseObj();

            try
            {
                
                var wallets = _hubtelWalletService.GetAllOwnerWallet(owner);
                foreach (var wallet in wallets)
                {
                    if (wallet.hubtelWallet == null)
                    {
                        response.Error = true;
                        response.Message = Messaging.OwnerNoData;
                        response.Data = null;
                        response.Code = StatusCodes.Status404NotFound;

                        return StatusCode(response.Code, response);
                    }
                   
                }
                response.Data = wallets;
              


                return StatusCode(response.Code, response.Data);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = Messaging.ExceptionMessaging;
                response.Data = null;
                response.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(response.Code, response);
            }  
        }


        //This endpoint is use to get all wallets for the admin
        //it calls the GetAllWallet wallet method from the service
        //This endpoint will have a url which looks like baseUrl/api/hubtelwallet
        [HttpGet]
        public ActionResult GetWalletList()
        {
            ApiResponseObj response = new ApiResponseObj();
            try
            {
                var wallets = _hubtelWalletService.GetAllWallet();
                foreach (var wallet in wallets)
                {
                    if (wallet.hubtelWallet == null)
                    {
                        response.Error = true;
                        response.Message = Messaging.OwnerNoData;
                        response.Data = null;
                        response.Code = StatusCodes.Status404NotFound;

                        return StatusCode(response.Code, response);
                    }

                }
                
                response.Data = wallets; 

                return StatusCode(response.Code, response.Data);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = Messaging.ExceptionMessaging;
                response.Data = null;
                response.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(response.Code, response);
            }
        }


        //This endpoint is use to get a single wallet with a specific Id
        //it calls the GetWallet wallet method from the service
        //This endpoint will have a url which looks like baseUrl/api/{Id}
        [HttpGet ("{Id}")]
        public ActionResult GetWallet(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            ApiResponseObj response = new ApiResponseObj();


            try
            {
                var Wallet = _hubtelWalletService.GetWallet(Id);
                
                    response.Error = Wallet.Error;
                    response.Message = Wallet.StatusMessage;
                    response.Data = Wallet.hubtelWallet;
                    response.Code = Wallet.Code;
                
                

                return StatusCode(response.Code, response);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = Messaging.ExceptionMessaging;
                response.Data = null;
                response.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(response.Code, response);
            }
        }


        //This endpoint is use to delete a single wallet with a specific Id
        //it calls the GetWallet wallet method from the service
        //This endpoint will have a url which looks like baseUrl/api/{Id}
        [HttpDelete("{Id}")]
        public ActionResult DeleteWallet(int Id) {

            if (Id == 0)
            {
                return BadRequest();
            }

            ApiResponseObj response = new ApiResponseObj();

            try
            {
                var Wallet = _hubtelWalletService.DeleteWallet(Id);


               
                    response.Error = Wallet.Error;
                    response.Message = Wallet.StatusMessage;
                    response.Data = null;
                    response.Code = Wallet.Code;
                
                

                return StatusCode(response.Code, response);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = Messaging.ExceptionMessaging;
                response.Data = null;
                response.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(response.Code, response);
            }
        }
    }
}
