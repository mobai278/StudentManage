using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.IRepository;
using Repository.Model;
using StudentManage.Models;

namespace StudentManage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthRepository _authRepository;
        private readonly IRelationRepository _relationRepository;

        public HomeController(ILogger<HomeController> logger, IAuthRepository authRepository, IRelationRepository relationRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns>显示登陆页面</returns>
        public IActionResult Login()
        {
            return PartialView();
        }

        /// <summary>
        /// 退出页面
        /// </summary>
        /// <returns>显示登陆页面</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return PartialView("Login");
        }

        /// <summary>
        /// 校验登录
        /// </summary>
        /// <returns>返回成功失败信息</returns>
        [HttpPost]
        public async Task<IActionResult> CheckLogin(string userName, string password, int role)
        {
            var userId = await _authRepository.Login(userName, password, role);

            if (userId > 0)
            {
                var token = Guid.NewGuid().ToString().GetHashCode().ToString("x");
                var currentUser = new CurrentUser {Role = role, UserId = userId};
                HttpContext.Session.SetString("UserName", userName);
         
                if (role == 0)
                { 
                    var roles = await _relationRepository.GetRelationsByFirstId("UserRole", userId);
                    currentUser.RoleIds = roles.Select(n => n.SecondKeyId).ToList();
                }
                if (role == 2) {
                    HttpContext.Session.SetString("studentName", userName);
                }
                HttpContext.Session.SetObjectAsJson(token, currentUser);
                return Json(new { status = "success", msg = "登录成功", Token = token });
            }

            return Json(new { status = "error", msg = "登录失败" });
        }

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            string name =HttpContext.Session.GetString("UserName");
            ViewBag.RealName = name;
            return View();
        }

        /// <summary>
        /// 错误页
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
