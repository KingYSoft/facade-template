namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public interface IFacadeConfiguration
    {
        string AppName { get; }
        string AppRootPath { get; }
        /// <summary>
        /// app running env name.
        /// </summary>
        string AppEnvName { get; }
        /// <summary>
        /// is local development
        /// </summary>
        bool IsDevelopment { get; }

        string SqlServerConnString { get; }
        string MySqlConnString { get; }
    }
}
