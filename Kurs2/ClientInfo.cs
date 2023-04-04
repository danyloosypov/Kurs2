using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs2
{
    public partial class ClientInfo : Form
    {
        public SqlConnection sqlconn;

        public ClientInfo(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // MessageBox.Show("Invalid data type");
                errorProvider1.SetError(textBox2, "Invalid data type");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // MessageBox.Show("Invalid data type");
                errorProvider1.SetError(textBox3, "Invalid data type");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && comboBox1.SelectedItem.ToString() != "" 
                && textBox4.Text.Trim() != "" && maskedTextBox1.Text != "" && textBox5.Text.Trim() != "" && textBox6.Text.Trim() != "")
            {
                string sqlExpression = "INSERT INTO Client (Surname, Name, Middle_name, Sex, Passport, Phone, Email, Password)" +
                " VALUES ('" + textBox1.Text.Trim() + "', '" + textBox2.Text.Trim() + "', '" + textBox3.Text.Trim() + "', '" + comboBox1.SelectedItem + "', '" +
                 textBox4.Text.Trim() + "', '" + maskedTextBox1.Text + "', '" + textBox5.Text.Trim() + "', '" + textBox6.Text.Trim() + "')"
                  + "SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);


                int modified = (int)command.ExecuteScalar();

                try
                {
                    // отправитель - устанавливаем адрес и отображаемое в письме имя
                    MailAddress from = new MailAddress("danylo.osypov@nure.ua", "Admin");
                    // кому отправляем
                    MailAddress to = new MailAddress($"{textBox5.Text}");
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Ваш ID та пароль";
                    // текст письма
                    m.Body = $"<h2>ID: {modified}" +
                        $" Password: {textBox6.Text}</h2>";
                    // письмо представляет код html
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера и порт, с которого будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("benderfarm123@gmail.com", "BenderFarm321-");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    Schedule form = new Schedule(sqlconn, modified);
                    this.Hide();
                    form.ShowDialog();
                }
                catch
                {
                    const string message = "Помилка введення даних";
                    const string caption = "Log In";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Exclamation);
                }
            } else
            {
                const string message = "Помилка введення даних";
                const string caption = "Log In";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
            
        }

        private void ClientInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
