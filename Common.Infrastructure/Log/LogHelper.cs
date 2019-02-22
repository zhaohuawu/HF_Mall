using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using log4net;
using log4net.Repository;
using System.Threading.Tasks;

namespace Common.Infrastructure.Log
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper : Interface.ILog
    {
        public static string APP_PATH = "";

        #region log4Net

        public static ILoggerRepository _repository;
        private readonly ILog _log;

        /// <summary>
        /// 创建一个记录实体
        /// </summary>
        public LogHelper()
        {
            //_repository = LogManager.CreateRepository(EnumLoggerReository.NETCoreRepository.ToString());
            _log = log4net.LogManager.GetLogger(_repository.Name, EnumLogger.commonLogger.ToString());
        }

        public LogHelper(EnumLogger logger)
        {
            //_repository = LogManager.CreateRepository(EnumLoggerReository.NETCoreRepository.ToString());
            _log = log4net.LogManager.GetLogger(_repository.Name, logger.ToString());
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message"></param>
        public async void Debug(string message)
        {
            await Task.Run(() =>
                {
                    _log.Debug(message);
                });
        }

        /// <summary>
        /// 记录消息日志
        /// </summary>
        /// <param name="message"></param>
        public async void Info(string message)
        {
            await Task.Run(() =>
            {
                _log.Info(message);
            });
        }

        /// <summary>
        /// 记录消息日志
        /// </summary>
        /// <param name="message"></param>
        public async void Info(object message)
        {
            await Task.Run(() =>
            {
                _log.Info(message);
            });
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message"></param>
        public async void Warning(string message)
        {
            await Task.Run(() =>
            {
                _log.Warn(message);
            });
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message"></param>
        public async void Error(string message)
        {
            await Task.Run(() =>
            {
                _log.Error(message);
            });
        }

        /// <summary>
        /// 记录指定的一个Exception的日志
        /// </summary>
        /// <param name="exception"></param>
        public async void Exception(Exception exception)
        {
            await Task.Run(() =>
            {
                _log.Error(exception.Message, exception);
            });
        }

        /// <summary>
        /// 记录指定的一个Exception的日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public async void Exception(string message, Exception exception)
        {
            await Task.Run(() =>
            {
                _log.Error(message, exception);
            });
        }
        #endregion


        /// <summary>
        /// 记录log文件夹
        /// </summary>
        /// <param name="info"></param>
        public static void Log(string info)
        {
            Log(info, "log.txt");
        }

        /// <summary>
        /// 记录不同的文件日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="filename"></param>
        public static void Log(string info, string filename)
        {
            string timeStr = DateTime.Today.ToString("yyyy-MM-dd");
            string logName = timeStr.Substring(0, 4) + "/" + timeStr.Substring(5, 2);   //日期文件名
            bool result = CreateDir(APP_PATH, logName, "Logs/");//建立“yyyy-MM”格式的文件夹
            filename = timeStr + "__" + filename;
            string path = APP_PATH + "Logs/" + logName + "/" + filename;
            try
            {
                try
                {
                    info = Regex.Unescape(info);//转码一下，有些时候json格式的不转看不到中文
                }
                catch
                {
                }
                StreamWriter sw = System.IO.File.AppendText(path);
                sw.WriteLine(DateTime.Now.ToString("【yyyy-MM-dd HH:mm:ss:fff】 ") + info);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                //Ignore
            }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="subdir"></param>
        public static bool CreateDir(string path, string subdir, string file)
        {
            if (file != "")
                path = path + file + subdir;
            else
                path = path + subdir;

            return FileHelper.CreatDir(path);
        }

        /// <summary>
        /// 获取txt文件夹中内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileText(string filePath)
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

    }
}
