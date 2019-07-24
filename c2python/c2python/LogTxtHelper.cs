using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c2python
{
    public class LogTxtHelper
    {
        public static string ReadToString(string path)
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line.ToString());
                }
                sr.Close();
                sr.Dispose();
                return sb.ToString();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
            }
        }

        public static bool CreateWrite(string path, string context)
        {
            bool b = false;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes(context);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return b;
            }
            finally
            {
            }
        }

        public static bool WriteAppend(string path, string context)
        {
            bool b = false;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(context);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return b;
            }
            finally
            {
            }
        }

        public static bool CreateOrWriteAppendLine(string path, string context)
        {
            bool b = false;
            try
            {
                if (!File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    long fl = fs.Length;
                    fs.Seek(fl, SeekOrigin.End);
                    sw.WriteLine(context);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    b = true;
                }
                else
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    long fl = fs.Length;
                    fs.Seek(fl, SeekOrigin.Begin);
                    sw.WriteLine(context);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    b = true;
                }
                return b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return b;
            }
            finally
            {

            }
        }


        #region 日志
        //public static void WriteLog(string content, int fileSize = 1, int fileCount = 20, string filePath = "")
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(filePath))
        //        {
        //            logPath = filePath;
        //        }
        //        LogLock.EnterWriteLock();
        //        logPath = logPath.Replace("file:\\", "");//这里为了兼容webapi的情况
        //        string dataString = DateTime.Now.ToString("yyyy-MM-dd");
        //        string path = logPath + "\\MyLog";
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //            path += "\\";
        //            path += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        //            FileStream fs = new FileStream(path, FileMode.Create);
        //            fs.Close();
        //        }
        //        else
        //        {
        //            int x = System.IO.Directory.GetFiles(path).Count();
        //            path += "\\";
        //            Dictionary<string, DateTime> fileCreateDate = new Dictionary<string, DateTime>();
        //            string[] filePathArr = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
        //            if (filePathArr.Length == 0)
        //            {
        //                string sourceFilePath = path;
        //                path += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                fs.Close();
        //                filePathArr = Directory.GetFiles(sourceFilePath, "*.txt", SearchOption.TopDirectoryOnly);
        //            }
        //            for (int i = 0; i < filePathArr.Length; i++)
        //            {
        //                FileInfo fi = new FileInfo(filePathArr[i]);
        //                fileCreateDate[filePathArr[i]] = fi.CreationTime;
        //            }
        //            fileCreateDate = fileCreateDate.OrderBy(f => f.Value).ToDictionary(f => f.Key, f => f.Value);
        //            FileInfo fileInfo = new FileInfo(fileCreateDate.Last().Key);
        //            if (fileInfo.Length < 1024 * 1024 * fileSize)
        //            {
        //                path = fileCreateDate.Last().Key;
        //            }
        //            else
        //            {
        //                path += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                fs.Close();
        //            }
        //            if (x > fileCount)
        //            {
        //                File.Delete(fileCreateDate.First().Key);
        //            }

        //        }
        //        FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
        //        StreamWriter sw = new StreamWriter(fs2);
        //        long fl = fs2.Length;
        //        fs2.Seek(fl, SeekOrigin.Begin);
        //        sw.WriteLine(DateTime.Now.ToString("hh:mm:ss") + "---> " + content);
        //        sw.Flush();
        //        sw.Close();
        //        fs2.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //    }

        //}
        #endregion


    }
}
