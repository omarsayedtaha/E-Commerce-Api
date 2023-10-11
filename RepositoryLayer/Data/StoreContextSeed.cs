using CoreLayer.Entities;
using CoreLayer.Entities.Order_Agregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brands = await File.ReadAllTextAsync("../RepositoryLayer/Data/DataSeed/brands.json");
                var brandData = JsonSerializer.Deserialize<List<ProductBrand>>(brands);

                if (brandData is not null && brandData.Count() > 0)
                {
                    foreach (var brand in brandData)
                        await context.Set<ProductBrand>().AddAsync(brand);

                    await context.SaveChangesAsync();

                }

            }

            if (!context.ProductType.Any())
            {
                var types = await File.ReadAllTextAsync("../RepositoryLayer/Data/DataSeed/types.json");
                var typeData = JsonSerializer.Deserialize<List<ProductType>>(types);

                if (typeData is not null && typeData.Count() > 0)
                {
                    foreach (var type in typeData)
                        await context.Set<ProductType>().AddAsync(type);

                    await context.SaveChangesAsync();


                }

            }
            if (!context.Products.Any())
            {
                var Products = await File.ReadAllTextAsync("../RepositoryLayer/Data/DataSeed/products.json");
                var ProductData = JsonSerializer.Deserialize<List<Product>>(Products);

                if (ProductData is not null && ProductData.Count() > 0)
                {
                    foreach (var product in ProductData)
                        await context.Set<Product>().AddAsync(product);

                    await context.SaveChangesAsync();

                }

            }

            if (!context.DeliveryMethodes.Any())
            {
                var DeliveryMethodes = File.ReadAllText("../RepositoryLayer/OrderData/DataSeed/delivery.json");
                var DeliveryMethodeData = JsonSerializer.Deserialize<List<DeliveryMethode>>(DeliveryMethodes);

                if (DeliveryMethodeData?.Count() > 0)
                {

                    foreach (var item in DeliveryMethodeData)
                    {
                        await context.Set<DeliveryMethode>().AddAsync(item);
                        await context.SaveChangesAsync();
                    }
                }
            }

        }
    }
}
