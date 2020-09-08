using ChartFX.WinForms.Galleries;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforsys.DBManagers;
using Winforsys.Util;

namespace TMS_Capacity_Monitoring
{
    public partial class MainForm : Form
    {
        private SshClient sshc = null;

        private DataSet fileDs = null;
        private DataSet tableSpaceDs = null;

        private Timer timer = new Timer();

        CapacityAlarmForm alarmForm = null;

        public MainForm()
        {
            InitializeComponent();

            this.Shown += TMS_Capacity_Viewer_Shown;
        }

        public void TMS_Capacity_Viewer_Shown(object sender, EventArgs e)
        {
            Start();
            CapacityCheckTimer();
        }

        /// <summary>
        /// 10초에 한번씩 모니터링 갱신
        /// </summary>
        public void CapacityCheckTimer()
        {
            timer.Interval = 1000 * 60; //10초에 한번 갱신
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// 타이머 Tick 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            Start();
        }
         
        public void Start()
        {
            //리눅스 이미지 서버 용량 정보 가져오기
            //ConnectLinux();

            //File Server Capacity 정보 가져오기
            GetCapacityData();

            //TableSpace Capactiy 정보 가져오기
            GetTableSpaceCapacity();
        }

        /// <summary>
        /// Linux 이미지 서버 연결 
        /// </summary>
        /// <param name="chart"></param>
        public void ConnectLinux()
        {
            if (sshc == null || sshc.IsConnected == false)
            {
                string addr = XmlManager.GetValue("IMG_FTP", "R_ADDR");

                ConnectionInfo ci = new ConnectionInfo(addr, "root", new PasswordAuthenticationMethod("root", "@123qwe"));
                sshc = new SshClient(ci);
                sshc.Connect();
            }

            //이미지 서버 용량 확인
            GetImageCapacityData();
        }

        /// <summary>
        /// 리눅스 이미지 서버 용량 확인 메서드
        /// </summary>
        public void GetImageCapacityData()
        {
            string cmd = string.Format(@"df -h"); //전체 용량 확인
            string[] sizes = sshc.RunCommand(cmd).Result.Split('\n');

            SetImageSize(sizes[9], uiPgb_ImageServer, uiLb_ImageServer_Title, uiLb_ImageServer);
        }

        /// <summary>
        /// 컨트롤에 이미지 사이즈 넣기
        /// </summary>
        /// <param name="size">리눅스 이미지 사이즈 정보</param>
        /// <param name="pb">ProgressBar</param>
        /// <param name="title">제목</param>
        /// <param name="lb">용량 정보</param>
        public void SetImageSize(string size, ProgressBar pb, Label title, Label lb)
        {
            ImageCapacity ic = new ImageCapacity();

            ic.TOTAL_SIZE = "88"; //현재 ImageServer 용량 88TB
            ic.FREE_SIZE = size.Substring(size.Length - 14, 2).ToString(); //남은 용량
            ic.USAGE = (Convert.ToInt32(ic.TOTAL_SIZE) - Convert.ToInt32(ic.FREE_SIZE)).ToString(); //사용 용량

            GetImageServerIcon(); // Drive 이미지 가져오기

            pb.ForeColor = Color.DeepSkyBlue;
            pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            pb.Maximum = Convert.ToInt32(ic.TOTAL_SIZE);
            pb.Value = Convert.ToInt32(ic.USAGE);

            //용량이 90%이상인 경우 ProgressBar 컬러 Red로 변경
            double percentage = Convert.ToDouble(ic.USAGE) / Convert.ToDouble(ic.TOTAL_SIZE) * 100;
            if (percentage >= 90)
            {
                pb.ForeColor = Color.Red;
                pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

                CreateAlarmForm(); //알람창 발생
            }

            title.Text = string.Format("Disk ({0})", "Image Server");
            title.AutoSize = true;

            lb.Text = string.Format("{0}TB of {1}TB available", ic.TOTAL_SIZE, ic.FREE_SIZE);
            lb.AutoSize = true;
        }

