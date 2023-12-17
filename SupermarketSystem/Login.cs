using SupermarketSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection cooon = new SqlConnection(@"Data Source=.;Initial Catalog=SupermarketSystem;Integrated Security=True");

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private int getCahier_id(string username, string pass)
        {

            #region get the cashierid

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand("SELECT CashierID FROM Cashier WHERE CName = '" + username + "' and CPass = '" + pass + "' ", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);



            if (dtable.Rows.Count > 0)
            {
                var index_Cashier = (int)dtable.Rows[0][0];
                //MessageBox.Show(index_Cashier.ToString());
                return index_Cashier;

            }
            return 0;
            #endregion

        }


        private void loginvar()
        {

            String usernaem, user_pass;
            usernaem = txtUsername.Text;
            user_pass = txtpassword.Text;

            if (usernaem == "Admin" && user_pass == "Admin" && comboSelectRole.Text =="ADMIN")
            {
                Admin admin = new Admin();
                admin.Show();

                this.Hide();
                Message message = new Message("success Login Admin", Message.AlertType.success);
                message.Show();
            }
            else if ( comboSelectRole.Text == "SELLER")
            {
                try
                {
                    String querry = "SELECT * FROM Cashier WHERE CName = '" + txtUsername.Text + "' AND CPass = '" + txtpassword.Text + "' ";
                    SqlDataAdapter sda = new SqlDataAdapter(querry, cooon);
                    DataTable dtable = new DataTable();
                    sda.Fill(dtable);

                    //MessageBox.Show(dtable.Rows.Count.ToString());
                    if (dtable.Rows.Count > 0)
                    {
                        usernaem = txtUsername.Text;
                        user_pass = txtpassword.Text;

                        int cashier_id = getCahier_id(usernaem, user_pass);

                       

                        cashier cashier = new cashier();
                        cashier.SetData(cashier_id);

                        cashier.Show();

                        Message message = new Message("success Login", Message.AlertType.success);
                        message.Show();

                        
                        this.Hide();
                    }

                    else
                    {
                        Message message = new Message("Please,Enter Correct data", Message.AlertType.wrong);
                        message.Show();
                        
                        txtUsername.Clear();
                        txtpassword.Clear();

                        //Focus username
                        txtUsername.Focus();

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString(), "Error");
                }
                finally
                {
                    cooon.Close();
                }
            }
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            loginvar();
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
