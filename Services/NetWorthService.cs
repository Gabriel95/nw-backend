using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using nw_api.Data.Entities;
using nw_api.Data.Interfaces;
using nw_api.Interfaces;
using nw_api.Models;

namespace nw_api.Services
{
    public class NetWorthService : INetWorthService
    {
        private readonly ICashRepository _cashRepository;
        private readonly IInvestedAssetsRepository _investedAssetsRepository;
        private readonly IUseAssetsRepository _useAssetsRepository;
        private readonly ILiabilitiesRepository _liabilitiesRepository;
        private readonly IMapper _mapper;
        private readonly INetWorthRepository _netWorthRepository;

        public NetWorthService(ICashRepository cashRepository, IInvestedAssetsRepository investedAssetsRepository, IUseAssetsRepository useAssetsRepository, IMapper mapper, INetWorthRepository netWorthRepository, ILiabilitiesRepository liabilitiesRepository)
        {
            _cashRepository = cashRepository;
            _investedAssetsRepository = investedAssetsRepository;
            _useAssetsRepository = useAssetsRepository;
            _mapper = mapper;
            _netWorthRepository = netWorthRepository;
            _liabilitiesRepository = liabilitiesRepository;
        }

        public void AddNetWorth(NetWorthModel netWorthModel)
        {
            var cash = new Cash {Id = Guid.NewGuid()};
            var investedAssets = new InvestedAssets{Id = Guid.NewGuid()};
            var useAssets = new UseAssets{Id = Guid.NewGuid()};
            var liabilities = new Liabilities { Id = Guid.NewGuid()};

            _mapper.Map(netWorthModel, cash);
            _mapper.Map(netWorthModel, investedAssets);
            _mapper.Map(netWorthModel, useAssets);
            _mapper.Map(netWorthModel, liabilities);
            
            var netWorth = new NetWorth
            {
                Id = Guid.NewGuid(),
                UserId = netWorthModel.UserId,
                DateTimeCreated = DateTime.Now,
                Total = cash.GetTotal() + investedAssets.GetTotal() + useAssets.GetTotal() - liabilities.GetTotal()
            };

            _netWorthRepository.InsertNetWorth(netWorth);
            _cashRepository.InsertCash(cash, netWorth.Id);
            _investedAssetsRepository.InsertInvestedAssets(investedAssets, netWorth.Id);
            _useAssetsRepository.InsertUseAssets(useAssets, netWorth.Id);
            _liabilitiesRepository.InsertLiabilities(liabilities, netWorth.Id);
        }

        public CurrentNetWorthModel GetCurrentNetWorth(Guid userId)
        {
            var top2NetWorth = _netWorthRepository.GetNetWorths(userId, amount: 2).ToArray();
            var model = new CurrentNetWorthModel{CurrentNetWorthDate = null, PreviousNetWorthDate = null};
            if (top2NetWorth.Length <= 0)
                return model;
            var currentNetWorth = top2NetWorth[0];
            model.CurrentNetWorthDate = currentNetWorth.DateTimeCreated.ToString("dd/MM/yyyy");
            model.CurrentNetWorth = currentNetWorth.Total;

            if (top2NetWorth.Length <= 1 || top2NetWorth[1].Total == 0)
                return model;

            var previousNetWorth = top2NetWorth[1];
            model.PreviousNetWorthDate = previousNetWorth.DateTimeCreated.ToString("dd/MM/yyyy");
            model.Increase = ((currentNetWorth.Total - previousNetWorth.Total) / previousNetWorth.Total) * 100;
            return model;
        }

        public IEnumerable<NetWorth> GetAllNetWorths(Guid userId)
        {
            return _netWorthRepository.GetAllNetWorths(userId);
        }

        public NetWorth DeleteNetWorth(Guid userId, Guid netWorthId)
        {
            return _netWorthRepository.DeleteNetWorth(userId, netWorthId);
        }

        public NetWorthDetailModel GetNetWorthById(Guid userId, Guid netWorthId)
        {
            var netWorthDetailModel = new NetWorthDetailModel();
            try
            {
                var netWorth = _netWorthRepository.GetNetWorth(userId, netWorthId);
                var cash = _cashRepository.GetCash(netWorth.Id);
                var investedAssets = _investedAssetsRepository.GetInvestedAssets(netWorth.Id);
                var useAssets = _useAssetsRepository.GetUseAssets(netWorth.Id);
                var liabilities = _liabilitiesRepository.GetLiabilities(netWorth.Id);

                var cashDetailModel = new CashDetailModel();
                var investedAssetsDetailModel = new InvestedAssetsDetailModel();
                var useAssetsDetailModel = new UseAssetsDetailModel();
                var liabilitiesDetailModel = new LiabilitiesDetailModel();

                _mapper.Map(cash, cashDetailModel);
                _mapper.Map(investedAssets, investedAssetsDetailModel);
                _mapper.Map(useAssets, useAssetsDetailModel);
                _mapper.Map(liabilities, liabilitiesDetailModel);
                
                netWorthDetailModel.Total = netWorth.Total;
                netWorthDetailModel.CashTotal = cash.GetTotal();
                netWorthDetailModel.Cash = cashDetailModel;
                netWorthDetailModel.InvestedAssetsTotal = investedAssets.GetTotal();
                netWorthDetailModel.InvestedAssets = investedAssetsDetailModel;
                netWorthDetailModel.UseAssetsTotal = useAssets.GetTotal();
                netWorthDetailModel.UseAssets = useAssetsDetailModel;
                netWorthDetailModel.LiabilitiesTotal = liabilities.GetTotal();
                netWorthDetailModel.Liabilities = liabilitiesDetailModel;
                
                return netWorthDetailModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}