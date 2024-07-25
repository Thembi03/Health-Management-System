using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Health_Management_System
{
    public partial class PatientDetails : Form
    {
        public PatientDetails()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private string GeneratePatientId()
        {
            const string prefix = "Pat";
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            string randomPart = new string(Enumerable.Repeat(characters, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string appointmentId = prefix + randomPart;
            return appointmentId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string DocId = GeneratePatientId();
            txtPatientId.Text = DocId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register registerform = new Register();
            registerform.Show();
        }
    }
}
