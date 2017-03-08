using NSass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SassServerCompile.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveStyles(string color,string font)
        {
            //Set the sass variables from the input field
            string scssString = @"$mainColor:"+color+";$mainFont:"+font+";";

            //Write the new variables to the custom variables scss file
            System.IO.File.WriteAllText(Server.MapPath("~/Content/scss/_variablesCustom.scss"), scssString);

            //Initiate a new sass compiler
            ISassCompiler sassCompiler = new SassCompiler();

            //Compile the styles.scss sass file
            string sassOutput = sassCompiler.CompileFile(Server.MapPath("~/Content/scss/styles.scss"), OutputStyle.Compressed, sourceComments: false);

            //And write the compiled output to the styles.css file, the one included on your page
            System.IO.File.WriteAllText(Server.MapPath("~/Content/styles.css"), sassOutput);

            return RedirectToAction("Index");
        }
    }
}