using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<Employee> EmployeeList { get; set; } = new();
        private readonly HttpClient _client;

        public IndexModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        public async Task OnGetAsync()
        {
            var result = await _client.GetFromJsonAsync<List<Employee>>("employee?order=id.asc");
            if (result != null)
                EmployeeList = result;
        }
    }
}