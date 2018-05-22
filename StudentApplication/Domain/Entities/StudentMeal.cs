using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentMeal
    {
        public StudentMeal()
        {

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentMealId { get; set; }
        public int MealId { get; set; }
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        public bool Cook { get; set; }
    }
}
