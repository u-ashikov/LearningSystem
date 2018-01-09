namespace LearningSystem.Web.Models
{
	using System;

    using static Common.Constants.WebConstants;

	public class PaginationViewModel
	{
		public string Action { get; set; }

		public string SearchTerm { get; set; }

		public int PageSize { get; set; }

		public int CurrentPage { get; set; }

		public int TotalElements { get; set; }

		public int TotalPages => (int)Math.Ceiling(this.TotalElements / (double)PageSize);

		public int NextPage => this.CurrentPage >= this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

		public int PreviousPage => this.CurrentPage <= MinPageSize ? 1 : this.CurrentPage - 1;
    }
}
