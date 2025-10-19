using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace EmployeeSkillBuilder.Pages.Skills
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Skill SelectedSkill { get; set; } = new Skill();
        private readonly HttpClient _client;
        private readonly string skill_endpoint = "skill?id=eq";
        private readonly string index_page = "/Skills/Index";

        public DetailsModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _client.GetAsync($"{skill_endpoint}.{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage(index_page);

            var json = await response.Content.ReadAsStringAsync();
            var ListOfSkill = JsonSerializer.Deserialize<List<Skill>>(json);

            SelectedSkill = ListOfSkill.FirstOrDefault();

            if (SelectedSkill ==  null)
                return RedirectToPage(index_page);

            return Page();
        }
    }
}
