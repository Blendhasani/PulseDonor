using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PulseDonor.Application.CurrentUser.Interface;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName => User?.FindFirstValue(ClaimTypes.Name);
    public string Email => User?.FindFirstValue(ClaimTypes.Email);
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(r => r.Value) ?? Enumerable.Empty<string>();
	public int BloodTypeId
	{
		get
		{
			var bloodTypeIdClaim = User?.FindFirstValue("BloodTypeId");
			if (int.TryParse(bloodTypeIdClaim, out var bloodTypeId))
			{
				return bloodTypeId;
			}
			throw new InvalidOperationException("BloodTypeId claim is missing or invalid.");
		}
	}

}
