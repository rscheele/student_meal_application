using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Student
    {
        public Student()
        {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Display(Name = "Naam")]
        [Required(ErrorMessage = "Voer je naam in")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Je kan alleen letters in je naam gebruiken")]
        public string Name { get; set; }
        [Display(Name = "Email adres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Voer een telefoonnummer in")]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{1,4}[-]{0,1}[0-9]{1,10}$", ErrorMessage = "Geen geldig telefoonnummer")]
        public string Phone { get; set; }
    }
}
