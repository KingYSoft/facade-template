using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Abp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.SettingDefinitions
{
    public class MyEmailSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(EmailSettingNames.Smtp.Host, "xxxx.mail.protection.xxx.com", L("SmtpHost")),
                       new SettingDefinition(EmailSettingNames.Smtp.Port, "25", L("SmtpPort")),
                       new SettingDefinition(EmailSettingNames.Smtp.UserName, "noreplyprd@xxxx.com", L("Username")),
                       new SettingDefinition(EmailSettingNames.Smtp.Password, "nopwd", L("Password")),
                       new SettingDefinition(EmailSettingNames.Smtp.Domain, "", L("DomainName")),
                       new SettingDefinition(EmailSettingNames.Smtp.EnableSsl, "false", L("UseSSL")),
                       new SettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials, "true", L("UseDefaultCredentials")),
                       new SettingDefinition(EmailSettingNames.DefaultFromAddress, "noreplyprd@xxx.com", L("DefaultFromSenderEmailAddress")),
                       new SettingDefinition(EmailSettingNames.DefaultFromDisplayName, "FacadeProjectName System", L("DefaultFromSenderDisplayName"))
                   };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }
}
