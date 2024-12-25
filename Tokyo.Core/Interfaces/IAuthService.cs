using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokyo.Core.ViewModel.Auth;

namespace Tokyo.Core.Interfaces
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<bool> CreateRoles(UserRoleNamesModel role);
        Task<bool> AssignRoleToUser(string nameIdentifier, UserRoleNamesModel role);
    }
}
