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
    public partial class AddTicket : Form
    {
        public SqlConnection sqlconn;
        public int orderID;
        public AddTicket(SqlConnection sqlconn, int orderID)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            this.orderID = orderID;

            FillTable();
        }

        public void FillTable()
        {
            SqlDataAdapter oda = new SqlDataAdapter($"exec podbor_reysov '{textBox3.Text.ToUpper()}', " +
                $"'{textBox4.Text.ToUpper()}', '{DateTime.Now.ToString("yyyyMMdd")}'", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void AddTicket_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                /*string where_f = "";
                string where_c = "";
                if (textBox1.Text != "")
                    where_f = $"flight_id = '{textBox1.Text}' ";
                if (textBox1.Text != "")
                    where_c = $"company = '{textBox2.Text}' ";

                SqlDataAdapter oda = new SqlDataAdapter($"select * from Flight where flight_id = '{textBox1.Text}' and aircraft_type = '{comboBox1.SelectedItem}'", sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;*/

                SqlDataAdapter oda = new SqlDataAdapter($"exec podbor_reysov '{textBox3.Text.ToUpper()}', '{textBox4.Text.ToUpper()}', '{dateTimePicker1.Value.ToString("yyyyMMdd")}'", sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt; 
                 
            }
            catch
            {
                const string message = "Нічого не було знайдено";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            textBox3.Text = "";
            textBox4.Text = "";
            FillTable();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    Form form = null;


                    // First airplane
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "2")
                    {
                        form = new Embraer170(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value));
                        
                    } else if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "5")
                    {
                        form = new Airbus310(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value));
                    }
                    else if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "6")
                    {
                        form = new Airbusa319(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value));
                    }
                    else if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "7")
                    {
                        form = new Airbus320(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value));
                    }

                    form.ShowDialog();

                    // Second airplane
                    if (dataGridView1.Rows[i].Cells[14].Value.ToString() != "0")
                    {
                        if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "2")
                        {
                            form = new Embraer170(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[14].Value));

                        }
                        else if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "5")
                        {
                            form = new Airbus310(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[14].Value));
                        }
                        else if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "6")
                        {
                            form = new Airbusa319(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[14].Value));
                        }
                        else if (dataGridView1.Rows[i].Cells[13].Value.ToString() == "7")
                        {
                            form = new Airbus320(sqlconn, orderID, Convert.ToInt32(dataGridView1.Rows[i].Cells[14].Value));
                        }

                        form.ShowDialog();
                    }
                }
            }
            this.Close();
        }

        
    }
}
