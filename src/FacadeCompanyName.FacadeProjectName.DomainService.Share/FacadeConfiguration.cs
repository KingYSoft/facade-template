﻿namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class FacadeConfiguration : IFacadeConfiguration
    {
        public string AppName { get; set; }
        public string Redis_Host { get; set; }
        public int Redis_Port { get; set; }
    }
}
