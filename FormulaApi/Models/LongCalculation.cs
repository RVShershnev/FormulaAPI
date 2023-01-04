using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaApi.Models
{
    public class LongCalculation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
    }
}
