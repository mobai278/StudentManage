using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.IRepository;

namespace StudentManage.Areas.StudentManage.Controllers
{
    [Area("StudentManage")]
    public class UserInfoController : Controller
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly IAuthRepository _authRepository;
        private readonly IRelationRepository _relationRepository;

        public UserInfoController(ILogger<UserInfoController> logger, IAuthRepository authRepository, IRelationRepository relationRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns>显示登陆页面</returns>
        public IActionResult Index()
        {
            return PartialView();
        }

    }
}
