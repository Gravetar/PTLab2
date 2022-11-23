using Microsoft.EntityFrameworkCore;

namespace PTLab2.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; } = null!;

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };

            // Определение пользователей
            User user1 = new User 
            { 
                Id = 1, 
                Mail = "mail@mail.ru", 
                Password = "Password", 
                FirstName = "Ivan", 
                LastName = "Ivanov", 
                TotalAmount = 0,

                RoleId = userRole.Id
            };

            User user2 = new User
            {
                Id = 2,
                Mail = "adminmail@mail.ru",
                Password = "Password",
                FirstName = "admin",
                LastName = "adminov",
                TotalAmount = 0,

                RoleId = adminRole.Id
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
            modelBuilder.Entity<User>().HasData(user1, user2);
            modelBuilder.Entity<Role>().HasData(adminRole, userRole);
        }

        public void FillDb()
        {
            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role userRole = new Role { Id = 2, Name = "user" };

            // Определение пользователей
            User user1 = new User
            {
                Id = 1,
                Mail = "mail@mail.ru",
                Password = "Password",
                FirstName = "Ivan",
                LastName = "Ivanov",
                TotalAmount = 0,

                RoleId = userRole.Id
            };

            User user2 = new User
            {
                Id = 2,
                Mail = "adminmail@mail.ru",
                Password = "Password",
                FirstName = "admin",
                LastName = "adminov",
                TotalAmount = 0,

                RoleId = adminRole.Id
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

            Users.Add(user1);
            Users.Add(user2);
            Products.Add(product1);
            Products.Add(product2);
        }
    }
}
