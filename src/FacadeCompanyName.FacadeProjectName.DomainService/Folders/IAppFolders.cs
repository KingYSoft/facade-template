﻿namespace FacadeCompanyName.FacadeProjectName.DomainService.Folders
{
    public interface IAppFolders
    {
        /// <summary>
        /// 临时文件下载文件夹 /downloads/temps
        /// </summary>
        string TempFileDownloadFolder { get; }
        /// <summary>
        /// 临时文件上传文件夹 /uploads/temps
        /// </summary>
        string TempFileUploadFolder { get; }

        /// <summary>
        /// 文件上传后保存的路径 /uploads/
        /// </summary>
        string FileUploadFolder { get; }
    }
}
