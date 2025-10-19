
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Contracts
{
    // Soyut metodumuz İnterface i devralacak.
    // Referans tipli veriler olması için ve newlernebilir 
    // olması için (where T : class, new()) kısmı ekliyoruz.
    // bu tipi kısıtlayıcı bir tanımdır.
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()
    {
        // Bu class ı devralan klaslarda veritabanıyla işlem yapmak
        // isteyebiliriz. Bunun için Protected DI kullandık.
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity); 
            //bu işlemden sonra save demedik çünkü bu işlemi servis yapacak
        }

        // Eğer değişikliklerin izlenmesi veya izlenmemesi durumu ile return 
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>() //değişiklikleri izle ve işlemi yap
                : _context.Set<T>().AsNoTracking(); // değişiklikleri izlemeden işlemi yap
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
             return trackChanges
                ? _context.Set<T>().Where(expression).SingleOrDefault() // bir ürün getir değişikliği izle
                : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault(); // değişiklikleri izlemeden işlemi yap
        }

        public void Remove(T entity)
        {
           _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}