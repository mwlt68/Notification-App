using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotrificationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<Alert> alerts = new List<Alert>();
        public void alertCounter()
        {
            int sucCounter = 0, errCounter = 0, warCounter = 0, infCounter = 0;
            foreach (var alert in alerts)
            {
                switch (alert.notification)
                {
                    case Alert.notificationType.success:
                        sucCounter++;
                        break;
                    case Alert.notificationType.info:
                        infCounter++;
                        break;
                    case Alert.notificationType.warning:
                        warCounter++;
                        break;
                    case Alert.notificationType.error:
                        errCounter++;
                        break;
                    default:
                        break;
                }
            }
            lbl_e.Text = errCounter.ToString();
            lbl_i.Text = infCounter.ToString();
            lbl_s.Text = sucCounter.ToString();
            lbl_w.Text = warCounter.ToString();

        }
        public void createAlert(Alert alert, Alert.notificationType notification)
        {
            alert.Tag = notification.ToString();
            alert.Show();
            alerts.Add(alert);
            alertCounter();
            alert.SetAlertCount();
        }
        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert(this,Alert.notificationType.success);
            createAlert(alert, Alert.notificationType.success);
        }

        private void BunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert(this, Alert.notificationType.info);
            createAlert(alert, Alert.notificationType.info);


        }

        private void BunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert(this, Alert.notificationType.warning);
            createAlert(alert, Alert.notificationType.warning);

        }

        private void BunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert(this, Alert.notificationType.error);
            createAlert(alert, Alert.notificationType.error);

        }

    }
}
