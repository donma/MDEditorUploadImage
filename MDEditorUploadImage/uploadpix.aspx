<%@ Page Language="C#" AutoEventWireup="true" %>

<script runat="server" language="C#">
    public class ResultInfo
    {
        public int success { get; set; }
        public string message { get; set; }
        public string url { get; set; }
    }
</script>

<script runat="server" language="C#">
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //儲存路徑
            System.IO.DirectoryInfo FileDir = new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "source\\");

            //如果檔案夾沒有建立則自行建立
            if (FileDir.Exists == false)
            {
                FileDir.Create();
            }

            HttpFileCollection uploadedFiles = Request.Files;

            if (uploadedFiles.Count > 0)
            {
                HttpPostedFile userPostedFile = uploadedFiles[0];

                if (userPostedFile.ContentLength > 0)
                {
                    userPostedFile.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "source" + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileName(userPostedFile.FileName));
                      Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new ResultInfo { message = "上傳成功", success = 1, url ="http://"+ Request.Url.Authority + "/source/" + System.IO.Path.GetFileName(userPostedFile.FileName) }));
                }
            }


        }
        catch (Exception ex)
        {
              Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(new ResultInfo { message = "上傳失敗:" + ex.Message, success = 0, url = "" }));
        }
    }
</script>


