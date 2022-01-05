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
    public partial class AddEditVechile : Form
    {
        private bool isEditMode;
        private ManageVechileListing _manageVechileListing;
        private readonly CarRentalApplicationEntities _db;
        public AddEditVechile(ManageVechileListing manageVechileListing = null)
        {
            InitializeComponent();
            lbTitle.Text = "Add New Vechile";
            this.Text = "Add New Vechile";
            isEditMode = false;
            _manageVechileListing = manageVechileListing;
            _db = new CarRentalApplicationEntities();
        }

        public AddEditVechile(TypesOfCar carToEdit, ManageVechileListing manageVechileListing = null)
        {
            InitializeComponent();
            lbTitle.Text = "Edit Vechile";
            this.Text = "Edit Vechile";
            _manageVechileListing = manageVechileListing;
            if(carToEdit == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit.");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalApplicationEntities();
                PopulateFields(carToEdit);
                _db.SaveChanges();
            }
        }

        private void PopulateFields(TypesOfCar car)
        {
            lbID.Text = car.id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicense.Text = car.LicensePlateNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbMake.Text) || string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    MessageBox.Show("Please ensure that you price a make and a model");
                }
                if (isEditMode)
                {
                    // edit
                    var id = int.Parse(lbID.Text);
                    var car = _db.TypesOfCars.FirstOrDefault(q => q.id == id);
                    car.Model = tbModel.Text;
                    car.Make = tbMake.Text;
                    car.VIN = tbVIN.Text;
                    car.Year = int.Parse(tbYear.Text);
                    car.LicensePlateNumber = tbLicense.Text;
                }
                else
                {
                    var newCar = new TypesOfCar
                    {
                        LicensePlateNumber = tbLicense.Text,
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        VIN = tbVIN.Text,
                        Year = int.Parse(tbYear.Text)
                    };
                    _db.TypesOfCars.Add(newCar);
                }
                _db.SaveChanges();
                _manageVechileListing.PopulateGrid();
                MessageBox.Show("Insert Operation Completed. Refresh grid to see changes.");
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }
    }
}
