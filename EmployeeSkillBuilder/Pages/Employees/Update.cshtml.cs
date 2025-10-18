using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace EmployeeSkillBuilder.Pages.Employees
{
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _client;

        public UpdateModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        [BindProperty]
        public Employee EmployeeToUpdate { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"employee?id=eq.{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<Employee>>(json);

            EmployeeToUpdate = employees.FirstOrDefault();
            if (EmployeeToUpdate == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployeeToUpdate.id == null) 
                return RedirectToPage("./Index");

            EmployeeToUpdate.date_modified = DateTime.UtcNow;
            var json = JsonSerializer.Serialize(EmployeeToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PatchAsync($"employee?id=eq.{EmployeeToUpdate.id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            return Page();
        }
    }
}
