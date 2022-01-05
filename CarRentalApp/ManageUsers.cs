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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalApplicationEntities _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalApplicationEntities();   
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if(!Utils.FormIsOpen("AddUser"))
            {
                var addUser = new AddUsers(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            } 
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //get id of selected row
                var id = (int)gvUsersList.SelectedRows[0].Cells["id"].Value;
                //query databsae for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                var hashedPassword = Utils.DefaultHashPassword();  

                user.password = hashedPassword; 
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s Password has been reset!");
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                //get id of selected row
                var id = (int)gvUsersList.SelectedRows[0].Cells["id"].Value;
                //query databsae for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                user.isActive = user.isActive == true ? false : true;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s active status has changed!");
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PopulateGrid()
        {
            var users = _db.Users
                .Select(q => new
                {
                    q.id,
                    q.username,
                    q.UserRoles.FirstOrDefault().Role.name,
                    q.isActive
                })
                .ToList();
            gvUsersList.DataSource = users;
            gvUsersList.Columns["username"].HeaderText = "Username";
            gvUsersList.Columns["name"].HeaderText = "Role Name";
            gvUsersList.Columns["isActive"].HeaderText = "Active";

            gvUsersList.Columns["id"].Visible = false;
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid(); 
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
