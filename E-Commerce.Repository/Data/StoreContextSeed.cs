﻿using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext _storeContext)
        {
            if (_storeContext.ProductBrands.Count() == 0)
            {
                var BrandData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeeding/brands.json");
                var Brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (Brand?.Count() > 0)
                {
                    foreach (var item in Brand)
                    {
                        _storeContext.Set<ProductBrand>().Add(item);
                    }
                    await _storeContext.SaveChangesAsync();
                } 
            }

            if (_storeContext.ProductTypes.Count() == 0)
            {
                var CategoryData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeeding/categories.json");
                var Category = JsonSerializer.Deserialize<List<ProductType>>(CategoryData);

                if (Category?.Count() > 0)
                {
                    foreach (var item in Category)
                    {
                        _storeContext.Set<ProductType>().Add(item);
                    }
                    await _storeContext.SaveChangesAsync();
                }
            }

            if (_storeContext.Products.Count() == 0)
            {
                var productData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeeding/products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(productData);

                if (product?.Count() > 0)
                {
                    foreach (var item in product)
                    {
                        _storeContext.Set<Product>().Add(item);
                    }
                    await _storeContext.SaveChangesAsync();
                }
            }

        }
    }
}
