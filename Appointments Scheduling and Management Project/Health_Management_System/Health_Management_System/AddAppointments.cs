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
    public partial class AddAppointments : Form

    {
        private string connectionString = "Data Source=SALVATOREM;Initial Catalog = Health_Management_System; Integrated Security = True;";
        public AddAppointments()
        {
            InitializeComponent();
        }

        private void AddAppointments_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'health_Management_SystemDataSet.spnmt' table. You can move, or remove it, as needed.
            this.spnmtTableAdapter.Fill(this.health_Management_SystemDataSet.spnmt);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.spnmtBindingSource.AddNew();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string nationalID = txtNatID.Text; // Assuming you have a textbox for appointment ID

            string deleteQuery = "DELETE FROM spnmt WHERE national_id = @national_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@national_ID", nationalID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                            ClearTextboxes();
                        }
                        else
                        {
                            MessageBox.Show("No appointment found with the provided national ID");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.spnmtBindingSource.MoveNext();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.spnmtBindingSource.MovePrevious();
        }
        private void ClearTextboxes()
        {
            // Clear all textboxes
            txtName.Text = "";
            txtNatID.Text = "";
            txtUsermail.Text = "";
            txtPhoneNumber.Text = "";
            txtDepartments.Text = "";
            txtPmessage.Text = "";
            txtAppointmentTime.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string nationalId = txtNatID.Text;
            string email = txtUsermail.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string department = txtDepartments.Text;
            string message = txtPmessage.Text;
            string appointmentTime = txtAppointmentTime.Text;

            string insertQuery = @"
        INSERT INTO spnmt (fullname, national_id, email, phone_number, department, Pmessage, appointment_datetime) 
        VALUES (@fullname, @national_id, @email, @phone_number, @department, @Pmessage, @appointment_datetime)
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fullname", name);
                        command.Parameters.AddWithValue("@national_id", nationalId);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phone_number", phoneNumber);
                        command.Parameters.AddWithValue("@department", department);
                        command.Parameters.AddWithValue("@Pmessage", message);
                        command.Parameters.AddWithValue("@appointment_datetime", appointmentTime);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment saved successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to save appointment.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNatID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsermail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDepartments_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPmessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAppointmentTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoctorAccounts acc = new DoctorAccounts();
            acc.Show();
            this.Hide();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User_Data users = new User_Data();
            users.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            User_Data users = new User_Data();
            users.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DoctorAccounts acc = new DoctorAccounts();
            acc.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
