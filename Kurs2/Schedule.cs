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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs2
{
    
    public partial class Schedule : Form
    {
        //public const string ConnectionString = @"Data Source=DESKTOP-7HEN73U\SQLEXPRESS;
        //Initial Catalog=kurs2;Persist Security Info=True;User ID=sa; Password=hansol";
        public SqlConnection sqlconn;
        public int userID;
        public Schedule(SqlConnection sqlconn, int userID)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            this.userID = userID;

            if (userID != 0)
            {
                //замовленняToolStripMenuItem.Visible = false;
                редагуватиToolStripMenuItem.Visible = false;
                видалитиToolStripMenuItem.Visible = false;

                пасажириToolStripMenuItem.Visible = false;
                додатиНапрямToolStripMenuItem.Visible = false;
                додатиЛітакToolStripMenuItem.Visible = false;
                додатиРейсToolStripMenuItem.Visible = false;
                редагуватиРейсToolStripMenuItem.Visible = false;
                видалитиРейсToolStripMenuItem.Visible = false;
            } else
            {
                зробитиЗамовленняToolStripMenuItem.Visible = false;
                змінитиПерсональніДаніToolStripMenuItem.Visible = false;
                редагуватиЛітакToolStripMenuItem.Visible = false;
                видалитиЛітакToolStripMenuItem.Visible = false;
                додатиЛітакToolStripMenuItem1.Visible = false;
            }

            FillTable();
        }
        public void FillTable()
        {
            SqlDataAdapter oda = new SqlDataAdapter("select Flight.flight_id, Flight.aircraft_id, Aircraft.aircraft_type as 'Тип літака', " +
                "Flight.company as 'Компанія', Flight.direction_id, Direction.FromCity as 'Звідки', Direction.ToCity as 'Куди', Flight.departure_date as 'Дата відправлення', Flight.Departure_time as 'Час відправлення', " +
                "Flight.arrival_date as 'Дата прибуття', Flight.arrival_time as 'Час прибуття', Flight.flight_cost as 'Вартість' from Flight left join Direction on Flight.direction_id = Direction.direction_id " +
                $"left join Aircraft on Aircraft.aircraft_id = Flight.aircraft_id", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Номер рейсу";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[4].Visible = false;

        }
        private void додатиНапрямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void Schedule_Load(object sender, EventArgs e)
        {

        }

        private void допомогаToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void зробитиЗамовленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder form = new AddOrder(sqlconn, userID);
            this.Hide();
            form.ShowDialog();
        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            this.Hide();
            form.ShowDialog();
        }

        private void змінитиПерсональніДаніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditClient form = new EditClient(sqlconn, userID);
            form.ShowDialog();
        }

        private void додатиЛітакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPlanes form = new AddPlanes(sqlconn);
            form.ShowDialog();
        }

        private void роздрукуватиУсіToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Літаки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиОбраніToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Літаки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиЛітакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Літаки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиЛітакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Літаки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиУсіToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Напрямки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиОбраніToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Напрямки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиНапрямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Напрямки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиНапрямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Напрямки";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиУсіToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиОбраніToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиОбраніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Замовлення";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиУсіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Замовлення";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Замовлення";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Замовлення";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void додатиНапрямToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDirection form = new AddDirection(sqlconn);
            form.ShowDialog();
        }

        private void додатиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlight form = new AddFlight(sqlconn, 0);
            form.ShowDialog();
            FillTable();
        }

        private void редагуватиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlExpression = "Select count(ticket.ticket_id) from flight left outer join ticket on flight.flight_id = ticket.flight_id " +
                "where flight.flight_id = " + dataGridView1.SelectedRows[0].Cells[0].Value;
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int cnt = Convert.ToInt32(reader.GetValue(0).ToString());
            reader.Close();
            if (cnt == 0)
            {
                AddFlight form = new AddFlight(sqlconn, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                form.ShowDialog();
                FillTable();
            } else
            {
                MessageBox.Show("На рейс придбані квитки");
            }
            
        }

        private void видалитиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
                DialogResult result = MessageBox.Show("Підтвердити видалення?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {


                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            string sqlExpression = $"Delete from Flight where flight_id = {dataGridView1.Rows[i].Cells[0].Value}";

                            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                            try
                            {
                                //sqlconn.InfoMessage += new SqlInfoMessageEventHandler(MySqlMessageHandler);
                                command.ExecuteScalar();
                                const string message = "Обране видалено успішно";
                                const string caption = "";
                                var result1 = MessageBox.Show(message, caption,
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                //Console.Write("executeSQLUpdate: problem with command:" + command + "e=" + ex);
                                //Console.Out.Flush();

                                MessageBox.Show("Рейс використовується в розкладі");
                            }
                        }
                    }
                    FillTable();
                    
                }


            }
            else
            {
                const string message = "Оберіть необхідний рейс";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter oda = new SqlDataAdapter($"exec filterschedule '{textBox1.Text.ToUpper()}', '{textBox2.Text.ToUpper()}', '{textBox3.Text.ToUpper()}', '{dateTimePicker1.Value.ToString("yyyyMMdd")}'", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            FillTable();
        }

        private void роздрукуватиУвесьРозкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate(), false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
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

            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Усі рейси").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);

            

            document.Add(emptyline);
            float[] columnwidth = { 50, 200, 150, 150, 150, 150, 150, 150, 150, 100 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Номер рейсу")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Тип літака")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Звідки")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Куди")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Дата відправлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Час відправлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Дата прибуття")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Час прибуття")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Вартість")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                count++;
                
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[0].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[5].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[6].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[7].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[8].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[9].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[10].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[11].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                



            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано рейсів: " + count)
                    .SetTextAlignment(TextAlignment.RIGHT);
            document.Add(countplanes);
            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@"a.pdf")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void роздрукуватиОбранеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate(), false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
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

            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Усі рейси").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);



            document.Add(emptyline);
            float[] columnwidth = { 50, 200, 150, 150, 150, 150, 150, 150, 150, 100 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Номер рейсу")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Тип літака")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Звідки")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Куди")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Дата відправлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Час відправлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Дата прибуття")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Час прибуття")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Вартість")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    count++;

                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[0].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[5].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[6].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[7].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[8].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[9].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[10].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[11].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);


                }



            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано рейсів: " + count)
                    .SetTextAlignment(TextAlignment.RIGHT);
            document.Add(countplanes);
            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@"a.pdf")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void топКомпанійЗаПрибуткомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopCompanies form = new TopCompanies(sqlconn);
            form.ShowDialog();
        }

        private void топКомпанійЗаПроданимиКвиткамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iText.Kernel.Pdf.PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate(), false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
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

            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Топ компаній за проданими квитками").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);

            string sqlExpression = "Select Flight.Company as 'Компанія', count(Ticket.ticket_id) as 'Кількість проданих квитків' " +
                "from Flight left outer join Ticket on Flight.flight_id = Ticket.flight_id group by Flight.Company " +
                "order by count(Ticket.ticket_id) desc";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();



            document.Add(emptyline);
            float[] columnwidth = { 250, 250 };
            Table table = new Table(columnwidth, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Кількість проданих квитків")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);


            while (reader.Read())
            {
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(0).ToString()}"))
                                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(1).ToString()}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
            }
            reader.Close();


            document.Add(table);


            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@"a.pdf")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void середняВартістьЗамовленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iText.Kernel.Pdf.PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate(), false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
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

            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Середня вартість квитка за компаніями").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);

            string sqlExpression = "Select Flight.Company as 'Компанія', avg(Ticket.cost) as 'Середня вартість проданих квитків' " +
                "from Flight left outer join Ticket on Flight.flight_id = Ticket.flight_id group by Flight.Company " +
                "order by avg(Ticket.cost) desc";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();



            document.Add(emptyline);
            float[] columnwidth = { 250, 250 };
            Table table = new Table(columnwidth, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Середня вартість квитка")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);


            while (reader.Read())
            {
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(0).ToString()}"))
                                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(1).ToString()}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
            }
            reader.Close();


            document.Add(table);


            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@"a.pdf")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void найпопулярнішіНапрямкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iText.Kernel.Pdf.PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate(), false);
            ImageData data = ImageDataFactory.Create("E:/gerb.png");

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
            img.ScaleToFit(100, 100);
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

            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Топ напрямків").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);

            string sqlExpression = "Select Direction.FromCity, Direction.ToCity, count(Ticket.ticket_id) as 'Кількість проданих квитків' " +
                "from Flight left outer join Direction on Flight.direction_id = Direction.direction_id " +
                "left outer join Ticket on Flight.flight_id = Ticket.flight_id group by Direction.FromCity, Direction.ToCity " +
                "order by count(Ticket.ticket_id) desc";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();



            document.Add(emptyline);
            float[] columnwidth = { 250, 250, 250 };
            Table table = new Table(columnwidth, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Звідки")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Куди")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Кількість проданих квитків")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            while (reader.Read())
            {
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(0).ToString()}"))
                                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(1).ToString()}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(2).ToString()}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
            }
            reader.Close();


            document.Add(table);


            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@"a.pdf")
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (userID == 0)
            {
                int flightid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                PdfWriter writer = new PdfWriter("a.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, PageSize.A4.Rotate(), false);
                ImageData data = ImageDataFactory.Create("E:/gerb.png");

                iText.Layout.Element.Image img = new iText.Layout.Element.Image(data);
                img.ScaleToFit(100, 100);
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

                String FONT = "arial.ttf";
                iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);


                LineSeparator ls = new LineSeparator(new SolidLine());
                Paragraph emptyline = new Paragraph(" ");
                document.Add(emptyline);
                Paragraph headertext = new Paragraph("Інформація по рейсу " + flightid).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

                document.Add(headertext);
                document.Add(emptyline);
                DateTime datetime = DateTime.Now;
                Paragraph time = new Paragraph(datetime.ToString());
                document.Add(time);

                document.Add(ls);

                document.Add(emptyline);
                float[] columnwidth = { 50, 200, 150, 150, 150, 150, 150, 150, 150, 150, 150 };
                Table table = new Table(columnwidth, false);
                Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Номер рейсу")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);

                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Дата відправлення")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);

                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Час відправлення")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Вартість квитка")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Ряд")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Місце")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Клас")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Прізвище")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Ім'я")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("По-батькові")).SetFont(f)
                    .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
                table.AddCell(celltext);

                String sqlExpression = $"select flight.flight_id, flight.Company, Flight.departure_date, Flight.departure_time, ticket.Cost, " +
                                        "seat.Row, seat.Place, seat.Class, " +
                                        "client.Surname, Client.Name, Client.Middle_name " +
                                        "from flight " +
                                        "left outer join ticket on  Ticket.flight_id = flight.flight_id " +
                                        "left outer join[order] on[order].order_id = ticket.order_id " +
                                        "left outer join client on Client.client_id = [order].client_id " +
                                        "left outer join seat on Seat.seat_id = ticket.seat_id " +
                                        $"where flight.flight_id = {flightid}";
                SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(0).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(1).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(2).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(3).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(4).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(5).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(6).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(7).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(8).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(9).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(10).ToString()}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                }

                reader.Close();

                document.Add(table);

                document.Close();

                new Process
                {
                    StartInfo = new ProcessStartInfo(@"a.pdf")
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
            
        }
    }
}
