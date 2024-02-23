using BookSale.Management.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookSale.Management.DataAccess.Repository
{
    public class GenericRepository<T> where T : class //T là một class
    {
        private readonly BookSaleDbContext _context;

        public GenericRepository(BookSaleDbContext context)
        {
            _context = context;
        }

        //Trả về toàn bộ danh sách class T
        //list.Where(x => x.name == "abc").ToList();
        //Expression is (x => x.name == "abc")

        //Ko return thì là Task
        //Return thì có thể nhiều trường hợp như: Task<IEnumerable<T>>, Task<T?>
        public async Task<IEnumerable<T>> GetALlAsync(Expression<Func<T,bool>> expression = null)
        {
            if (expression is null)
            {
                //Db trỏ đến table T trả về ToList 
                return await _context.Set<T>().ToListAsync();
            }
            
            return await _context.Set<T>().Where(expression).ToListAsync();
        }


        //Trả về 1 dòng dữ liệu // T? có thể trả về null
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression = null)
        {
            //Tìm thấy nhiều dòng -> trả về dòng đầu tiên
            //Không tìm được trả về null
            //await _context.Set<T>().FirstOrDefaultAsync();


            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        #region CRUD DB
        //Tạo 1 đối tượng vào Db
        public async Task Create(T entity)
        {
            //Chưa thực sự lưu vào Db với AddAsync
            await _context.Set<T>().AddAsync(entity);
        }

        //Update 1 dữ liệu đc truyền vào
        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        //Xoá 1 dữ liệu đc truyền vào
        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
