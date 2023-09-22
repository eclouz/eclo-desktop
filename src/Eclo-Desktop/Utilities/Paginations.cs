namespace Eclo_Desktop.Utilities;

public class Paginations
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Paginations(int page, int pageSize)
    {
        PageNumber = page;
        PageSize = pageSize;
    }
    public Paginations()
    {
    }
}
