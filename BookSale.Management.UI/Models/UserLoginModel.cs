﻿using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.UI.Models
{
	public class UserLoginModel
	{
		[Required(ErrorMessage = "Username không bỏ trống")]
		public string Username { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password không bỏ trống")]
		[MinLength(6, ErrorMessage = "Password từ 6 ký tự")]
		public string Password { get; set; } = string.Empty;

		public bool HasRememberMe { get; set; }
	}
}