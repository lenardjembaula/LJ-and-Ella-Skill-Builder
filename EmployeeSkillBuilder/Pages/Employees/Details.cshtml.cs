using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _client;

        public DetailsModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        [BindProperty]
        public Employee EmployeeDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"employee?id=eq.{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Employees/Index");

            var json = await response.Content.ReadAsStringAsync();
            var GetEmployees = JsonSerializer.Deserialize<List<Employee>>(json);

            EmployeeDetails = GetEmployees.FirstOrDefault();
            if (EmployeeDetails == null)
                return NotFound();

            return Page();
        }
    }
}
