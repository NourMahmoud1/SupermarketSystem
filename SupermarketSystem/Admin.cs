using Guna.UI2.WinForms;
using SupermarketSystem.BLL;
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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

       

        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'supermarketSystemDataSet1.Supplier' table. You can move, or remove it, as needed.
            this.supplierTableAdapter.Fill(this.supermarketSystemDataSet1.Supplier);
            // TODO: This line of code loads data into the 'supermarketSystemDataSet1.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter1.Fill(this.supermarketSystemDataSet1.Product);
            // TODO: This line of code loads data into the 'supermarketSystemDataSet1.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.supermarketSystemDataSet1.Category);
            // TODO: This line of code loads data into the 'supermarketSystemDataSet1.Cashier' table. You can move, or remove it, as needed.
            this.cashierTableAdapter.Fill(this.supermarketSystemDataSet1.Cashier);
            LoadDataProduct();
        }

        Conn con = new Conn();
        private void LoadDataProduct()
        {
            //Conn conn = new Conn();
            con.connect.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select p.ProdID, p.ProdName , p.ProdQty , p.ProdPrice, c.CategoryName , s.SupplierName From Product as p ,Supplier as s ,Category as c, Supplier_Product as sp where p.ProdID = sp.ProdID and p.CategoryID = c.CategoryID and s.SupplierID = sp.SupplierID ", con.connect);

            //SqlDataAdapter adapt = new SqlDataAdapter("select CashierID, CName, CPass, CAge, CPhone from Cashier", con.connect);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
           // guna2DataGridView2.DataSource = ds.Tables[0];
            con.connect.Close();
        }

        private void DisplayDataSeller()
        {
            con.connect.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select CashierID, CName, CPass, CAge, CPhone from Cashier", con.connect);
            adapt.Fill(dt);
            DataGridViewSeller.DataSource = dt;
            con.connect.Close();

        }

        private void DisplayDataCategory()
        {
            con.connect.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select CategoryID, CategoryName, categoryDesc from Category", con.connect);
            adapt.Fill(dt);
            guna2DataGridView3.DataSource = dt;
            con.connect.Close();

        }

        private void DisplayDataSupplier()
        {
            con.connect.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Supplier", con.connect);
            adapt.Fill(dt);
            DataGridViewSupplier.DataSource = dt;
            con.connect.Close();

        }

        private void DisplayDataProduct()
        {
            con.connect.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT p.ProdID, p.ProdName, p.ProdQty, p.ProdPrice, c.CategoryName FROM  Product AS p INNER JOIN     Category AS c ON p.CategoryID = c.CategoryID", con.connect);
            adapt.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            con.connect.Close();

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtNameProduct.Text != "" && txtQTYProduct.Text != "" && txtPriceProduct.Text != "" && comboCategoryProduct.Text != "" && comboBoxSupplier.Text != "")
            {

                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string query = "Update Product set ProdName='" + txtNameProduct.Text + "', ProdQty= '" + txtQTYProduct.Text + "', ProdPrice= '" + txtPriceProduct.Text + "' where ProdID = '" + txtIDProduct.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con.connect);
                cmd.ExecuteNonQuery();
                con.connect.Close();
                Message message = new Message("You Update a Product Information", Message.AlertType.success);
                message.Show();

                LoadDataProduct();
                txtNameProduct.Clear();
                txtIDProduct.Clear();
                txtPriceProduct.Clear();
                txtQTYProduct.Clear();

            }
            else
            {
                MessageBox.Show("Falid");

            }

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Product_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtNameProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtQTYProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtPriceProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboCategoryProduct.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBoxSupplier.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (txtIDProduct.Text != "")
            {


                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string queryy = "Delete Product where ProdID = '" + txtIDProduct.Text + "'";
                SqlCommand cmsd = new SqlCommand(queryy, con.connect);

                cmsd.ExecuteNonQuery();
                Message message = new Message("Product Deleted Successfully", Message.AlertType.success);
                message.Show();
                con.connect.Close();
                txtIDProduct.Clear();

                LoadDataProduct();

            }
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {


            string cat = comboCategoryProduct.Text;
            string supplier = comboBoxSupplier.Text;
            var index_category = 0;
            var index_supplier = 0;
            var index_product = 0;
            #region get the id for Category 

            Conn co = new Conn();
            co.connect.Open();
            //for get the id of category
            SqlCommand cmdd = new SqlCommand("SELECT CategoryID FROM Category WHERE CategoryName = '" + cat + "' ", co.connect);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);


            DataTable dtable = new DataTable();
            da.Fill(dtable);

            

            if (dtable.Rows.Count > 0 )
            {
                index_category = (int)dtable.Rows[0][0];
               //MessageBox.Show(index.ToString());
                
            }

            #endregion


            #region get the id for Supplier 

           
            SqlCommand cmd = new SqlCommand("SELECT SupplierID FROM Supplier WHERE SupplierName = '" + supplier + "' ", co.connect);
            SqlDataAdapter data = new SqlDataAdapter(cmd);


            DataTable datatable = new DataTable();
            data.Fill(datatable);

            if (datatable.Rows.Count > 0)
            {
                index_supplier = (int)datatable.Rows[0][0];
                //MessageBox.Show(index.ToString());

            }
            #endregion




            if (txtNameProduct.Text != "" && txtQTYProduct.Text != "" && txtPriceProduct.Text != "" && comboCategoryProduct.Text != "" && comboBoxSupplier.Text != "")
            {
                SaveData objbll = new SaveData();
                if (objbll.saveItemsProduct(txtNameProduct.Text, txtPriceProduct.Text, txtQTYProduct.Text, index_category))
                {
                    
                    
                   #region get the product id the insert the supplierID, ProductID into supplier_Product table

                    string querryProduct = "select ProdID from Product order by ProdID desc";
                    SqlCommand command = new SqlCommand(querryProduct, co.connect);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataProduct = new DataTable();
                    dataAdapter.Fill(dataProduct);

                    if (dtable.Rows.Count > 0 && dtable.Rows.Count > 0)
                    {
                        index_product = (int)dataProduct.Rows[0][0];

                        string querry = "insert into Supplier_Product(ProdID,SupplierID) values('" + index_product.ToString() + "','" + index_supplier.ToString() + "')";

                        SqlDataAdapter sda = new SqlDataAdapter(querry, co.connect);
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                    }

                        #endregion
                    
                    Message message = new Message("You Added a Product", Message.AlertType.success);
                    message.Show();

                    //DisplayDataProduct();
                    LoadDataProduct();
                    txtNameProduct.Clear();
                    txtIDProduct.Clear();
                    txtPriceProduct.Clear();
                    txtQTYProduct.Clear();
                }
                else
                {
                    MessageBox.Show("Falid");
                }
            }
            else
            {
                Message message = new Message("Please Fill All Requared Data", Message.AlertType.wrong);
                message.Show();
            }

        }



        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Categorie_Click(object sender, EventArgs e)
        {

        }

        private void Sellers_Click(object sender, EventArgs e)
        {

        }

        private void Logout_Click(object sender, EventArgs e)
        {

        }

        private void pnlTopSide_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (txtNameCategory.Text != ""  && txtDescriptionCategory.Text != "" )
            {
                SaveData objbll = new SaveData();
                if (objbll.saveItemsCategory(txtNameCategory.Text, txtDescriptionCategory.Text))
                {
                    Message message = new Message("You Added a Category", Message.AlertType.success);
                    message.Show();

                    DisplayDataCategory();
                    txtNameCategory.Clear();
                    txtIDCategory.Clear();
                    txtDescriptionCategory.Clear();
                }
                else
                {
                    MessageBox.Show("Falid");
                }
            }
            else
            {
                Message message = new Message("Please Fill All Requared Data", Message.AlertType.wrong);
                message.Show();
            }
        }
         
        
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (txtNameSellers.Text != "" && txtAge.Text != ""&& txtPassword.Text != ""&& txtPhone.Text != "")
            {
                SaveData objbll = new SaveData();
                if (objbll.saveItemsSeller(txtNameSellers.Text, txtAge.Text, txtPassword.Text, txtPhone.Text))
                {
                    Message message = new Message("You Added a Seller", Message.AlertType.success);
                    message.Show();

                    DisplayDataSeller();
                    txtNameSellers.Clear();
                    txtIDSellers.Clear();
                    txtAge.Clear();
                    txtPassword.Clear();
                    txtPhone.Clear();
                }
                else
                {
                    MessageBox.Show("Falid");

                }
            }
            else
            {
                Message message = new Message("Please Fill All Requared Data",Message.AlertType.wrong);
                message.Show();
            }
        }

        private void DataGridViewSeller_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDSellers.Text = DataGridViewSeller.SelectedRows[0].Cells[0].Value.ToString();
            txtNameSellers.Text = DataGridViewSeller.SelectedRows[0].Cells[1].Value.ToString();
            txtPhone.Text = DataGridViewSeller.SelectedRows[0].Cells[2].Value.ToString();
            txtPassword.Text = DataGridViewSeller.SelectedRows[0].Cells[3].Value.ToString();
            txtAge.Text = DataGridViewSeller.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtIDSellers.Text !="" &&txtNameSellers.Text != "" && txtAge.Text != "" && txtPassword.Text != "" && txtPhone.Text != "")
            {
                

                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string query = "Update Cashier set CName='" + txtNameSellers.Text + "', CPhone = '" + txtPhone.Text + "', CAge = '" +txtAge.Text+ "', CPass = '" + txtPassword.Text+ "'  where CashierID = '" + txtIDSellers.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con.connect);
                cmd.ExecuteNonQuery();
                con.connect.Close();
                Message message = new Message("You Update a Cashier Information", Message.AlertType.success);
                message.Show();

                DisplayDataSeller();
                txtNameSellers.Clear();
                txtIDSellers.Clear();
                txtAge.Clear();
                txtPassword.Clear();
                txtPhone.Clear();

            }
            else
            {
                MessageBox.Show("Falid");

            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (txtIDSellers.Text != "")
            {
               

                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string queryy = "Delete Cashier where CashierID = '" + txtIDSellers.Text + "'";
                SqlCommand cmsd = new SqlCommand(queryy, con.connect);

                cmsd.ExecuteNonQuery();
                Message message = new Message("Seller Deleted Successfully", Message.AlertType.success);
                message.Show();
                con.connect.Close();
                txtIDSellers.Clear();

                DisplayDataSeller();

            }

        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (txtIDCategory.Text != "" && txtNameCategory.Text != "" && txtDescriptionCategory.Text != "") 
            {

                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string query = "Update Category set CategoryName='" + txtNameCategory.Text + "', categoryDesc = '" + txtDescriptionCategory.Text + "' where CategoryID = '" + txtIDCategory.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con.connect);
                cmd.ExecuteNonQuery();
                con.connect.Close();
                Message message = new Message("You Update a Category Information", Message.AlertType.success);
                message.Show();

                DisplayDataCategory();
                txtNameCategory.Clear();
                txtIDCategory.Clear();
                txtDescriptionCategory.Clear();

            }
            else
            {
                MessageBox.Show("Falid");

            }
        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDCategory.Text = guna2DataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            txtNameCategory.Text = guna2DataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            txtDescriptionCategory.Text = guna2DataGridView3.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (txtIDCategory.Text != "")
            {


                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string queryy = "Delete Category where CategoryID = '" + txtIDCategory.Text + "'";
                SqlCommand cmsd = new SqlCommand(queryy, con.connect);

                cmsd.ExecuteNonQuery();
                Message message = new Message("Category Deleted Successfully", Message.AlertType.success);
                message.Show();
                con.connect.Close();
                txtIDCategory.Clear();

                DisplayDataCategory();

            }
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void comboCategoryProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            if (txtNameSupplier.Text != "" && txtPhoneSupplier.Text != "")
            {
                SaveData objbll = new SaveData();
                if (objbll.saveItemsSupplier(txtNameSupplier.Text, txtPhoneSupplier.Text))
                {
                    Message message = new Message("You Added a Supplier", Message.AlertType.success);
                    message.Show();

                    DisplayDataSupplier();
                    txtNameSupplier.Clear();
                    txtIDSupplier.Clear();
                    txtPhoneSupplier.Clear();
                }
                else
                {
                    MessageBox.Show("Falid");

                }
            }
            else
            {
                Message message = new Message("Please Fill All Requared Data", Message.AlertType.wrong);
                message.Show();
            }
        }

        private void DataGridViewSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDSupplier.Text = DataGridViewSupplier.SelectedRows[0].Cells[0].Value.ToString();
            txtNameSupplier.Text = DataGridViewSupplier.SelectedRows[0].Cells[1].Value.ToString();
            txtPhoneSupplier.Text = DataGridViewSupplier.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void btnEditSupplier_Click(object sender, EventArgs e)
        {
            if (txtIDSupplier.Text != "" && txtNameSupplier.Text != "" && txtPhoneSupplier.Text != "")
            {

                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string query = "Update Supplier set SupplierName='" + txtNameSupplier.Text + "', SupplierPhone = '" + txtPhoneSupplier.Text + "' where SupplierID = '" + txtIDSupplier.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con.connect);
                cmd.ExecuteNonQuery();
                con.connect.Close();
                Message message = new Message("You Update a Supplier Information", Message.AlertType.success);
                message.Show();

                DisplayDataSupplier();
                txtNameSupplier.Clear();
                txtIDSupplier.Clear();
                txtPhoneSupplier.Clear();

            }
            else
            {
                MessageBox.Show("Falid");

            }
        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            if (txtIDSupplier.Text != "")
            {


                if (ConnectionState.Closed == con.connect.State)
                {
                    con.connect.Open();
                }
                string queryy = "Delete Supplier where SupplierID = '" + txtIDSupplier.Text + "'";
                SqlCommand cmsd = new SqlCommand(queryy, con.connect);

                cmsd.ExecuteNonQuery();
                Message message = new Message("Supplier Deleted Successfully", Message.AlertType.success);
                message.Show();
                con.connect.Close();
                txtIDSupplier.Clear();

                DisplayDataSupplier();

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataProduct();

        }
    }
}
