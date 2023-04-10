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
    /// 角色管理
    /// </summary>
    [Area("StudentManage")]
    public class RoleController : Controller
    {

        private readonly ILogger<RoleController> _logger;
        private readonly IRoleRepository _roleRepository;
        private readonly IRelationRepository _relationRepository;

        public RoleController(ILogger<RoleController> logger, IRoleRepository roleRepository,IRelationRepository relationRepository)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _relationRepository = relationRepository;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqRoleList req)
        {
            var result = new ResponseMessage();
            try
            {
                var roles = await _roleRepository.GetRolePageDataAsync(req.Name, req.Page, req.Limit);
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = roles.Items;
                result.Count = roles.TotalCount;
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
        public async Task<IActionResult> Add(Role role)
        {
            var result = new ResponseMessage();
            try
            {
                if (await _roleRepository.AddAsync(role) > 0)
                {
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
        public async Task<IActionResult> Edit(Role role)
        {
            var result = new ResponseMessage();
            try
            {
                if (await _roleRepository.UpdateAsync(role))
                {
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
                var role = await _roleRepository.SingleAsync(id);
                if (role == null) return Json(result);

                if (await _roleRepository.DeleteAsync(role))
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

        
        /// <summary>
        /// 通过userId获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ListByUser(int userId)
        {
            var result = new ResponseMessage();
            try
            {
                var roles = await _roleRepository.GetRoleListByUserIdAsync(userId);
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = roles;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> SaveRoleModule(ReqSaveRoleModule req)
        {
            var result = new ResponseMessage();
            try
            {
                //删除现有关联
                var roleModules =  await _relationRepository.GetRelationsByFirstId("RoleModule", req.RoleId);
                await _relationRepository.DeleteListAsync(roleModules);

                //新增关联
                if (!string.IsNullOrEmpty(req.ModuleIds))
                {
                    var relations = new List<Relation>();
                    var modules = req.ModuleIds.Split(',').Select(n => Convert.ToInt32(n)).ToList();
                    foreach (var moduleId in modules)
                    {
                        relations.Add(new Relation()
                        {
                            Key = "RoleModule",
                            FirstKeyId = req.RoleId,
                            SecondKeyId = moduleId
                        });
                    }

                    if (relations.Any())
                    {
                        await _relationRepository.AddListAsync(relations);
                    }
                }
                result.Message = "保存成功";
                result.Msg = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }
    }
}