using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace WeChat
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = UpLoadFile();
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 上传文件 方法
        /// </summary>
        /// <param name="fileNamePath"></param>
        /// <param name="toFilePath"></param>
        /// <returns>返回上传处理结果   格式说明： 0|file.jpg|msg   成功状态|文件名|消息    </returns>
        public string UpLoadFile()
        {
            try
            {
                HttpPostedFile uploadFile = null;
                try
                {
                    uploadFile = HttpContext.Current.Request.Files["Fdata"];
                }
                catch(HttpException ex)
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：服务器不能接受您的文件!";
                }
                string fileType = HttpContext.Current.Request["filetype"];
                string toFilePath = "/upload/";
                //文件为空
                if (uploadFile == null || string.IsNullOrEmpty(uploadFile.FileName))
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：未选择任何文件！";
                }
                //获取要保存的文件信息
                FileInfo file = new FileInfo(uploadFile.FileName);
                //获得文件扩展名
                string fileNameExt = file.Extension;

                //验证合法的文件
                if (CheckFileExt(fileNameExt, fileType))
                {
                    toFilePath += fileType.Replace('.', ' ').Trim() + "/";
                    //生成将要保存的随机文件名
                    string fileName = Guid.NewGuid().ToString() + fileNameExt;
                    //检查保存的路径 是否有/结尾
                    if (toFilePath.EndsWith("/") == false) toFilePath = toFilePath + "/";

                    //按日期归类保存
                    string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
                    if (true)
                    {
                        toFilePath += datePath;
                    }

                    //获得要保存的文件路径
                    string serverFileName = toFilePath + fileName;
                    string serverFileNameThumb = toFilePath + "Thumb_" + fileName;
                    //物理完整路径                    
                    string toFileFullPath = HttpContext.Current.Server.MapPath(toFilePath);

                    //检查是否有该路径  没有就创建
                    if (!Directory.Exists(toFileFullPath))
                    {
                        Directory.CreateDirectory(toFileFullPath);
                    }

                    //将要保存的完整文件名                
                    string toFile = toFileFullPath + fileName;

                    #region 使用文件流想上传方法
                    /////创建WebClient实例       
                    //System.Net.WebClient myWebClient = new System.Net.WebClient();
                    ////设定windows网络安全认证   方法1
                    //myWebClient.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    //////设定windows网络安全认证   方法2
                    ////NetworkCredential cred = new NetworkCredential("UserName", "UserPWD");
                    ////CredentialCache cache = new CredentialCache();
                    ////cache.Add(new Uri("UploadPath"), "Basic", cred);
                    ////myWebClient.Credentials = cache;

                    ////要上传的文件       
                    //FileStream fs = new FileStream(uploadFile.FileName, FileMode.Open, FileAccess.Read);
                    ////FileStream fs = OpenFile();       
                    //BinaryReader r = new BinaryReader(fs);
                    ////使用UploadFile方法可以用下面的格式       
                    ////myWebClient.UploadFile(toFile, "PUT",fileNamePath);       
                    //byte[] postArray = r.ReadBytes((int)fs.Length);
                    //Stream postStream = myWebClient.OpenWrite(toFile, "PUT");
                    //if (postStream.CanWrite)
                    //{
                    //    postStream.Write(postArray, 0, postArray.Length);
                    //}
                    //else
                    //{
                    //    return "0|" + serverFileName + "|" + "文件上传失败,错误原因：文件目前不可写";
                    //}
                    //postStream.Close();
                    #endregion

                        uploadFile.SaveAs(toFile);
                        if (IsPic(fileNameExt))
                        {
                            try
                            {
                                System.Util.MakeThumbnail(PathHelper.Map(serverFileName), PathHelper.Map(serverFileNameThumb), 80, 80, "HW");
                                return "1|" + serverFileName + "|" + "恭喜你，文件上传成功!|" + serverFileNameThumb;
                            }
                            catch
                            {
                                return "1|" + serverFileName + "|" + "恭喜你，文件上传成功，但生成缩略图错误!";
                            }
                        }
                    return "1|" + serverFileName + "|" + "恭喜你，文件上传成功!";
                }
                else
                {
                    return "0|errorfile|" + "文件上传失败,错误原因：您选择的文件格式错误";
                }
            }
            catch (DirectoryNotFoundException e)
            {
                return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
            }
            catch (Exception e)
            {
                //return "0|errorfile|" + "文件上传失败,错误原因：您的浏览器安全设置禁止读取上传文件";
                return "0|errorfile|" + "文件上传失败,错误原因：" + e.Message;
            }
        }

        private bool CheckFileExt(string ext,string type)
        {
            string extlist=Tool.GetConfiger("UploadExt");
            if (extlist.Contains(ext))
            {
                if (type == "pic")
                {
                    return IsPic(ext);
                }
                else if (type == "audio")
                {
                    return IsAudio(ext);
                }
            } 
            return false;
        }

        private bool IsPic(string ext)
        {
            if (".jpg,.gif,.png".Contains(ext))
            {
                return true;
            }
            return false;
        }
        private bool IsAudio(string ext)
        {
            if (".mp3,.avi,.rm".Contains(ext))
            {
                return true;
            }
            return false;
        }
    }
}