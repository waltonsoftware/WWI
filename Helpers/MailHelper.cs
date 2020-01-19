using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

namespace WWI.Helpers
{
	public class MailHelper
	{
		public static bool SendMail(string emailAddress, string body, string subject)
		{
			bool _ret = false;
			MailMessage msg = new MailMessage();

			try
			{
				msg.From = new MailAddress("waltonsoftware@gmail.com");
				msg.To.Add(emailAddress);
				msg.Subject = subject;
				msg.IsBodyHtml = true;
				msg.Body = GetEmailConfirmationHtml(body);

				using (SmtpClient smtpClient = new SmtpClient())
				{
					smtpClient.Host = "smtp.gmail.com";
					smtpClient.Port = 587;
					smtpClient.EnableSsl = true;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = new System.Net.NetworkCredential("waltonsoftware@gmail.com", "keunkbbvbxhrfzak");
					smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

					smtpClient.Send(msg);

					_ret = true;
				}
			}
			catch(System.Exception ex)
			{
				throw ex;
			}

			return _ret;
		}

		public static string GetEmailConfirmationHtml(string link)
		{
			StringBuilder sb = new StringBuilder(@"<!DOCTYPE html><html><head><meta charset='utf - 8' /><title></title></head><body>");
			sb.Append(@"<div style='font-family: Segoe UI, Tahoma, Geneva, Verdana, sans-serif; font-size: 22px;'>To confirm your email address, click on the link.</div><div>");
			sb.Append(link);
			sb.Append(@"</div></body></html>");
			return sb.ToString();
		}
	}
}