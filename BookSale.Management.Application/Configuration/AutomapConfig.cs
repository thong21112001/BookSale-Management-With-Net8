using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Application.DTOs.Checkout;
using BookSale.Management.Application.DTOs.Report;
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

            CreateMap<Book, BookDetailViewModel>()
               .ReverseMap();

            CreateMap<ApplicationUser, UserProfileDTO>()
               .ReverseMap();
            
            CreateMap<UserAddress, UserProfileDTO>()
               .ReverseMap();

            CreateMap<ApplicationUser, UserProfileViewModel>()
               .ReverseMap();

            CreateMap<CartRequestDTO, Cart>()
                .ForMember(dest => dest.Status, source => source.MapFrom(src => Convert.ToUInt16(src.Status)))
                .ReverseMap();

            CreateMap<Order, OrderRequestDTO>()
              .ReverseMap();

			CreateMap<ApplicationUser, OrderAddressDTO>()
				.ForMember(dest => dest.Name, source => source.MapFrom(src => src.Fullname))
				.ForMember(dest => dest.Phone, source => source.MapFrom(src => src.PhoneNumber))
			    .ReverseMap();

			CreateMap<OrderDetail, OrderDetailDTO>()
			  .ReverseMap();
		}

    }
}
