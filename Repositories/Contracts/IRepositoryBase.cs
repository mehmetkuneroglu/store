using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        // Burada temel bir interface tanımlaması yaptık. 
        // Tip konsunda sınırlama getirmedik 
        // Concrete versiyonunda tipler daha açık yazılacak.
        // Şimdilik tablodaki tüm verileri getirecek ve 
        // üzerinde yeniden sorgulama yapılabilecek bir 
        // IQueryable metod tanımladık. 
        // Parametre olarak değişiklikleri izle anlamında bir 
        // Bool değer atadık. Interface yapıları soyuttur. Soyut metodlarda gövde olmaz.
        IQueryable<T> FindAll(bool trackChanges);
        // Burada tek bir ürünü getirecek base kuralını tanımlıyoruz. Tek bir ürünü bul,
        // içeriye bir fonksiyon gönder, değişiklikleri izleyip izlemeyeceğini sor.
        // Buradaki T yukarıdaki T, yani type karşılığı. Type sınırlaması yok.
        T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);

        // Kaydetme işlemini yapacak metdodumuzu tanımlıyoruz.
        void Create(T entity);
        void Remove(T entity);
        void Update(T entity);
    }

}
