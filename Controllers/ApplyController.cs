using MySoft.Institute.Entities;
using MySoftCorporation.Helpers;
using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System;
using System.Web.Mvc;

namespace MySoftCorporation.Controllers
{
    [Authorize]
    public class ApplyController : Controller
    {
        private readonly CityService cityService = new CityService();
        private readonly StudentService studentService = new StudentService();
        private readonly BloodGroupService bloodGroupService = new BloodGroupService();

        // GET: Apply
        public ActionResult Index(int courseID)
        {
            Student student = studentService.GetStudentByUserId(UserHelperInfo.GetUserId());
            if (student != null)
            {
                StudentsActionModel studentsActionModel = new StudentsActionModel
                {
                    ID = student.ID,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    CNIC = student.CNIC,
                    FatherName = student.FatherName,
                    FatherCNIC = student.FatherCNIC,
                    FatherProfession = student.FatherProfession,
                    StudentContact = student.StudentContact,
                    FatherContact = student.FatherContact,
                    EmergencyContact = student.EmergencyContact,
                    DateOfBirth = student.DateOfBirth,
                    Gender = student.Gender,
                    PresentAddress = student.PresentAddress,
                    PermenantAddress = student.PermenantAddress,
                    MaritalStatus = student.MaritalStatus,
                    CityID = student.CityID,
                    BloodGroupID = student.BloodGroupID,
                    BloodGroups = bloodGroupService.GetAll(),
                    Cities = cityService.GetAll(),
                    CourseID = courseID
                };
                return View(studentsActionModel);
            }
            else
            {
                StudentsActionModel studentsActionModel = new StudentsActionModel
                {
                    BloodGroups = bloodGroupService.GetAll(),
                    Cities = cityService.GetAll(),
                    CourseID = courseID
                };
                return View(studentsActionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public JsonResult ApplyNow(StudentsActionModel studentsActionModel)
        {
            JsonResult jsonResult = new JsonResult();
            Student student = new Student
            {
                ID = studentsActionModel.ID,
                FirstName = studentsActionModel.FirstName,
                LastName = studentsActionModel.LastName,
                CNIC = studentsActionModel.CNIC,
                FatherName = studentsActionModel.FatherName,
                FatherCNIC = studentsActionModel.FatherCNIC,
                FatherProfession = studentsActionModel.FatherProfession,
                StudentContact = studentsActionModel.StudentContact,
                FatherContact = studentsActionModel.FatherContact,
                EmergencyContact = studentsActionModel.EmergencyContact,
                DateOfBirth = studentsActionModel.DateOfBirth,
                Gender = studentsActionModel.Gender,
                PresentAddress = studentsActionModel.PresentAddress,
                PermenantAddress = studentsActionModel.PermenantAddress,
                UserID = UserHelperInfo.GetUserId(),
                ModifiedOn = DateTime.Now,
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent(),
                Location = UserInfo.Location(),
                MaritalStatus = studentsActionModel.MaritalStatus,
                CityID = studentsActionModel.CityID,
                BloodGroupID = studentsActionModel.BloodGroupID
            };
            Admission admission = new Admission
            {
                CourseID = studentsActionModel.CourseID,
                StudentID = studentsActionModel.ID,
                UserID = UserHelperInfo.GetUserId(),
                ModifiedOn = DateTime.Now,
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent(),
                Location = UserInfo.Location()
            };
            AdmissionService admissionService = new AdmissionService();
            var (isTrue, Msg) = admissionService.Admission(student, admission);

            jsonResult.Data = new { Success = isTrue, Message = Msg };
            return jsonResult;
        }
    }
}