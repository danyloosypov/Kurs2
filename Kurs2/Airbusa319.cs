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
    public partial class Airbusa319 : Form
    {
        public SqlConnection sqlconn;
        public int orderID;
        public int flightID;
        public int aircraft_id = 6;
        private bool isselected;

        public Airbusa319(SqlConnection sqlconn, int orderID, int flightID)
        {
            InitializeComponent();

            this.sqlconn = sqlconn;
            this.orderID = orderID;
            this.flightID = flightID;
            isselected = false;

            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType() == typeof(Panel))
                {
                    ((Panel)ctl).Click += SeatClick;
                }
            }

            String sqlExpression2 = "Select flight.flight_id, flight.company, direction.fromcity, " +
                "direction.tocity, flight.departure_date, flight.departure_time, flight.arrival_date, flight.arrival_time, flight.flight_cost" +
                " from Flight left outer join Direction on flight.direction_id = direction.direction_id where flight_id = " + flightID;
            SqlCommand command2 = new SqlCommand(sqlExpression2, sqlconn);

            SqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            string result = "Номер рейсу " + reader2.GetValue(0).ToString() + " Компанія " + reader2.GetValue(1).ToString() + " Звідки " + reader2.GetValue(2).ToString() +
                " Куди " + reader2.GetValue(3).ToString() + " Дата відправлення " + reader2.GetValue(4).ToString().Substring(0, 10) + " Час відправлення " + reader2.GetValue(5).ToString()
                + " Дата прибуття  " + reader2.GetValue(6).ToString().Substring(0, 10) + " Час прибуття " + reader2.GetValue(7).ToString() + " Вартість " + reader2.GetValue(8).ToString();
            label2.Text = result;
            reader2.Close();

            Seats seat = new Seats(sqlconn);
            List<string> taken_list = seat.getTakenSeats(flightID);
            for (int i = 0; i < taken_list.Count; i++)
            {
                Control control = FindControl(this, taken_list[i].ToLower());
                if (control != null)
                {
                    ((Panel)control).BackColor = Color.Red;
                }
            }
        }

        private Control FindControl(Control parent, string name)
        {
            // Check the parent.
            if (parent.Name == name) return parent;

            // Recursively search the parent's children.
            foreach (Control ctl in parent.Controls)
            {
                Control found = FindControl(ctl, name);
                if (found != null) return found;
            }

            // If we still haven't found it, it's not here.
            return null;
        }

        private void SeatClick(object sender, EventArgs e)
        {
            Control str = (Control)sender;

            if (((Panel)str).BackColor == Color.Red)
            {
                MessageBox.Show("Это место уже занято!");
                return;
            }

            //MessageBox.Show(str.Name);
            string place = str.Name.Substring(0, 1);
            string row = str.Name.Substring(1);

            //MessageBox.Show(place + " / " + row);


            String sqlExpression1 = "Select flight_cost from Flight where flight_id = " + flightID;
            SqlCommand command = new SqlCommand(sqlExpression1, sqlconn);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int cnt = Convert.ToInt32(reader.GetValue(0));
            reader.Close();

            String sqlExpression = "insert into Ticket (order_id, flight_id, Cost, seat_id) " +
                                   $"values( {orderID}, {flightID}, {cnt}, " +
                                   "(select seat_id from seat " +
                                   "where [row] = " + row + " and place = '" + place.ToUpper() + $"' and aircraft_id = {aircraft_id}) )";
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
            cmd.ExecuteScalar();

            ((Panel)str).BackColor = Color.Red;
            MessageBox.Show("Место успешно забронировано!");
            isselected = true;
            this.Close();
        }


        private void Airbusa319_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isselected)
                this.Close();
            else
                MessageBox.Show("Ви не обрали місце");
        }
    }
}
