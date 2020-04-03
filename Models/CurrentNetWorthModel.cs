using System;

namespace nw_api.Models
{
    public class CurrentNetWorthModel
    {
        public decimal CurrentNetWorth { get; set; }
        public decimal Increase { get; set; }
        public string? CurrentNetWorthDate { get; set; }
        public string? PreviousNetWorthDate { get; set; }
    }
}