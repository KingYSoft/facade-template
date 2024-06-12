namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class FacadeConfiguration : IFacadeConfiguration
    {
        public string AppName { get; set; }
        public string AppRootPath { get; set; }
        public string AppEnvName { get; set; }
        public bool IsDevelopment { get; set; }
    }
}
