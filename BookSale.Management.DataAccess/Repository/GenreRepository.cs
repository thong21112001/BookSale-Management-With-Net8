﻿using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly BookSaleDbContext _context;

        public GenreRepository(BookSaleDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await GetALlAsync();
        }

        public async Task<Genre> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task CreateGenre(Genre genre)
        {
            await Create(genre);
        }

        public void UpdateGenre(Genre genre)
        {
           Update(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            Delete(genre);
        }

        public async Task SaveGenre()
        {
            await Commit();
        }
	}
}
