using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig() 
        {
            CreateMap<ApplicationUser, CreateAccountDTO>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(src => src.PhoneNumber))    
                .ReverseMap();

            CreateMap<Genre, GenreDTO>()
                .ReverseMap();

            CreateMap<Genre, GenreViewModel>()
               .ReverseMap();

            CreateMap<Book, BookViewModel>()
               .ReverseMap();
        }

    }
}
