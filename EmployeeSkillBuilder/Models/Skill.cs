using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeSkillBuilder.Models
{
    [Table("skill")]
    public class Skill
    {
        [Column("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ID { get; set; }


        [Column("skill_name")]
        public string? skill_name { get; set; }


        [Column("description")]
        public string? description { get; set; }


        [Column("created_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? created_at { get; set; }


        [NotMapped]
        [JsonIgnore]
        public string created_at_display =>
            created_at.HasValue ? created_at.Value.ToString("MMMM dd, yyyy") : "N/A";

    }
}
