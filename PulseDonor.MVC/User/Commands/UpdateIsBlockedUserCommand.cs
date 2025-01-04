namespace PulseDonor.MVC.User.Commands
{
	public class UpdateIsBlockedUserCommand
	{
		public string UserId { get; set; }
		public bool IsBlocked { get; set; }
	}
}
