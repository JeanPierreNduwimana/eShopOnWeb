using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.CatalogBrands.AnyAsync())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(
                    GetPreconfiguredCatalogBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogTypes.AnyAsync())
            {
                await catalogContext.CatalogTypes.AddRangeAsync(
                    GetPreconfiguredCatalogTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.CatalogItems.AddRangeAsync(
                    GetPreconfiguredItems());

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>
            {
                new("Choix du président"),
                new("Bonne Maman"),
                new("La Maison du Chocolat"),
                new("Maison Blé"),
                new("Maison Gosselin")
            };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Pains"),
                new("Viennoiseries"),
                new("Pâtisseries"),
                new("Sandwiches et en-cas"),
                new("Gâteaux")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
               
                new(1,2, "Pain de campagne", "Pain de campagne", 2.5M, "http://catalogbaseurltobereplaced/images/products/21.png"),
                new(1,1, "Baguette", "Baguette", 3.7M, "http://catalogbaseurltobereplaced/images/products/22.png"),
                new(1,3, "Pain complet", "Pain complet", 5.5M, "http://catalogbaseurltobereplaced/images/products/23.png"),
                new(1,5, "Pain de mie", "Pain de mie", 7.2M, "http://catalogbaseurltobereplaced/images/products/24.png"),

                new(2,4, "Croissant", "Croissant", 2.5M, "http://catalogbaseurltobereplaced/images/products/25.png"),
                new(2,5, "Pain au chocolat","Pain au chocolat", 2.5M, "http://catalogbaseurltobereplaced/images/products/26.png"),
                new(2,4, "Chausson aux pommes","Chausson aux pommes", 2.5M, "http://catalogbaseurltobereplaced/images/products/27.png"),
                new(2,3, "Pain au raisin","Pain au raisin", 2.5M, "http://catalogbaseurltobereplaced/images/products/28.png"),

                new(3,2, "Tartelette","Tartelette", 2.5M, "http://catalogbaseurltobereplaced/images/products/29.png"),
                new(3,1, "Éclair au chocolat","Éclair au chocolat", 2.5M, "http://catalogbaseurltobereplaced/images/products/30.png"),
                new(3,5, "Mille-feuille","Mille-feuille", 2.5M, "http://catalogbaseurltobereplaced/images/products/31.png"),
                new(3,4, "Macaron","Macaron", 2.5M, "http://catalogbaseurltobereplaced/images/products/32.png"),

                new(4,3, "Quiche","Quiche", 2.5M, "http://catalogbaseurltobereplaced/images/products/13.png"),
                new(4,2, "Croque-monsieur","Croque-monsieur", 2.5M, "http://catalogbaseurltobereplaced/images/products/14.png"),
                new(4,1, "Pâté en croûte","Pâté en croûte", 2.5M, "http://catalogbaseurltobereplaced/images/products/15.png"),
                new(4,5, "Salade composée","Salade composée", 2.5M, "http://catalogbaseurltobereplaced/images/products/16.png"),

                new(5,4, "Gâteau au chocolat","Gâteau au chocolat", 2.5M, "http://catalogbaseurltobereplaced/images/products/17.png"),
                new(5,3, "Gâteau de famille","Gâteau de famille", 2.5M, "http://catalogbaseurltobereplaced/images/products/18.png"),
                new(5,2, "Tarte au citron","Tarte au citron", 2.5M, "http://catalogbaseurltobereplaced/images/products/19.png"),
                new(5,1, "Tarte aux fruits","Tarte aux fruits", 2.5M, "http://catalogbaseurltobereplaced/images/products/20.png"),

                /*
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png") */
            };
    }
}
