using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static MS.Identity.API.Models.UserViewModels;

namespace MS.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManger;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManger)
        {
            _signInManager = signInManager;
            _userManger = userManger;
        }

        [HttpPost("create-login")]
        public async Task<ActionResult> Register(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = userRegistration.Email,
                Email = userRegistration.Email,
                EmailConfirmed = true
            };

            var result = await _userManger.CreateAsync(user, userRegistration.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userName: userLogin.Email, password: userLogin.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
