namespace FacadeCompanyName.FacadeProjectName.DomainService.Share
{
    public class PagedOutput
    {
        /// <summary>
        /// 当前第几页，和入参一样
        /// </summary>
        public int current_page { get; set; }
        /// <summary>
        /// 每页条目数，和入参一样
        /// </summary>
        public int per_page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int page_count { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int total_count { get; set; }
    }
}
