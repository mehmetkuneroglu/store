namespace Entities.RequestParameters
{
    public class ProductRequestParameters :RequestParameters
    {
        public int? CategoryId { get; set; }
        public int MinPrice { get; set; } = 0; // Bu değer decimal larak da tanımlanabilir
        public int MaxPrice { get; set; } = int.MaxValue; // Bu değer decimal larak da tanımlanabilir
        public bool IsValidPrice => MaxPrice > MinPrice; // Girilen fiyat aralığı geçerli mi?
        public int PageNumber { get; set; } // Sayfa numarasını tutacak
        public int PageSize { get; set; } // bir sayfada bulunacak ürün sayısını tutacak
        public ProductRequestParameters() : this(1,6) // eğer default ctor istenirse yine aşağıdaki ctor içinedeğer alarak döndür
        {
            
        }
        //Burada parametreli Constructor inşa ettik
        public ProductRequestParameters(int pageNumber =1, int pageSize = 6)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}