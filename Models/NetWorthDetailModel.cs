
namespace nw_api.Models
{
    public class NetWorthDetailModel
    {
        public CashDetailModel Cash { get; set; }
        public decimal CashTotal { get; set; }
        public InvestedAssetsDetailModel InvestedAssets { get; set; }
        public decimal InvestedAssetsTotal { get; set; }
        public UseAssetsDetailModel UseAssets { get; set; }
        public decimal UseAssetsTotal { get; set; }
        public LiabilitiesDetailModel Liabilities { get; set; }
        public decimal LiabilitiesTotal { get; set; }
        public decimal Total { get; set; }
    }
}