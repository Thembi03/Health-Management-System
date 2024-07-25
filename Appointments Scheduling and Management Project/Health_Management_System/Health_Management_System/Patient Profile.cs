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
    public partial class PatientProfile : Form
    {
        public PatientProfile()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Appointment appointmentform = new Appointment();
            appointmentform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Feedback_Users feedback_Usersform = new Feedback_Users();
            feedback_Usersform.Show();
        }
    }
}
