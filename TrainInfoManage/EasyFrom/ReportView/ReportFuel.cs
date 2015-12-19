using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainView.ChildFrom;
using TrainCommon;

namespace EasyFrom.ReportView
{
    public partial class ReportFuel : UserControl
    {
        /// <summary>
        /// 报单ID
        /// </summary>
        private int reportID = 0;
        /// <summary>
        /// 输入框索引
        /// </summary>
        private int index;
        /// <summary>
        /// 数字输入框窗体
        /// </summary>
        private Dlg_Number dlg_Number = null;

        public ReportFuel()
        {
            InitializeComponent();
            initReportId();
        }

        /// <summary>
        /// 显示输入数字对话框
        /// </summary>
        private void ShowDlgNumber()
        {
            if (dlg_Number == null)
            {
                dlg_Number = new Dlg_Number();
                dlg_Number.Closed += new EventHandler(GetNumber);
            }
            tagGet.Focus();//利用控件转移焦点
            dlg_Number.ShowDialog();
        }

        /// <summary>
        /// 获取数字
        /// </summary>
        private void GetNumber(object sender, EventArgs e)
        {
            string num = (string)((Dlg_Number)sender).Tag;
            using (DataTable ft = DBAction.GetDTFromSQL("select ID from TrainGetFuel where RHId=" + reportID))
            {
                int row = ft.Rows.Count;
                if (String.IsNullOrEmpty(num))
                {
                    return;
                }
                using (RParams param = new RParams())
                {
                    switch (index)
                    {
                        case 0:
                            txtGet.Text = num;
                            if (txtGet.Text.Trim().Length > 0)
                            {
                                param.Items.Clear();
                                param.Add("Receive", txtGet.Text);
                                if (row > 0)
                                {
                                    DBAction.Update(ETableName.TrainGetFuel, "Receive", "RHId=" + reportID, param);
                                }
                                else
                                {
                                    param.Add("RHId", reportID);
                                    DBAction.Insert("TrainGetFuel", param);
                                }

                            }
                            break;
                        case 1:
                            txtSubmit.Text = num;
                            if (txtSubmit.Text.Trim().Length > 0)
                            {
                                param.Items.Clear();
                                param.Add("Deliver", txtSubmit.Text);
                                if (row > 0)
                                {
                                    DBAction.Update(ETableName.TrainGetFuel, "Deliver", "RHId=" + reportID, param);
                                }
                                else
                                {
                                    param.Add("RHId", reportID);
                                    DBAction.Insert("TrainGetFuel", param);
                                }

                            }
                            break;
                        default:
                            break;
                    }
                    tagGet.Focus();//利用控件转移焦点
                }

            }

        }


        /// <summary>
        /// 交电量
        /// </summary>
        private void txtSubmit_GotFocus(object sender, EventArgs e)
        {
            index = 1;
            ShowDlgNumber();
        }

        /// <summary>
        /// 接电量
        /// </summary>
        private void txtGet_GotFocus(object sender, EventArgs e)
        {
            index = 0;
            ShowDlgNumber();
        }

        /// <summary>
        /// 初始化报单ID
        /// </summary>
        private void initReportId()
        {

            if (reportID == 0)
            {
                //XmlAction xa = new XmlAction();
                //XmlNode node = xa.GetNode("/RoboConfig/ReportID", null, null);//报单ID节点
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig");
                string rID = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ReportID"].ToString().Trim();//报单ID
                reportID = Convert.ToInt32(rID);
            }
            using (DataTable ft = DBAction.GetDTFromSQL("select * from TrainGetFuel where RHId=" + reportID))
            {
                if (ft.Rows.Count > 0)
                {
                    txtGet.Text = ft.Rows[0]["Receive"].ToString();
                    txtSubmit.Text = ft.Rows[0]["Deliver"].ToString();
                }
            }

        }

        /// <summary>
        /// 获取数字
        /// </summary>
        private void GetPassword(object sender, EventArgs e)
        {
            string pwd = (string)((Dlg_Number)sender).Tag;
            if (string.IsNullOrEmpty(pwd))
            {
                return;
            }
            else
                txtGet.Text = pwd;
        }
        /// <summary>
        /// 获取数字
        /// </summary>
        private void GetPassword1(object sender, EventArgs e)
        {
            string pwd1 = (string)((Dlg_Number)sender).Tag;
            if (string.IsNullOrEmpty(pwd1))
            {
                return;
            }
            else
                txtSubmit.Text = pwd1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
                dlg_Number = new Dlg_Number();
                dlg_Number.Closed += new EventHandler(GetPassword);
           
            dlg_Number.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                dlg_Number = new Dlg_Number();
                dlg_Number.Closed += new EventHandler(GetPassword1);
            
            dlg_Number.ShowDialog();
        }

     

    }
}
