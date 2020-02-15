using DataBaseConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPorject.Models;

namespace WebPorject.Controllers
{
    
    public class msController : Controller
    {
        public int ID { set; get; }
        // GET: ms
        ////////////////////////////////////////// show View master
        [HttpGet]
        public ActionResult Index()
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
                    ad.text = dt.Rows[i]["text"].ToString();
                    obj.Add(ad);
                }
                ViewBag.Tit = obj;
                db.DC();
            }
            return View();
        }
        
        ////////////////////////////////////////// show post master
        [HttpGet]
        public ActionResult post(int id)
        {
           
            List<postpage> obj = new List<postpage>();
            PDBC db = new PDBC("webmvc", true);
            db.Connect();
            using (DataTable dt = db.Select($"SELECT [name],[text],[image]FROM [dbo].[postpage] where id ='{id}'"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    postpage ad = new postpage();
                    ad.id = Convert.ToString(id);
                    ad.name = dt.Rows[i]["name"].ToString();
                    ad.image = dt.Rows[i]["image"].ToString();
                    ad.text = dt.Rows[i]["text"].ToString();
                    obj.Add(ad);
                }
                ID=Convert.ToInt32(obj[0].id);
                Session["idd"] = ID;
                ViewBag.post = obj;
                db.DC();
            }
            return View();
        }
        ////////////////////////////////////////// comment post
        [HttpPost]
        public ActionResult GetUser(postpage viewModel)
        {
            ID = Convert.ToInt32(Session["idd"].ToString());
            PDBC db = new PDBC("webmvc", true);
            //List<ExcParameters> paramss = new List<ExcParameters>();
            //ExcParameters parameters = new ExcParameters()
            //{
            //    _KEY = "@id",
            //    _VALUE = posted
            //};
            //paramss.Add(parameters);

            db.Connect();
            string res = db.Script($"INSERT INTO [dbo].[commentpost]([username],[email],[comment],[idpost])VALUES('{viewModel.name}','{viewModel.image}','{viewModel.text}','{ID}')");
            return Content(res);
        }
       
    }
}