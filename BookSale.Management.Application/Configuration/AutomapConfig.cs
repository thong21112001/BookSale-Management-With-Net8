using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Book;
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
                .ForMember(dest => dest.Image, opt => opt.Ignore()) //Bỏ map dữ liệu Image
                .ReverseMap();

            CreateMap<Book, BookDTO>()
               .ReverseMap();

            CreateMap<Book, BookForCart>()
               .ReverseMap();
        }

    }
}
