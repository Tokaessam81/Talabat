using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeedData
    {
        
        public async static Task SeedingAsync(StoreDbContext _dbContext)
        {
            var BrandsData=File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData) ;
            if (brands?.Count > 0) 
            {
                
                if (_dbContext.productBrands.Count()==0) 
                {
                    foreach (var brand in brands)
                    {
                      await  _dbContext.productBrands.AddAsync(brand);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
       
            var CategoriesData=File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData) ;
            if (categories?.Count > 0)
            {

                if (_dbContext.ProductCategories.Count() == 0)
                {
                    foreach (var category in categories)
                    {
                        await _dbContext.ProductCategories.AddAsync(category);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }


            var ProductData=File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData) ;
            if (Products?.Count > 0)
            {

                if (_dbContext.products.Count() == 0)
                {
                    foreach (var product in Products)
                    {
                        await _dbContext.products.AddAsync(product);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            } 
            
            
            
            var DeliveryData=File.ReadAllText("../Talabat.Repository/Data/DataSeeding/delivery.json");
            var Deliverymethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData) ;
            if (Deliverymethods?.Count > 0)
            {

                if (_dbContext.DeliveryMethods.Count() == 0)
                {
                    foreach (var method in Deliverymethods)
                    {
                         _dbContext.Set<DeliveryMethod>().Add(method);

                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
