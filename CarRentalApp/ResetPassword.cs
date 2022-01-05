﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ResetPassword : Form
    {
        private readonly CarRentalApplicationEntities _db;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalApplicationEntities();
            _user = user;   
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbNewPassword.Text;
                var confirm_password = tbConfirmPassword.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);
                if (confirm_password != password)
                {
                    MessageBox.Show("Password do not match. Please try again!");
                }

                _user.password = Utils.HashPassword(password);
                _db.SaveChanges();

                MessageBox.Show("Password was rest succesfully!");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
