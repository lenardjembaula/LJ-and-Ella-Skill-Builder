using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _client;

        public DeleteModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        [BindProperty]
        public Employee EmployeeToDelete { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"employee?id=eq.{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Employees/Index");

            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<Employee>>(json);

            EmployeeToDelete = employees.FirstOrDefault();
            if (EmployeeToDelete == null)
                return RedirectToPage("/Employees/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (EmployeeToDelete.id == null)
                return RedirectToPage("/Employees/Index");

            var response = await _client.DeleteAsync($"employee?id=eq.{EmployeeToDelete.id}");

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Employees/Index");

            return Page();
        }
    }
}
