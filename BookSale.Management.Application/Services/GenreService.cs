using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.Genre;
using BookSale.Management.Application.DTOs.ViewModels;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #region Implement function into Admin
        //Lấy genre theo id truyền vào
        public async Task<GenreViewModel> GetById(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);

            //Map dữ liệu get đc thông qua Id sau đó map từ genre -> GenreDTO
            return _mapper.Map<GenreViewModel>(genre);
        }

        //Hàm lấy tất cả thể loại hiển thị lên datatable ở Genre/Index
        public async Task<ResponseDataTable<GenreDTO>> GetAllGenre(RequestDataTable request)
        {
            var genres = await _unitOfWork.GenreRepository.GetAllGenre();

            var genresDTO = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            //Pagination
            int totalRecords = genres.Count();

            var result = genresDTO.Skip(request.SkipItems).Take(request.PageSize).ToList();

            return new ResponseDataTable<GenreDTO>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = result
            };
        }

        //Hàm tạo mới và cập nhập
        public async Task<ResponseModel> Save(GenreViewModel request)
        {
            if (request.Id == 0)
            {
                var genreSave = new Genre
                {
                    Name = request.Name,
                    Description = request.Description,
                    IsActive = request.IsActive
                };
                await _unitOfWork.GenreRepository.CreateGenre(genreSave);
            }
            else
            {
                _unitOfWork.GenreRepository.UpdateGenre(_mapper.Map<Genre>(request));
            }

            await _unitOfWork.GenreRepository.SaveGenre();

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Update,
                Message = $"{(request.Id == 0 ? "Thêm mới" : "Cập nhập")} thành công !!!",
                Status = true
            };
        }

        //Hàm xoá genre
        public async Task<bool> Delete(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);

            if (genre is not null)
            {
                _unitOfWork.GenreRepository.DeleteGenre(genre);
                await _unitOfWork.GenreRepository.SaveGenre();

                return true;
            }
            return false;
        }
        #endregion




        #region Implement function into Customer
        //Lấy toàn bộ sách trong genre ví dụ ( Comic - Tổng sách = 5)
        public IEnumerable<GenreSiteDTO> GetSumBookOfGenre()
        {
            var result = _unitOfWork.GenreRepository.Table.Select(x => new GenreSiteDTO
            {
                Id = x.Id,
                Name = x.Name,
                TotalBook = x.Books.Count()
            });

            return result;
        }

        //Lấy toàn bộ genre ra ngoài customer
        public async Task<IEnumerable<GenreDTO>> GetAllGenreForCustomer()
        {
            var genreList = await _unitOfWork.GenreRepository.GetAllGenre();

            return _mapper.Map<IEnumerable<GenreDTO>>(genreList);
        }

        public async Task<string> GetGenreForBookVM(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);

            if (genre.Name != null)
            {
                return genre.Name;
            }

            return "Unknow";
        }
        #endregion
    }
}
