using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotrificationApp
{
    public partial class Alert : Form
    {
        const int size =5;
        const int paddingBetweenAlert = 50;
        const int paddingTemp = 50 ;
        bool isClick = false;
        List<Point> posList = new List<Point>();
        public enum notificationType { success=0,info=1,warning=2,error=3}
        public notificationType notification;
        Form1 form1;
        public Alert(Form1 frm,notificationType type)
        {
            form1 = frm;
            InitializeComponent();
            FillImageList();
            createAlert(type);

        }
        public void createAlert(notificationType type)
        {
            notification = type;
            switch (type)
            {
                case notificationType.success:
                    pictureBox1.Image=ımageList1.Images["success"];
                    this.BackColor = Color.FromName("Green");
                    label1.Text = "Process complete";
                    break;
                case notificationType.info:
                    pictureBox1.Image = ımageList1.Images["info"];
                    this.BackColor = Color.FromName("Gray");
                    label1.Text = "Process have info";
                    break;
                case notificationType.warning:
                    pictureBox1.Image = ımageList1.Images["warning"];
                    this.BackColor = Color.FromArgb(255,128, 0);
                    label1.Text = "Process have warning";
                    break;
                case notificationType.error:
                    pictureBox1.Image = ımageList1.Images["error"];
                    this.BackColor = Color.FromName("Red");
                    label1.Text = "Process have some errors";
                    break;
                default:
                    MessageBox.Show("Error when opening form !","Error");
                    break;
            }
        }
        public void FillImageList()
        {
            try
            {
                 ımageList1.Images.Add("error", Image.FromFile("error.png"));
                 ımageList1.Images.Add("info", Image.FromFile("info.jpg"));
                 ımageList1.Images.Add("warning", Image.FromFile("warning.png"));
                 ımageList1.Images.Add("success", Image.FromFile("suc.png"));

            }
            catch (Exception)
            {
                MessageBox.Show("Error while assigning images to images!");
                throw;
            }
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            this.Size = new Size(350,100);
            Rectangle screenRec = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(screenRec.Width-this.Width-paddingTemp, screenRec.Height - this.Height - paddingTemp);
        }
        public void SetAlertCount()
        {
            if (form1.alerts.Count == size)
            {
                form1.alerts.ElementAt(0).Close();
            }
            OrderAlerts();
        }
        void OrderAlerts()
        {
            int counter = 1;
            foreach (var alert in form1.alerts)
            {
                Point pos = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width - paddingTemp,(counter*alert.Height)+((counter-1)*paddingBetweenAlert));
                posList.Add(pos);
                counter++;
            }
            MoveToPositionAndRemoveAlertFirs();
        }
        void MoveToPositionAndRemoveAlertFirs()
        {
            int counter = 0;
            foreach (var alert in form1.alerts)
            {
                Move(alert, posList.ElementAt(counter++));
            }
        }
        void Move(Alert alert,Point target,int threadSleep=5,int moveSpeed=10)
        {
            while ((alert.Location.Y-target.Y) > 5)
            {
                alert.Location = new Point(alert.Location.X, alert.Location.Y - moveSpeed);
                Thread.Sleep(threadSleep);
            }
        }


        private void BunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.015;
            }
            else
            {
                form1.alerts.Remove(this);
                this.Close();
            }
        }

        private void Alert_MouseHover(object sender, EventArgs e)
        {
            if (this.Opacity<101)
            {
                this.Opacity += 1;
            }
            timer1.Stop();
        }

        private void Alert_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            isClick = true;
        }

        private void Alert_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Alert_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.alerts.Remove(this);
            form1.alertCounter();
        }

        private void Alert_MouseLeave(object sender, EventArgs e)
        {
            if (!isClick)
                timer1.Start();
        }
    }
}
