using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMS_Capacity_Monitoring
{
    public partial class CapacityAlarmForm : Form
    {
        Timer timer = new Timer();
        int count = 0;

        public CapacityAlarmForm()
        {
            InitializeComponent();
            this.Load += CapacityAlarmForm_Load;
        }

        /// <summary>
        /// Alarm 창 Load 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CapacityAlarmForm_Load(object sender, EventArgs e)
        {
            TimerStart();
        }

        /// <summary>
        /// Timer Start 시작 메서드
        /// </summary>
        public void TimerStart() 
        {
            timer.Interval = 500;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        /// <summary>
        /// Tick 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Tick(object sender, EventArgs e)
        {
            count++;

            if(count % 2 == 0)
            {
                uiPn_Alarm.BackColor = Color.White;
            }
            else
            {
                uiPn_Alarm.BackColor = Color.Red;
            }
        }
    }
}
