public class ShopCatalogDto
{
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public string ShopCardImage { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public int ReviewCount { get; set; }
    public decimal AverageRating { get; set; }
    public decimal PopularityScore { get; set; }
    public List<string> ImagePaths { get; set; } = new List<string>();
}
