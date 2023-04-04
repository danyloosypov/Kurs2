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
    public partial class SignInForm : Form
    {
        public SqlConnection sqlconn;

        public SignInForm(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {
            label1.Text = "When everything seems\n" +
                "to be going against you,\n" +
                "remember that the airplane\n" +
                "takes off against the wind,\n" +
                "not with it. If you can walk\n" +
                "away from a landing,\n" +
                "it's a good landing.";
            passwordbox.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            this.Hide();
            form.ShowDialog();
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (loginbox.Text == "Admin" && passwordbox.Text == "admin")
            {
                this.Hide();
                Schedule form = new Schedule(sqlconn, 0);
                form.ShowDialog();
                this.Close();
            } 
            else
            {
                const string message =
                "Invalid login or password";
                const string caption = "Log In";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            passwordbox.PasswordChar = '\0';

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            passwordbox.PasswordChar = '*';
        }

        private void loginbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // MessageBox.Show("Invalid data type");
                errorProvider1.SetError(loginbox, "Invalid data type");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void passwordbox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void passwordbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
