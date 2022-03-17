namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class PagedInput
    {
        /// <summary>
        /// 查询字段
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// 当前第几页，默认第1页
        /// </summary>
        public int current_page { get; set; } = 1;
        /// <summary>
        /// 每页条目数，默认20000
        /// </summary>
        public int per_page { get; set; } = 20000;
    }
}
