﻿using iText.IO.Image;
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
    public partial class Clients : Form
    {
        public SqlConnection sqlconn;
        public Clients(SqlConnection sqlconn)
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            this.sqlconn = sqlconn;
            зробитиЗамовленняToolStripMenuItem.Visible = false;
            змінитиПерсональніДаніToolStripMenuItem.Visible = false;
            FillTable();
            додатиЛітакToolStripMenuItem1.Visible = false;
            редагуватиЛітакToolStripMenuItem.Visible = false;
            видалитиЛітакToolStripMenuItem.Visible = false;
        }

        private void Clients_Load(object sender, EventArgs e)
        {

        }

        public void FillTable()
        {
            SqlDataAdapter oda = new SqlDataAdapter("select * from Client", sqlconn);
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

        private void змінитиПерсональніДаніToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вийтиToolStripMenuItem_Click_1(object sender, EventArgs e)
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
            Paragraph headertext = new Paragraph("Усі клієнти")
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
            float[] columnwidth = { 50, 150, 150, 150, 100, 200, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID клієнта"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Прізвище"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Ім'я"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("По батькові"))
               .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Гендер"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Паспорт"))
               .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Телефон"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Email"))
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
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                table.AddCell(celltext);
                celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[4].Value}"))
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

            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано клієнтів: " + count)
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

        private void роздрукуватиОбраніToolStripMenuItem1_Click(object sender, EventArgs e)
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
            Paragraph headertext = new Paragraph("Обрані клієнти")
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
            float[] columnwidth = { 50, 150, 150, 150, 100, 200, 250, 250 };
            Table table = new Table(columnwidth, false);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("ID клієнта"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Прізвище"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Ім'я"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("По батькові"))
               .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Гендер"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Паспорт"))
               .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Телефон"))
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN).SetFont(f);
            table.AddCell(celltext);
            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Email"))
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
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[3].Value}"))
                    .SetTextAlignment(TextAlignment.CENTER).SetFont(f);
                    table.AddCell(celltext);
                    celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph($"{dataGridView1.Rows[i].Cells[4].Value}"))
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
                }
                

            }

            document.Add(table);

            Paragraph countplanes = new Paragraph("Надруковано клієнтів: " + count)
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

        private void редагуватиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected)
                {
                    EditClient form = new EditClient(sqlconn, Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value));
                    form.ShowDialog();
                    FillTable();
                }
            }
        }

        private void видалитиToolStripMenuItem1_Click(object sender, EventArgs e)
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
                            try
                            {
                                string sqlExpression = $"Delete from Client where client_id = {dataGridView1.Rows[i].Cells[0].Value}";

                                SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
                                command.ExecuteScalar();
                                const string message = "Обране видалено успішно";
                                const string caption = "";
                                var result1 = MessageBox.Show(message, caption,
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                            }
                            catch
                            {
                                const string message = "Клієнт має квитки";
                                const string caption = "";
                                var result1 = MessageBox.Show(message, caption,
                                                             MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                            }



                        }
                    }
                    FillTable();
                    
                }


            }
            else
            {
                const string message = "Оберіть необхідного клієнта";
                const string caption = "";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
            }
        }

        private void роздрукуватиОбраніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void роздрукуватиУсіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
            const string caption = "";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Перейдіть в пункт Клієнти";
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

        private void додатиЛітакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddPlanes form = new AddPlanes(sqlconn);
            form.ShowDialog();
            FillTable();
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
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = 0;
            FillTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter oda = new SqlDataAdapter($"exec filterclients '{textBox1.Text.ToUpper().Trim()}', " +
                $"'{textBox2.Text.ToUpper().Trim()}', " +
                $"'{textBox3.Text.ToUpper().Trim()}', " +
                $"'{textBox4.Text.ToUpper().Trim()}', " +
                $"'{comboBox1.SelectedItem.ToString()}'", sqlconn);
            DataTable dt = new DataTable();
            oda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
