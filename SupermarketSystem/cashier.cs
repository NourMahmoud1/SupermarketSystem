using SupermarketSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SupermarketSystem
{
    public partial class cashier : Form
    {
        int cashier_id = 0;
        public cashier()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=SupermarketSystem;Integrated Security=True");
        public void SetData(int data)
        {
            // Do something with the data in Form2
            cashier_id = data;
        }
        private void populate()
        {
            /*
            con.Open();
            string query = "select ProdName, ProdQty from Product";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder db = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DataGridViewProduct.DataSource = ds.Tables[0];
            con.Close();
            */

            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select ProdName, ProdQty from Product", con);
            adapt.Fill(dt);
            DataGridViewProduct.DataSource = dt;
            con.Close();

        }
        private int getID(string prodName)
        {
            var index_Product = 0;
            #region get the id for Category 

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand("SELECT ProdID FROM Product WHERE ProdName = '" + prodName + "' ", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);



            if (dtable.Rows.Count > 0)
            {
                index_Product = (int)dtable.Rows[0][0];
                //MessageBox.Show(index.ToString());

            }

            #endregion
            return index_Product;
        }

        private string getPric(int id)
        {
            //var index_Product = 0;
            #region get the Price 

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand("SELECT ProdPrice FROM Product WHERE ProdID = '" + id + "' ", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);



            if (dtable.Rows.Count > 0)
            {
                var index_Product = dtable.Rows[0][0];
                //MessageBox.Show(index_Product.ToString());
                return index_Product.ToString();

            }
            return " ";
            #endregion

        }
        private void cashier_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarketSystemDataSet11.ORDER_DETAILS' table. You can move, or remove it, as needed.
            this.oRDER_DETAILSTableAdapter1.Fill(this.supermarketSystemDataSet11.ORDER_DETAILS);
            // TODO: This line of code loads data into the 'supermarketSystemDataSet1.ORDER_DETAILS' table. You can move, or remove it, as needed.
          //  this.oRDER_DETAILSTableAdapter.Fill(this.supermarketSystemDataSet1.ORDER_DETAILS);
            populate();
            DisplayOrder();
           // MessageBox.Show(cashier_id.ToString());

        }

        private void DataGridViewProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductName.Text = DataGridViewProduct.SelectedRows[0].Cells[0].Value.ToString();
            //txtquantity.Text = DataGridViewProduct.SelectedRows[0].Cells[1].Value.ToString();
            // txtQTYProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            int productID = getID(txtProductName.Text);
            txtPrice.Text = getPric(productID).ToString();
        }


        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            clear_data();
            Application.Exit();

        }

        public bool AddItemTo_orderDetails(int id,string name, string qty, string price, string total)
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            string query = "Insert into ORDER_DETAILS(ProdID, ProductName, ProdQTY, ProdPRICE, ProdTOTAL)values(@ProdID, @ProductName,  @ProdQTY, @ProdPRICE, @ProdTOTAL)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@ProdID", id.ToString().Trim());
                    cmd.Parameters.AddWithValue("@ProductName", name.Trim());
                    cmd.Parameters.AddWithValue("@ProdQTY", qty.Trim());
                    cmd.Parameters.AddWithValue("@ProdPRICE", price.Trim());
                    cmd.Parameters.AddWithValue("@ProdTOTAL", total.Trim());


                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                throw;
            }

        }
        private int getQTY(int id)
        {
             
            #region get the qty

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand("SELECT ProdQty FROM Product WHERE ProdID = '" + id + "' ", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);



            if (dtable.Rows.Count > 0)
            {
                var index_Product = (int)dtable.Rows[0][0];
               // MessageBox.Show(index_Product.ToString());
                return index_Product;

            }
            return 0;
            #endregion

        }
        private void DisplayDataDetails()
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select ItemID,ProductName, ProdQTY, ProdPRICE, ProdTOTAL from ORDER_DETAILS", con.connect);
            adapt.Fill(dt);
            guna2DataGridView2.DataSource = dt;
            con.connect.Close();

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int productID = getID(txtProductName.Text);
            string strqty = txtquantity.Text;
            string strprice = txtPrice.Text;
            string newStrprice = strprice.Substring(0, strprice.Length - 3);

            if (txtquantity.Text == "")
            {
                strqty = "1";
                txtquantity.Text = "1";
            }
            
            int qtys = int.Parse(strqty);
            int prices = int.Parse(newStrprice);
            var total = qtys * prices;

            //MessageBox.Show(total.ToString());
            int check_qty = getQTY(productID) - qtys;
            if (check_qty >= 0)
            {
                AddItemTo_orderDetails(productID, txtProductName.Text, txtquantity.Text, txtPrice.Text, total.ToString());
                DisplayDataDetails();

                #region change the prodQty in the product table
                if (ConnectionState.Closed == con.State)
                {
                    con.Open();
                }
                string query = "Update Product set ProdQty='" + check_qty.ToString() + "' where ProdID = '" + productID.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                #endregion


                populate();
            }
            else
            {
                Message message = new Message("There is no enough Products", Message.AlertType.wrong);
                message.Show();
            }

        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            //lblDate.Text = DateTime.Now.ToShortDateString();
            // lblDate.Text = DateTime.Today.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void clear_data()
        {
            if (ConnectionState.Closed == con.State)
            {
                con.Open();
            }
            string queryy = "Delete From ORDER_DETAILS";
            SqlCommand cmsd = new SqlCommand(queryy, con);

            cmsd.ExecuteNonQuery();
           // Message message = new Message("you Canceled This Order", Message.AlertType.success);
            //message.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear_data();
            DisplayDataDetails();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
        private void DisplayOrder()
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select OrderID,CName , OrderDate, TotalPrice  from Order_  as o, Cashier as c where o.CashierID = c.CashierID;", con.connect);
            adapt.Fill(dt);
            DataGridViewOrder.DataSource = dt;
            con.connect.Close();

        }

        private string get_totalPrice()
        {
            #region get the Price 

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand(" SELECT SUM(ProdTOTAL) FROM ORDER_DETAILS;", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);



            if (dtable.Rows.Count > 0)
            {
                var index_Product = dtable.Rows[0][0];
                //MessageBox.Show(index_Product.ToString());
                return index_Product.ToString();

            }
            return " ";
            #endregion

        }

        private void insert_Order()
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }


            DateTime currentDate = DateTime.Now;
            string strtotal = get_totalPrice();
            //MessageBox.Show(strtotal.ToString());

            //var totalPrice = int.Parse(strtotal);


            string query = "Insert into Order_(OrderDate, TotalPrice, CashierID)values(@OrderDate, @TotalPrice, @CashierID)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@OrderDate", currentDate.ToString().Trim());
                    cmd.Parameters.AddWithValue("@TotalPrice", strtotal.Trim());
                    cmd.Parameters.AddWithValue("@CashierID", cashier_id.ToString().Trim());


                    cmd.ExecuteNonQuery();
                }
                
            }
            catch
            {
                throw;
            }
        }
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            insert_Order();
            DisplayOrder();
            clear_data();
            DisplayDataDetails();
        }
    }
}
