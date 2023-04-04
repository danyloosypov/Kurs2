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
    public partial class TopCompanies : Form
    {
        public SqlConnection sqlconn;

        public TopCompanies(SqlConnection sqlconn)
        {
            InitializeComponent();
            this.sqlconn = sqlconn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
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
            Paragraph headertext = new Paragraph("Топ компаній за прибутком").SetFont(f)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);

            document.Add(headertext);
            document.Add(emptyline);
            DateTime datetime = DateTime.Now;
            Paragraph time = new Paragraph(datetime.ToString());
            document.Add(time);

            document.Add(ls);

            string sqlExpression = "Select Flight.Company as 'Компанія', sum(Ticket.Cost) as 'Прибуток' " +
                "from Flight left outer join Ticket on Flight.flight_id = Ticket.flight_id  " +
                $"where flight.departure_date >= '{dateTimePicker1.Value.ToString("yyyyMMdd")}' and flight.departure_date <= '{dateTimePicker2.Value.ToString("yyyyMMdd")}' " +
                "group by Flight.Company order by sum(Ticket.Cost) desc";
            SqlCommand command = new SqlCommand(sqlExpression, sqlconn);
            SqlDataReader reader = command.ExecuteReader();
            


            document.Add(emptyline);
            float[] columnwidth = { 250, 250 };
            Table table = new Table(columnwidth, false).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            Cell celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Компанія")).SetFont(f)
                .SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.GREEN);
            table.AddCell(celltext);

            celltext = new Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Прибуток")).SetFont(f)
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
            this.Close();
        }
    }
}
