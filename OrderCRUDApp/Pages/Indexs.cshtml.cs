using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderCRUDApp.Data;
using OrderCRUDApp.Models;
using System.Collections.Generic;
using System.Linq;

public class OrderModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public OrderModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Order> Orders { get; set; }

    public void OnGet()
    {
        Orders = _context.Orders.ToList();
    }
}

