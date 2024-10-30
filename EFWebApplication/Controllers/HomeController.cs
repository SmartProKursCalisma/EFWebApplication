using EFWebApplication.Models;
using EFWebApplication.Models.FilterModels.Product;
using EFWebApplication.Models.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFWebApplication.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Products.Include(x => x.Category).Select(x => new ProductWithCategoryVM
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.CategoryName
            }).ToList();
            ProductTableAndSortWithSearchParam model = new()
            {
                Products = result,
                Parameters = new SortedAndSearchParameters()
                
            };

            return View(model);
        }
        public IActionResult SortedProducts([FromQuery] int sortedType)
        {
            var result = _context.Products.Include(x => x.Category).Select(x => new ProductWithCategoryVM
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                CategoryName = x.Category.CategoryName
            }).ToList();
            if (sortedType == 1)
            {
                result = result.OrderBy(x => x.ProductName).ToList();
            }
            else
            {
                result = result.OrderByDescending(x => x.ProductName).ToList();
            }
            return View("Index", result);
        }
        public IActionResult FilteredProducts(SortedAndSearchParameters parameters)
        {
            

            var result = _context.Products.Include(x => x.Category)
                .Select(x => new ProductWithCategoryVM
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    QuantityPerUnit = x.QuantityPerUnit,
                    UnitPrice = x.UnitPrice,
                    UnitsInStock = x.UnitsInStock,
                    CategoryName = x.Category.CategoryName
                }).Where(x => x.ProductName.Contains(parameters.SearchTerm) & x.UnitPrice >= parameters.MinPrice);
            if (parameters.ZaSort)
            {
               result =  result.OrderByDescending(x => x.ProductName).AsQueryable();
            }
            else
            {
                result = result.OrderBy(x => x.ProductName).AsQueryable();
            }
           
            if(parameters.MaxPrice > 0)
            {
                result = result.Where(x => x.UnitPrice <= parameters.MaxPrice);
            }
            ProductTableAndSortWithSearchParam model = new()
            {
                Parameters = parameters,
                Products = result.ToList()
            };
            return View("Index", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }



    }
}
