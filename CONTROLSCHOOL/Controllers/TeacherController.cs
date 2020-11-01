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
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetByID(int ID)
        {
            var result = new ResponseObject();
            result.Message = "ok";

            try
            {
                var mgr = new TeacherManager();
                result.Result = mgr.GetByID(ID);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }

            return Json(result);
        }


        [HttpPost]
        public JsonResult Get(string text)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new TeacherManager();
                result.Result = mgr.Get(text);
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(Teacher info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new TeacherManager();
                result.Result = mgr.Register(info);
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult Modify(Teacher info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new TeacherManager();
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
        public JsonResult Delete(Teacher info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new TeacherManager();
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