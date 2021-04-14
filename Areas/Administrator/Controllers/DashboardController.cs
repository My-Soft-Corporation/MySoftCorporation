using Microsoft.AspNet.Identity;
using MySoft.Employee.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using MySoftCorporation.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class DashboardController : AdminAuthorizeController
    {
        private readonly EmployeeService _employeeService;
        private readonly StudentService _studentService;
        private readonly UserService _userService;
        private readonly AdmissionService _admissionService;
        // GET: Administrator/Dashboard
        public DashboardController()
        {
            _employeeService = new EmployeeService();
            _studentService = new StudentService();;
            _userService = new UserService();
            _admissionService = new AdmissionService();
        }
        public async Task<ActionResult> Index()
        {
            DashboardModel model = new DashboardModel() {
                Employees = await _employeeService.GetCount(),
                Students = await _studentService.GetCount(),
                Users = await _userService.GetCount(),
                Admissions = await _admissionService.GetCount()
            };
            
            return View(model);
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
                    ModifiedOn = DateTimeHelper.Now()
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