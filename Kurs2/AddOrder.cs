using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs2
{
    public partial class AddOrder : Form
    {
        public SqlConnection sqlconn;
        public int userID;
        public int orderID;

        public AddOrder(SqlConnection sqlconn, int userID)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;

            //замовленняToolStripMenuItem.Visible = false;
            редагуватиToolStripMenuItem.Visible = false;
            видалитиToolStripMenuItem.Visible = false;

            пасажириToolStripMenuItem.Visible = false;
            додатиНапрямToolStripMenuItem.Visible = false;
            додатиЛітакToolStripMenuItem.Visible = false;
            додатиРейсToolStripMenuItem.Visible = false;
            редагуватиРейсToolStripMenuItem.Visible = false;
            видалитиРейсToolStripMenuItem.Visible = false;

            string sqlExpression = "INSERT INTO [Order] (client_id, payment_type)" +
                " VALUES ('" + userID + "', '" + null + "')"
                  + "SELECT CAST(scope_identity() AS int)";

            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            orderID = (int)command.ExecuteScalar();

            this.userID = userID;
        }

        public void FillTable()
        {

            SqlDataAdapter oda = new SqlDataAdapter("select ticket.ticket_id, ticket.order_id, ticket.seat_id, ticket.flight_id, " +
                "Ticket.Cost, Aircraft.aircraft_type, Seat.Row, Seat.Place, Seat.Class from[Ticket] left outer join Seat " +
                "on Ticket.seat_id = Seat.seat_id left outer join Aircraft on Aircraft.aircraft_id = Seat.aircraft_id " +
                "where order_id = " + orderID, sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
            int totalcost = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                totalcost = totalcost + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            textBox2.Text = totalcost.ToString();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddTicket form = new AddTicket(sqlconn, orderID);
            form.ShowDialog();
            FillTable();
        }

        

        private void перейтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Schedule form = new Schedule(sqlconn, userID);
            this.Hide();
            form.ShowDialog();
        }

        private void перейтиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Orders form = new Orders(sqlconn, userID);
            this.Hide();
            form.ShowDialog();
        }

        private void перейтиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Clients form = new Clients(sqlconn);
            this.Hide();
            form.ShowDialog();
        }

        private void допомогаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            /*Help form = new Help();
            form.ShowDialog();*/
            if (userID != 0)
            {
                Process.Start(@"ForUser.docx");
            }
            else
            {
                Process.Start(@"ForAdmin.docx");

            }
        }

        private void зробитиЗамовленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder form = new AddOrder(sqlconn, userID);
            this.Hide();
            form.ShowDialog();
        }

        private void перейтиToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Directions form = new Directions(sqlconn);
            this.Hide();
            form.ShowDialog();
        }

        private void перейтиToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Airplanes form = new Airplanes(sqlconn);
            this.Hide();
            form.ShowDialog();
        }

        private void системаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void змінітиПерсональніДаніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditClient form = new EditClient(sqlconn, userID);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    string sqlExpression = $"Delete from [Ticket] where ticket_id = {dataGridView1.Rows[i].Cells[0].Value}";

                    SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                    command.ExecuteScalar();
                }
            }
            FillTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteOrder();
            Schedule form = new Schedule(sqlconn, userID);
            this.Hide();
            form.ShowDialog();
        }

        public void PrintTickets()
        {
            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
            iText.Kernel.Pdf.PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
            

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                float[] colwidth = { 200, 500, 200 };
                Table header = new Table(colwidth, false);

                document.Add(header);

                Cell a = new Cell(1, 2)
                        .SetBackgroundColor(new DeviceRgb(50, 109, 168))
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontColor(ColorConstants.WHITE)
                        .SetBold()
                        .Add(new iText.Layout.Element.Paragraph("Made in UA"))
                        .SetFontSize(25)
                        .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                        .SetVerticalAlignment(VerticalAlignment.MIDDLE);
                header.AddCell(a);

                a = new Cell(1, 1)
                    .Add(img)
                    .SetBorder(iText.Layout.Borders.Border.NO_BORDER)
                    .SetBackgroundColor(new DeviceRgb(50, 109, 168))
                    .SetTextAlignment(TextAlignment.RIGHT);


                header.AddCell(a);

                header.UseAllAvailableWidth();
                document.Add(header);


                LineSeparator ls = new LineSeparator(new SolidLine());
                Paragraph emptyline = new Paragraph(" ");
                document.Add(emptyline);
                Paragraph headertext = new Paragraph("Квиток на рейс").SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

                document.Add(headertext);
                document.Add(emptyline);
                
                var flightid = new Paragraph($"Номер рейсу: {dataGridView1.Rows[i].Cells[3].Value}").SetFont(f).SetFontSize(20);
                var flightcost = new Paragraph($"Вартість квитка: {dataGridView1.Rows[i].Cells[4].Value}").SetFont(f).SetFontSize(20);
                var aircraftType = new Paragraph($"Літак: {dataGridView1.Rows[i].Cells[5].Value}").SetFont(f).SetFontSize(20);
                var Row = new Paragraph($"Ряд: {dataGridView1.Rows[i].Cells[6].Value}").SetFont(f).SetFontSize(20);
                var Place = new Paragraph($"Місце: {dataGridView1.Rows[i].Cells[7].Value}").SetFont(f).SetFontSize(20);
                var seatClass = new Paragraph($"Клас: {dataGridView1.Rows[i].Cells[8].Value}").SetFont(f).SetFontSize(20);

                document.Add(flightid);
                document.Add(flightcost);
                document.Add(aircraftType);
                document.Add(Row);
                document.Add(Place);
                document.Add(seatClass);

                ImageData imageData = ImageDataFactory.Create(@".\airplane.png");
                iText.Layout.Element.Image pdfImg = new iText.Layout.Element.Image(imageData);
                pdfImg.ScaleToFit(400, 200);

                document.Add(pdfImg);


                document.Add(new AreaBreak());

            }


            document.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && dataGridView1.Rows.Count > 0 && maskedTextBox1.MaskCompleted)
            {
                string sqlExpression = "UPDATE [Order] SET " +
                                  "payment_type = '" + comboBox1.SelectedItem.ToString() + "'";
                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                command.ExecuteScalar();

                const string message = "Замовлення прийнято";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);

                string sqlExpression1 = "select email from Client where client_id = " + userID;
                SqlCommand command1 = new SqlCommand(sqlExpression1, sqlconn);
                SqlDataReader reader1 = command1.ExecuteReader();
                reader1.Read();
                string email = reader1.GetValue(0).ToString();
                reader1.Close();

                PrintTickets();


                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("danylo.osypov@nure.ua", "Admin");
                // кому отправляем
                MailAddress to = new MailAddress($"{email}");
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Копії квитків та замовлення";
                // текст письма
                m.Body = $"<h2>Загальна вартість замовлення: {textBox2.Text}" +
                            $"<h2>Номер замовлення: {orderID}";
                m.Attachments.Add(new Attachment("a.pdf"));  //!!

                // письмо представляет код html
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("benderfarm123@gmail.com", "BenderFarm321-");
                smtp.EnableSsl = true;
                smtp.Send(m);

                Schedule form = new Schedule(sqlconn, userID);
                this.Hide();
                form.ShowDialog();

            } else
            {
                const string message = "Сплатіть замовлення";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            } else
            {
                errorProvider1.SetError(maskedTextBox1, "Invalid data");
            }
        }

        private void AddOrder_Leave(object sender, EventArgs e)
        {
            DeleteOrder();
        }

        private void AddOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteOrder();
        }

        private void DeleteOrder()
        {
            string sqlExpression = $"Delete from [Order] where order_id = {orderID}";

            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            try
            {
                command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Виникла помилка");
            }
        }
    }
}
