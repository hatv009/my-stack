namespace Shared.Domain.Common
{
    public class BaseOpts
    {
        public BaseOpts()
        {
            pageIndex = 1;
            pageSize = 20;
            sortName = "Id";
            sortType = "DESC";
        }
        public BaseOpts(string s)
        {
            search = s;
            pageIndex = 1;
            pageSize = 20;
            sortName = "Id";
            sortType = "DESC";
        }

        public BaseOpts(int pageIndex, int pageSize)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            sortName = "Id";
            sortType = "DESC";
        }

        public string search { get; set; } // biến search nhanh nếu yêu cầu cần thì thêm
        /// <summary>
        /// number of items in page (default = 20)
        /// </summary>
        public int pageSize { get; set; } = 20;
        /// <summary>
        /// page Index (default = 1)
        /// </summary>
        public int pageIndex { get; set; } = 1;
        /// <summary>
        /// search header <br/>
        /// filter = {"key":"value"} - key as propertyName <br/>
        /// multiple: filter = {"key":"value", "key":"value",...}
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// sort with propertyName (default: "id")
        /// </summary>
        public string sortName { get; set; } = "Id";
        /// <summary>
        /// true: ascending, false: descending (default: false)
        /// </summary>
        public string sortType { get; set; } = "DESC";
        /// <summary>
        /// format: yyyy-mm-dd
        /// </summary>
        public DateTime? fromDate { get; set; }
        /// <summary>
        /// format: yyyy-mm-dd
        /// </summary>
        public DateTime? toDate { get; set; }
        //public string createdBy { get; set; }
        //public DateTime createdDate { get; set; }
        //public int branchId { get; set; }
        public bool? ShowWebsite { get; set; }
    }
}
