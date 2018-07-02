using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MDEditorUploadImage
{
    
    public class uploadimage : IHttpHandler
    {

        public class ResultInfo
        {
            public int success { get; set; }
            public string message { get; set; }
            public string url { get; set; }
        }


        public void ProcessRequest(HttpContext context)
        {
            try
            {

                //儲存路徑
                DirectoryInfo FileDir = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "source\\");

                //如果檔案夾沒有建立則自行建立
                if (FileDir.Exists == false)
                {
                    FileDir.Create();
                }

                HttpFileCollection uploadedFiles = context.Request.Files;

                if (uploadedFiles.Count > 0)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[0];

                    if (userPostedFile.ContentLength > 0)
                    {
                        userPostedFile.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "source" + Path.DirectorySeparatorChar + Path.GetFileName(userPostedFile.FileName));
                        context.Response.Write(new JavaScriptSerializer().Serialize(new ResultInfo { message = "上傳成功", success = 1, url = "http://"+context.Request.Url.Authority + "/source/" + Path.GetFileName(userPostedFile.FileName) }));
                    }
                }

               
            }
            catch (Exception ex)
            {
                context.Response.Write(new JavaScriptSerializer().Serialize(new ResultInfo { message = "上傳失敗:"+ex.Message, success = 0, url ="" }));
            }
        }

        //將收到的stream 寫入檔案
        private void SaveFile(Stream stream, FileStream fs)
        {
            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {
                //這邊可以放入錯誤的Log
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}