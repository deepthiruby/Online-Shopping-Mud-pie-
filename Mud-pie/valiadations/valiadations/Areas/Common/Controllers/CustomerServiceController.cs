using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Hosting;
using System.Net.Mail;
using System.Text;

namespace valiadations.Areas.Common.Controllers
{
    public class CustomerServiceController : Controller
    {
        private ContactBs objBs;
        public CustomerServiceController()
        {
            objBs = new ContactBs();
        }
        // GET: Common/CustomerSrvice
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {


            //validating google recaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = "6LezYMgZAAAAAOb6YiTD80a8Y5DQy-bsyjv7-_AG";
            var client = new WebClient();


           var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
             var objJo = JObject.Parse(result);
             var CaptchaStatus = (bool)objJo.SelectToken("success");
            // if(objJo.HasValues == true)
            //{
            //    ViewBag.Message = "Google recaptcha Validation success";
            //}
            if(CaptchaStatus)
            {
                ViewBag.Message = "";
            }
            else
            {
                ViewBag.Message = "validation is invalid";
            }
          //  ViewBag.Message = CaptchaStatus ? "" : "validation is invalid";
        
                if (CaptchaStatus && ModelState.IsValid)
                {
               
                    objBs.Insert(contact);
                    TempData["Msg"] = "Successfully posted";
                     BulidEmailTemplate(contact.ContactID);



                return RedirectToAction("Index");
                }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public string ReadHtml(string path)
        {
            string htmlbody = "";
            using (StreamReader reader = new StreamReader(path))
            {
                htmlbody += reader.ReadToEnd();
            }
            return htmlbody;
        }
        public void BulidEmailTemplate(int RegID)
        {
            ViewBag.ConfirmationLink = "nothing";
            // var regInfo = objBs.Con.Where(x => x.Cont == RegID).FirstOrDefault();
            var regInfo = objBs.GetByID(RegID);

           // string body = "";

          string  body = ReadHtml(HostingEnvironment.ApplicationPhysicalPath + @"/EmailTemplate/NotificationMail.html");


            //string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            //var regInfo = db.Contacts.Where(x => x.ContactId == RegID).FirstOrDefault();
            //ViewBag.Name = regInfo.Name;
            //  var url = "http://localhost:59552/" + "CustomerService/Confirm?ConfirmRegId=" + RegID;
            //body = body.Replace("@ViewBag.Email", url);
            var name = regInfo.Name;
            body = body.Replace("@ViewBag.Name", name);

            body = body.ToString();
            BulidEmailTemplate("Your Email was Sent Sucessfully", body, regInfo.Email);
        }
        public static void BulidEmailTemplate(string subjectText, string bodyText, string SendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "deepthisreekalidindi@gmail.com";
            to = SendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;

            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.Bcc.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("dummyexampletrail@gmail.com", "Dummy@098");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Cindex()
        {
            return View();
        }

    }
}