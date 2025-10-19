using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        // Veritabanı bağlantışı yapabilmek için context i buraya ekledik.
        private readonly RepositoryContext _context;
        // IProduct Repository üzerinde işlem yapa bilmek için de DI yapıyoruz
        private readonly IProductRepository _pruductRepository;   
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;

        public RepositoryManager(RepositoryContext context, IProductRepository pruductRepository, ICategoryRepository categoryRepository, IOrderRepository orderRepository)
        {
            _context = context;
            _pruductRepository = pruductRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }
        // Pruduct nesnesi çağrıldığında ProductRepositori enjecte edilecek.
        public IProductRepository Product => _pruductRepository;

        public ICategoryRepository Category => _categoryRepository;

        public IOrderRepository Order => _orderRepository;

        public void Save()
        {
            // Yapılan değişikliklerin kaydedilmesi durumunda kayıt işlemi burada gerçekleştirilecek
            _context.SaveChanges();
        }
    }
}