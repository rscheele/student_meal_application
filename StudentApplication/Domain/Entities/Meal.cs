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
        public DateTime MealDateTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Cook { get; set; }
        public int MaxGuests { get; set; }
        public decimal Price { get; set; }
        public int CurrentGuests { get; set; }
    }
}
