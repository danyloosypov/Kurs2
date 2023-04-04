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

    public partial class LoginForm : Form
    {
        public const string ConnectionString = @"Data Source=DESKTOP-7HEN73U\SQLEXPRESS;
        Initial Catalog=kurs2;Persist Security Info=True;User ID=sa; Password=hansol";
        public SqlConnection sqlconn;
        public LoginForm()
        {
            InitializeComponent();

            try
            {
                sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SignInForm form = new SignInForm(sqlconn);
            this.Hide();
            form.ShowDialog();
            //this.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UserEnter form = new UserEnter(sqlconn);
            this.Hide();
            form.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
