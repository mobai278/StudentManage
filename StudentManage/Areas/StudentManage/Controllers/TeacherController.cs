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
using StudentManage.Areas.StudentManage.Models.Response.Tree;

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 教师管理
    /// </summary>
    [Area("StudentManage")]
    public class TeacherController : Controller
    {

        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ILogger<TeacherController> logger, ITeacherRepository teacherRepository)
        {
            _logger = logger;
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// 教师管理页面
        /// </summary>
        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqTeacherList req)
        {
            var result = new ResponseMessage();
            try
            {
                var users = await _teacherRepository.GetPageDataAsync(req.UserName, req.Phone, req.Page, req.Limit);
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
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> TeacherList()
        {
            var result = new ResponseMessage();
            try
            {
                var teachers = await _teacherRepository.FindAllAsync<Teacher>();
                var teacherSelectData = teachers.Select(teacher => new XmSelectTree() {Value = teacher.Id.ToString(), Name = teacher.RealName}).ToList();
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = teacherSelectData;
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
        public async Task<IActionResult> Add(Teacher teacher)
        {
            var result = new ResponseMessage();
            try
            {
                teacher.Password = "123456"; //默认密码 123456
                await _teacherRepository.AddAsync(teacher);

                result.Message = "新增成功";
                result.Msg = true;

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
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            var result = new ResponseMessage();
            try
            {
                var userDb = await _teacherRepository.SingleAsync(teacher.Id);
                teacher.Password = userDb.Password;

                await _teacherRepository.UpdateAsync(teacher);

                result.Message = "编辑成功";
                result.Msg = true;
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
                var teacher = await _teacherRepository.SingleAsync(id);
                if (teacher == null) return Json(result);

                if (await _teacherRepository.DeleteAsync(teacher))
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