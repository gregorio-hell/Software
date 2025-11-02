using System.ComponentModel.DataAnnotations;

namespace pruebasoftware.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El código es requerido")]
    [Display(Name = "Código")]
    public string Code { get; set; } = "";
    
    [Required(ErrorMessage = "El nombre es requerido")]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = "";
    
    [Required(ErrorMessage = "La categoría es requerida")]
    [Display(Name = "Categoría")]
    public string CategoryName { get; set; } = "";
    
    [Required(ErrorMessage = "La cantidad es requerida")]
    [Display(Name = "Cantidad")]
    public string Quantity { get; set; } = "";
    
    [Required(ErrorMessage = "La ubicación es requerida")]
    [Display(Name = "Ubicación")]
    public string Location { get; set; } = "";
    
    [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
    [Display(Name = "Fecha de Vencimiento")]
    public DateTime ExpirationDate { get; set; }
    
    [Display(Name = "Estado")]
    public string Status { get; set; } = "";
    
    [Display(Name = "Estado de Stock")]
    public string StockStatus { get; set; } = "";
    
    [Required(ErrorMessage = "El valor es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
    [Display(Name = "Valor")]
    public decimal Value { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Product> Products { get; set; } = new();
}

public class DashboardViewModel
{
    public int TotalProducts { get; set; }
    public decimal TotalValue { get; set; }
    public int LowStockCount { get; set; }
    public int AlertsCount { get; set; }
    public List<CategorySummary> CategoryInventory { get; set; } = new();
    public List<CategoryValue> CategoryValues { get; set; } = new();
    public InventoryStatus InventoryStatus { get; set; } = new();
}

public class CategorySummary
{
    public string CategoryName { get; set; } = "";
    public int Units { get; set; }
}

public class CategoryValue
{
    public string CategoryName { get; set; } = "";
    public decimal Value { get; set; }
}

public class InventoryStatus
{
    public int OptimalProducts { get; set; }
    public int LowStock { get; set; }
    public int ExpiringSoon { get; set; }
}