using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.DbEntity;
using Repository.IRepository;
using StudentManage.Areas.StudentManage.Models;
using StudentManage.Areas.StudentManage.Models.Requests;

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Area("StudentManage")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRelationRepository _relationRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IRelationRepository relationRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// 用户管理页面
        /// </summary>
        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqUserList req)
        {
            var result = new ResponseMessage();
            try
            {
                var users = await _userRepository.GetPageDataAsync(req.UserName, req.Phone, req.Page, req.Limit);
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = users.Items;
                result.Count = users.TotalCount;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add(User user, string role)
        {
            var result = new ResponseMessage();
            try
            {
                user.Password = "111111"; //默认密码 111111
                if (await _userRepository.AddAsync(user) > 0)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        var roleList = role.Split(',').ToList();
                        var relations = new List<Relation>();
                        foreach (var item in roleList)
                        {
                            relations.Add(new Relation
                            {
                                Key = "UserRole",
                                FirstKeyId = user.Id,
                                SecondKeyId = Convert.ToInt32(item)
                            });
                        }

                        if (relations.Any())
                        {
                            await _relationRepository.AddListAsync(relations);
                        }
                    }
                    result.Message = "新增成功";
                    result.Msg = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(User user, string role)
        {
            var result = new ResponseMessage();
            try
            {
                var userDb = await _userRepository.SingleAsync(user.Id);
                user.Password = userDb.Password;

                if (await _userRepository.UpdateAsync(user))
                {
                    //删除原有角色
                    var roleListDb = await _relationRepository.GetRelationsByFirstId("UserRole", user.Id);
                    await _relationRepository.DeleteListAsync(roleListDb);
                    //添加新角色
                    if (!string.IsNullOrEmpty(role))
                    {
                        var roleList = role.Split(',').ToList();
                        var relations = new List<Relation>();
                        foreach (var item in roleList)
                        {
                            relations.Add(new Relation
                            {
                                Key = "UserRole",
                                FirstKeyId = user.Id,
                                SecondKeyId = Convert.ToInt32(item)
                            });
                        }

                        if (relations.Any())
                        {
                            await _relationRepository.AddListAsync(relations);
                        }
                    }

                    result.Message = "编辑成功";
                    result.Msg = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ResponseMessage();
            try
            {
                var user = await _userRepository.SingleAsync(id);
                if (user == null) return Json(result);

                if (await _userRepository.DeleteAsync(user))
                {
                    result.Message = "删除成功";
                    result.Msg = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }


    }
}