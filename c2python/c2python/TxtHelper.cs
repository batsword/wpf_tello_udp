
using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.IO;
using System.Windows;

namespace c2python

{

    class TxtHelper

    {
        private const string _path = "自动化.txt";

        public TxtHelper()

        {

        }



        public void Write( string content, string path = _path)

        {

            try

            {

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

                StreamWriter sr = new StreamWriter(fs);

                sr.WriteLine(content);

                sr.Close();

                fs.Close();

            }

            catch (Exception)

            {

                throw;

            }

        }

        private StreamReader sreader = null;
        public void Init(string path = _path)
        {
            if (!File.Exists(path))
                return ;

            try
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

                sreader = new StreamReader(fs, Encoding.Default);;

                return ;

            }

            catch (Exception ex)

            {
                MessageBox.Show("文件初始化异常 :"+ ex.ToString());
                throw;

            }
        }

        public  string ReadLine()
        {
            if (sreader == null)

                return "";

            try

            {
                if (sreader.EndOfStream == true)
                    return "";

                String line = sreader.ReadLine();

                return line;

            }

            catch (Exception ex)
            {
                MessageBox.Show("读取文件异常 :" + ex.ToString());
                throw;

            }
        }

        //public static string ReadFirstLine(string path = _path)

        //{



        //    if (!File.Exists(path))

        //        return "";



        //    try

        //    {

        //        FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

        //        StreamReader sr = new StreamReader(fs, Encoding.Default);

        //        String line = sr.ReadLine();



        //        return line;

        //    }

        //    catch (Exception)

        //    {

        //        throw;

        //    }

        //}

    }

}
