using System.ComponentModel.DataAnnotations;


namespace NLayerApp.PLL.Models
{
    public class EditModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
    }
}