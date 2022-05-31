namespace ImportTrialAccount.Models
{
    public class PagingModel
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int totalRecords { get; set; }
        public int totalPage { get; set; }

    }
}
