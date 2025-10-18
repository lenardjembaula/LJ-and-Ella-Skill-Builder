using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;

        public CreateModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        [BindProperty]
        public Employee EmployeeToCreate { get; set; } = new Employee();

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(EmployeeToCreate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Use full URL if needed
            var response = await _client.PostAsync("employee", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Employees/Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Error: {response.StatusCode} - {error}");
                return Page();
            }
        }
    }
}
