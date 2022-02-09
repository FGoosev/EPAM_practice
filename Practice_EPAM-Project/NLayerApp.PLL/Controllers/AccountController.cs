using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NLayerApp.PLL.Models;
using Entities;
using BLayer.Interfaces;
using BLayer.DTO;

namespace NLayerApp.PLL.Controllers
{
    public class AccountController : Controller
    {

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IFileService FileService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IFileService>();
            }
        }



        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            IEnumerable<File> files = FileService.GetFiles();

            return View(files);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {

                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthenticateAsync(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                    return RedirectToAction("Login");
                }

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "Account");


            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return View("Login");
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditModel editModel)
        {
            
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Name = editModel.Name, Surname = editModel.Surname, Age = editModel.Age };
                await UserService.EditAsync(userDto);
                return RedirectToAction("Edit");

            }
            return View(editModel);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Name = model.Name,
                    Password = model.Password,
                    Age = model.Age,
                    Surname = model.Surname,
                    Email = model.Email
                };
                await UserService.CreateAsync(userDto);
                return View("Index");
                

            }
            return View(model);
        }

        public ActionResult FileDownloader()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FileDownloader(FileViewModel file, HttpPostedFileBase uploadFile)
        {

            if (ModelState.IsValid && uploadFile != null)
            {
                byte[] fileData = null;
                using (var binaryReader = new System.IO.BinaryReader(uploadFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(uploadFile.ContentLength);

                    var fileDTO = new FileDTO
                    {
                        FileName = file.FileName,
                        Data = fileData
                    };
                    FileService.Create(fileDTO);
                    return RedirectToAction("Index", "Account");
                }
            }
            return View(file);
        }

        public ActionResult FileUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FileUpdate(FileUpdate model)
        {
            if (ModelState.IsValid)
            {
                FileDTO fileDTO = new FileDTO { FileName = model.FileName };
                FileService.Update(fileDTO);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}