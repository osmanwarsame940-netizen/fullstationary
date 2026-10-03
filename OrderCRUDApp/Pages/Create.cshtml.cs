
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OrderCRUDApp.Data;
using OrderCRUDApp.Models;

namespace OrderCRUDApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int ID { get; set; }
        [BindProperty]
        public string CustomerName { get; set; }
        [BindProperty]
        public string ProductName { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public decimal Price { get; set; }
        [BindProperty]
        public DateTime OrderDate { get; set; }

        public IActionResult OnPost()
        {
            var order = new Order
            {
                CustomerName = CustomerName,
                ProductName = ProductName,
                Quantity = Quantity,
                Price = Price,
                OrderDate = OrderDate
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}

