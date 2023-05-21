namespace Application.Common.DTOs
{
    public class PaginationInfoDto
    {
        public bool AscSort { get; set; } = true;
        public string SortByColumn { get; set; } = "Id";
        public int PageNumber { get; set; } = 1;
        public int AmountPerPage { get; set; } = 5;
    }
}
