namespace FrontEnd.Models
{
    public class PagePagination
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
        public int TotalPageShow => TotalPages > 5 ? 5 : TotalPages;
        public int Skip => (CurrentPage - 1) * PageSize;
        public bool IsShowPrevious => CurrentPage > 1;
        public bool IsShowNext => CurrentPage < TotalPages;
        public bool IsShowFirst => CurrentPage > 3;
        public bool IsShowLast => CurrentPage < TotalPages - 2;
    }
}
