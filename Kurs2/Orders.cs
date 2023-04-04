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
    public partial class Orders : Form
    {
        public SqlConnection sqlconn;
        public int userID;
        public Orders(SqlConnection sqlconn, int userID)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            this.userID = userID;
            if (userID != 0)
            {
                редагуватиToolStripMenuItem.Visible = false;
                видалитиToolStripMenuItem.Visible = false;
                додатиЛітакToolStripMenuItem.Visible = false;
                додатиНапрямToolStripMenuItem.Visible = false;
                пасажириToolStripMenuItem.Visible = false;
                додатиРейсToolStripMenuItem.Visible = false;
                редагуватиРейсToolStripMenuItem.Visible = false;
                видалитиРейсToolStripMenuItem.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                comboBox1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;

            }
            else
            {
                зробитиЗамовленняToolStripMenuItem.Visible = false;
                змінитиПерсональніДаніToolStripMenuItem.Visible = false;
                редагуватиToolStripMenuItem.Visible = false;
                додатиЛітакToolStripMenuItem1.Visible = false;
                редагуватиЛітакToolStripMenuItem.Visible = false;
                видалитиЛітакToolStripMenuItem.Visible = false;
            }
            
            FillTable();
        }

        public void FillTable()
        {
            if (userID != 0)
            {
                SqlDataAdapter oda = new SqlDataAdapter("select o.order_id, o.client_id, payment_type, " +
                                                       "c.surname + ' '+Left(c.name,1) +'.'+ Left(c.Middle_name,1)+'.' as fio " +
                                                        "from [Order] o " +
                                                        $"left outer join Client c ON c.client_id = o.client_id where o.client_id = {userID}", sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].Visible = false;
            
            }
            else
            {
                SqlDataAdapter oda = new SqlDataAdapter("select o.order_id, o.client_id, payment_type, " +
                                                       "c.surname + ' '+Left(c.name,1) +'.'+ Left(c.Middle_name,1)+'.' as fio "+
                                                        "from [Order] o "+
                                                        "left outer join Client c ON c.client_id = o.client_id", sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
                //dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Visible = false;
            }
            
        }

        private void Orders_Load(object sender, EventArgs e)
        {

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

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            this.Hide();
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
            const string message = "Перейдіть в пункт ЛІтаки";
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
            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
            PdfWriter writer = new PdfWriter("a.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, false);
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


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Усі замовлення").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);


            document.Add(emptyline);
            float[] columnwidth = { 250, 250, 250, 250, 250, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID замовлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID клієнта")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Спосіб оплати")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Місце")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Клас білету")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Номер рейсу")).SetFont(f)
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
                    try
                    {
                        
                        String sqlExpression = $"select * from [Ticket] left outer join Seat on Ticket.seat_id = Seat.seat_id where order_id = {dataGridView1.Rows[i].Cells[0].Value}";
                        SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[0].Value}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                            .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                            .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(7).ToString() + reader.GetValue(8).ToString()}"))
                            .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(9).ToString()}"))
                            .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(3).ToString()}"))
                            .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(4).ToString()}"))
                           .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                            table.AddCell(celltext);
                        }


                        reader.Close();
                    }
                    catch
                    {
                        const string message = "Немає інформації";
                        const string caption = "";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Information);
                    }
                }
                


            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано замовлень: " + count).SetFont(f)
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
            const string message = "Перейдіть в пункт Розклад";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиУвесьРозкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Розклад";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Розклад";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Розклад";
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

        private void додатиЛітакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPlanes form = new AddPlanes(sqlconn);
            form.ShowDialog();
        }

        private void додатиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlight form = new AddFlight(sqlconn, 0);
            form.ShowDialog();
        }

        private void змінитиПерсональніДаніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditClient form = new EditClient(sqlconn, userID);
            form.ShowDialog();
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && comboBox1.SelectedItem == null)
            {
                FillTable();
            } else if (textBox1.Text != "" && comboBox1.SelectedItem == null)
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select o.order_id, o.client_id, payment_type, " +
                                                       "c.surname + ' '+Left(c.name,1) +'.'+ Left(c.Middle_name,1)+'.' as fio " +
                                                        "from [Order] o " +
                                                        "left outer join Client c ON c.client_id = o.client_id "+
                                                        $"where c.surname like '%{textBox1.Text}%'", sqlconn);

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
            } else if (textBox1.Text == "" && comboBox1.SelectedItem != null)
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select o.order_id, o.client_id, payment_type, " +
                                                       "c.surname + ' '+Left(c.name,1) +'.'+ Left(c.Middle_name,1)+'.' as fio " +
                                                        "from [Order] o " +
                                                        "left outer join Client c ON c.client_id = o.client_id " +
                                                        $"where payment_type = '{comboBox1.SelectedItem}'", sqlconn);
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
            else if (textBox1.Text != "" && comboBox1.SelectedItem != null)
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select o.order_id, o.client_id, payment_type, " +
                                                       "c.surname + ' '+Left(c.name,1) +'.'+ Left(c.Middle_name,1)+'.' as fio " +
                                                        "from [Order] o " +
                                                        "left outer join Client c ON c.client_id = o.client_id " +
                                                        $"where payment_type = '{comboBox1.SelectedItem}' and c.surname like '%{textBox1.Text}%'", sqlconn);

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.SelectedItem = null;
            FillTable();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OrderInfo openForm = new OrderInfo(sqlconn, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
            
            openForm.ShowDialog(this);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // MessageBox.Show("Invalid data type");
                errorProvider1.SetError(textBox1, "Invalid data type");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void роздрукуватиУсіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Guid.NewGuid().ToString()+".pdf";
            String FONT = "arial.ttf";
            iText.Kernel.Font.PdfFont f = iText.Kernel.Font.PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, false);
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


            LineSeparator ls = new LineSeparator(new SolidLine());
            Paragraph emptyline = new Paragraph(" ");
            document.Add(emptyline);
            Paragraph headertext = new Paragraph("Усі замовлення").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);


            document.Add(emptyline);
            float[] columnwidth = { 250, 250, 250, 250, 250, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID замовлення")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID клієнта")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Спосіб оплати")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Місце")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Клас білету")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Номер рейсу")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Вартість")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                count++;
                try
                {
                    
                    String sqlExpression = $"select * from [Ticket] left outer join Seat on Ticket.seat_id = Seat.seat_id where order_id = {dataGridView1.Rows[i].Cells[0].Value}";
                    SqlCommand cmd = new SqlCommand(sqlExpression, sqlconn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[0].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(7).ToString() + reader.GetValue(8).ToString()}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(9).ToString()}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(3).ToString()}"))
                        .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                        celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{reader.GetValue(4).ToString()}"))
                       .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                        table.AddCell(celltext);
                    }

                    
                    reader.Close();
                }
                catch
                {
                    const string message = "Немає інформації";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Information);
                }
                

            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано замовень: " + count).SetFont(f)
                    .SetTextAlignment(TextAlignment.RIGHT);
            document.Add(countplanes);
            document.Close();



            new Process
            {
                StartInfo = new ProcessStartInfo(@fileName)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    string sqlExpression = $"Delete from [Order] where order_id = {Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value)}";

                    SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                    try
                    {
                        //sqlconn.InfoMessage += new SqlInfoMessageEventHandler(MySqlMessageHandler);
                        command.ExecuteScalar();

                    }
                    catch (Exception ex)
                    {
                        //Console.Write("executeSQLUpdate: problem with command:" + command + "e=" + ex);
                        //Console.Out.Flush();

                        MessageBox.Show("Виникла помилка");
                    }
                }
            }
            FillTable();
            
        }
    }
}
