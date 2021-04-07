using MySoft.Employee.Entities;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class DashboardController : Controller
    {
        // GET: Administrator/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPictures()
        {
            var files = Request.Files;
            JsonResult jsonResult = new JsonResult();
            List<Picture> listPics = new List<Picture>();
            for (int i = 0; i < files.Count; i++)
            {
                var picture = files[i];
                var pathToImagesFolder = Server.MapPath("~/Images/Course/");
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = pathToImagesFolder + fileName;
                picture.SaveAs(filePath);
                Picture dbPicture = new Picture
                {
                    URL = fileName,
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location(),
                    ModifiedOn = DateTime.Now
                };
                DashboardService dashboardService = new DashboardService();
                if (dashboardService.SavePicture(dbPicture))
                {
                    listPics.Add(dbPicture);
                }
            }
            jsonResult.Data = listPics;
            return jsonResult;
        }
    }
}