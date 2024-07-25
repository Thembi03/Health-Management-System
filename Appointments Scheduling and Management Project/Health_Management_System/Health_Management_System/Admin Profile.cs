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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Health_Management_System
{
    public partial class Admin : Form
    {

        private string connectionString = "Data Source=SALVATOREM;Initial Catalog = Health_Management_System; Integrated Security = True;";
        public Admin()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            
        }
        private string GenerateDocId()
        {
            const string prefix = "HSF";
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            string randomPart = new string(Enumerable.Repeat(characters, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string appointmentId = prefix + randomPart;
            return appointmentId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string DocId = GenerateDocId();
            txtDocId.Text = DocId;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDocId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User_Data User_Appointment = new User_Data();
            User_Appointment.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

        }

        private void button10_Click(object sender, EventArgs e)
        {
           Environment.Exit(0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string doctor_ID = txtDocId.Text;
            string First_Name = txtDocName.Text;
            string specialty = txtSpeciality.Text;
            string Last_Name = txtDocSName.Text;
            string Phone_Number = txtPhoneNum.Text;
            string Email = txtEmail.Text;
            string Telephone_Number = txtTelephone.Text;
            string Pword = txtpassword.Text;
            string pass2 = txtpass2.Text;
            // SQL query to insert data into the doctor table
            string insertQuery = @"
    INSERT INTO doctor (Doctor_ID, First_Name, Last_Name, Specialty, Phone_Number, Email, Telephone_Number, Pword) 
    VALUES (@doctorID, @firstName, @lastName, @specialty, @phoneNumber, @email, @telephoneNumber, @password)
";
            string insertUserQuery = "INSERT INTO [users] (Users_ID, Pass, Access) VALUES (@doctor_ID, @pass2, 'Doctor')";
            // Create a SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand object with the insert query and connection
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@doctorID", doctor_ID);
                        command.Parameters.AddWithValue("@firstName", First_Name);
                        command.Parameters.AddWithValue("@lastName", Last_Name);
                        command.Parameters.AddWithValue("@specialty", specialty);
                        command.Parameters.AddWithValue("@phoneNumber", Phone_Number);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@telephoneNumber", Telephone_Number);
                        command.Parameters.AddWithValue("@password", Pword);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Update command for user table
                        command.CommandText = insertUserQuery;

                        // Add parameters for user table
                        command.Parameters.AddWithValue("@pass2", pass2);
                        command.Parameters.AddWithValue("@doctor_ID", doctor_ID);
                        // Execute the command for user table
                        command.ExecuteNonQuery();
                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Account Created successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to Create Account");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            doctorBindingSource.MoveNext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            doctorBindingSource.AddNew();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string doctor_ID = txtDocId.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Define your SQL DELETE command
                    string deleteCommand = "DELETE FROM Doctor WHERE Doctor_ID = @doctor_ID";

                    // Create a SqlCommand object
                    SqlCommand command = new SqlCommand(deleteCommand, connection);

                    // Replace PrimaryKeyColumn with the name of your primary key column, and PrimaryKeyValue with the value of the primary key of the record you want to delete
                    command.Parameters.AddWithValue("@doctor_ID", doctor_ID);

                    // Execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Record not found or could not be deleted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
                doctorBindingSource.MoveNext();
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            doctorBindingSource.MovePrevious();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
            txtpassword.UseSystemPasswordChar = !txtpassword.UseSystemPasswordChar;
            txtpass2.UseSystemPasswordChar = !txtpass2.UseSystemPasswordChar;
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            txtpassword.UseSystemPasswordChar = true;
        }

        private void txtpass2_TextChanged(object sender, EventArgs e)
        {
            txtpass2.UseSystemPasswordChar = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            User_Data User_Appointment = new User_Data();
            User_Appointment.Show();
            this.Hide();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            LogIn login = new LogIn();
            login.Show();
            this.Hide();
        }
    }
}
