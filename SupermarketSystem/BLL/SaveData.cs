using SupermarketSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermarketSystem.BLL
{
    internal class SaveData
    {

        public bool saveItemsSeller( string name , string age,string pass, string phone)
        {
            try
            {
                ClassDAL objdal = new ClassDAL();
                return objdal.AddItemsToTableSeller(name , age , pass , phone);
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return false;
            }
        }

        public bool saveItemsCategory(string name, string desc)
        {
            try
            {
                ClassDAL objdal = new ClassDAL();
                return objdal.AddItemsToTableCategory(name, desc);
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return false;
            }
        }

        public bool saveItemsSupplier(string name, string phone)
        {
            try
            {
                ClassDAL objdal = new ClassDAL();
                return objdal.AddItemsToTableSupplier(name, phone);
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return false;
            }
        }

        public bool saveItemsProduct(string name, string price, string qty , int index)
        {
            try
            {
                ClassDAL objdal = new ClassDAL();
                return objdal.AddItemsToTableProduct(name, price, qty, index.ToString());
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return false;
            }
        }
    }
}
