using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Yaxon.ObtainBaiDuAddress.Common;
using Yaxon.ObtainBaiDuAddress.BLL;

namespace Yaxon.ObtainBaiDuAddress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitThread();

        }

        private Dictionary<string, Thread> DictThread = new Dictionary<string, Thread>();//线程
        public static int[] obtainBaiDuAddressThreadCount = new int[5] { 0, 0, 0, 0, 0 };//镇5个线程跑
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
        private void InitThread()
        {

            DictThread.Add("ObtainBaiDuAddress1", new Thread(CenterThread));
            /* DictThread.Add("ObtainBaiDuAddress2", new Thread(CenterThread));
             DictThread.Add("ObtainBaiDuAddress3", new Thread(CenterThread));
             DictThread.Add("ObtainBaiDuAddress4", new Thread(CenterThread));
             DictThread.Add("ObtainBaiDuAddress5", new Thread(CenterThread));*/
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
                    #region 正常获取地址
                    if (DictThread["ObtainBaiDuAddress1"].IsAlive == false)
                    {
                        DictThread["ObtainBaiDuAddress1"] = null;
                        DictThread["ObtainBaiDuAddress1"] = new Thread(CenterThread);
                        DictThread["ObtainBaiDuAddress1"].Start(new object[] { "ObtainBaiDuAddress1" });
                        Thread.Sleep(100);
                    }
                    /*if (DictThread["ObtainBaiDuAddress2"].IsAlive == false)
                    {
                        DictThread["ObtainBaiDuAddress2"] = null;
                        DictThread["ObtainBaiDuAddress2"] = new Thread(CenterThread);
                        DictThread["ObtainBaiDuAddress2"].Start(new object[] { "ObtainBaiDuAddress2" });
                        Thread.Sleep(100);
                    }
                    if (DictThread["ObtainBaiDuAddress3"].IsAlive == false)
                    {
                        DictThread["ObtainBaiDuAddress3"] = null;
                        DictThread["ObtainBaiDuAddress3"] = new Thread(CenterThread);
                        DictThread["ObtainBaiDuAddress3"].Start(new object[] { "ObtainBaiDuAddress3" });
                        Thread.Sleep(100);
                    }
                    if (DictThread["ObtainBaiDuAddress4"].IsAlive == false)
                    {
                        DictThread["ObtainBaiDuAddress4"] = null;
                        DictThread["ObtainBaiDuAddress4"] = new Thread(CenterThread);
                        DictThread["ObtainBaiDuAddress4"].Start(new object[] { "ObtainBaiDuAddress4" });
                        Thread.Sleep(100);
                    }
                    if (DictThread["ObtainBaiDuAddress5"].IsAlive == false)
                    {
                        DictThread["ObtainBaiDuAddress5"] = null;
                        DictThread["ObtainBaiDuAddress5"] = new Thread(CenterThread);
                        DictThread["ObtainBaiDuAddress5"].Start(new object[] { "ObtainBaiDuAddress5" });
                        Thread.Sleep(100);
                    }*/
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
                #region 正常获取地址

                if (DictThread["ObtainBaiDuAddress1"].IsAlive)
                {
                    DictThread["ObtainBaiDuAddress1"].Abort();
                }
                /* if (DictThread["ObtainBaiDuAddress2"].IsAlive)
                 {
                     DictThread["ObtainBaiDuAddress2"].Abort();
                 }
                 if (DictThread["ObtainBaiDuAddress3"].IsAlive)
                 {
                     DictThread["ObtainBaiDuAddress3"].Abort();
                 }
                 if (DictThread["ObtainBaiDuAddress4"].IsAlive)
                 {
                     DictThread["ObtainBaiDuAddress4"].Abort();
                 }
                 if (DictThread["ObtainBaiDuAddress5"].IsAlive)
                 {
                     DictThread["ObtainBaiDuAddress5"].Abort();
                 }*/
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
        int endBaiDuAddress = 0;
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

            while (true)
            {
                try
                {
                    switch (threadName)
                    {
                        case "ObtainBaiDuAddress1":
                            /*  case "ObtainBaiDuAddress2":
                                case "ObtainBaiDuAddress3":
                                case "ObtainBaiDuAddress4":
                                case "ObtainBaiDuAddress5":*/

                            //int num = int.Parse(threadName.Substring(threadName.Length - 1, 1));
                            //int sleepTime = num * 1000;
                            endBaiDuAddress = ShopBLL.GetTopShopModel();
                            break;

                        default:
                            break;
                    }

                    if (endBaiDuAddress == 1)
                    {
                        InsertListBox("【执行完毕】。。。。。。。。");
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

        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ThreadWatch != null && this.ThreadWatch.IsAlive)
            {
                this.ThreadWatch.Abort();
                this.ThreadWatch = null;
            }
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._SmarkFlag = "-1";
            this.开始ToolStripMenuItem.Enabled = false;
            InitStart();
        }

    }
}
