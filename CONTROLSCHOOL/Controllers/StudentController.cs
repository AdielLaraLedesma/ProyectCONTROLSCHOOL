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
    public class StudentController : Controller
    {
        // GET: Student
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
                var mgr = new StudentManager();
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
                var mgr = new StudentManager();
                result.Result = mgr.Get(text);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult Register(Student info)
        {
            var result = new ResponseObject();
            try
            {
                var mgr = new StudentManager();
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
        public JsonResult Modify(Student info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new StudentManager();
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
        public JsonResult Delete(Student info)
        {
            var result = new ResponseObject();
            result.Message = "ok";
            try
            {
                var mgr = new StudentManager();
                result.Result = mgr.Delete(info);
            }
            catch(Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            }
            return Json(result);
        }


    }
}