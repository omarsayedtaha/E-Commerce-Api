using AdminPanel.Models;
using AutoMapper;
using CoreLayer.Entities;

namespace AdminPanel.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product ,ProductViewModel>().ReverseMap();
        }
    }
}
