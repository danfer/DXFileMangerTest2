using DXFileMangerTest2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXFileMangerTest2.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult GetDocuments()        
        {
            var provider = new AwsS3FileProvider("SomeSystemsTest");
            return PartialView("_DocumentManagerPartial", provider);
        }
    }
}