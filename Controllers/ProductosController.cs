using Microsoft.AspNetCore.Mvc;
using pruebasoftware.Models;

namespace pruebasoftware.Controllers
{
    public class ProductosController : Controller
    {
        private static List<Product> _productos = new List<Product>
        {
            new Product { 
                Id = 1, 
                Code = "PROD001", 
                Name = "Leche Entera", 
                CategoryName = "Lácteos", 
                Quantity = "45 Litros",
                Location = "Almacén A - Estante 1",
                ExpirationDate = new DateTime(2025, 11, 07),
                Status = "Próximo a vencer",
                StockStatus = "Disponible",
                Value = 112.50M
            },
            new Product { 
                Id = 2, 
                Code = "PROD002", 
                Name = "Yogur Natural", 
                CategoryName = "Lácteos", 
                Quantity = "15 Unidades",
                Location = "Almacén A - Estante 2",
                ExpirationDate = new DateTime(2025, 11, 04),
                Status = "Próximo a vencer",
                StockStatus = "Bajo Stock",
                Value = 27.00M
            },
            new Product { 
                Id = 3, 
                Code = "PROD003", 
                Name = "Pan Integral", 
                CategoryName = "Panadería", 
                Quantity = "80 Unidades",
                Location = "Almacén B - Estante 1",
                ExpirationDate = new DateTime(2025, 12, 02),
                Status = "Vigente",
                StockStatus = "Disponible",
                Value = 96.00M
            },
            new Product { 
                Id = 4, 
                Code = "PROD004", 
                Name = "Aceite de Oliva", 
                CategoryName = "Aceites", 
                Quantity = "25 Botellas",
                Location = "Almacén C - Estante 3",
                ExpirationDate = new DateTime(2026, 05, 01),
                Status = "Vigente",
                StockStatus = "Disponible",
                Value = 212.50M
            },
            new Product { 
                Id = 5, 
                Code = "PROD005", 
                Name = "Arroz Blanco", 
                CategoryName = "Granos", 
                Quantity = "120 Kg",
                Location = "Almacén C - Estante 1",
                ExpirationDate = new DateTime(2026, 11, 02),
                Status = "Vigente",
                StockStatus = "Disponible",
                Value = 180.00M
            },
            new Product { 
                Id = 6, 
                Code = "PROD006", 
                Name = "Queso Fresco", 
                CategoryName = "Lácteos", 
                Quantity = "8 Kg",
                Location = "Almacén A - Refrigerador 1",
                ExpirationDate = new DateTime(2025, 11, 01),
                Status = "Vencido",
                StockStatus = "Bajo Stock",
                Value = 96.00M
            }
        };

        public IActionResult Index()
        {
            return View(_productos);
        }

        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product, string QuantityValue, string QuantityUnit)
        {
            if (ModelState.IsValid)
            {
                product.Id = _productos.Count + 1;
                product.Quantity = $"{QuantityValue} {QuantityUnit}";
                
                // Determine status based on expiration date
                var today = DateTime.Today;
                var daysUntilExpiration = (product.ExpirationDate - today).Days;
                
                if (daysUntilExpiration < 0)
                {
                    product.Status = "Vencido";
                }
                else if (daysUntilExpiration <= 30)
                {
                    product.Status = "Próximo a vencer";
                }
                else
                {
                    product.Status = "Vigente";
                }

                // Set initial stock status
                product.StockStatus = "Disponible";

                _productos.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}