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
    public partial class OrderInfo : Form
    {
        public SqlConnection sqlconn;
        public int orderID;
        public OrderInfo(SqlConnection sqlconn, int orderID)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            this.orderID = orderID;

            String sqlExpression = $"select * from [Order] where order_id = {orderID}";
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            
            textBox2.Text = reader.GetValue(1).ToString();
            textBox3.Text = reader.GetValue(2).ToString();

            reader.Close();

            textBox1.Text = orderID.ToString();
            FillTable();
        }

        public void FillTable()
        {
            SqlDataAdapter oda = new SqlDataAdapter($"select Ticket.flight_id, Ticket.Cost, Seat.Place, Seat.Row, Seat.Class " +
                $"from [Ticket] left outer join seat on ticket.seat_id = seat.seat_id where order_id = {orderID}", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void OrderInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
