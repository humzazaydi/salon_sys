using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokyo.Core.ViewModel.Auth
{
    public class UserRoleNamesModel
    {
        [Required(ErrorMessage = "Please specify role name.")]
        public List<string> RoleNames { get; set; }
    }
}
