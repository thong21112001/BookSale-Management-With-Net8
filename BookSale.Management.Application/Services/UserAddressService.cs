using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Management.Application.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserAddressService(UserManager<ApplicationUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Get list address user
        public async Task<IEnumerable<UserProfileDTO>> GetAllListAddressUser(string id)
        {
            var listAddressUser = await _unitOfWork.UserAddressRepository.GetAllListAddressUser(id);

            var result = _mapper.Map<IEnumerable<UserProfileDTO>>(listAddressUser);

            return result;
        }

        //Hàm tạo mới và cập nhập
        public async Task<ResponseModel> Save(UserProfileDTO request,string userId)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var usesAddress = new UserAddress
                {
                    Fullname = request.Fullname,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Address = request.Address,
                    IsActive = true,
                    UserId = userId,
                };
                await _unitOfWork.UserAddressRepository.CreateUserAddress(usesAddress);
            }
            else
            {
                //_unitOfWork.GenreRepository.UpdateGenre(_mapper.Map<Genre>(request));
            }

            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Update,
                Message = "Thành công!!!",
                Status = true
            };
        }
    }
}
