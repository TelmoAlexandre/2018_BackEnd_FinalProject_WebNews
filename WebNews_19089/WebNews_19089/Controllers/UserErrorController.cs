using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNews_19089.Models;

namespace WebNews_19089.Controllers
{
    public class UserErrorController : Controller
    {
        // GET: UserError
        public ActionResult Index(string error, string details)
        {
            // Verificar se recebeu o erro
            if(error != null)
            {
                return View(new UserErrorModelView {
                    Error = error,
                    AdditionalDetails = details
                });
            }
            else
            {
                // Caso não tenha recebido um erro, criar um erro genérico
                return View(new UserErrorModelView
                {
                    Error = "Something went wrong.",
                    AdditionalDetails = null
                });
            }
        }
    }
}