using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IImageService imageService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ResponseDataTable<UserModel>> GetAllUser(RequestDataTable request)
        {
            var users = await _userManager.Users
                                    .Where
                                    (
                                        x => x.IsActive && string.IsNullOrEmpty(request.keyword)
                                        || (x.UserName.Contains(request.keyword))
                                        || (x.Fullname.Contains(request.keyword))
                                        || (x.Email.Contains(request.keyword))
                                        || (x.PhoneNumber.Contains(request.keyword))
                                    )
                                    .Select
                                    (x => new UserModel
                                    {
                                        Email = x.Email,
                                        Fullname = x.Fullname,
                                        Username = x.UserName,
                                        Phone = x.PhoneNumber,
                                        Id = x.Id,
                                        IsActive = x.IsActive ? "Yes" : "No"
                                    })
                                    .ToListAsync();

            //Pagination
            int totalRecords = users.Count();

            var result = users.Skip(request.SkipItems).Take(request.PageSize).ToList();

            return new ResponseDataTable<UserModel>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = result
            };
        }

        public async Task<CreateAccountDTO> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var getRole = (await _userManager.GetRolesAsync(user)).First();

            var userDTOs = _mapper.Map<CreateAccountDTO>(user);

            userDTOs.RoleName = getRole;
            
            return userDTOs;
        }

        public async Task<ResponseModel> Save(CreateAccountDTO request)
        {
            string errors = string.Empty;

            var applicationUser = new ApplicationUser
            {
                Fullname = request.Fullname,
                Email = request.Email,
                UserName = request.Username,
                IsActive = request.IsActive,
                PhoneNumber = request.Phone,
                Address = request.Address,
            };

            if (string.IsNullOrEmpty(request.Id))
            {
                var identityUser = await _userManager.CreateAsync(applicationUser, request.Password);

                if (identityUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, request.RoleName);

                    await _imageService.SaveImage(new List<IFormFile> { request.Avatar }, "images/users", $"{applicationUser.Id}.png");

                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Insert,
                        Message = "Insert Complete",
                        Status = true
                    };
                }

                errors = string.Join("<br/>", identityUser.Errors.Select(x => x.Description));
            }
            else
            {
                var userUpdate = await _userManager.FindByIdAsync(request.Id);

                userUpdate.Fullname = request.Fullname;
                userUpdate.Email = request.Email;
                userUpdate.IsActive = request.IsActive;
                userUpdate.PhoneNumber = request.Phone;
                userUpdate.Address = request.Address;

                var identityUser = await _userManager.UpdateAsync(userUpdate);

                if (identityUser.Succeeded)
                {
                    await _imageService.SaveImage(new List<IFormFile> { request.Avatar }, "images/users", $"{userUpdate.Id}.png");

                    var hasRole = await _userManager.IsInRoleAsync(userUpdate, request.RoleName);

                    if (!hasRole)
                    {
                        var oldRole = (await _userManager.GetRolesAsync(userUpdate)).FirstOrDefault();
                        var removeOldRole = await _userManager.RemoveFromRoleAsync(userUpdate, oldRole);
                        if (removeOldRole != null)
                        {
                            await _userManager.AddToRoleAsync(userUpdate, request.RoleName);
                        }
                    }

                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Update,
                        Message = "Update successfully !!!",
                        Status = true
                    };
                }
                errors = string.Join("<br/>", identityUser.Errors.Select(x => x.Description));
            }

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(request.Id) ? "Insert" : "Update")} fail. {errors}",
                Status = false
            };
        }
    
        public async Task<bool> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);

                return true;
            }
            return false;
        }
    }
}
