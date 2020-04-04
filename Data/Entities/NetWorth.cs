using System;

namespace nw_api.Data.Entities
{
    public class NetWorth
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal Total { get; set; }
    }
}