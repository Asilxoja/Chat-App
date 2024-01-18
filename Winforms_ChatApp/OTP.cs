using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class OTP : Form
    {
        public OTP()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Login login = new();
            this.Hide();
            login.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form1 form = new();
            this.Hide();
            form.Show();
        }
    }
}
