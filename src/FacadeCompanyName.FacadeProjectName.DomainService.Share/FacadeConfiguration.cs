namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class FacadeConfiguration : IFacadeConfiguration
    {
        public string AppName { get; set; }
        /// <summary>
        /// app root dir
        /// </summary>
        public string AppRootPath { get; set; }
        /// <summary>
        /// eg. Production
        /// </summary>
        public string AppEnvName { get; set; }
        public bool IsDevelopment { get; set; }
        public string SqlServerConnString { get; set; }
        public string MySqlConnString { get; set; }
    }
}
