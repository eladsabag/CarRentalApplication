using System;
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
    public partial class AddUsers : Form
    {
        private readonly CarRentalApplicationEntities _db;
        private ManageUsers _manageUsers;
        public AddUsers(ManageUsers manageUsers)
        {
            InitializeComponent();
            _db = new CarRentalApplicationEntities();
            _manageUsers = manageUsers;
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList(); 
            cbRoles.DataSource = roles;
            cbRoles.ValueMember = "id";
            cbRoles.DisplayMember = "name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUser.Text;
                var roleId = (int)cbRoles.SelectedValue;
                var password = Utils.DefaultHashPassword();
                var user = new User
                {
                    username = username,
                    password = password,
                    isActive = true
                };
                _db.Users.Add(user);
                _db.SaveChanges();

                var userid = user.id;

                var userRole = new UserRole
                {
                    roleid = roleId,
                    userid = userid
                };
                _db.UserRoles.Add(userRole);
                _db.SaveChanges();

                MessageBox.Show("New User Added Succesfully!");
                _manageUsers.PopulateGrid();    
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
