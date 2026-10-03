using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderCRUDApp.Data;
using OrderCRUDApp.Models;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; }

    public void OnGet(int id)
    {
        Order = _context.Orders.Find(id);
    }

    public IActionResult OnPost()
    {
        _context.Orders.Remove(Order);
        _context.SaveChanges();

        return RedirectToPage("Index");
    }
}

