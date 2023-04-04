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
    public partial class EditDirection : Form
    {
        public SqlConnection sqlconn;

        public EditDirection(SqlConnection sqlconn, int direction_id, string from, string to)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            textBox1.Text = direction_id.ToString();
            textBox2.Text = from;
            textBox3.Text = to;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sqlExpression1 = "select count(*) as cnt from Direction where UPPER(fromcity) = '" +
                textBox2.Text.Trim().ToUpper() + "'" +
                " and UPPER(tocity) ='" + textBox3.Text.Trim().ToUpper() + "'";
            SqlCommand command1 = new SqlCommand(sqlExpression1, sqlconn);
            SqlDataReader reader1 = command1.ExecuteReader();
            reader1.Read();
            int cnt = Convert.ToInt32(reader1.GetValue(0).ToString());
            reader1.Close();

            if (cnt == 0)
            {
                string sqlExpression = $"UPDATE Direction SET fromcity = '{textBox2.Text}', tocity = '{textBox3.Text}' " +
                $"WHERE direction_id = '{Convert.ToInt32(textBox1.Text)}'";

                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                command.ExecuteScalar();
                const string message = "Редагування пройшло успішно";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                this.Close();
            } else
            {
                const string message = "Такий напрям вже існує";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }

            
        }

        private void EditDirection_Load(object sender, EventArgs e)
        {

        }
    }
}
