using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace SupermarketSystem
{
    public partial class Message : Form
    {
        public Message(string _message,AlertType type)
        {
            InitializeComponent();
            txtmessage.Text = _message;
            switch (type)
            {
                case AlertType.success:
                    this.BackColor = Color.SeaGreen;
                    picIcon.Image = imageList1.Images[0];
                    // this.guna2HtmlLabel1 = message;
                    break;
                case AlertType.wrong:
                    this.BackColor = Color.Crimson;
                    picIcon.Image = imageList1.Images[1];
                    
                    break;
            }
        }

        public enum AlertType
        {
            success,wrong

        }

        private void Message_Load(object sender, EventArgs e)
        {
            //set position
            this.Top = 10;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 10;
        }

        private void timerout_Tick(object sender, EventArgs e)
        {
            this.Close(); 
        }
        void animatform()
        {

        }
        int inter = 0;
        private void shwo_Tick(object sender, EventArgs e)
        {
            if (this.Top > 60)
            {
                this.Top += inter;
                inter += 2;
            }
            else
            {
                shwo.Stop();
            Closealart.Start();
            }
                
        }

        private void Close_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.07; 
            }
            else
                this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Closealart.Start(); 
        }
    }
}
