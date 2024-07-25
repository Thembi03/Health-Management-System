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
    public partial class DoctorAccounts : Form
    {
        public DoctorAccounts()
        {
            InitializeComponent();
        }

        private void DoctorAccounts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'health_Management_SystemDataSet.Users' table. You can move, or remove it, as needed.
           // this.usersTableAdapter.Fill(this.health_Management_SystemDataSet.Users);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            User_Data users = new User_Data();
            users.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=SALVATOREM;Initial Catalog=Health_Management_system;Integrated Security=True";

            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                try
                {
                    con1.Open();

                    string str2 = "SELECT * FROM Users WHERE Users_ID=@Users_ID";
                    SqlCommand cmd2 = new SqlCommand(str2, con1);
                    cmd2.Parameters.AddWithValue("@Users_ID", txtUserID.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    usersDataGridView.DataSource = new BindingSource(dt, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            User_Data users = new User_Data();
            users.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
