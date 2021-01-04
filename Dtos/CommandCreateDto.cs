using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class CommandCreateDto
    {
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Platform { get; set; }
    }
}