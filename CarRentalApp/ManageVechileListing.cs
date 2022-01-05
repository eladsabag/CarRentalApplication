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
    public partial class ManageVechileListing : Form
    {
        private readonly CarRentalApplicationEntities _db;
        public ManageVechileListing()
        {
            InitializeComponent();
            _db = new CarRentalApplicationEntities();
        }

        private void ManageVechileListing_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVechile = new AddEditVechile(this);
            addEditVechile.MdiParent = this.MdiParent;
            addEditVechile.Show();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get id of selected row
                var id = (int)gvVechileList.SelectedRows[0].Cells["Id"].Value;
                //query databsae for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

                //launch AddEditVechile window with data
                var addEditVechile = new AddEditVechile(car,this);
                addEditVechile.MdiParent = this.MdiParent;
                addEditVechile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get id of selected row
                var id = (int)gvVechileList.SelectedRows[0].Cells["Id"].Value;
                //query databsae for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if(dr == DialogResult.Yes)
                {
                    //delete vechile from table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }              
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            // Select a custom model coolection of cars from data base
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,    
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlaterNumber = q.LicensePlateNumber,
                    q.id
                })
                .ToList();
            gvVechileList.DataSource = cars;
            gvVechileList.Columns[4].HeaderText = "License Plate Number";
            gvVechileList.Columns["id"].Visible = false;
        }
    }
}
