using System.Data;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dapper;
using Facade.Dapper.MySql;
using Facade.Dapper.Oracle;
using Facade.Dapper.SqlServer;
using Facade.NLogger;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    [DependsOn(
           typeof(FacadeNLoggerModule),
           typeof(FacadeDapperOralceModule),
           typeof(FacadeDapperSqlServerModule),
           typeof(FacadeDapperMySqlModule)
           )]
    public class FacadeProjectNameDomainServiceShareModule : AbpModule
    {

        public FacadeProjectNameDomainServiceShareModule()
        {
        }
        public override void PreInitialize()
        {
            SqlMapper.AddTypeHandler(new GuidToStringHandler());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FacadeProjectNameDomainServiceShareModule).GetAssembly());
            Configuration.Auditing.Selectors.RemoveByName("Abp.ApplicationServices");
        }

    }
    public class GuidToStringHandler : SqlMapper.TypeHandler<string>
    {
        public override void SetValue(IDbDataParameter parameter, string value)
        {
            parameter.Value = System.Guid.Parse(value);
        }

        public override string Parse(object value)
        {
            return value.ToString();
        }
    }
}