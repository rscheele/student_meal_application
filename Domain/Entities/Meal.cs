using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Meal
    {
        public Meal()
        {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MealId { get; set; }
        [Display(Name = "Maaltijd datum")]
        public DateTime MealDateTime { get; set; }
        [Display(Name = "Maaltijd naam")]
        [Required(ErrorMessage = "Voer een naam voor je maaltijd in")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Je kan alleen letters in de naam van je maaltijd gebruiken")]
        public string Name { get; set; }
        [Display(Name = "Beschrijving")]
        [Required(ErrorMessage = "Geef een beschrijving van je maaltijd")]
        public string Description { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Cook { get; set; }
        [Display(Name = "Maximum aantal gasten")]
        [Required(ErrorMessage = "Voer een maximum aantal in")]
        [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Je kan alleen cijfers gebruiken")]
        public int MaxGuests { get; set; }
        [Display(Name = "Prijs")]
        [Required(ErrorMessage = "Voer een prijs voor je maaltijd in")]
        // COMMENT OUT THE LINE BELOW IF BROWSER LOCALISATION IS IN ENGLISH
        //[RegularExpression(@"^[0-9]{0,3}[,]{1}[0-9]{2,2}|[0-9]{1,3}$", ErrorMessage = "Geen geldige invoer voor prijs, gebruik de vorm '1,23' voor €1,12 of '5' voor €5,00 of '0' als je maaltijd gratis is.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Ingeschreven gasten")]
        public int CurrentGuests { get; set; }
    }
}
