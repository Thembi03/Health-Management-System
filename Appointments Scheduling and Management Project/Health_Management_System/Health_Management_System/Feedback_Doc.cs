using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Health_Management_System
{
    public partial class Feedback_Doc : Form
    {
        public Feedback_Doc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Doc_Profile doc_Profileform = new Doc_Profile();
            doc_Profileform.Show();
        }

        private void Feedback_Doc_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'health_Management_SystemDataSet.Feedback' table. You can move, or remove it, as needed.
            this.feedbackTableAdapter.Fill(this.health_Management_SystemDataSet.Feedback);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check if a printer is available
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("No printer is available. Please connect a printer and try again.", "Printer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method
            }

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.Print();
        }

      private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define page margins and borders
            float margin = 50; // Adjust as needed
            RectangleF pageRect = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
            Pen borderPen = new Pen(Color.Black, 5); // Adjust color and thickness as needed

            // Draw page border
            e.Graphics.DrawRectangle(borderPen, Rectangle.Round(pageRect));

            // Get the name for the heading
            string name = "AppointEase Clinic";

            // Set up the content to be printed
            string prescriptionText = txtPrescription.Text;

            // Set up the font and brush for printing
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            Font prescriptionFont = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Set up the printing area
            float x = e.MarginBounds.Left + margin; // Adjust margin
            float y = e.MarginBounds.Top + margin; // Start at the top margin
            float width = e.MarginBounds.Width - (2 * margin); // Adjust width to account for margins
            float headingHeight = headingFont.GetHeight();
            float lineHeight = prescriptionFont.GetHeight();

            // Print the heading (centered)
            SizeF nameSize = e.Graphics.MeasureString(name, headingFont);
            float nameX = e.MarginBounds.Left + (e.MarginBounds.Width - nameSize.Width) / 2;
            e.Graphics.DrawString(name, headingFont, brush, nameX, y);

            // Move to the next line
            y += headingHeight + margin; // Add margin between heading and text

            // Print the prescription text (justified)
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            format.FormatFlags = StringFormatFlags.FitBlackBox; // Justify text
            RectangleF textRect = new RectangleF(x, y, width, e.MarginBounds.Height - (y - e.MarginBounds.Top));
            e.Graphics.DrawString(prescriptionText, prescriptionFont, brush, textRect, format);

            // Dispose resources
            borderPen.Dispose();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Doc_Profile doc_Profileform = new Doc_Profile();
            doc_Profileform.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtPrescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

