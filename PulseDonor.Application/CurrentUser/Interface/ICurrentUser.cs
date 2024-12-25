using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.CurrentUser.Interface
{
    public interface ICurrentUser
    {
        string UserId { get; }
        string UserName { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Roles { get; }
    }
}
