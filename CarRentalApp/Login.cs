using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalApplicationEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalApplicationEntities();
        }

        private void lgBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();

                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                //convert the input string to a byte array and compute the hash
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                //create a new stringbuilder to collect the bytes
                //and create a string
                StringBuilder sb = new StringBuilder();

                //loop through each byte if the hashed data
                //and format each one as a hexadecimal string
                for(int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                

                var hashed_password = sb.ToString();    
                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashed_password && q.isActive == true);
                if(user == null)
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    var mainWindow = new MainWindow(this,user);
                    mainWindow.Show();
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong, Please try again.");
            }
        }
    }
}
