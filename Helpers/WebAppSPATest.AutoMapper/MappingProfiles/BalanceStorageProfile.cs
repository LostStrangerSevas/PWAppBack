using AutoMapper;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using PWApp.ViewModels.Classes;

namespace PWApp.AutoMapper.MappingProfiles
{
    /// <summary>
    /// Класс профиля маппинга истории балансов транзакций
    /// </summary>
    public class BalanceStorageProfile : Profile
    {
        public BalanceStorageProfile()
        {
            CreateMap<BalanceStorageDto, BalanceStorage>().ReverseMap();
            CreateMap<BalanceStorageDto, BalanceStorageViewModel<int>>().ReverseMap();
        }
    }
}
