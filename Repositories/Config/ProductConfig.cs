using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p=>p.ProductId); // belirtilen alanın Primary Key olmasını sağlar.
            builder.Property(p=>p.ProductName).IsRequired(); // Alanın zorunlu olmasını sağlar.
            builder.Property(p=>p.Price).IsRequired(); // Alanın zorunlu olmasını sağlar.
            builder.HasData(
                new Product(){ProductId=1, CategoryId=2, ImageUrl="/img/1.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Computer", Price=17_000, ShowCase=false},
                new Product(){ProductId=2, CategoryId=2, ImageUrl="/img/3.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Keyboard", Price=1_000,ShowCase=true},
                new Product(){ProductId=3, CategoryId=2, ImageUrl="/img/2.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Mouse", Price=500,ShowCase=false},
                new Product(){ProductId=4, CategoryId=2, ImageUrl="/img/4.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Monitor", Price=7_000,ShowCase=true},
                new Product(){ProductId=5, CategoryId=2, ImageUrl="/img/5.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Deck", Price=1_500,ShowCase=false},
                new Product(){ProductId=6, CategoryId=1, ImageUrl="/img/6.jpg", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="History", Price=250, ShowCase=true},
                new Product(){ProductId=7, CategoryId=1, ImageUrl="/img/7.jpeg", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Hamlet", Price=200,ShowCase=true},
                new Product(){ProductId=8, CategoryId=2, ImageUrl="/img/telefon.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Telefon", Price=55000, ShowCase=true},
                new Product(){ProductId=9, CategoryId=2, ImageUrl="/img/tablet.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Tablet", Price=11500, ShowCase=true},
                new Product(){ProductId=10, CategoryId=2, ImageUrl="/img/kulaklık.png", Summary="Burası açıklama kısıdır. Ürün açılaması buraya gelecek", ProductName="Kulaklık", Price=450, ShowCase=true}
            );
        }
    }
}