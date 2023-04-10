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

namespace StudentManage.Areas.StudentManage.Controllers
{
    /// <summary>
    /// 授课信息管理
    /// </summary>
    [Area("StudentManage")]
    public class TeachInfoController : Controller
    {

        private readonly ILogger<TeachInfoController> _logger;
        private readonly ITeachInfoRepository _teachInfoRepository;

        public TeachInfoController(ILogger<TeachInfoController> logger, ITeachInfoRepository teachInfoRepository)
        {
            _logger = logger;
            _teachInfoRepository = teachInfoRepository;
        }

        /// <summary>
        /// 授课信息管理页面
        /// </summary>
        public IActionResult Index()
        {
            return PartialView();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> List(ReqTeachInfoList req)
        {
            var result = new ResponseMessage();
            try
            {
                var users = await _teachInfoRepository.GetPageDataAsync(req.TeacherName, req.CourseName, req.Page, req.Limit);
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
        public async Task<IActionResult> Add(TeachInfoModel model)
        {
            var result = new ResponseMessage();
            try
            {
                var teachInfo = new TeachInfo
                {
                    MajorId = model.MajorId,
                    CourseId = model.CourseId,
                    TeacherId = model.TeacherId,
                    Hours = model.Hours,
                    Credit = model.Credit
                };
                await _teachInfoRepository.AddAsync(teachInfo);

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
        public async Task<IActionResult> Edit(TeachInfoModel model)
        {
            var result = new ResponseMessage();
            try
            {

                var modelDb = await _teachInfoRepository.SingleAsync(model.Id);
                if (modelDb == null) return Json(result);

                modelDb.MajorId = model.MajorId;
                modelDb.ClassId = model.ClassId;
                modelDb.TeacherId = model.TeacherId;
                modelDb.Hours = model.Hours;
                modelDb.Credit = model.Credit;

                await _teachInfoRepository.UpdateAsync(modelDb);

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
                var teachInfo = await _teachInfoRepository.SingleAsync(id);
                if (teachInfo == null) return Json(result);

                if (await _teachInfoRepository.DeleteAsync(teachInfo))
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