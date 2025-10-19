using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(cl => cl.Product)
            .OrderBy(o => o.Shipped)
            .ThenByDescending(o => o.OrderId);
            //Burada öncelikle context yani veritabanına gideceğim ve siparişleri alacağım, 
            // bunlara CartLine üzerindeki Lines yani satırları ekleyeceğim
            // Bu satırlarda pruduct nesnesi var onu da ekleyeceğim. 
            // sonra siparişin kargoya verilişine göre bunları sıralayacağım
            // ve yine son gelen siparişleri daha rahat görebilmek için sipariş Id değerine
            // göre yeniden sıralama yapacağım.

        public int NumberOfInProcess => _context.Orders.Count(o => o.Shipped.Equals(false));
        //eğer kargoya verilmediyse verilmeyenleri sayacak bir işlem

        public void Complete(int id)
        {
            //burada bir güncelleme işlemi yapacağız. eğer sipariş tamamlandıysa shipped alanı true olacak
            var order = FindByCondition(o => o.OrderId.Equals(id),true);
            if (order is null)
                throw new Exception("Order could not found!");
            order.Shipped = true;
            //_context.SaveChanges();     bu işlemi servis katmanına vereceğimiz için buradan kaldırdık.
        }

        public Order? GetOneOrder(int id)
        {
            // tek bir siparişi döndürecek fonksiyonu burada tanımlayacağız.
            return FindByCondition(o => o.OrderId.Equals(id), false);           
        }

        public void SaveOrder(Order order)
        {
            // Burada sipariş kaydı oluşturmak için bir metod yazacağız. 
            //birden fazla kayıt gelebilir bunun için AttactRange ifadesini kullanacağız.
            _context.AttachRange(order.Lines.Select(l=>l.Product));
            if(order.OrderId==0)
                _context.Orders.Add(order);
            // _context.SaveChanges();  bu işlemi servis katmanına vereceğimiz için buradan kaldırdık.
        }
    }
}