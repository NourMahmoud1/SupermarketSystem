using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSystem.DAL
{
    internal class ClassDAL
    {

        public bool AddItemsToTableSeller(string name, string age, string pass, string phone)
        {
            Conn con = new Conn();
           
            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            string query = "Insert into Cashier(CName, CAge, CPass, CPhone)values(@CName, @CAge, @CPass, @CPhone)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@CName", name.Trim());
                    cmd.Parameters.AddWithValue("@CAge", age.Trim());
                    cmd.Parameters.AddWithValue("@CPass", pass.Trim());
                    cmd.Parameters.AddWithValue("@CPhone", phone.Trim());

                    /* MemoryStream ms = new MemoryStream();
                     img.Save(ms, img.RawFormat);
                     cmd.Parameters.AddWithValue("@Image", ms.ToArray());*/
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                throw;
            }

        }


        public bool AddItemsToTableCategory(string name, string desc)
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            string query = "Insert into Category(CategoryName, categoryDesc)values(@CategoryName, @categoryDesc)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", name.Trim());
                    cmd.Parameters.AddWithValue("@categoryDesc", desc.Trim());
                    
                    
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                throw;
            }

        }


        public bool AddItemsToTableSupplier(string name, string phone)
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            string query = "Insert into Supplier(SupplierName, SupplierPhone)values(@SupplierName, @SupplierPhone)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", name.Trim());
                    cmd.Parameters.AddWithValue("@SupplierPhone", phone.Trim());


                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                throw;
            }

        }

        public bool AddItemsToTableProduct(string name, string price, string qty, string index)
        {
            Conn con = new Conn();

            if (ConnectionState.Closed == con.connect.State)
            {
                con.connect.Open();
            }

            string query = "Insert into Product(ProdName, ProdQty, ProdPrice, CategoryID)values(@ProdName, @ProdQty, @ProdPrice, @CategoryID)";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con.connect))
                {
                    cmd.Parameters.AddWithValue("@ProdName", name.Trim());
                    cmd.Parameters.AddWithValue("@ProdQty", qty.Trim());
                    cmd.Parameters.AddWithValue("@ProdPrice", price.Trim());
                    cmd.Parameters.AddWithValue("@CategoryID", index.Trim());


                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                throw;
            }

        }
    }
}
