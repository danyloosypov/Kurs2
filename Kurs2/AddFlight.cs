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
    struct ComboItem
    {
        public int idx;
        public string name;
        public override string ToString()
        {
            return name;
        }
    }
    public partial class AddFlight : Form
    {
        public SqlConnection sqlconn;
        private int idx;
        public AddFlight(SqlConnection sqlconn, int idx)
        {
            InitializeComponent();

            this.sqlconn = sqlconn;
            this.idx = idx;
            String sqlExpression = "select * from Direction";
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ComboItem item = new ComboItem();
                item.idx = Convert.ToInt32(reader.GetValue(0));
                item.name = reader.GetValue(1).ToString() + " - " + reader.GetValue(2).ToString();
                comboBox2.Items.Add(item);
            }
            reader.Close();
            comboBox2.SelectedIndex = 0;
            //----------------------------
            sqlExpression = "select * from Aircraft";
            cmd = new SqlCommand(sqlExpression, sqlconn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ComboItem item = new ComboItem();
                item.idx = Convert.ToInt32(reader.GetValue(0));
                item.name = reader.GetValue(1).ToString();
                comboBox1.Items.Add(item);
            }
            reader.Close();
            comboBox1.SelectedIndex = 0;
            //----------------------------
            if (idx > 0)
            {
                sqlExpression = $"select * from Flight where flight_id = {idx}";
                cmd = new SqlCommand(sqlExpression, sqlconn);
                reader = cmd.ExecuteReader();
                reader.Read();
                var fitem = findByIdx(comboBox1, Convert.ToInt32(reader.GetValue(1)));
                comboBox1.SelectedItem = comboBox1.Items.IndexOf(fitem);
                textBox1.Text = reader.GetValue(2).ToString();
                fitem = findByIdx(comboBox2, Convert.ToInt32(reader.GetValue(3)));
                comboBox2.SelectedItem = comboBox1.Items.IndexOf(fitem);

                dateTimePicker1.Value = Convert.ToDateTime(reader.GetValue(4));
                var t1 = DateTime.ParseExact(reader.GetValue(5).ToString(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None);
                var t2 = DateTime.ParseExact(reader.GetValue(7).ToString(), "HH:mm:ss", null, System.Globalization.DateTimeStyles.None);
                dateTimePicker2.Value = Convert.ToDateTime(t1);
                dateTimePicker4.Value = Convert.ToDateTime(reader.GetValue(6));
                dateTimePicker3.Value = Convert.ToDateTime(t2);
                textBox2.Text = reader.GetValue(8).ToString();


                reader.Close();
            }
            else
            {
                comboBox1.SelectedIndex = 0;
                textBox1.Text = "";
                comboBox2.SelectedIndex = 0;
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker4.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
                textBox2.Text = "0";
            }
        }

        private ComboItem findByIdx(ComboBox combo, int idx)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                var comboItem = (ComboItem)combo.Items[i];
                if (comboItem.idx == idx)
                    return comboItem;
            }
            return new ComboItem(); ;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AddFlight_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var comboItem = (ComboItem)comboBox2.SelectedItem;
            var comboItem1 = (ComboItem)comboBox1.SelectedItem;

            string sqlExpression = $"select count(*) as cnt from flight where flight_id <> {idx} and UPPER(aircraft_id) = '" +
                comboBox1.Text.Trim().ToUpper() + "'" +
                " and UPPER(Company) ='" + textBox1.Text.Trim().ToUpper() + "'" +
                " and direction_id ='" + comboItem.idx + "'" +
                " and departure_date ='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'" +
                " and departure_time ='" + dateTimePicker2.Value.ToString("HH:mm") + "'" +
                " and arrival_date ='" + dateTimePicker4.Value.ToString("yyyyMMdd") + "'" +
                " and arrival_time ='" + dateTimePicker3.Value.ToString("HH:mm") + "'";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int cnt = Convert.ToInt32(reader.GetValue(0).ToString());
            reader.Close();
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            textBox2.Text = textBox2.Text.Replace(",", ".");
            if (cnt != 0)
            {
                const string message = "Такий рейс вже існує";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (idx != 0)
                {
                    sqlExpression = "UPDATE flight SET " +
                                 "aircraft_id =" + comboItem1.idx + "," +
                                 "company = '" + textBox1.Text.Trim() + "'," +
                                  "direction_id = " + comboItem.idx + "," +
                                  "departure_date = '" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'," +
                                  "departure_time = '" + dateTimePicker2.Value.ToString("HH:mm") + "'," +
                                  "arrival_date = '" + dateTimePicker4.Value.ToString("yyyyMMdd") + "'," +
                                  "arrival_time = '" + dateTimePicker3.Value.ToString("HH:mm") + "', " +
                                  "flight_cost = '" + textBox2.Text.Trim() + "'" +
                                  " where flight_id = " + idx ;
                    command = new SqlCommand(sqlExpression, sqlconn);
                    command.ExecuteScalar();

                    const string message = "Рейс редаговано успішно";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                }
                else
                {
                    sqlExpression = "INSERT INTO flight (aircraft_id, company, direction_id, departure_date, departure_time, arrival_date, arrival_time, flight_cost)" +
                     " VALUES (" +
                         comboItem1.idx + "," +
                         "'" + textBox1.Text.Trim() + "'," +
                          comboItem.idx + "," +
                          "'" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'," +
                          "'" + dateTimePicker2.Value.ToString("HH:mm") + "'," +
                          "'" + dateTimePicker4.Value.ToString("yyyyMMdd") + "'," +
                          "'" + dateTimePicker3.Value.ToString("HH:mm") + "'," +
                          "'" + textBox2.Text.Trim() + "')";


                    command = new SqlCommand(sqlExpression, sqlconn);
                    command.ExecuteScalar();

                    const string message = "Рейс додано успішно";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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
    }
}
