using System.ComponentModel.DataAnnotations;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Models.Dto
{
    public class AuthenticateInput
    {
        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
