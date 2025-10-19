using Entities.Models;

namespace Services.Contracts
{
    public interface IOrderService 
    {
        // buradaki imzalarımız Repository'dekilerle aynı. 
        //Oyüzden oradaki imzaları doğrudan buraya kopyalayabiliriz.

         //Burada yine kurallarımızı koyacağız.
        IQueryable<Order> Orders { get; }
        // tek bir siparişi döndürmek için 
        Order? GetOneOrder(int id);
        //şipariş tamamlamak için
        void Complete(int id);
        // siparişi kaydetmek için
        void SaveOrder(Order order);
        // gönderilen gönderilmeyen kaç sipariş var diye bakmak için
        int NumberOfInProcess {get;}
        
    }
}