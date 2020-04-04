using AutoMapper;
using nw_api.Data.Entities;
using nw_api.Models;

namespace nw_api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NetWorthModel, Cash>();
            CreateMap<NetWorthModel, InvestedAssets>();
            CreateMap<NetWorthModel, Liabilities>();
            CreateMap<NetWorthModel, UseAssets>();
            CreateMap<Cash, CashDetailModel>();
            CreateMap<InvestedAssets, InvestedAssetsDetailModel>();
            CreateMap<Liabilities, LiabilitiesDetailModel>();
            CreateMap<UseAssets, UseAssetsDetailModel>();
        }
    }
}