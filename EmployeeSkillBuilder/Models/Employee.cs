using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeSkillBuilder.Models
{
    public class Employee
    {
        [Column("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? id { get; set; }


        [Column("first_name")]
        [Required(ErrorMessage = "First name is required.")]
        public string? first_name { get; set; }


        [Column("last_name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string? last_name { get; set; }


        [Column("email_address")]
        [EmailAddress(ErrorMessage = "Invalid email address."), Required(ErrorMessage = "Email address is required.")]
        public string? email_address { get; set; }


        [Column("date_of_birth")]
        [Required(ErrorMessage = "Select your date of birth.")]
        public DateTime? date_of_birth { get; set; }


        [Column("created_at")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? created_at { get; set; }


        [Column("date_modified")]
        public DateTime? date_modified { get; set; }


        [JsonIgnore]
        public string full_name
        {
            get { return $"{first_name} {last_name}"; }
        }

        [NotMapped]
        [JsonIgnore]
        public string date_of_birth_display =>
            date_of_birth.HasValue ? date_of_birth.Value.ToString("MMMM dd, yyyy") : "N/A";

        [NotMapped]
        [JsonIgnore]
        public string date_modified_display =>
            date_modified.HasValue ? date_modified.Value.ToString("MMMM dd, yyyy") : "N/A";
    }
}
