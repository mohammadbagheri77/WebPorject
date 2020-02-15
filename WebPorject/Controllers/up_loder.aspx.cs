using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPorject.Models;

namespace WebPorject.Controllers
{
    public partial class up_loder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Action = Request["action"];
            if (Action != null || !string.IsNullOrEmpty(Action))
            {
                if (Action == "New")
                {
                    StringBuilder IMGS = new StringBuilder("");
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {

                            string fname = Path.GetFileName(file.FileName);
                            string EX = Path.GetExtension(file.FileName);
                            string FileNAME = Guid.NewGuid().ToString() + "-" + fname;
                            string address = Server.MapPath("~/images/" + FileNAME);
                            string URLIMG = Statics.AppendServername("images/" + FileNAME);
                            if (Directory.Exists(Server.MapPath("~/images")))
                            {
                                file.SaveAs(address);
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/images"));
                                file.SaveAs(address);
                            }
                            StringBuilder str = new StringBuilder("");
                            string iiid = $"{DateTime.Now.Millisecond}{DateTime.Now.Minute}{DateTime.Now.Hour}{DateTime.Now.Second}";
                            str.Append("<div class=\"col-xl-3 col-lg-3 col-md-3\"><div class=\"kt-portlet\"><div class=\"kt-portlet__body\"><div class=\"kt-widget__files\"><div class=\"kt-widget__media\"><img class=\"kt-widget__img\" style=\"height:200px;width:200px;\" src=\"");
                            str.Append(URLIMG);
                            str.Append("\" alt=\"image\"></div><input style=\"width:50px;height:50px;background-color:transparent; border:none;\" type=\"text\" value=\"" + URLIMG + "\" id=\"");
                            //str.Append("imgid");
                            str.Append($"img{iiid}");
                            str.Append("\" readonly><button onclick=\"return copytoclipboard('");
                            str.Append($"img{iiid}");
                            str.Append("')\" class=\"w-100 btn btn-success\">کپی کردن آدرس تصویر</button></div></div></div></div>");
                            IMGS.Append(str.ToString());

                        }

                    }
                    Response.Write(IMGS.ToString());


                }

            }
        }
    }
}