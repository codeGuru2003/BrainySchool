using HealthRecordsPro.Data;
using HealthRecordsPro.Models;
using HealthRecordsPro.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthRecordsPro.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Index()
        {
            var users =  _userManager.Users.ToList();
            return View(users);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user;
                    user = await _userManager.FindByNameAsync(model.Username);
                    var SUser = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (!SUser)
                    {
                        TempData["ErrorMessage"] = "Username or Password is Incorrect";
                        return RedirectToAction("Login", "Account");
                    }
                    else 
                    {

                        var Luser = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                        var FindUser = await _userManager.FindByNameAsync(model.Username);
                        var isSuperAdmin = await _userManager.IsInRoleAsync(FindUser, "SuperAdmin");
                        var isFaculty = await _userManager.IsInRoleAsync(FindUser, "Faculty");

                        if (Luser.Succeeded)
                        {
                            if (isSuperAdmin)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (isFaculty)
                            {

                            }
                            else
                            {
                                return RedirectToAction("AccessDenied", "Account");
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    ViewBag.ErrorMessage = "Username or Password Incorrect";
                    return View();
                }
            }
            ViewBag.ErrorMessage = "Username and Password is Wrong";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "You are not allowed to view this page";
            return View();
        }

        //Logout for Administrative Users
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        // GET:ApplicationUsers/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationuser = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (applicationuser == null)
            {
                return NotFound();
            }

            return View(applicationuser);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(string id)
        {
            return _userManager.Users.Any(i => i.Id == id);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SigninViewModel());
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SigninViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var NewUser = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        NormalizedUserName = model.Username.ToUpper(),
                        NormalizedEmail = model.Username.ToUpper(),
                        LoginHint = model.Password,
                    };

                    var Result = await _userManager.CreateAsync(NewUser, model.Password);
                    await _userManager.AddToRoleAsync(NewUser, "SuperAdmin");

                    if (Result.Succeeded)
                    {
                        await _signInManager.SignInAsync(NewUser, isPersistent: false);
                        return RedirectToAction("Index", "Account");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "User Could not be created" + e;
                    return View();
                }
            }
            return RedirectToAction("Create", "Account");
        }

        // GET: SuperAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var M = new ApplicationUser
            {
                Id = user.Id,
                LoginHint = user.LoginHint,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
                NormalizedUserName = user.NormalizedUserName,
                NormalizedEmail = user.NormalizedEmail,
            };
            return View(M);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



    }
}
