using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.StudentPortal.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentAuthorizeController : Controller
    {
    }
}