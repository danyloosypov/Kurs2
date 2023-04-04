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
    public partial class UserEnter : Form
    {
        public SqlConnection sqlconn;

        public UserEnter(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            const string message =
                "Ваше ID було надіслано Вам на пошту при риєстрації";
            const string caption = "Log In";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Exclamation);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            ClientInfo form = new ClientInfo(sqlconn);
            form.ShowDialog();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // MessageBox.Show("Invalid data type");
                errorProvider1.SetError(textBox1, "Invalid data type");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void UserEnter_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter oda = new SqlDataAdapter($"select * from client where client_id = '{textBox1.Text}' " +
                $" and password = '{textBox2.Text}'", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                Schedule form = new Schedule(sqlconn, Convert.ToInt32(textBox1.Text));
                this.Hide();
                form.ShowDialog();
            } else
            {
                const string message =
               "Invalid ID";
                const string caption = "Log In";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }
    }
}
