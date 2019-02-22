using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure
{
    public class FileHelper
    {
        #region common function
        public static bool CreatDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetLength(string file)
        {
            return (int)new FileInfo(file).Length;
        }

        /// <summary>
        /// 获取指定缩略图
        /// </summary>
        /// <param name="strDBPath">图片原始路径</param>
        /// <param name="index">图片标识，此标识参考File.Config中的缩略图标识 thumbnailmark</param>
        /// <returns></returns>
        public static string GetThumbnail(string strDBPath, object strMark)
        {
            string returnString = "";
            if (!string.IsNullOrEmpty(strDBPath))
            {
                int Mark = strDBPath.LastIndexOf(".");
                string strFront = strDBPath.Substring(0, Mark);
                string strBehind = strDBPath.Substring(Mark, strDBPath.Length - Mark);
                returnString = string.Format("{0}{1}{2}", strFront, strMark, strBehind);
            }
            return returnString;
        }
        
        /// <summary>
        /// 获取所有图片的缩略图的标记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<string> GetImagethumbnailmark(FileHelperParam param)
        {
            List<string> list = new List<string>();
            if (param.fileType == FileType.image)
            {
                string strTags = "";
                switch (param.appType)
                {
                    case AppType.Users:
                        strTags = string.Format("{0}-{1}-{2}", param.appType.ToString().ToLower(), param.fileType.ToString().ToLower(), param.imageType.ToString().ToLower());
                        break;
                    case AppType.Holiday:
                        strTags = string.Format("{0}-{1}", param.appType.ToString().ToLower(), param.fileType.ToString().ToLower());
                        break;
                    case AppType.Hotel:
                        strTags = string.Format("{0}-{1}", param.appType.ToString().ToLower(), param.fileType.ToString().ToLower());
                        break;
                }
               
                XmlElement xmlElement = GetXmlElement(strTags);
                XmlNodeList xmlNodeList = xmlElement.GetElementsByTagName("wh");
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    string strThumbnailMark = xmlNode.Attributes["thumbnailmark"].Value;
                    list.Add(strThumbnailMark);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取文件上传允许的后缀名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<string> GetExtensions(FileHelperParam param)
        {
            string strExtensions = GetNodeValue(GetXmlElement(string.Format("extensions-{0}", param.fileType.ToString().ToLower())));
            List<string> allowExtensions = strExtensions.Split(",".ToCharArray()).ToList<string>();
            return allowExtensions;
        }
        
        /// <summary>
        /// 获取随机文件名
        /// </summary>
        /// <returns></returns>
        static string GetNumberRandom()
        {
            int intNum;
            long lngNum;
            string timeTickets = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            lngNum = long.Parse(timeTickets);
            System.Random ran = new Random();
            intNum = ran.Next(1, 99999);
            ran = null;
            lngNum += intNum;
            return lngNum.ToString();
        }
        #endregion

        #region 获取配置文件中的值
        /// <summary>
        /// 获取多级唯一父节点名称下的唯一节点
        /// </summary>
        /// <param name="strTagNames">e.g. A-B-C，代表A节点下的B节点下的C节点</param>
        /// <returns></returns>
        static XmlElement GetXmlElement(string strTagNames)
        {
            List<string> tagNames = strTagNames.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            //XmlDocument xmlDocument = XmlConfigurator.xmlDocument;
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement xmlElement = xmlDocument.GetElementsByTagName(tagNames[0].ToLower())[0] as XmlElement;
            tagNames.RemoveAt(0);
            foreach (var obj in tagNames)
            {
                xmlElement = xmlElement.GetElementsByTagName(obj.ToLower())[0] as XmlElement;
            }
            return xmlElement;
        }

        /// <summary>
        /// 根据XmlElement获取节点value
        /// </summary>
        /// <param name="strTagName"></param>
        /// <returns></returns>
        static string GetNodeValue(XmlElement xmlElement)
        {
            return xmlElement.InnerText;
        }

        /// <summary>
        /// 根据XmlElement获取节点属性
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="strAttributeName"></param>
        /// <returns></returns>
        static string GetNodeAttribute(XmlElement xmlElement, string strAttributeName)
        {
            return xmlElement.GetAttribute(strAttributeName);
        }
        #endregion

        #region 读文件

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="filePath">带文件名的路径</param>
        public static string ReadFile(string filePath, Encoding encoding)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath, encoding);
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            catch (IOException iex)
            {
                throw new Exception(iex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ReadFile(string filePath)
        {
            Encoding enc = Encoding.Default;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] buffer = br.ReadBytes(2);
            if (buffer[0] == 0xEF && buffer[1] == 0xBB)
            {
                enc = Encoding.UTF8;
            }
            else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            {
                enc = Encoding.BigEndianUnicode;
            }
            else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            {
                enc = Encoding.Unicode;
            }

            fs.Close();
            br.Close();

            return System.IO.File.ReadAllText(filePath, Encoding.Default);
        }

        #endregion


        #region 进制转换
        /// <summary>
        /// 将Byte数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] bytes)
        {
            StringBuilder sbHex = new StringBuilder("0x");
            if (null != bytes && bytes.Length > 0)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sbHex.Append(bytes[i].ToString("X2"));
                }
            }
            return sbHex.ToString();
        }
        /// <summary>
        /// 将Stream 转化为字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string StreamToHexString(Stream stream)
        {
            StringBuilder sbHex = new StringBuilder("0x");
            if (null != stream && stream.Length > 0)
            {
                stream.Position = 0;
                byte[] bytes = new byte[4096];
                int count = 0;
                while ((count = stream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sbHex.Append(bytes[i].ToString("X2"));
                    }
                }
            }
            return sbHex.ToString();
        }
        /// <summary>
        /// 将Byte对应的字符串转为字节数组
        /// </summary>
        /// <param name="bytesString"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string bytesString)
        {
            if (!string.IsNullOrEmpty(bytesString) && bytesString.StartsWith("0x"))
            {
                string hexString = bytesString.Substring(2);
                byte[] rtnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < rtnBytes.Length; i++)
                {
                    rtnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }
                return rtnBytes;
            }
            return null;
        }
        /// <summary>
        /// StreamString 转化为Stream
        /// </summary>
        /// <param name="streamString"></param>
        /// <returns></returns>
        public static Stream HexStringToStream(string streamString)
        {
            if (!string.IsNullOrEmpty(streamString) && streamString.StartsWith("0x"))
            {
                string hexString = streamString.Substring(2);
                byte[] bytes = new byte[hexString.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }
                return new MemoryStream(bytes);
            }
            return null;
        }
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        /// 将 Stream 转成 byte[]

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        #endregion


        //上传缓冲
        private static readonly int _UPLOAD_BUFFER_SIZE = 4096;
        /// <summary>
        ///  保存上传图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static bool UploadFileWithStream(Stream stream, string savePath)
        {
            bool rtnValue = false;
            try
            {
                if (stream.Length > 0)
                {
                    using (stream)
                    {
                        //if (System.IO.File.Exists(savePath)) { System.IO.File.Delete(savePath); }
                        new FileInfo(savePath).Directory.Create();
                        using (FileStream file = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            file.SetLength(0);//先把数据流设置为0，防止原来就有数据
                            stream.Position = 0;
                            int count = -1;
                            byte[] bytes = new byte[_UPLOAD_BUFFER_SIZE];
                            while ((count = stream.Read(bytes, 0, bytes.Length)) > 0)
                            {
                                file.Write(bytes, 0, count);
                            }
                            file.Close();
                        }
                        stream.Close();
                    }
                    rtnValue = true;
                }
            }
            catch (Exception)
            {
                rtnValue = false;
            }
            return rtnValue;
        }

        /// <summary>
        /// 移到文件
        /// </summary>
        /// <param name="Path">绝对路径，原文件的物理路径</param>
        /// <param name="targetPath">绝对路径，目录文件的物理路径</param>
        public static void Copy(string Path, string targetPath)
        {
            //Path = Server.MapPath(Path);//原文件的物理路径
            //targetPath = Server.MapPath(targetPath);//移动到的新位置的物理路径(如果还是当前文件夹,则会重命名文件)
            //判断到的新地址是否存在重命名文件
            if (System.IO.File.Exists(targetPath))
            {
                //判断是新位置是否存在同名(判断重命名是狗和其他文件冲突)
                //throw new Exception("已经存在同名文件");
                //删除同名的旧文件
                System.IO.File.Delete(targetPath);

            }
            System.IO.File.Copy(Path, targetPath);//2个文件在不同目录则是移动,如果在相同目录下则是重命名
        }

    }
}
