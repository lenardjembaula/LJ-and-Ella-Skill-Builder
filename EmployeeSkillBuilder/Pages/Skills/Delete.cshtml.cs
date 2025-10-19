using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Skills
{
    public class DeleteModel : PageModel
    {
        public Skill SkillToDelete { get; set; } = new Skill();
        private readonly HttpClient _client;
        private readonly string skill_endpoint = "skill?id=eq";
        private readonly string index_page = "/Skills/Index";

        public DeleteModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"{skill_endpoint}.{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage(index_page);

            var json = await response.Content.ReadAsStringAsync();
            var ListOfSkills = JsonSerializer.Deserialize<List<Skill>>(json);

            SkillToDelete = ListOfSkills.FirstOrDefault();

            if (SkillToDelete ==  null)
                return RedirectToPage(index_page);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _client.DeleteAsync($"{skill_endpoint}.{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToPage(index_page);

            return Page();
        }
    }
}
