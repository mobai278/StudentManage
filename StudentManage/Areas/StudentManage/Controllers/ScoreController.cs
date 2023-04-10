using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.DbEntity;
using Repository.IRepository;
using Repository.Model;
using StudentManage.Areas.StudentManage.Models;
using StudentManage.Areas.StudentManage.Models.Requests;

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 成绩管理
    /// </summary>
    [Area("StudentManage")]
    public class ScoreController : Controller
    {
        private readonly ILogger<ScoreController> _logger;
        private readonly IScoreRepository _scoreRepository;
        private readonly IStudentRepository _studentRepository;
        private int aa;

        public ScoreController(ILogger<ScoreController> logger, IScoreRepository scoreRepository, IStudentRepository studentRepository)
        {
            _logger = logger;
            _scoreRepository = scoreRepository;
            _studentRepository = studentRepository;

        }

        /// <summary>
        /// 成绩管理页面
        /// </summary>
        public IActionResult Index()
        {
            return PartialView();
        }

        public IActionResult StudentIndex()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqScoreList req)
        {
            var result = new ResponseMessage();
            try
            {
                string studentName= HttpContext.Session.GetString("studentName");
                string id=null;
                if (studentName != null) {
                   id = studentName;
                }

                var users = await _scoreRepository.GetPageDataAsync(id,req.StudentName,req.CourseName, req.Page, req.Limit);
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

        ///// <summary>
        ///// 新增
        ///// </summary>
        //[HttpPost]
        public async Task<IActionResult> Add(StudentScoreModel model)
        {
            var result = new ResponseMessage();
            string name = null ;
            string majorName = null;
            string courseName = null;
            
            try
            {
                IEnumerable<string> enumerable = await _studentRepository.getStudent(model.studentId);
                name = enumerable.ToList()[0];

                IEnumerable<string> majorErable = await _studentRepository.getMajor(model.majorId);
                majorName = majorErable.ToList()[0];

                IEnumerable<string> courseErable = await _studentRepository.getCourse(model.courseId);
                courseName = courseErable.ToList()[0];

                var score = new StudentScore
                {
                    Id = aa,
                    studentId = model.studentId,
                    studentName = name,
                    majorId = model.majorId,
                    majorName = majorName,
                    courseId = model.courseId,
                    courseName = courseName,
                    stuTime = model.stuTime,
                    stuGrade = model.stuGrade,
                    stuScore = model.stuScore
                };
                await _scoreRepository.AddAsync(score);

                result.Message = "新增成功";
                result.Msg = true;

            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        ///// <summary>
        ///// 编辑
        ///// </summary>
        //[HttpPost]
        public async Task<IActionResult> Edit(StudentScore model)
        {
           var result = new ResponseMessage();
            try
            {
                IEnumerable<string> enumerable = await _studentRepository.getStudent(model.studentId);
                string  name = enumerable.ToList()[0];

                IEnumerable<string> majorErable = await _studentRepository.getMajor(model.majorId);
                string majorName = majorErable.ToList()[0];

                IEnumerable<string> courseErable = await _studentRepository.getCourse(model.courseId);
                string courseName = courseErable.ToList()[0];
                model.studentName = name;
                model.majorName = majorName;
                model.courseName = courseName;
                await _scoreRepository.UpdateAsync(model);

                result.Message = "编辑成功";
                result.Msg = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return Json(result);
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        //[HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ResponseMessage();
            try
            {
                var score = await _scoreRepository.SingleAsync(id);
                if (score == null) return Json(result);

               if (await _scoreRepository.DeleteAsync(score))
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