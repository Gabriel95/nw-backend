using System;

namespace nw_api.Data.Entities
{
    public class UseAssets
    {
        public Guid Id { get; set; }
        public decimal PrincipalHome { get; set; }
        public decimal VacationHome { get; set; }
        public decimal CarsTrucksBoats { get; set; }
        public decimal HomeFurnishings { get; set; }
        public decimal ArtAntiquesCoinsCollectibles { get; set; }
        public decimal JewelryFurs { get; set; }

        public decimal GetTotal()
        {
            return PrincipalHome + VacationHome + CarsTrucksBoats + HomeFurnishings + ArtAntiquesCoinsCollectibles +
                   JewelryFurs;
        }
    }
}