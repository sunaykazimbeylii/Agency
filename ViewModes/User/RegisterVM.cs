using System.ComponentModel.DataAnnotations;

namespace Agency.ViewModes
{
    public class RegisterVM
    {
        [MinLength(3,ErrorMessage ="3den kicik ola bilmez")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName{ get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
