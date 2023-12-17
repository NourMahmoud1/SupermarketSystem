using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketSystem.DAL
{
    internal class Conn
    {
        public SqlConnection connect = new SqlConnection(@"Data Source=.;Initial Catalog=SupermarketSystem;Integrated Security=True");
        
    }
}
