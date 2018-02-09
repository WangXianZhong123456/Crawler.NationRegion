using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Yaxon.NationRegion.Common;
using Yaxon.NationRegion.BLL;

namespace Yaxon.NationRegion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetControl(true, false, false, false, false, true);
        }

        private Dictionary<string, Thread> DictThread = new Dictionary<string, Thread>();//线程
        public static int[] readTownHtmlThreadCount = new int[5] { 0, 0, 0, 0, 0 };//镇5个线程跑
        public static int[] readVillageHtmlThreadCount = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//村10个线程跑

        private int LEVEL = 0;

        #region 私有方法

        /// <summary>
        /// -1线程异常;0不上传数据;1上传数据
        /// </summary>
        private string _SmarkFlag = "-1";

        /// <summary>
        /// 0不上传数据到云端；1上传数据至云端
        /// </summary>
        private string SmarkFlag
        {
            get
            {
                if (this._SmarkFlag == "-1")//如果是异常数据将重新取
                    _SmarkFlag = "1";
                return _SmarkFlag;
            }
        }

        /// <summary>
        /// 线程监控    
        /// </summary>
        private Thread ThreadWatch;


        int IsStart = 0;
        /// <summary>
        /// 初始化线程
        /// </summary>
        private void InitThread(int level)
        {

            if (level == 1)
                DictThread.Add("ReadProvinceHtml", new Thread(CenterThread));
            if (level == 2)
                DictThread.Add("ReadCityHtml", new Thread(CenterThread));
            if (level == 3)
                DictThread.Add("ReadCountHtml", new Thread(CenterThread));
            #region 镇(5个线程跑)
            if (level == 4)
            {
                DictThread.Add("ReadTownHtml1", new Thread(CenterThread));
                DictThread.Add("ReadTownHtml2", new Thread(CenterThread));
                DictThread.Add("ReadTownHtml3", new Thread(CenterThread));
                DictThread.Add("ReadTownHtml4", new Thread(CenterThread));
                DictThread.Add("ReadTownHtml5", new Thread(CenterThread));
            }
            #endregion
            #region 村(10个线程跑)
            if (level == 5)
            {
                DictThread.Add("ReadVillageHtml1", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml2", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml3", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml4", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml5", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml6", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml7", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml8", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml9", new Thread(CenterThread));
                DictThread.Add("ReadVillageHtml10", new Thread(CenterThread));
            }
            #endregion
            #region 异常处理
            if (level == 6)
            {
                DictThread.Add("ReadExceptionHtml", new Thread(CenterThread));
            }
            #endregion



        }

        /// <summary>
        /// 线程开始
        /// </summary>
        private void InitStart()
        {
            try
            {
                if (SmarkFlag == "1")
                {
                    if (this.LEVEL == 1)
                    {
                        if (DictThread["ReadProvinceHtml"].IsAlive == false)
                        {
                            DictThread["ReadProvinceHtml"] = null;
                            DictThread["ReadProvinceHtml"] = new Thread(CenterThread);
                            DictThread["ReadProvinceHtml"].Start(new object[] { "ReadProvinceHtml" });
                            Thread.Sleep(100);
                        }
                    }
                    if (this.LEVEL == 2)
                    {
                        if (DictThread["ReadCityHtml"].IsAlive == false)
                        {
                            DictThread["ReadCityHtml"] = null;
                            DictThread["ReadCityHtml"] = new Thread(CenterThread);
                            DictThread["ReadCityHtml"].Start(new object[] { "ReadCityHtml" });
                            Thread.Sleep(100);
                        }
                    }
                    if (this.LEVEL == 3)
                    {
                        if (DictThread["ReadCountHtml"].IsAlive == false)
                        {
                            DictThread["ReadCountHtml"] = null;
                            DictThread["ReadCountHtml"] = new Thread(CenterThread);
                            DictThread["ReadCountHtml"].Start(new object[] { "ReadCountHtml" });
                            Thread.Sleep(100);
                        }
                    }
                    #region 镇
                    if (this.LEVEL == 4)
                    {
                        if (DictThread["ReadTownHtml1"].IsAlive == false)
                        {
                            DictThread["ReadTownHtml1"] = null;
                            DictThread["ReadTownHtml1"] = new Thread(CenterThread);
                            DictThread["ReadTownHtml1"].Start(new object[] { "ReadTownHtml1" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadTownHtml2"].IsAlive == false)
                        {
                            DictThread["ReadTownHtml2"] = null;
                            DictThread["ReadTownHtml2"] = new Thread(CenterThread);
                            DictThread["ReadTownHtml2"].Start(new object[] { "ReadTownHtml2" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadTownHtml3"].IsAlive == false)
                        {
                            DictThread["ReadTownHtml3"] = null;
                            DictThread["ReadTownHtml3"] = new Thread(CenterThread);
                            DictThread["ReadTownHtml3"].Start(new object[] { "ReadTownHtml3" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadTownHtml4"].IsAlive == false)
                        {
                            DictThread["ReadTownHtml4"] = null;
                            DictThread["ReadTownHtml4"] = new Thread(CenterThread);
                            DictThread["ReadTownHtml4"].Start(new object[] { "ReadTownHtml4" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadTownHtml5"].IsAlive == false)
                        {
                            DictThread["ReadTownHtml5"] = null;
                            DictThread["ReadTownHtml5"] = new Thread(CenterThread);
                            DictThread["ReadTownHtml5"].Start(new object[] { "ReadTownHtml5" });
                            Thread.Sleep(100);
                        }
                    }
                    #endregion
                    #region 村
                    if (this.LEVEL == 5)
                    {
                        if (DictThread["ReadVillageHtml1"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml1"] = null;
                            DictThread["ReadVillageHtml1"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml1"].Start(new object[] { "ReadVillageHtml1" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml2"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml2"] = null;
                            DictThread["ReadVillageHtml2"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml2"].Start(new object[] { "ReadVillageHtml2" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml3"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml3"] = null;
                            DictThread["ReadVillageHtml3"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml3"].Start(new object[] { "ReadVillageHtml3" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml4"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml4"] = null;
                            DictThread["ReadVillageHtml4"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml4"].Start(new object[] { "ReadVillageHtml4" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml5"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml5"] = null;
                            DictThread["ReadVillageHtml5"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml5"].Start(new object[] { "ReadVillageHtml5" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml6"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml6"] = null;
                            DictThread["ReadVillageHtml6"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml6"].Start(new object[] { "ReadVillageHtml6" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml7"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml7"] = null;
                            DictThread["ReadVillageHtml7"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml7"].Start(new object[] { "ReadVillageHtml7" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml8"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml8"] = null;
                            DictThread["ReadVillageHtml8"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml8"].Start(new object[] { "ReadVillageHtml8" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml9"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml9"] = null;
                            DictThread["ReadVillageHtml9"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml9"].Start(new object[] { "ReadVillageHtml9" });
                            Thread.Sleep(100);
                        }
                        if (DictThread["ReadVillageHtml10"].IsAlive == false)
                        {
                            DictThread["ReadVillageHtml10"] = null;
                            DictThread["ReadVillageHtml10"] = new Thread(CenterThread);
                            DictThread["ReadVillageHtml10"].Start(new object[] { "ReadVillageHtml10" });
                            Thread.Sleep(100);
                        }
                    }
                    #endregion
                    #region
                    if (this.LEVEL == 6)
                    {
                        if (DictThread["ReadExceptionHtml"].IsAlive == false)
                        {
                            DictThread["ReadExceptionHtml"] = null;
                            DictThread["ReadExceptionHtml"] = new Thread(CenterThread);
                            DictThread["ReadExceptionHtml"].Start(new object[] { "ReadExceptionHtml" });
                            Thread.Sleep(100);
                        }
                    }
                    #endregion


                }
                else if (SmarkFlag == "0")
                {
                    LogUtil.WriteInfo("系统设置成不上传进出数据");
                    InsertListBox(string.Format("{0} 系统设置成不上传进出数据", DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException.GetBaseException();
                }
                LogUtil.WriteInfo("InitStart异常：" + ex.Message);
                InsertListBox(string.Format("{0} InitStart异常：{1}", DateTime.Now.ToString(), ex.Message));
                this._SmarkFlag = "-1";
            }
        }
        /// <summary>
        /// 线程结束
        /// </summary>
        private void InitStop(int level)
        {
            if (this._SmarkFlag == "-1")
            {
                if (level == 1)
                {
                    if (DictThread["ReadProvinceHtml"].IsAlive)
                    {
                        DictThread["ReadProvinceHtml"].Abort();
                    }
                }
                if (level == 2)
                {
                    if (DictThread["ReadCityHtml"].IsAlive)
                    {
                        DictThread["ReadCityHtml"].Abort();
                    }
                }
                if (level == 3)
                {
                    if (DictThread["ReadCountHtml"].IsAlive)
                    {
                        DictThread["ReadCountHtml"].Abort();
                    }
                }
                #region 镇
                if (level == 4)
                {
                    if (DictThread["ReadTownHtml1"].IsAlive)
                    {
                        DictThread["ReadTownHtml1"].Abort();
                    }
                    if (DictThread["ReadTownHtml2"].IsAlive)
                    {
                        DictThread["ReadTownHtml2"].Abort();
                    }
                    if (DictThread["ReadTownHtml3"].IsAlive)
                    {
                        DictThread["ReadTownHtml3"].Abort();
                    }
                    if (DictThread["ReadTownHtml4"].IsAlive)
                    {
                        DictThread["ReadTownHtml4"].Abort();
                    }
                    if (DictThread["ReadTownHtml5"].IsAlive)
                    {
                        DictThread["ReadTownHtml5"].Abort();
                    }
                }
                #endregion
                #region 村
                if (level == 5)
                {
                    if (DictThread["ReadVillageHtml1"].IsAlive)
                    {
                        DictThread["ReadVillageHtml1"].Abort();
                    }
                    if (DictThread["ReadVillageHtml2"].IsAlive)
                    {
                        DictThread["ReadVillageHtml2"].Abort();
                    }
                    if (DictThread["ReadVillageHtml3"].IsAlive)
                    {
                        DictThread["ReadVillageHtml3"].Abort();
                    }
                    if (DictThread["ReadVillageHtml4"].IsAlive)
                    {
                        DictThread["ReadVillageHtml4"].Abort();
                    }
                    if (DictThread["ReadVillageHtml5"].IsAlive)
                    {
                        DictThread["ReadVillageHtml5"].Abort();
                    }
                    if (DictThread["ReadVillageHtml6"].IsAlive)
                    {
                        DictThread["ReadVillageHtml6"].Abort();
                    }
                    if (DictThread["ReadVillageHtml7"].IsAlive)
                    {
                        DictThread["ReadVillageHtml7"].Abort();
                    }
                    if (DictThread["ReadVillageHtml8"].IsAlive)
                    {
                        DictThread["ReadVillageHtml8"].Abort();
                    }
                    if (DictThread["ReadVillageHtml9"].IsAlive)
                    {
                        DictThread["ReadVillageHtml9"].Abort();
                    }
                    if (DictThread["ReadVillageHtml10"].IsAlive)
                    {
                        DictThread["ReadVillageHtml10"].Abort();
                    }
                }
                #endregion
                #region 异常数据
                if (level == 6)
                {
                    if (DictThread["ReadExceptionHtml"].IsAlive)
                    {
                        DictThread["ReadExceptionHtml"].Abort();
                    }
                }
                #endregion

            }
        }

        #endregion

        #region 线程中心
        /// <summary>
        /// 智能线程监控
        /// </summary>
        private void SmartWatch()
        {
            do
            {
                try
                {
                    if (this._SmarkFlag == "-1")
                    {
                        LogUtil.WriteInfo(string.Format("SmartWatch:开始停止线程 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                        //InitStop(this.LEVEL - 1);
                        //this.ThreadWatch.Join(5000);//停止5秒等待线程完全结束
                        LogUtil.WriteInfo(string.Format("CloudWatch:开始重启线程 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                        InitStart();
                        LogUtil.WriteInfo(string.Format("CloudWatch:重启线程完成 _SmarkFlag = {0}", this._SmarkFlag, this._SmarkFlag));
                    }

                }
                catch (ThreadAbortException ex)
                {
                    LogUtil.WriteInfo("SmartWatch(智能线程监控)：" + ex.Message);
                    break;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteInfo("SmartWatch(智能线程监控)关掉：" + ex.Message);
                    continue;
                }
                this.ThreadWatch.Join(5000);
            }
            while (ThreadWatch.IsAlive);
        }

        #endregion 线程中心
        bool IsBeginReadTownHtml = false, IsEndReadTownHtml = false,
             IsBeginReadVillageHtml = false, IsEndReadVillageHtml = false;
        public void CenterThread(object obj)
        {
            object[] objs = (object[])obj;
            string threadName = (string)objs[0];
            Thread tempThread = this.DictThread[threadName];

            if (tempThread == null)
            {
                InsertListBox(string.Format("上报线程threadName【{0}】启动失败，请检查！", threadName));
                LogUtil.WriteInfo(string.Format("上报线程threadName【{0}】启动失败，请检查！", threadName));
                return;
            }
            else
            {
                InsertListBox(string.Format("上报线程threadName【{0}】启动成功！", threadName));
                LogUtil.WriteInfo(string.Format("上报线程threadName【{0}】启动成功！", threadName));
            }

            if (threadName == "ReadProvinceHtml")
            {
                InsertListBox("【省份】正在插入。。。。。。。。");
                ReadProvinceHtmlBLL.GetProvinceHtmlContent();
                InsertListBox("【省份】插入成功。。。。。。。。");
                SetControl(false, true, false, false, false, false);
                return;
            }
            if (threadName == "ReadCityHtml")
            {
                InsertListBox("【市级】正在插入。。。。。。。。");
                ReadCityHtmlBLL.GetCityHtmlContent(0);
                InsertListBox("【市级】插入成功。。。。。。。。");
                SetControl(false, false, true, false, false, false);
                return;
            }
            if (threadName == "ReadCountHtml")
            {
                InsertListBox("【区县】正在插入。。。。。。。。");
                ReadAreaCountyHtmlBLL.GetAreaCountyHtmlContent(0);
                InsertListBox("【区县】插入成功。。。。。。。。");
                SetControl(false, false, false, true, false, false);
                return;
            }
            if (threadName == "ReadExceptionHtml")
            {
                InsertListBox("【异常数据】正在插入。。。。。。。。");
                ReadExceptionHtmlBLL.GetAllExpetionData();
                InsertListBox("【异常数据】插入成功。。。。。。。。");
                SetControl(false, false, false, true, false, false);
                return;
            }

            if (threadName.Contains("ReadTownHtml") && !IsBeginReadTownHtml)
            {
                IsBeginReadTownHtml = true;
                InsertListBox("【镇】正在插入。。。。。。。。");
            }
            if (threadName.Contains("ReadVillageHtml") && !IsBeginReadVillageHtml)
            {
                IsBeginReadVillageHtml = true;
                InsertListBox("【村】正在插入。。。。。。。。");
            }
            while (true)
            {
                try
                {
                    switch (threadName)
                    {
                        case "ReadTownHtml1":
                        case "ReadTownHtml2":
                        case "ReadTownHtml3":
                        case "ReadTownHtml4":
                        case "ReadTownHtml5":

                            int num = int.Parse(threadName.Substring(threadName.Length - 1, 1));
                            int sleepTime = num * 1000;
                            ReadTownHtmlBLL readTownHtmlBLL = new ReadTownHtmlBLL();
                            readTownHtmlBLL.GetTownHtmlContent(sleepTime, num - 1);
                            break;
                        case "ReadVillageHtml1":
                        case "ReadVillageHtml2":
                        case "ReadVillageHtml3":
                        case "ReadVillageHtml4":
                        case "ReadVillageHtml5":
                        case "ReadVillageHtml6":
                        case "ReadVillageHtml7":
                        case "ReadVillageHtml8":
                        case "ReadVillageHtml9":
                        case "ReadVillageHtml10":
                            int num2 = int.Parse(threadName.Substring(threadName.Length - 1, 1));
                            if (threadName.Length > 16)
                            {
                                num2 = int.Parse(threadName.Substring(threadName.Length - 2, 2));
                            }
                            int sleepTime2 = num2 * 1000;
                            ReadVillageHtmlBLL.GetVillageHtmlContent(sleepTime2, num2 - 1);
                            break;
                        default:
                            break;
                    }
                    if (threadName.Contains("ReadTownHtml") && readTownHtmlThreadCount.Sum() == 5 && !IsEndReadTownHtml)
                    {
                        IsEndReadTownHtml = true;
                        InsertListBox("【镇】插入成功。。。。。。。。");
                        SetControl(false, false, false, false, true, false);
                        break;
                    }
                    if (threadName.Contains("ReadVillageHtml") && readVillageHtmlThreadCount.Sum() == 10 && !IsEndReadVillageHtml)
                    {
                        IsEndReadVillageHtml = true;
                        InsertListBox("【村】插入成功。。。。。。。。");
                        SetControl(false, false, false, false, false, false);
                        break;
                    }
                }
                catch (ThreadAbortException ex)
                {
                    string logMg = string.Format("线程{0}：{1}", threadName, ex.Message);
                    LogUtil.WriteInfo(logMg);
                    InsertListBox(logMg);
                    break;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException.GetBaseException();
                    }
                    string logMg = string.Format("线程{0}执行失败  {1} {2}", threadName, ex.Message, ex.StackTrace);
                    LogUtil.WriteInfo(logMg);
                    InsertListBox(string.Format("{0} {1}", DateTime.Now.ToString(), logMg));
                    tempThread.Join(1000);
                    continue;
                }
                tempThread.Join(1000);
            }


        }

        #region 插入listbox和设置控件
        delegate void SetListBoxCallback(string str);
        public void InsertListBox(string info)
        {
            try
            {
                if (listBox1.InvokeRequired)  //控件是否跨线程？如果是，则执行括号里代码          
                {
                    if (this.IsDisposed == false || this.IsHandleCreated)
                    {
                        SetListBoxCallback setListCallback = new SetListBoxCallback(InsertListBox);   //实例化委托对象          
                        listBox1.Invoke(setListCallback, info);   //重新调用SetListBox函数          
                    }
                }
                else  //否则，即是本线程的控件，控件直接操作            
                {
                    if (listBox1.Items.Count > 100)
                        listBox1.Items.Clear();
                    listBox1.Items.Insert(0, string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), info));
                }
            }
            catch
            {
                ;
            }
        }
        delegate void SetControlCallback(bool Pro_IsEnabled, bool City_IsEnabled, bool Area_IsEnabled, bool Town_IsEnabled, bool Village_IsEnabled, bool Exception_IsEnabled);
        public void SetControl(bool Pro_IsEnabled, bool City_IsEnabled, bool Area_IsEnabled, bool Town_IsEnabled, bool Village_IsEnabled, bool Exception_IsEnabled)
        {
            try
            {

                if (this.InvokeRequired)  //控件是否跨线程？如果是，则执行括号里代码          
                {
                    if (this.IsDisposed == false || this.IsHandleCreated)
                    {
                        SetControlCallback setControlCallback = new SetControlCallback(SetControl);   //实例化委托对象    
                        this.Invoke(setControlCallback, new object[] { Pro_IsEnabled, City_IsEnabled, Area_IsEnabled, Town_IsEnabled, Village_IsEnabled, Exception_IsEnabled });

                    }
                }
                else  //否则，即是本线程的控件，控件直接操作            
                {
                    this.省ToolStripMenuItem.Enabled = Pro_IsEnabled;
                    this.市级ToolStripMenuItem.Enabled = City_IsEnabled;
                    this.区县ToolStripMenuItem.Enabled = Area_IsEnabled;
                    this.镇ToolStripMenuItem.Enabled = Town_IsEnabled;
                    this.村ToolStripMenuItem.Enabled = Village_IsEnabled;
                    this.异常数据处理ToolStripMenuItem.Enabled = Exception_IsEnabled;
                }
            }
            catch
            {
                ;
            }

        }
        #endregion

        #region 省市区县镇村(按钮)

        public void StartMethod(string LevelName, int Level)
        {
            LogUtil.WriteInfo(LevelName + "正在启动...");
            InsertListBox(LevelName + "正在启动...");
            this._SmarkFlag = "-1";
            this.LEVEL = Level;
            ThreadWatch = new Thread(new ThreadStart(SmartWatch));
            ThreadWatch.Start();
        }

        private void 省ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            //InitStop(6);
            InitThread(1);
            SetControl(false, false, false, false, false, false);
            StartMethod("【省】", 1);
        }

        private void 市级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            SetControl(false, false, false, false, false, false);
            InitStop(1);
            InitThread(2);
            StartMethod("【市级】", 2);
        }

        private void 区县ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            SetControl(false, false, false, false, false, false);
            InitStop(2);
            InitThread(3);
            StartMethod("【区县】", 3);
        }

        private void 镇ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            SetControl(false, false, false, false, false, false);
            InitStop(3);
            InitThread(4);
            StartMethod("【镇】", 4);
        }

        private void 村ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            SetControl(false, false, false, false, false, false);
            InitStop(4);
            InitThread(5);
            StartMethod("【村】", 5);
        }
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ThreadWatch != null && this.ThreadWatch.IsAlive)
            {
                this.ThreadWatch.Abort();
                this.ThreadWatch = null;
            }
        }

        private void 异常数据处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            SetControl(false, false, false, false, false, false);
            InitThread(6);
            StartMethod("【异常数据】", 6);
        }


    }
}
