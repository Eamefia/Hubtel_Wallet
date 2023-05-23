using System;

namespace Hubtel.Wallets.Api.Helpers
{
   

    public class ApiResponseObj
    {
        public bool Error { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
