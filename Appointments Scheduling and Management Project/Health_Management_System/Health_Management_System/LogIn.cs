using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Health_Management_System
{
    public partial class LogIn : Form
    {
        private string connectionString = "Data Source=SALVATOREM;Initial Catalog=Health_Management_System;Integrated Security=True;";
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string loginAs = cboLoginAs.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(loginAs))
            {
                MessageBox.Show("Please enter all fields.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = "SELECT Access FROM Users WHERE Users_ID = @Users_ID AND Pass = @Pass";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Users_ID", username);
                    command.Parameters.AddWithValue("@Pass", password);

                    object Access = command.ExecuteScalar();

                    if (Access != null)
                    {
                        OpenAppropriateForm(Access.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void OpenAppropriateForm(string Access)
        {
            
                switch (Access)
                {
                 
                    case "Admin":
                        User_Data Adminform = new User_Data();
                        Adminform.Show();
                        this.Hide();
                    break;

                    case "Doctor":
                        this.Hide();
                        Doc_Profile Doc_ProfileForm = new Doc_Profile();
                        Doc_ProfileForm.Show();
                        break;
                    default:
                        MessageBox.Show("Invalid role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

               

            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form Admin = new Form();
            Admin.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Welcome_Page welcome = new Welcome_Page();
            welcome.Show();
            this.Hide();
        }

        private void cboLoginAs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             txtUsername.Text = "";
            txtPassword.Text = "";
            cboLoginAs.SelectedIndex = -1; ;
          
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}
    
    

