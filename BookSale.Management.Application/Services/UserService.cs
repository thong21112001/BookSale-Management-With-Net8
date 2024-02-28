using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDataTable<UserModel>> GetAllUser(RequestDataTable request)
        {
            var users = await _userManager.Users
                                    .Where
                                    (
                                        x => string.IsNullOrEmpty(request.keyword)
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
                //var identityUser = await _userManager.UpdateAsync(applicationUser);
            }

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(request.Id) ? "Insert" : "Update")} fail. {errors}",
                Status = false
            };
        }
    }
}
