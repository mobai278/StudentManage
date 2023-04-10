using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.DbEntity;
using Repository.IRepository;
using Repository.Model;
using StudentManage.Areas.StudentManage.Models;
using StudentManage.Areas.StudentManage.Models.Module;
using StudentManage.Areas.StudentManage.Models.Requests;

namespace StudentManage.Areas.StudentManage.Controllers
{
    [Area("StudentManage")]
    public class ModuleController : Controller
    {
        private readonly ILogger<ModuleController> _logger;
        private readonly IModuleRepository _moduleRepository;
       
        public ModuleController(ILogger<ModuleController> logger, IModuleRepository moduleRepository)
        {
            _logger = logger;
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// 获取模块
        /// </summary>
        /// 
        public async Task<IActionResult> Modules(string token)
        {
            var user = HttpContext.Session.GetObjectFromJson<CurrentUser>(token);
            if (user == null) return Json(new { status = "error", msg = "获取菜单失败" });

            var treeModel =new List<Module>();
            if (user.Role == 1)
            {
                treeModel = await _moduleRepository.GetTeacherModules(user.RoleIds);
            }
            else if (user.Role == 2)
            {
                treeModel = await _moduleRepository.GetStudentModules(user.RoleIds);
            }
            else
            {
                treeModel = await _moduleRepository.GetModules(user.RoleIds);
            }


            var model = treeModel.Where(n => n.ParentId == 0).Select(n => new TreeModel(n, treeModel)).ToList();

            return Json(new { status = "success", msg = "获取成功", Info = model });
        }

        /// <summary>
        /// 通过角色获取模块
        /// </summary>
        /// <param name="req">角色id，如果角色id为0，获取所有的菜单</param>
        [HttpGet]
        public async Task<IActionResult> ListByRoleId(ReqListByRoleId req)
        {
            var result = new ResponseMessage();
            try
            {
                //获取全部菜单
                var allModules = await this._moduleRepository.FindAllAsync<Module>();
                var manageModules = allModules.Where(n => n.Type == 0).ToList();
                var roleModuleIds = new List<int>();
                //已经拥有的菜单权限
                if (req.RoleId > 0)
                {
                    var modules = await this._moduleRepository.GetModules(new List<int> { req.RoleId });
                    roleModuleIds = modules.Select(n => n.Id).ToList();
                }

                //获取一级菜单
                var parent = manageModules.Where(n => n.ParentId == 0).Select(n => new LayTreeModel()
                {
                    Id = n.Id.ToString(),
                    Title = n.Name,
                    Checked = roleModuleIds.Contains(n.Id),
                }).ToList();

                //获取子菜单
                parent.ForEach(n =>
                {
                    n.Children.AddRange(GetChildrenModule(manageModules, n, roleModuleIds));
                });

                result.Message = "获取成功";
                result.Msg = true;
                result.Data = parent;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        private List<LayTreeModel> GetChildrenModule(List<Module> allModules, LayTreeModel parent, List<int> roleModuleIds)
        {
            var children = allModules.Where(n => n.ParentId.ToString().Equals(parent.Id)).Select(n => new LayTreeModel()
            {
                Id = n.Id.ToString(),
                Title = n.Name,
                Checked = roleModuleIds.Contains(n.Id),
            }).ToList();

            //获取子菜单
            children.ForEach(n =>
            {
                n.Children.AddRange(GetChildrenModule(allModules, n, roleModuleIds));
            });

            return children;
        }
    }
}