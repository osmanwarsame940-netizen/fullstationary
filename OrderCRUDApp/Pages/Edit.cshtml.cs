using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderCRUDApp.Data;
using OrderCRUDApp.Models;

namespace OrderCRUDApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int Id { get; set; }
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

        public void OnGet(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                Id = order.Id;
                CustomerName = order.CustomerName;
                ProductName = order.ProductName;
                Quantity = order.Quantity;
                Price = order.Price;
                OrderDate = order.OrderDate;
            }
        }

        public IActionResult OnPost()
        {
            var order = _context.Orders.Find(Id);
            if (order != null)
            {
                order.CustomerName = CustomerName;
                order.ProductName = ProductName;
                order.Quantity = Quantity;
                order.Price = Price;
                order.OrderDate = OrderDate;
                _context.SaveChanges();
            }
            return RedirectToPage("Index");
        }
    }
}
