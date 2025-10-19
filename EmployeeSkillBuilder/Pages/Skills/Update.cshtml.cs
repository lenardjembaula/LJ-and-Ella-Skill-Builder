using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Skills
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Skill SkillToUpdate { get; set; } = new Skill();
        public readonly HttpClient _client;
        private readonly string skill_endpoint = "skill?id=eq";
        private readonly string index_page = "/Skills/Index";

        public UpdateModel(IHttpClientFactory factory)
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

            SkillToUpdate = ListOfSkills.FirstOrDefault();
            if (SkillToUpdate == null)
                return RedirectToPage(index_page);

            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var json = JsonSerializer.Serialize(SkillToUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PatchAsync($"{skill_endpoint}.{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Skills/Index");

            return Page();
        }
    }
}
