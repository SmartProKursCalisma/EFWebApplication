namespace EFWebApplication.Models.FilterModels.Product
{
    public class SortedAndSearchParameters
    {
        public bool AzSort { get; set; }
        public bool ZaSort { get; set; }
        public string SearchTerm { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
