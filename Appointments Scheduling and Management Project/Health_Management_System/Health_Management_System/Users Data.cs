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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Management_System
{
    public partial class User_Data : Form
    {
        public User_Data()
        {
            InitializeComponent();
        }

        private void User_Datas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'health_Management_SystemDataSet.spnmt' table. You can move, or remove it, as needed.
            this.spnmtTableAdapter.Fill(this.health_Management_SystemDataSet.spnmt);
        }

        private void spnmtBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.spnmtBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.health_Management_SystemDataSet);

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


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddAppointments admin = new AddAppointments();
            admin.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DoctorAccounts admin = new DoctorAccounts();
            admin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            DoctorAccounts admin = new DoctorAccounts();
            admin.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AddAppointments admin = new AddAppointments();
            admin.Show();
        }
    }
}
