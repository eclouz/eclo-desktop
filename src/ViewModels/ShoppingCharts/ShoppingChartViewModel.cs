namespace ViewModels.ShoppingCharts;

public class ShoppingChartViewModel
{
    public string ProductName { get;set; }=String.Empty;
    public string ProductDescription { get;set; } = String.Empty;
    public double ProductPrice { get;set; }
    public float ProductDiscount { get; set; } = 0;
    public string ProductColor { get; set;} = String.Empty;

    public string ProductImage { get; set;} = String.Empty;

    public int ProductQuantity { get; set;}

    public int ItemCount { get; set;}

    public string ProductSize { get; set; } = String.Empty;
    public bool IsChecked=false;

}
