using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StoreApp.Infrastructure
{
    public static class SessionExtention
    {
        
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
            //Burada value değerini Serialize ederek string olarak hafızaya kaydettiğimizi söyleyebiliriz.
            //bu şekilde herhangi bir sınıf yapısını veya bir objeyi serialize edip hafızada yani sessionda 
            // saklayabiliriz.
        }
        // Bu işlemin aynısını Generic olarak da yapabiliriz. 
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        // Yukarıdaki iki metod arasındaki tek fark birisin objeye bağlı olarak çalışması diğerinin sizden 
        // tip istemesi. Aslında ikisi de aynı işlemi yapmakta.
        }
        // Bu işlemi yaptıktan sonra da gerektiğinde bu veriyi okuyabilmeniz lazım. Bunun için de GetJson 
        // diyerek okunabilmesi lazım.
        public static T? GetJson<T>(this ISession session, string key) // extension bir metod yazarken neyi genişlettiğimizi 
        // ilk parametrede vermemiz gerekiyor
        {
            // Burada geri dönüş değeri herhangi bir Tip olabilir ve Null da olabilir. 
            // Biz bu değeri anahtara göre alacağımızdan metod içerisine key değerini vermemiz gerek.
            // Burada bir logic işleteceğiz ve diyeceğiz ki bizim sana anahtar değerle verdiğimiz bilgiyi anahtara bağlı olarak getir.
            // Eğer anahtarını verdiğimiz değer sessionda saklıysa o bilgiyi alacağız.
            // Eğer saklı değilse default T ile dönüş yapacağız yani T nin varsayılan değeriyle dönüş yapacağız.
            var data = session.GetString(key);
            return data is null ? default(T) : JsonSerializer.Deserialize<T>(data);
        }

    }
}