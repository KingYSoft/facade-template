﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of FacadeCompanyName.FacadeProjectName.DomainService.Share assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "FacadeCompanyName.FacadeProjectName.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }


            var webHostFolder = Path.Combine(directoryInfo.FullName, "src", "FacadeCompanyName.FacadeProjectName.Web.Host");
            if (Directory.Exists(webHostFolder))
            {
                return webHostFolder;
            }

            throw new Exception("Could not find root folder of the web project!");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
