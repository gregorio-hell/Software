using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pruebasoftware.Models;

namespace pruebasoftware.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Simulated data for the dashboard
        var dashboardData = new DashboardViewModel
        {
            TotalProducts = 6,
            TotalValue = 724.00M,
            LowStockCount = 2,
            AlertsCount = 3,
            CategoryInventory = new List<CategorySummary>
            {
                new CategorySummary { CategoryName = "Lácteos", Units = 68 },
                new CategorySummary { CategoryName = "Panadería", Units = 80 },
                new CategorySummary { CategoryName = "Aceites", Units = 25 },
                new CategorySummary { CategoryName = "Granos", Units = 120 }
            },
            CategoryValues = new List<CategoryValue>
            {
                new CategoryValue { CategoryName = "Lácteos", Value = 235.50M },
                new CategoryValue { CategoryName = "Aceites", Value = 212.50M },
                new CategoryValue { CategoryName = "Granos", Value = 180.00M },
                new CategoryValue { CategoryName = "Panadería", Value = 96.00M }
            },
            InventoryStatus = new InventoryStatus
            {
                OptimalProducts = 3,
                LowStock = 2,
                ExpiringSoon = 3
            }
        };

        return View(dashboardData);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
