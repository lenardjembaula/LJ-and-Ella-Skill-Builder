using EmployeeSkillBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeSkillBuilder.Pages.Skills
{
    public class IndexModel : PageModel
    {
        public List<Skill> SkillList { get; set; } = new List<Skill>();
        private readonly HttpClient _client;

        public IndexModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Supabase");
        }


        public async Task OnGetAsync()
        {
            var result = await _client.GetFromJsonAsync<List<Skill>>("skill?order=id.asc");
            if (result !=  null)
                SkillList = result;
        }
    }
}
