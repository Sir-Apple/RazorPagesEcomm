using Ecommerce.Data;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Pages.Categories;

[BindProperties]

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Category Category { get; set; }

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (Category.Name == Category.DisplayOrder.ToString()) 
        {
            ModelState.AddModelError("Category.Name", "The Display Order cannot exactlly match the Name.");
        }
        if (ModelState.IsValid)
        {
			await _db.Category.AddAsync(Category);
			await _db.SaveChangesAsync();
			return RedirectToPage("Index");
		}
        return Page();
    }
}
