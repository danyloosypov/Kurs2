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
    public partial class EditPlane : Form
    {
        public SqlConnection sqlconn;

        public EditPlane(SqlConnection sqlconn, int aircraft_id, string type, int seats)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            textBox1.Text = aircraft_id.ToString();
            comboBox1.Text = type;
            textBox2.Text = seats.ToString();
        }

        private void EditPlane_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlExpression = $"UPDATE Aircraft SET aircraft_type = '{comboBox1.SelectedItem}' " +
                $"WHERE aircraft_id = '{textBox1.Text}'";

            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            command.ExecuteScalar();
            const string message = "Редагування пройшло успішно";
            const string caption = "Log In";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Embraer170")
                textBox2.Text = "78";

            if (comboBox1.SelectedItem.ToString() == "AirbusA310")
                textBox2.Text = "220";

            if (comboBox1.SelectedItem.ToString() == "AirbusA319")
                textBox2.Text = "156";

            if (comboBox1.SelectedItem.ToString() == "AirbusA320")
                textBox2.Text = "150";
        }
    }
}