        /// <summary>
        /// 이미지 아이콘 가져오기
        /// </summary>
        public void GetImageServerIcon()
        {
            Image bMap = Properties.Resources.Folder;
            uiPb_ImageServer.Image = bMap;
            uiPb_ImageServer.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// File Server 드라이브 컨트롤에 표시
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="pb"></param>
        /// <param name="title"></param>
        /// <param name="lb"></param>
        public void SetDriveSize(FileCapacity drive, ProgressBar pb, Label title, Label lb)
        {
            GetDriveImage(); // Drive 이미지 가져오기

            pb.ForeColor = Color.DeepSkyBlue;
            pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            pb.Maximum = Convert.ToInt32(drive.TOTAL_SIZE);
            pb.Value = Convert.ToInt32(drive.USAGE);

            //용량이 90%이상인 경우 ProgressBar 컬러 Red로 변경
            double percentage = Convert.ToDouble(drive.USAGE) / Convert.ToDouble(drive.TOTAL_SIZE) * 100;
            if(percentage >= 90)
            {
                pb.ForeColor = Color.Red;
                pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

                CreateAlarmForm(); //알람창 발생
            }

            title.Text = string.Format("Disk ({0}:)", drive.DRIVE_NAME);
            title.AutoSize = true;

            lb.Text = string.Format("{0}GB of {1}GB available", drive.TOTAL_SIZE, drive.FREE_SIZE);
            lb.AutoSize = true;
        }

        /// <summary>
        /// 이미지 아이콘 가져오기
        /// </summary>
        public void GetDriveImage()
        {
            Image bMap = Properties.Resources.Folder;
            uiPb_CDrive.Image = bMap;
            uiPb_CDrive.SizeMode = PictureBoxSizeMode.StretchImage;

            uiPb_DDrive.Image = bMap;
            uiPb_DDrive.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// TableSpace 값들 
        /// ProgressBar , Label 컨트롤에 표시
        /// </summary>
        /// <param name="table"></param>
        /// <param name="pb"></param>
        /// <param name="title"></param>
        /// <param name="lb"></param>
        public void SetTableSpaceSize(TableSpace table, ProgressBar pb, Label title, Label lb)
        {
            GetTableSpaceImage();

            pb.ForeColor = Color.DeepSkyBlue;
            pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            pb.Maximum = Convert.ToInt32(table.TOTAL_SIZE) / 1024; //GB 로 변환
            pb.Value = Convert.ToInt32(table.USAGE) / 1024; //GB 로 변환

            string[] name = table.TABLESPACE_NAME.Split('_');
            title.Text = string.Format("TableSpace({0} : {1})", UserInfo.DEPT.ToString(), name[2]);
            title.AutoSize = true;

            //용량이 90%이상인 경우 ProgressBar 컬러 Red로 변경
            double percentage = Convert.ToDouble(table.USAGE) / Convert.ToDouble(table.TOTAL_SIZE) * 100;
            if (percentage >= 90)
            {
                pb.ForeColor = Color.Red;
                pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

                CreateAlarmForm(); //알람창 발생
            }

            lb.Text = string.Format("{0}GB of {1}GB available", Convert.ToInt32(table.TOTAL_SIZE) / 1024, Convert.ToInt32(table.FREE_SIZE) / 1024);
            lb.AutoSize = true;
        }

        /// <summary>
        /// 이미지 아이콘 가져오기
        /// </summary>
        public void GetTableSpaceImage()
        {
            Image bMap = Properties.Resources.DataBase;
            uiPb_DFS.Image = bMap;
            uiPb_DFS.SizeMode = PictureBoxSizeMode.StretchImage;

            uiPb_JPC.Image = bMap;
            uiPb_JPC.SizeMode = PictureBoxSizeMode.StretchImage;

            uiPb_RCP.Image = bMap;
            uiPb_RCP.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /// <summary>
        /// DB에 저장되어 있는 File Server 드라이브 용량
        /// 가져오기
        /// </summary>
        public void GetCapacityData()
        {
            fileDs = new DataSet();
            
            string query = string.Empty;
            
            query = @"
SELECT DRIVE, TOTAL_SIZE, USAGE, FREE_SIZE, UPDATE_TIME
FROM WIN_RCP_#DEPT.#DEPT_FILE_SERVER_CAPACITY_TBL
WHERE 1 = 1
";

            query = query.Replace("#DEPT", UserInfo.DEPT.ToString());

            OracleDBManager.Instance.ExecuteDsQuery(fileDs, query);

            if(fileDs != null && fileDs.Tables.Count > 0 && fileDs.Tables[0].Rows.Count > 0)
            {
                for (int idx = 0; idx < fileDs.Tables[0].Rows.Count; idx++)
                {
                    FileCapacity cap = new FileCapacity();

                    cap.DRIVE_NAME = fileDs.Tables[0].Rows[idx]["DRIVE"].ToString();
                    cap.TOTAL_SIZE = fileDs.Tables[0].Rows[idx]["TOTAL_SIZE"].ToString();
                    cap.USAGE = fileDs.Tables[0].Rows[idx]["USAGE"].ToString();
                    cap.FREE_SIZE = fileDs.Tables[0].Rows[idx]["FREE_SIZE"].ToString();

                    if (cap.DRIVE_NAME == "C") //C 드라이브
                    {
                        SetDriveSize(cap, uiPgb_CDrive, uiLb_CDrive_Title, uiLb_CDrive);
                    }
                    else //D 드라이브
                    {
                        SetDriveSize(cap, uiPgb_DDrive, uiLb_DDrive_Title, uiLb_DDrive);
                    }
                }
            }
        }

        /// <summary>
        /// TableSpace 용량 확인
        /// MB 단위로 가져오기
        /// </summary>
        public void GetTableSpaceCapacity()
        {
            string query = string.Empty;

            try
            {
                tableSpaceDs = new DataSet();

                query = @"
SELECT NAME, TOTAL_SIZE, USAGE, FREE_SIZE, PERCENTAGE, UPDATE_TIME
FROM WIN_RCP_#DEPT.#DEPT_TABLESPACE_CAPACITY_TBL
WHERE 1 = 1
";

                query = query.Replace("#DEPT", UserInfo.DEPT.ToString());

                OracleDBManager.Instance.ExecuteDsQuery(tableSpaceDs, query);

                if (tableSpaceDs != null && tableSpaceDs.Tables.Count > 0 && tableSpaceDs.Tables[0].Rows.Count > 0)
                {
                    for (int idx = 0; idx < tableSpaceDs.Tables[0].Rows.Count; idx++)
                    {
                        TableSpace ts = new TableSpace();

                        ts.TABLESPACE_NAME = tableSpaceDs.Tables[0].Rows[idx]["NAME"].ToString();
                        ts.TOTAL_SIZE = tableSpaceDs.Tables[0].Rows[idx]["TOTAL_SIZE"].ToString();
                        ts.FREE_SIZE = tableSpaceDs.Tables[0].Rows[idx]["FREE_SIZE"].ToString();
                        ts.USAGE = tableSpaceDs.Tables[0].Rows[idx]["USAGE"].ToString();
                        ts.PERCENTAGE = tableSpaceDs.Tables[0].Rows[idx]["PERCENTAGE"].ToString();

                        if(ts.TABLESPACE_NAME.Contains("DFS"))
                        {
                            SetTableSpaceSize(ts, uiPgb_DFS, uiLb_TableSpace_DFS_Title, uiLb_TableSpace_DFS);
                        }
                        else if(ts.TABLESPACE_NAME.Contains("JPC"))
                        {
                            SetTableSpaceSize(ts, uiPgb_JPC, uiLb_TableSpace_JPC_Title, uiLb_TableSpace_JPC);
                        }
                        else
                        {
                            SetTableSpaceSize(ts, uiPgb_RCP, uiLb_TableSpace_RCP_Title, uiLb_TableSpace_RCP);
                        }
                    }
                }
            }
            catch { }
        }

        public void CreateAlarmForm()
        {
            alarmForm = new CapacityAlarmForm(); //Alarm 창 객체 생성
            alarmForm.StartPosition = FormStartPosition.CenterParent;
            alarmForm.ShowDialog();
        }
    }

    public class FileCapacity
    {
        public string DRIVE_NAME = string.Empty;
        public string TOTAL_SIZE = string.Empty;
        public string USAGE = string.Empty;
        public string FREE_SIZE = string.Empty;
    }

    public class TableSpace
    {
        public string TABLESPACE_NAME = string.Empty;
        public string TOTAL_SIZE = string.Empty;
        public string USAGE = string.Empty;
        public string FREE_SIZE = string.Empty;
        public string PERCENTAGE = string.Empty;
    }

    public class ImageCapacity
    {
        public string TOTAL_SIZE = string.Empty;
        public string USAGE = string.Empty;
        public string FREE_SIZE = string.Empty;
    }
}
