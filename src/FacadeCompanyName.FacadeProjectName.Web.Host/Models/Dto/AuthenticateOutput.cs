namespace FacadeCompanyName.FacadeProjectName.Web.Host.Models.Dto
{
    public class AuthenticateOutput
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }
    }
}
