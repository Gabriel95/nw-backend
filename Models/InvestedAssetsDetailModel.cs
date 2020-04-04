namespace nw_api.Models
{
    public class InvestedAssetsDetailModel
    {
        public decimal Brokerage { get; set; }
        public decimal Ira { get; set; }
        public decimal RothIra { get; set; }
        public decimal K401 { get; set; }
        public decimal SepIra { get; set; }
        public decimal Keogh { get; set; }
        public decimal Pension { get; set; }
        public decimal Annuity { get; set; }
        public decimal RealEstate { get; set; }
        public decimal SoleProprietorship { get; set; }
        public decimal Partnership { get; set; }
        public decimal CCorporation { get; set; }
        public decimal SCorporation { get; set; }
        public decimal LimitedLiabilityCompany { get; set; }
    }
}