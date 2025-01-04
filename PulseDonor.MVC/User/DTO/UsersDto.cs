namespace PulseDonor.MVC.User.DTO
{
	public class UsersDto
	{
		public string Id { get; set; }	
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public int? Age { get; set; }
		public string BloodType { get; set; }	
		public string Role { get; set; }
		public bool IsBlocked { get; set; }
		public string InsertedDate { get; set; }	
	}
}
