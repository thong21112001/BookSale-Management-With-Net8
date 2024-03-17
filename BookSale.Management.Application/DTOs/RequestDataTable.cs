using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.Application.DTOs
{
    public class RequestDataTable
    {
        [BindProperty(Name = "length")]
        public int PageSize { get; set; }

        [BindProperty(Name = "start")]
        public int SkipItems { get; set; }

        [BindProperty(Name = "search[value]")]
        public string? keyword { get; set; }

		[BindProperty(Name = "draw")]
		public int Draw { get; set; }
    }
}
