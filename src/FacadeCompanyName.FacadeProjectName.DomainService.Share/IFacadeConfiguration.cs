namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public interface IFacadeConfiguration
    {
        string AppName { get; }

        string Redis_Host { get; }
        int Redis_Port { get; }

    }
}
