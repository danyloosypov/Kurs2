using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurs2
{
    class Seats
    {
        public SqlConnection sqlconn;
        public Seats(SqlConnection sqlconn)
        {
            this.sqlconn = sqlconn;
        }
        public List<string> getTakenSeats(int flight_id)
        {
            List<string> taken_list = new List<string>();
            String sqlExpression = "select s.*  from ticket t " +
                                   "left outer join Seat s on s.seat_id = t.seat_id "+
                                   "where t.flight_id = " + flight_id;
            SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                taken_list.Add(reader.GetValue(3).ToString() + reader.GetValue(2).ToString());
            }
            reader.Close();

            return taken_list;
        }
    }
}
