using Ecommerce.DataAccess.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Pages.Admin.Categories;

[BindProperties]

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Category Category { get; set; }

    public DeleteModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Category = _db.Category.Find(id);
		//Category = _db.Category.FirstOrDefault(u => u.Id == id);
		//Category = _db.Category.SingleOrDefault(u => u.Id == id);
		//Category = _db.Category.Where(u => u.Id == id).FirstOrDefault();
	}

    public async Task<IActionResult> OnPost()
    {
        var categoryFromDb = _db.Category.Find(Category.Id);
        if (categoryFromDb != null)
        {
            _db.Category.Remove(categoryFromDb);
			await _db.SaveChangesAsync();
			TempData["success"] = "Category deleted sucessfully";
			return RedirectToPage("Index");
		}		
        return Page();
    }
}
