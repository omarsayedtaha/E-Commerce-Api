namespace TalabatApi.Helper
{
    public class ProductSpecParams
    {
        public int MaxSize { get; set; } = 10;

        private int pagesize = 5;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > MaxSize ? MaxSize : value; }
        }

        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }

        public string? Search { get; set; }
        public int? typeId { get; set; }
        public int? brandId { get; set; }
    }
}
