using AutoMapper;
using BusinessLogic.Models.ClassesDto;
using DataAccessLayer.Models.Classes;
using PWApp.ViewModels.Classes;

namespace PWApp.AutoMapper.MappingProfiles
{
    /// <summary>
    /// Класс профиля маппинга баланса
    /// </summary>
    public class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            CreateMap<BalanceDto, Balance>().ReverseMap();
            CreateMap<BalanceDto, BalanceViewModel<int>>().ReverseMap();
        }
    }
}
