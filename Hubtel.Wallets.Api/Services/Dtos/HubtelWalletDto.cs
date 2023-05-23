using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Api.Services.Dtos
{
    public class HubtelWalletDto : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string AccountScheme { get; set; }
        [Required]  
        public string Owner { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
              
            if (!(Type.ToLower() == Constants.WalletTypeAsCard || Type.ToLower() == Constants.WalletTypeAsMomo))
            {
                yield return new ValidationResult("Type must be only " + Constants.WalletTypeAsCard + " or " + Constants.WalletTypeAsMomo, new[] { "Type" });
            }
            else if (Type.ToLower() == Constants.WalletTypeAsCard && 
                !(AccountScheme.ToLower() == Constants.WalletSchemeAsVisa || AccountScheme.ToLower() == Constants.WalletSchemeAsMastercard))
            {
                yield return new ValidationResult("Account scheme must be only " + 
                    Constants.WalletSchemeAsVisa + " or " + Constants.WalletSchemeAsMastercard, new[] { "AccountScheme" });
            }
            else if (Type.ToLower() == Constants.WalletTypeAsMomo &&
                !(AccountScheme.ToLower() == Constants.WalletSchemeAsMtn || AccountScheme.ToLower() == Constants.WalletSchemeAsVodafone || AccountScheme.ToLower() == Constants.WalletSchemeAsAirtelTigo))
            {
                yield return new ValidationResult("Account scheme must be only " +
                    Constants.WalletSchemeAsMtn + ", " + Constants.WalletSchemeAsVodafone + " or " + Constants.WalletSchemeAsAirtelTigo, new[] { "AccountScheme" });
            }
        }
    }
}
