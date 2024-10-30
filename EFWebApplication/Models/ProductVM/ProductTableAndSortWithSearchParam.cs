using EFWebApplication.Models.FilterModels.Product;

namespace EFWebApplication.Models.ProductVM
{
    public class ProductTableAndSortWithSearchParam
    {
        public List<ProductWithCategoryVM>Products{ get; set; }
        public SortedAndSearchParameters Parameters { get; set; }
    }
}
