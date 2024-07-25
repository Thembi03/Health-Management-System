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
    public partial class Feedback_Users : Form
    {
        public Feedback_Users()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PatientProfile patientProfileForm = new PatientProfile();
            patientProfileForm.Show();
        }
    }
}
