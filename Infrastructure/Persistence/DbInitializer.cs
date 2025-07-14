using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly EcommerceDbContext _dbContext;

        public DbInitializer(EcommerceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task IntilializeAsync()
        {
        
            //Cteate Datbase If Doesn't Exists && Applay To Any Pending Migrations

            if(_dbContext.Database.GetPendingMigrations().Any())
            {
              await  _dbContext.Database.MigrateAsync();
            }
            //Data Seeding
            //Seeding ProducType From Json File 
            if(!_dbContext.ProductTypes.Any())
            {
                //1. Read All Data From Types Json File As String
               var typesData=await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                //2. Transform String To C# Objects [List<ProductType>]
               var types= JsonSerializer.Deserialize<List<ProductType>>(typesData);
                //3. Add To Database
                if(types is not null &&types.Any())
                {
                   await _dbContext.ProductTypes.AddRangeAsync(types);

                   await _dbContext.SaveChangesAsync();
                }
            }



            //Seeding ProducBrand From Json File 

            if (!_dbContext.ProductBrands.Any())
            {
                //1. Read All Data From Types Json File As String
                var brandssData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                //2. Transform String To C# Objects [List<ProductBrand>]
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandssData);
                //3. Add To Database
                if (brands is not null && brands.Any())
                {
                    await _dbContext.ProductBrands.AddRangeAsync(brands);

                    await _dbContext.SaveChangesAsync();
                }
            }
            //Seeding Produc From Json File

            if (!_dbContext.Products.Any())
            {
                //1. Read All Data From Types Json File As String
                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                //2. Transform String To C# Objects [List<Product>]
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                //3. Add To Database
                if (products is not null && products.Any())
                {
                    await _dbContext.Products.AddRangeAsync(products);

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
