using Entities.Models;

namespace Repositories.Contracts
{
    public interface IOrderRepository
    {
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