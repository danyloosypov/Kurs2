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
    public partial class EditClient : Form
    {
        public SqlConnection sqlconn;
        public int userID;
        public EditClient(SqlConnection sqlconn, int userID)
        {
            InitializeComponent();

            this.sqlconn = sqlconn;
            this.userID = userID;

            String sqlExpression = $"select * from Client where client_id = {userID}";
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader.GetValue(1).ToString();
                textBox2.Text = reader.GetValue(2).ToString();
                textBox3.Text = reader.GetValue(3).ToString();
                comboBox1.SelectedItem = reader.GetValue(4).ToString();
                textBox4.Text = reader.GetValue(5).ToString();
                maskedTextBox1.Text = reader.GetValue(6).ToString();
                textBox5.Text = reader.GetValue(7).ToString();
                textBox6.Text = reader.GetValue(8).ToString();
            }
            reader.Close();
        }

        private void EditClient_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && comboBox1.SelectedItem.ToString() != ""
                && textBox4.Text.Trim() != "" && maskedTextBox1.Text != "" && textBox5.Text.Trim() != "" && textBox6.Text.Trim() != "")
            {
                string sqlExpression = $"select count(*) as cnt from client where client_id <> {userID} and UPPER(surname) = '" +
                textBox1.Text.Trim().ToUpper() + "'" +
                " and UPPER(name) ='" + textBox2.Text.Trim().ToUpper() + "'" +
                " and UPPER(Middle_name) ='" + textBox3.Text.Trim().ToUpper() + "'" +
                " and sex ='" + comboBox1.SelectedItem.ToString() + "'" +
                " and UPPER(passport) ='" + textBox4.Text.Trim().ToUpper() + "'" +
                " and phone ='" + maskedTextBox1.Text + "'" +
                " and email ='" + textBox5.Text.Trim() + "'" +
                " and password ='" + textBox6.Text.Trim() + "'";
                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int cnt = Convert.ToInt32(reader.GetValue(0).ToString());
                reader.Close();

                if (cnt != 0)
                {
                    const string message = "Такий клієнт вже існує";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    sqlExpression = "UPDATE client SET " +
                                     "surname ='" + textBox1.Text.Trim() + "'," +
                                     "name = '" + textBox2.Text.Trim() + "'," +
                                      "middle_name = '" + textBox3.Text.Trim() + "'," +
                                      "sex = '" + comboBox1.SelectedItem.ToString() + "'," +
                                      "passport = '" + textBox4.Text.Trim() + "'," +
                                      "phone = '" + maskedTextBox1.Text + "'," +
                                      "email = '" + textBox5.Text.Trim() + "', " +
                                      "password = '" + textBox6.Text.Trim() + "' where client_id = " + userID;
                    command = new SqlCommand(sqlExpression, sqlconn);
                    command.ExecuteScalar();

                    const string message = "Клієнт редаговано успішно";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

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
                        m.Body = $"<h2>ID: {userID}" +
                            $" Password: {textBox6.Text}</h2>";
                        // письмо представляет код html
                        m.IsBodyHtml = true;
                        // адрес smtp-сервера и порт, с которого будем отправлять письмо
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        // логин и пароль
                        smtp.Credentials = new NetworkCredential("benderfarm123@gmail.com", "BenderFarm321-");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        /*Schedule form = new Schedule(sqlconn, userID);
                        this.Hide();
                        form.ShowDialog();*/
                        this.Close();
                    }
                    catch
                    {
                        const string message1 = "Помилка введення даних";
                        const string caption1 = "Log In";
                        var result1 = MessageBox.Show(message1, caption1,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Exclamation);
                    }

                }
            } else
            {
                const string message = "Заповніть данні";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
            }
                
        }
    }
}
