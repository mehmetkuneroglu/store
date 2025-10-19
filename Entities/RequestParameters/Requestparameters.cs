namespace Entities.RequestParameters
{
    // abstrac classlar yarım bırakılmış bir class olarak düşünebiliriz
    // abstrac classlar newlenemez ancak kalıtımla başka bir sınıf devralabilir 
    public abstract class RequestParameters
    {
        // ! Arama kutusunun name özelliği ile aynı isimde tanımladık.
        public String? SearchTerm { get; set; }
    }
}