﻿namespace BookSale.Management.Application.DTOs
{
	//Phải đặt đúng thứ tự như trong store procedure Select
	public class BookDTO
	{
		public string? GenreName { get; set; }
		public string? Code { get; set; }
		public string? Title { get; set; }
		public int Available { get; set; }
		public double Price { get; set; }
		public string? Publisher { get; set; }
		public string? Author { get; set; }
		public DateTime CreatedOn { get; set; }
		public int Id { get; set; }
	}
}