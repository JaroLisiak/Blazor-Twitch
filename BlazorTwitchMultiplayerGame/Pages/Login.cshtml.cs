using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BlazorTwitchMultiplayerGame.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {

            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, TwitchAuthenticationDefaults.AuthenticationScheme);
        }

    }
}
