using Microsoft.EntityFrameworkCore;

namespace PTLab2.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определение пользователей
            User user1 = new User 
            { 
                Id = 1, 
                Mail = "mail@mail.ru", 
                Password = "Password", 
                FirstName = "Ivan", 
                LastName = "Ivanov", 
                TotalAmount = 0 
            };

            // Определение товаров
            Product product1 = new Product
            {
                Id = 1,
                Name = "Стиральная машина",
                Price = 20000,
                NewPrice = 20000
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Вентилятор",
                Price = 3000,
                NewPrice = 3000
            };

            modelBuilder.Entity<Product>().HasData(product1, product2);
            modelBuilder.Entity<User>().HasData(user1);

        }
    }
}
