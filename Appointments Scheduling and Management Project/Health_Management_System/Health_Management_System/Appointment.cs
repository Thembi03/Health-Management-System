using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Health_Management_System
{
    public partial class Appointment : Form
    {
        // Your database connection string
        private string connectionString = "Data Source=SALVATOREM;Initial Catalog = Health_Management_System; Integrated Security = True;";

        private List<Appointment> appointments = new List<Appointment>();
        private DateTime nextAvailableTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

        public Appointment()
        {
            InitializeComponent();
        }
        private int GetAppointmentDuration(string appointmentType)
        {
            switch (appointmentType)
            {
                case "General":
                    return 25;
                case "Follow-up":
                    return 15;
                case "Special":
                    return 45;
                default:
                    return 0;
            }
        }
        private bool IsTimeSlotAvailable(DateTime startTime, int duration)
        {
            DateTime endTime = startTime.AddMinutes(duration);

            // Check if the time slot overlaps with existing appointments in the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Appointment WHERE StartTime < @EndTime AND EndTime > @StartTime";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartTime", startTime);
                command.Parameters.AddWithValue("@EndTime", endTime);

                connection.Open();
                int overlappingAppointmentsCount = (int)command.ExecuteScalar();

                // If there are no overlapping appointments, the time slot is available
                return overlappingAppointmentsCount == 0;
            }
        }
        private string FormatTimeSlot(DateTime startTime, int duration)
        {
            DateTime endTime = startTime.AddMinutes(duration);

            return $"{startTime.ToString("hhmm tt")} - {endTime.ToString("hhmm tt")}";
        }
        private DateTime ScheduleAppointment(int duration)
        {
            DateTime currentTime = DateTime.Now;

            // Define time restrictions
            DateTime lunchStartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 13, 0, 0);
            DateTime lunchEndTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 14, 0, 0);

            // Check if the next available time is beyond today's working hours, move to the next day
            if (nextAvailableTime.Hour >= 17 || nextAvailableTime >= lunchEndTime)
            {
                nextAvailableTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day + 1, 8, 0, 0);
            }

            // Schedule the next available time slot
            while (nextAvailableTime < lunchStartTime || (nextAvailableTime >= lunchEndTime && nextAvailableTime.AddMinutes(duration) <= lunchStartTime))
            {
                // Check if the time slot is already occupied in the database
                if (IsTimeSlotAvailable(nextAvailableTime, duration))
                {
                    // Schedule the appointment
                    DateTime scheduledTime = nextAvailableTime;
                    // Move to the next available time slot
                    nextAvailableTime = nextAvailableTime.AddMinutes(duration + 10);
                    return scheduledTime;
                }

                // Move to the next time slot
                nextAvailableTime = nextAvailableTime.AddMinutes(15);
            }

            return DateTime.MinValue; // No available time slot
        }

     

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private string GenerateAppointmentId()
        {
            const string prefix = "AP";
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            string randomPart = new string(Enumerable.Repeat(characters, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string appointmentId = prefix + randomPart;
            return appointmentId;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string appointmentId = GenerateAppointmentId();
            txtAppointmentId.Text = appointmentId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtAppointmentId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cboAppointmentType.SelectedItem == null)
            {
                MessageBox.Show("Please select an appointment type.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string appointmentType = cboAppointmentType.SelectedItem.ToString();
            int duration = GetAppointmentDuration(appointmentType);

            DateTime scheduledTime = ScheduleAppointment(duration);
            if (scheduledTime != DateTime.MinValue)
            {
                // Display the scheduled time in the TextBox
                txtScheduledTime.Text = FormatTimeSlot(scheduledTime, duration);
            }
            else
            {
                MessageBox.Show("No available time slots for the selected appointment type.", "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
   
}

public class Appointment
{
    public string Type { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public int Duration { get; }

    public Appointment(string type, DateTime startTime, int duration)
    {
        Type = type;
        StartTime = startTime;
        EndTime = startTime.AddMinutes(duration);
        Duration = duration;
    }
}


