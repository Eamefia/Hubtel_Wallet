using Microsoft.EntityFrameworkCore;

namespace Hubtel.Wallets.Api.Models
{
    public class HubtelWalletContext : DbContext
    {
        public HubtelWalletContext(DbContextOptions options): base(options) { }
        public DbSet<HubtelWalletDetailsModel> HubtelWalletDetails { get; set; }
    }
}
