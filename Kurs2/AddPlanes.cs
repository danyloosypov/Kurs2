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
    public partial class AddPlanes : Form
    {
        public SqlConnection sqlconn;
        public AddPlanes(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void AddPlanes_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                int seat_count = 0;
                if (comboBox1.SelectedItem.ToString() == "Embraer170")
                    seat_count = 78;

                if (comboBox1.SelectedItem.ToString() == "AirbusA310")
                    seat_count = 220;

                if (comboBox1.SelectedItem.ToString() == "AirbusA319")
                    seat_count = 156;

                if (comboBox1.SelectedItem.ToString() == "AirbusA320")
                    seat_count = 150;

                string sqlExpression = "INSERT INTO Aircraft (aircraft_type, seats_quantity)" +
                " VALUES ('" + comboBox1.SelectedItem.ToString() + "', '" + seat_count + "')"
                  + "SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                int modified = (int)command.ExecuteScalar();

                const string message = "Літак додано успішно";
                const string caption = "Log In";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);

            }
            else
            {
                const string message = "Оберіть тип літака";
                const string caption = "Log In";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);




            }
        }
    }
}
