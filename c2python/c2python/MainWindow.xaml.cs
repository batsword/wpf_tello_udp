using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Forms;

namespace c2python
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btn_up.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_up_MouseDown), true);
            btn_up.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_up_MouseUp), true);

            btn_dwon.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_dwon_MouseDown), true);
            btn_dwon.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_dwon_MouseUp), true);

            btn_left.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_left_MouseDown), true);
            btn_left.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_left_MouseUp), true);

            btn_right.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_right_MouseDown), true);
            btn_right.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_right_MouseUp), true);

            btn_pre.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_pre_MouseDown), true);
            btn_pre.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_pre_MouseUp), true);

            btn_pro.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.btn_pro_MouseDown), true);
            btn_pro.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.btn_pro_MouseUp), true);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象

            ////pyEngine.SetSearchPaths(new[] { @"C:\Users\14923\Documents\Visual Studio 2015\Projects\c2python\packages\IronPython.2.7.9\lib" });

            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.initTello();//调用脚本文件中对应的函数
            ////string dd = py.main(textBox1.Lines);//调用脚本文件中对应的函数

            

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ////TestPython();
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件

            //py.takeoffex();//调用脚本文件中对应的函数



            //ScriptRuntime pyRunTime = Python.CreateRuntime();
            //dynamic obj = pyRunTime.UseFile(@"tello.py");
            //var service = obj.Tello();
            //service.takeOff();//调用脚本文件中对应的函数

            //ScriptEngine engine = Python.CreateEngine();
            //ScriptScope scope = engine.CreateScope();
            //ScriptSource script = engine.CreateScriptSourceFromFile(@"run.py");
            //script.Execute(scope);
            int iCmd = 100;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private byte[] Change2Cmd(int iCmd)
        {
            //byte[] cmd = System.BitConverter.GetBytes(iCmd);
            //byte[] buf = cmd.Skip(0).Take(2).Reverse().ToArray();

            byte[] cmd = SimpleUdpHelper.String2ByteArray(iCmd.ToString());
            return cmd;
        }

        ScriptEngine engine;
        ScriptScope scope;
        dynamic game;
        private void TestPython()
        {
            this.engine = Python.CreateEngine();
            this.engine.SetSearchPaths(new[] { "tello" });
            this.scope = this.engine.CreateScope();

            this.scope.ImportModule("tello");
            this.engine.Execute("game = tello.Tello()", this.scope);
            this.game = this.scope.GetVariable("game");

            game.takeOff();
        }

        private void UsePython(string cmd)
        {
            ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            string dd = py.main(cmd);//调用脚本文件中对应的函数
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.land();//调用脚本文件中对应的函数
            int iCmd = 200;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void button1_Copy1_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flaW();//调用脚本文件中对应的函数
            //上();
        }

        private void 上()
        {
            Dispatcher.Invoke(new Action(()=>{
                int iCmd = 900;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
            
        }

        private void 下()
        {
            Dispatcher.Invoke(new Action(() => {
                int iCmd = 2000;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
        }

        private void 前()
        {
            Dispatcher.Invoke(new Action(() => {
                int iCmd = 600;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
        }

        private void 后()
        {
            Dispatcher.Invoke(new Action(() => {
                int iCmd = 700;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
        }

        private void 左()
        {
            Dispatcher.Invoke(new Action(() => {
                int iCmd = 400;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
        }

        private void 右()
        {
            Dispatcher.Invoke(new Action(() => {
                int iCmd = 500;
                udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
            }));
        }

        private void button1_Copy6_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flyS();//调用脚本文件中对应的函数
            int iCmd = 2000;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void button1_Copy2_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flyUp();//调用脚本文件中对应的函数
            int iCmd = 600;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void button1_Copy4_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flyDown();//调用脚本文件中对应的函数
            int iCmd = 700;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void button1_Copy3_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flyLeft();//调用脚本文件中对应的函数
            int iCmd = 400;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void button1_Copy5_Click(object sender, RoutedEventArgs e)
        {
            //ScriptEngine pyEngine = Python.CreateEngine();//创建Python解释器对象
            //dynamic py = pyEngine.ExecuteFile(@"run.py");//读取脚本文件
            //py.flyRight();//调用脚本文件中对应的函数
            int iCmd = 500;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private SimpleUdpHelper udp = new SimpleUdpHelper();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            udp.OnSocketReceive += Udp_OnSocketReceive;
            udp.Start(9600);

            var primaryScreen = Screen.AllScreens.FirstOrDefault(s => s.Primary);
            if (primaryScreen != null)
            {
                //var workingArea = primaryScreen.WorkingArea;
                Left = primaryScreen.Bounds.Left;
                Top = primaryScreen.Bounds.Top;
                WindowState = WindowState.Maximized;
            }
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            udp.SendTo(SimpleUdpHelper.String2ByteArray("sign"), ip.Text, int.Parse(port.Text));
        }

        private void Udp_OnSocketReceive(byte[] obj)
        {
            Dispatcher.Invoke(new Action(()=> {
                if((obj!=null)||(obj.Count()>0))
                    tb_rev.Text = obj.ToString();
            })
                
            );
        }

        private void btn_360_Click(object sender, RoutedEventArgs e)
        {
            int iCmd = 3000;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }

        private void btn_bounce_Click(object sender, RoutedEventArgs e)
        {
            int iCmd = 4000;
            udp.SendTo(Change2Cmd(iCmd), ip.Text, int.Parse(port.Text));
        }


        bool isPress = false;
        private void btn_up_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("上");
        }

        private void Send20ms(string cmd)
        {
            isPress = true;
            new Thread(() => {
                while (isPress)
                {
                    Thread.Sleep(20);
                    switch (cmd)
                    {
                        case "上": { 上(); } break;
                        case "下": { 下(); } break;
                        case "左": { 左(); } break;
                        case "右": { 右(); } break;
                        case "前": { 前(); } break;
                        case "后": { 后(); } break;
                        default: break;
                    }
                }
            }).Start();
            
        }

        private void btn_up_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void btn_dwon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("下");
        }

        private void btn_dwon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void btn_left_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("左");
        }

        private void btn_left_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void btn_right_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("右");
        }

        private void btn_right_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void btn_pre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("前");
        }

        private void btn_pre_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void btn_pro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Send20ms("后");
        }

        private void btn_pro_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPress = false;
        }

        private void WaitTimeFly(int delayTime,int Time,string Cmd)
        {
            for (int i = 0; i < Time * 1000 / delayTime; i++)
            {
                Thread.Sleep(delayTime);
                switch (Cmd)
                {
                    case "上": { 上(); } break;
                    case "下": { 下(); } break;
                    case "左": { 左(); } break;
                    case "右": { 右(); } break;
                    case "前": { 前(); } break;
                    case "后": { 后(); } break;
                    default: break;
                }
            }
        }

        private void BtnRTxt_Click(object sender, RoutedEventArgs e)
        {
            TxtHelper th = new TxtHelper();
            th.Init();

            int msTime = 20;//ms

            string tx = "有";
            while (tx != "")
            {
                tx = th.ReadLine();
                if (tx == "")
                    break;


                string[] buf = tx.Split('=');

                string cmd = buf[0];
                string second = buf[1];

                switch (cmd.ToUpper())
                {
                    case "TIME": { msTime = int.Parse(second); }; break;
                    case "L"://左
                        {
                            
                            int time = int.Parse(second);

                            WaitTimeFly(msTime, time,"左");

                        }; break;
                    case "R"://右
                        { int time = int.Parse(second);
                            WaitTimeFly(msTime, time, "右");
                        }; break;
                    case "U"://上
                        { int time = int.Parse(second);
                            WaitTimeFly(msTime, time, "上");
                        }; break;
                    case "D"://下
                        { int time = int.Parse(second);
                            WaitTimeFly(msTime, time, "下");
                        }; break;
                    case "E"://前
                        { int time = int.Parse(second);
                            WaitTimeFly(msTime, time, "前");
                        }; break;
                    case "O"://后
                        { int time = int.Parse(second);
                            WaitTimeFly(msTime, time, "后");
                        }; break;
                    default:break;
                }

                Console.WriteLine(tx);
            }

        }

        private void writeFile_Click(object sender, RoutedEventArgs e)
        {
            TxtHelper th = new TxtHelper();
            th.Write("11","写文件.txt");
        }

        int tt = 0;
        private void writeFile_other_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.CurrentDirectory + @"\" + "ttt" + @"\" + "追加测试.log";
            tt++;
            LogTxtHelper.CreateOrWriteAppendLine(path, tt.ToString());
        }
        private System.Timers.Timer _advTimer = new System.Timers.Timer(1000);

        private void processing_Click(object sender, RoutedEventArgs e)
        {
            //_advTimer.Elapsed += _advTimer_Elapsed;
            //_advTimer.Start();
            start();
        }
        int ii = 0;
        private void _advTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ii++;
            if (ii == 1)
            {
                string path = Environment.CurrentDirectory + @"\NDTerminal\NDTerminal.exe";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = $@"{Environment.CurrentDirectory}\NDTerminal";//\NDTerminal\
                psi.FileName = path;
                Process.Start(psi);
            }
        }

        public static void start()
        {
            string path = Environment.CurrentDirectory + @"\NDTerminal\NDTerminal.exe";

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WorkingDirectory = $@"{Environment.CurrentDirectory}\NDTerminal";//\NDTerminal\
            psi.FileName = path;

            Process.Start(psi);
        }
    }
}
