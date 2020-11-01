using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Common.Models;
using SCHOOLCONTROL.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CONTROLSCHOOL.Controllers
{
    public class CourseStudentController : Controller
    {
        // GET: CourseStudent
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Get()
        {
            var result = new ResponseObject();
            result.Message = "ok";

            try
            {
                var mgr = new CourseStudentManager();
                result.Result = mgr.Get();
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult Register(CourseStudent info)
        {
            var result = new ResponseObject();
            try
            {
                var mgr = new CourseStudentManager();
                result.Result = mgr.Register(info);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult Modify(CourseStudent info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new CourseStudentManager();
                result.Result = mgr.Modify(info);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult Delete(CourseStudent info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new CourseStudentManager();
                result.Result = mgr.Delete(info);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }



    }
}