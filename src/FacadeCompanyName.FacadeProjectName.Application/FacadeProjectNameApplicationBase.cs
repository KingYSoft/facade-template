using Abp.Application.Services;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Application
{
    public abstract class FacadeProjectNameApplicationBase : ApplicationService, IFacadeProjectNameApplicationBase
    {
        protected FacadeProjectNameApplicationBase()
        {
            LocalizationSourceName = FacadeProjectNameConsts.LocalizationSourceName;
        }
        /// <summary>
        /// Normalize value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static string NormalizeInput(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value.Trim();
        }
        /// <summary>
        /// 字符串 空，返回 false
        /// ^(0|[1-9][0-9]{0,8})$
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsTestInt(string str)
        {
            var reg = new System.Text.RegularExpressions.Regex("^(0|[1-9][0-9]{0,8})$");
            return reg.IsMatch(string.IsNullOrWhiteSpace(str) ? "" : str);
        }
        /// <summary>
        /// 字符串 空，返回 false
        /// ^(0.\\d{1,5}|0|[1-9][0-9]{0,8}|[1-9][0-9]{0,8}.\\d{1,5})$
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsTestDecimal(string str)
        {
            var reg = new System.Text.RegularExpressions.Regex("^(0.\\d{1,5}|0|[1-9][0-9]{0,8}|[1-9][0-9]{0,8}.\\d{1,5})$");
            return reg.IsMatch(string.IsNullOrWhiteSpace(str) ? "" : str);
        }
    }
}


