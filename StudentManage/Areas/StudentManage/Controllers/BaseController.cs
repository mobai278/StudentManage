using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.DbEntity;
using Repository.IRepository;
using StudentManage.Areas.StudentManage.Models;
using StudentManage.Areas.StudentManage.Models.Response.Tree;

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 基础信息
    /// </summary>
    [Area("StudentManage")]
    public class BaseController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IBaseRepository _baseRepository;
        private readonly IStudentRepository _studentRepository;

        public BaseController(ILogger<StudentController> logger, IBaseRepository baseRepository, IStudentRepository studentRepository)
        {
            _logger = logger;
            _baseRepository = baseRepository;
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 通过学生id获取学生所在院校，专业，班级
        /// </summary>
        /// <param name="studentId">学生id</param>
        /// <returns>返回全部的院校专业班级多级tree，如果学生id>0返回的数据中该学生所在班级是选中状态</returns>
        [HttpGet]
        public async Task<IActionResult> DepartmentMajorClass(int studentId)
        {
            var result = new ResponseMessage();
            try
            {
                var departmentList = await _baseRepository.FindAllAsync<Department>();
                var majorList = await _baseRepository.FindAllAsync<Major>();
                var classList = await _baseRepository.FindAllAsync<Class>();
                var student = studentId > 0 ? await _studentRepository.SingleAsync(studentId) : new Student();
                var departmentMajorClassTree = new List<XmSelectTree>();
                foreach (var department in departmentList)
                {
                    departmentMajorClassTree.Add(new XmSelectTree()
                    {
                        Value = "d" + department.Id,
                        Name = department.Name
                    });
                }

                foreach (var tree in departmentMajorClassTree)
                {
                    var majorTreeList = majorList.Where(m => m.DepartmentId.ToString() == tree.Value.Substring(1))
                        .Select(m =>
                            new XmSelectTree()
                            {
                                Value = "m" + m.Id,
                                Name = m.Name
                            }).ToList();
                    foreach (var majorTree in majorTreeList)
                    {
                        var classTreeList = classList.Where(c => c.MajorId.ToString() == majorTree.Value.Substring(1))
                            .Select(c => new XmSelectTree()
                            {
                                Value = "c" + c.Id,
                                Name = c.Name,
                                Selected = student.ClassId == c.Id
                            }).ToList();
                        majorTree.Children.AddRange(classTreeList);
                        if (!classTreeList.Any())
                        {
                            majorTree.Disabled = true;
                        }
                    }
                    tree.Children.AddRange(majorTreeList);
                }

                result.Data = departmentMajorClassTree;
                result.Message = "获取成功";
                result.Msg = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        /// <summary>
        /// 获取专业
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> MajorList()
        {
            var result = new ResponseMessage();
            try
            {
                //获取全部
                var data = await _baseRepository.FindAllAsync<Major>();
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = data.Select(n => new { Name = n.Name, Value = n.Id });
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
    }
}