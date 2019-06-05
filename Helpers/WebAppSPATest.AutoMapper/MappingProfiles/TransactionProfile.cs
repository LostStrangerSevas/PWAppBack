using AutoMapper;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using PWApp.ViewModels.Classes;
using System.Linq;

namespace PWApp.AutoMapper.MappingProfiles
{
    /// <summary>
    /// Класс профиля маппинга транзакции
    /// </summary>
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDto, Transaction>().ReverseMap();
            CreateMap<TransactionDto, TransactionViewModel<int>>().ReverseMap();
            CreateMap<TransactionCommonDto, TransactionCommonViewModel<int>>().ReverseMap();
            /*CreateMap<Transaction, TransactionCommonDto>()
                .ForMember("Sender", opt => opt.MapFrom(c => c.BalanceStorages.ToList().FirstOrDefault(i => i.IsSender).User.))
                .ForMember("Recipient", opt => opt.MapFrom(c => c.BalanceStorages.ToList().FirstOrDefault(i => !i.IsSender)));*/
        }
    }
}
