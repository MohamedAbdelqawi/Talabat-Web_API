using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext Context)
        {
            if (!Context.ProductBrands.Any())
            {

                var brandsDate = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var item in brands)
                    {

                        await Context.Set<ProductBrand>().AddAsync(item);
                    }

                    await Context.SaveChangesAsync();
                }
            }

            if (!Context.ProductTypes.Any())
            {

                var typessDate = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typessDate);
                if (types is not null && types.Count > 0)
                {
                    foreach (var item in types)
                    {
                        await Context.Set<ProductType>().AddAsync(item);
                    }

                    await Context.SaveChangesAsync();
                }
            }

            if (!Context.Products.Any())
            {

                var productssDate = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productssDate);
                if (products is not null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        await Context.Set<Product>().AddAsync(item);
                    }

                    await Context.SaveChangesAsync();
                }
            }


            if (!Context.DeliveryMethods.Any())
            {

                var deliverymethodDate = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

                var deliverymethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverymethodDate);
                if (deliverymethod is not null && deliverymethod.Count > 0)
                {
                    foreach (var item in deliverymethod)
                    {
                        await Context.Set<DeliveryMethod>().AddAsync(item);
                    }

                    await Context.SaveChangesAsync();
                }
            }
        }
    }
}
