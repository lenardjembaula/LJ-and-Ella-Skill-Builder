using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Skills
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Skill SkillToCreate { get; set; } = new Skill();
        private readonly HttpClient _client;

        public CreateModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(SkillToCreate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("skill", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Skills/Index");

            return Page();
        }
    }
}
