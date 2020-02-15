using DataBaseConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using WebPorject.Models;

namespace WebPorject.Controllers
{
    public class logController : Controller
    {
        /////////////////////////////////////////// user
        [HttpGet]
        public new ActionResult User()
        {
            return View();
        }
        public user obj;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logina(user us)
        {
            if (ModelState.IsValid)
            {
                PDBC db = new PDBC("webmvc", true);
                //List<ExcParameters> paramss = new List<ExcParameters>();
                //ExcParameters parameters = new ExcParameters()
                //{
                //    _KEY = "@id",
                //    _VALUE = posted
                //};
                //paramss.Add(parameters);

                db.Connect();
                using (DataTable dt = db.Select($"SELECT [id],[username],[password],[name],[email] FROM [dbo].[user] where [username] like N'{us.username}' AND [password] like N'{us.password}'"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        user data = new user()
                        {
                            name = dt.Rows[0]["name"].ToString()
                        };
                        Session["d1"] = data;
                        return RedirectToAction("hellow");
                    }
                    else
                    {
                        return Content("failed");
                    }
                }
            }
            else
            {
               return Content("failed");
            }
        }
        // GET: log
        ///////////////////////////////////////// hellow
        [HttpGet]
        public ActionResult hellow()
        {
            obj = (user)Session["d1"];
            ViewBag.name = obj.name;
            return View();
        }
        ///////////////////////////////////////// newpost
        [HttpGet]
        public ActionResult newpost()
        {
            if (Session["d1"]!=null)
            {
             return View();
            }
            else
            {
                return RedirectToAction("User");
            }
           
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult getpost(string name, string text, string image)
        {

            PDBC db = new PDBC("webmvc", true);
            //List<ExcParameters> paramss = new List<ExcParameters>();
            //ExcParameters parameters = new ExcParameters()
            //{
            //    _KEY = "@id",
            //    _VALUE = posted
            //};
            //paramss.Add(parameters);

            db.Connect();
            string res = db.Script($"INSERT INTO [dbo].[postpage]([name],[text],[image])VALUES('{name}','{text}','{image}')");
            return Content(res);
        }


        ///////////////////////////////////////// showpost
        [HttpGet]
        public ActionResult showpost()
        {
            List<postpage> obj = new List<postpage>();
            PDBC db = new PDBC("webmvc", true);
            db.Connect();
            using (DataTable dt = db.Select("SELECT [id],[name],[text],[image] FROM [dbo].[postpage] order by [id]  desc"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    postpage ad = new postpage();
                    ad.id = dt.Rows[i]["id"].ToString();
                    ad.name = dt.Rows[i]["name"].ToString();
                    ad.image = dt.Rows[i]["image"].ToString();
                    ad.namber = i + 1;
                    obj.Add(ad);
                }
                ViewBag.Title = obj;
                db.DC();
            }

            return View();
        }
        ////////////////////////////////////////// delete post
        [WebMethod]
        [ScriptMethod]
        internal string insertsToTUBUSER(string posted)
        {
            PDBC db = new PDBC("webmvc", true);
            //List<ExcParameters> paramss = new List<ExcParameters>();
            //ExcParameters parameters = new ExcParameters()
            //{
            //    _KEY = "@id",
            //    _VALUE = posted
            //};
            //paramss.Add(parameters);

            db.Connect();
            string res = db.Script($"DELETE FROM [dbo].[postpage] WHERE id ='{posted}'");
            return res;

        }

    }
}