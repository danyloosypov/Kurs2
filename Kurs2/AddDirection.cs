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

namespace Kurs2
{
    public partial class AddDirection : Form
    {
        public SqlConnection sqlconn;
        public AddDirection(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void AddDirection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlExpression = "select count(*) as cnt from Direction where UPPER(fromcity) = '"+
                textBox1.Text.Trim().ToUpper()+"'"+
                " and UPPER(tocity) ='" + textBox2.Text.Trim().ToUpper() + "'";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read(); 
            int cnt = Convert.ToInt32(reader.GetValue(0).ToString());
            reader.Close();

            if (cnt == 0)
            {
                sqlExpression = "INSERT INTO Direction (FromCity, ToCity)" +
            " VALUES ('" + textBox1.Text.Trim() + "', '" + textBox2.Text.Trim() + "')"
                + "SELECT CAST(scope_identity() AS int)";

                
                command = new SqlCommand(sqlExpression, sqlconn);
                command.ExecuteScalar();

                const string message = "Напрям додано успішно";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
            }
            else
            {
                const string message = "Такий напрям вже існує";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
            }


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
