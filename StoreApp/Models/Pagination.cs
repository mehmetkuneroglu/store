namespace StoreApp.Models
{
    public class Pagination
    {
        public int TotalItems { get; set; } //toplam ürün sayısı
        public int ItemsPerPage { get; set; } // sayfa başına düşen ürün sayısı
        public int CurrentPage { get; set; } //mevcut bulunulan sayfa
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); //toplam sayfa (readonly tanım)
        // iki int değer bölündüğünde ondalıklı bir değer çıkabilir bunu decimale cast ettik
        // bu ondalıklı değeri yuvarlamak için Math sınıfının Ceiling metodunu kullandık.
        // gelen bu değeri yine int olarak cast ettik. Toplam sayfa sayısına atadık.
    }
}