using System.ComponentModel.DataAnnotations;

namespace Skipper.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


    }
}
