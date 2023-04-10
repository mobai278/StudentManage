using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.DbEntity;
using Repository.IRepository;
using Repository.Model;
using StudentManage.Areas.StudentManage.Models;
using StudentManage.Areas.StudentManage.Models.Requests;
using StudentManage.Areas.StudentManage.Models.Response.Tree;

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Area("StudentManage")]
    public class ClassController : Controller
    {

        private readonly ILogger<ClassController> _logger;
        private readonly IClassRepository _classRepository;

        public ClassController(ILogger<ClassController> logger, IClassRepository classRepository)
        {
            _logger = logger;
            _classRepository = classRepository;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqClassList req)
        {
            var result = new ResponseMessage();
            try
            {
                var classes = await _classRepository.GetPageDataAsync(req.Name, req.Page, req.Limit);
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = classes.Items;
                result.Count = classes.TotalCount;
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
        public async Task<IActionResult> ClassList()
        {
            var result = new ResponseMessage();
            try
            {
                var classes = await _classRepository.FindAllAsync<Class>();
                var classSelectData = classes.Select(cla => new XmSelectTree() { Value = cla.Id.ToString(), Name = cla.Name }).ToList();

                result.Message = "获取成功";
                result.Msg = true;
                result.Data = classSelectData;
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
        public async Task<IActionResult> Add(ClassModel classModel)
        {
            var result = new ResponseMessage();
            try
            {
                var classInfo = new Class()
                {
                    Id = classModel.Id,
                    SerialNumber = classModel.SerialNumber,
                    MajorId = classModel.MajorId,
                    Name = classModel.Name,
                    Description = classModel.Description
                };
                if (await _classRepository.AddAsync(classInfo) > 0)
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
        public async Task<IActionResult> Edit(ClassModel classModel)
        {
            var result = new ResponseMessage();
            try
            {
                var classInfo = new Class()
                {
                    Id = classModel.Id,
                    SerialNumber = classModel.SerialNumber,
                    MajorId = classModel.MajorId,
                    Name = classModel.Name,
                    Description = classModel.Description
                };
                if (await _classRepository.UpdateAsync(classInfo))
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
                var Class = await _classRepository.SingleAsync(id);
                if (Class == null) return Json(result);

                if (await _classRepository.DeleteAsync(Class))
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