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
    /// 课程管理
    /// </summary>
    [Area("StudentManage")]
    public class CourseController : Controller
    {

        private readonly ILogger<CourseController> _logger;
        private readonly ICourseRepository _courseRepository;

        public CourseController(ILogger<CourseController> logger, ICourseRepository courseRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
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
                var classes = await _courseRepository.GetPageDataAsync(req.Name, req.Page, req.Limit);
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
        public async Task<IActionResult> CourseList(int studentId)
        {
            var result = new ResponseMessage();
            try
            {
                var courses = await _courseRepository.FindAllAsync<Course>();
                if (studentId > 0)
                {
                    var studentCourses = await _courseRepository.GetCoursesByStudentId(studentId);
                    var studentCourseIds = studentCourses.Select(n => n.Id).ToList();
                    courses = courses.Where(n => studentCourseIds.Contains(n.Id)).ToList();
                }
                var courseSelectData = courses.Select(c => new XmSelectTree() { Value = c.Id.ToString(), Name = c.Name }).ToList();
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = courseSelectData;
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
        public async Task<IActionResult> Add(CourseModel courseModel)
        {
            var result = new ResponseMessage();
            try
            {
                var courseInfo = new Course()
                {
                    Id = courseModel.Id,
                    MajorId = courseModel.MajorId,
                    SerialNumber = courseModel.SerialNumber,
                    Name = courseModel.Name,
                    Description = courseModel.Description
                };
                if (await _courseRepository.AddAsync(courseInfo) > 0)
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
        public async Task<IActionResult> Edit(CourseModel courseModel)
        {
            var result = new ResponseMessage();
            try
            {
                var courseInfo = new Course()
                {
                    Id = courseModel.Id,
                    MajorId = courseModel.MajorId,
                    SerialNumber = courseModel.SerialNumber,
                    Name = courseModel.Name,
                    Description = courseModel.Description
                };
                if (await _courseRepository.UpdateAsync(courseInfo))
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
                var Class = await _courseRepository.SingleAsync(id);
                if (Class == null) return Json(result);

                if (await _courseRepository.DeleteAsync(Class))
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