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

namespace Health_Management_System
{
    public partial class Doc_Profile : Form
    {
        public Doc_Profile()
        {
            InitializeComponent();
            // Start the timer when the form loads
            timer1.Start();

            // Call the Tick event handler immediately to update the label with the current time and date
            timer1_Tick(null, EventArgs.Empty);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the label text with the current time and date
            label3.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback_Doc feedback_Docform = new Feedback_Doc();
            feedback_Docform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Doc_Profile_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'health_Management_SystemDataSet.spnmt' table. You can move, or remove it, as needed.
            this.spnmtTableAdapter.Fill(this.health_Management_SystemDataSet.spnmt);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=SALVATOREM;Initial Catalog=Health_Management_system;Integrated Security=True";

            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                try
                {
                    con1.Open();

                    string str2 = "SELECT * FROM spnmt WHERE fullname=@fullname";
                    SqlCommand cmd2 = new SqlCommand(str2, con1);
                    cmd2.Parameters.AddWithValue("@fullname", textBox5.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    spnmtDataGridView.DataSource = new BindingSource(dt, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Feedback_Doc feedback_Docform = new Feedback_Doc();
            feedback_Docform.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
