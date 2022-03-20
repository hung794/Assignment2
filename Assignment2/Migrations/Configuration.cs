namespace Assignment2.Migrations
{
    using Assignment2.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.MyDbContext context)
        {
            context.Categories.AddOrUpdate
                (
                    new Category { Id = 0, Name = "All" },
                    new Category { Id = 1, Name = "Food"},
                    new Category { Id = 2, Name = "Drink"},
                    new Category { Id = 3, Name = "Candy"}
                );
            context.Products.AddOrUpdate
                (
                    new Product { Id = 1, Name = "Chicken Fried", CategoryId = 1, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746849/ga-nuong-muoi-ot_xisihp.png", Price = 30000 },
                    new Product { Id = 2, Name = "Pizza", CategoryId = 1, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746848/Neapolitan_pizza_in_Fort_Lauderdale__Florida_hcvlps.jpg", Price = 45000 },
                    new Product { Id = 3, Name = "Mango Blended", CategoryId = 2, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746868/1584606961_cach-lam-sinh-to-xoai-ngon_lccank.jpg", Price = 15000 },
                    new Product { Id = 4, Name = "Strawberry Juice", CategoryId = 2, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746848/v-620x620-28_o6tge9.jpg", Price = 10000 },
                    new Product { Id = 5, Name = "Dynamite", CategoryId = 3, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746849/Keo-Chew-3-Vien-Dynamite-125gr-Vi-Socola_mjuxqu.jpg", Price = 20000 },
                    new Product { Id = 6, Name = "Chupa Chups", CategoryId = 3, Image = "https://res.cloudinary.com/hung71294/image/upload/v1647746848/aee074c9b819632ab69264adb2825c56_tlvcp8.jpg", Price = 5000 }
                );
        }
    }
}
