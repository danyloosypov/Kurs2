/*using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;*/
//using iTextSharp.text;
//using iTextSharp.text.pdf;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Document = iTextSharp.text.Document;

namespace Kurs2
{
    public partial class Directions : Form
    {
        public SqlConnection sqlconn;
        public Directions(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
            зробитиЗамовленняToolStripMenuItem.Visible = false;
            змінитиПерсональніДаніToolStripMenuItem.Visible = false;
            FillTable();

            додатиЛітакToolStripMenuItem1.Visible = false;
            редагуватиЛітакToolStripMenuItem.Visible = false;
            видалитиЛітакToolStripMenuItem.Visible = false;
        }

        private void Directions_Load(object sender, EventArgs e)
        {

        }

        public void FillTable()
        {
            SqlDataAdapter oda = new SqlDataAdapter("select * from Direction", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void перейтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Schedule form = new Schedule(sqlconn, 0);
            this.Hide();
            form.ShowDialog();
        }

        private void перейтиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Orders form = new Orders(sqlconn, 0);
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
            
            Process.Start(@"ForAdmin.docx");

        }

        private void зробитиЗамовленняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder form = new AddOrder(sqlconn, 0);
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

        private void редагуватиНапрямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    string sqlExpression1 = "Select count(flight.flight_id) from flight left outer join direction " +
                    "on flight.direction_id = direction.direction_id where direction.direction_id = " + dataGridView1.Rows[i].Cells[0].Value;
                    SqlCommand command1 = new SqlCommand(sqlExpression1, sqlconn);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    reader1.Read();
                    int cnt = Convert.ToInt32(reader1.GetValue(0).ToString());
                    reader1.Close();
                    if (cnt == 0)
                    {
                        EditDirection form = new EditDirection(sqlconn, Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value),
                                                Convert.ToString(dataGridView1.Rows[i].Cells[1].Value),
                                                Convert.ToString(dataGridView1.Rows[i].Cells[2].Value));
                        form.ShowDialog();
                        FillTable();
                    } else
                    {
                        const string message = "Такий напрям використовується у розкладі";
                        const string caption = "";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Information);
                    }
                    
                }
            }
        }

        private void видалитиНапрямToolStripMenuItem_Click(object sender, EventArgs e)
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
                            string sqlExpression = $"Delete from Direction where direction_id = {dataGridView1.Rows[i].Cells[0].Value}";

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

                                MessageBox.Show("Напрям використовується в розкладі");
                            }
                        }
                    }
                    FillTable();
                    
                }


            }
            else
            {
                const string message = "Оберіть необхідний напрям";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }
        }

        
        private void додатиНапрямToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDirection form = new AddDirection(sqlconn);
            form.ShowDialog();
        }

        private void роздрукуватиУсіToolStripMenuItem2_Click(object sender, EventArgs e)
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
            Paragraph headertext = new Paragraph("ALL Directions")
                .SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);


            document.Add(emptyline);
            float[] columnwidth = { 250, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID напрямку"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Звідки"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Куди"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);

            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                count++;
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[0].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[1].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);

            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано напрямків: " + count)
                    .SetTextAlignment(TextAlignment.RIGHT).SetFont(f);
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

        private void роздрукуватиОбраніToolStripMenuItem2_Click(object sender, EventArgs e)
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
            Paragraph headertext = new Paragraph("ALL Directions")
                .SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);


            document.Add(emptyline);
            float[] columnwidth = { 250, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID напрямку"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Звідки"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Куди"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
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
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[1].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[2].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                }
                

            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано напрямків: " + count)
                    .SetTextAlignment(TextAlignment.RIGHT).SetFont(f);
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

        private void додатиЛітакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPlanes form = new AddPlanes(sqlconn);
            form.ShowDialog();
            FillTable();
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
            const string message = "Перейдіть в пункт Замволення";
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

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Замовлення";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
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

        private void додатиРейсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFlight form = new AddFlight(sqlconn, 0);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            FillTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where direction_id = {textBox1.Text} and FromCity = '{textBox2.Text}' and ToCity = '{textBox3.Text}'", sqlconn);
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
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text != "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where ToCity = '{textBox3.Text}'", sqlconn);
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
            else if (textBox1.Text == "" && textBox2.Text != "" && textBox3.Text == "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where FromCity = '{textBox2.Text}'", sqlconn);
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
            else if (textBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where direction_id = {textBox1.Text}", sqlconn);
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
            else if (textBox1.Text != "" && textBox2.Text == "" && textBox3.Text != "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where direction_id = {textBox1.Text} and ToCity = '{textBox3.Text}'", sqlconn);
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
            else if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text == "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where direction_id = {textBox1.Text} and FromCity = '{textBox2.Text}'", sqlconn);
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
            else if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
            {
                FillTable();

            }
            else if (textBox1.Text == "" && textBox2.Text != "" && textBox3.Text != "")
            {
                try
                {
                    SqlDataAdapter oda = new SqlDataAdapter($"select * from Direction where FromCity = '{textBox2.Text}' and ToCity = '{textBox3.Text}'", sqlconn);
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
    }
}
