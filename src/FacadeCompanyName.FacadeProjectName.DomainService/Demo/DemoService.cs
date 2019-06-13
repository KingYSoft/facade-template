using FacadeCompanyName.FacadeProjectName.DomainService.Demo.Dto;
using FacadeCompanyName.FacadeProjectName.Oracle;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.DomainService.Demo
{
    public class DemoService : FacadeProjectNameDomainServiceBase, IDemoService
    {
        private readonly IFacadeProjectNameOracleRepository _oracleRepository;
        public DemoService(IFacadeProjectNameOracleRepository oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }
        public async Task<string> Check(CheckInput input)
        {
            var duals = await _oracleRepository.QueryAsync<DualQuery_>(@"
select sysdate  from dual  where 1=:id
", new { id = input.Id });
            return duals.FirstOrDefault()?.SysDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public class DualQuery_
        {
            public DateTime SysDate { get; set; }
        }
    }
}
