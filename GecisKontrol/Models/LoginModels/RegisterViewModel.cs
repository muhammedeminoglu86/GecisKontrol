using System.ComponentModel.DataAnnotations;

namespace GecisKontrol.Models.LoginModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Kullanıcı Adı")]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Parola")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Parolayı Onayla")]
		[Compare("Password", ErrorMessage = "Parolalar birbirleriyle uyuşmuyor.")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Aktif")]
		public int IsActive { get; set; } = 1;

		[Display(Name = "Oluşturma Zamanı")]
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
	}

}
