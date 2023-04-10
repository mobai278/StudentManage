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
    /// 学生信息管理
    /// </summary>
    [Area("StudentManage")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqStudentList req)
        {
            var result = new ResponseMessage();
            try
            {
                var users = await _studentRepository.GetPageDataAsync(req.UserName, req.Phone, req.Page, req.Limit);
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

        public async Task<IActionResult> ListTotal(ReqStudentList req)
        {
            var result = new ResponseMessage();

      
            try
            {
                //  var sql = "select Id ,SerialNumber, UserName,RealName as name";
                //  var users = await _studentRepository.FindAllAsync<StudentModel>(sql);
                //result.Message = "获取成功";
                //  result.Msg = true;
                //  result.Data = classSelectData;
                //获取全部

                var data = await _studentRepository.FindAllAsync<Student>();
                result.Message = "获取成功";
                result.Msg = true;
                result.Data = data.Select(n => new { Name = n.RealName, Value = n.SerialNumber });

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
        public async Task<IActionResult> Add(Student student, string select,string enterTime)
        {
            var result = new ResponseMessage();
            if (!select.Contains("c")) return Json(result);
            try
            {
                student.ClassId = Convert.ToInt32(select.Substring(1));
                student.EnterDate = "2020";
                student.Password = "000000"; //默认密码 000000
                await _studentRepository.AddAsync(student);

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
        public async Task<IActionResult> Edit(Student student)
        {
            var result = new ResponseMessage();
            try
            {
                var userDb = await _studentRepository.SingleAsync(student.Id);
                student.Password = userDb.Password;

                await _studentRepository.UpdateAsync(student);

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
                var student = await _studentRepository.SingleAsync(id);
                if (student == null) return Json(result);

                if (await _studentRepository.DeleteAsync(student))
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