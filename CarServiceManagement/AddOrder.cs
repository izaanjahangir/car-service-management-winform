using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace CarServiceManagement
{
    public partial class AddOrder : Form
    {
        private FirestoreDb db;
        public AddOrder()
        {
            InitializeComponent();
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");

            string[] allMakes = { "Toyota", "Honda", "Suzuki", "Nissan", "Audi", "Chevolet", "Hummer", "Hyundai", "KIA", "Land Rover", "MG", "Proton", "Prince" };
            string[] allRequestTypes = { "Dropoff", "Pickup" };

            this.make_cb.Items.Clear();
            foreach (string type in allMakes)
            {
                this.make_cb.Items.Add(type);
            }

            this.request_type_cb.Items.Clear();
            foreach (string type in allRequestTypes)
            {
                this.request_type_cb.Items.Add(type);
            }
        }

        private bool isServicesSelect() {
            return buildServiceList().Count > 0;
        }

        private List<string> buildServiceList() {
            List<string> services = new List<string>();
            bool oilChange = this.oil_change_cb.Checked;
            bool engineTuneUp = this.engine_tuneup_cb.Checked;
            bool denting = this.denting_cb.Checked;
            bool paintWork = this.paint_work_cb.Checked;
            bool tireReplacement = this.tire_replacement_cb.Checked;

            if(oilChange)
            {
                services.Add("Oil Change");
            }

            if (engineTuneUp)
            {
                services.Add("Engine Tune up");
            }

            if (denting)
            {
                services.Add("Denting");
            }

            if (paintWork)
            {
                services.Add("Paint Work");
            }

            if (tireReplacement)
            {
                services.Add("Tire Replacement");
            }

            return services;
        }

        private void clearFields() {
            this.make_cb.Text = "";
            this.vehicle_name_tb.Text = "";
            this.vehicle_reg_tb.Text = "";
            this.owner_contact_tb.Text = "";
            this.owner_email_tb.Text = "";
            this.request_type_cb.Text = "";
            this.owner_address_tb.Text = "";
            this.total_cost_tb.Text = "";
            this.oil_change_cb.Checked = false;
            this.engine_tuneup_cb.Checked = false ;
            this.denting_cb.Checked = false;
            this.paint_work_cb.Checked = false;
            this.tire_replacement_cb.Checked = false;
        }

        private async void add_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string make = this.make_cb.Text;
                string vehicleName = this.vehicle_name_tb.Text;
                string vehicleRegistrationNumber = this.vehicle_reg_tb.Text;
                string ownerContact = this.owner_contact_tb.Text;
                string ownerEmail = this.owner_email_tb.Text;
                string requestType = this.request_type_cb.Text;
                string ownerAddress = this.owner_address_tb.Text;
                string totalCost = this.total_cost_tb.Text;
                bool oilChange = this.oil_change_cb.Checked;
                bool engineTuneUp = this.engine_tuneup_cb.Checked;
                bool denting = this.denting_cb.Checked;
                bool paintWork = this.paint_work_cb.Checked;
                bool tireReplacement = this.tire_replacement_cb.Checked;
            
                if (make.Length == 0 || vehicleName.Length == 0 || vehicleRegistrationNumber.Length == 0 || ownerContact.Length == 0 || requestType.Length == 0 || totalCost.Length == 0 || !isServicesSelect())
                {
                    throw new Exception("Please fill all required fields");
                }

                int totalCostCasted;
                if(!int.TryParse(totalCost, out totalCostCasted))
                {
                    throw new Exception("Please enter only number in price field");
                }

                if(requestType == "Dropoff" && ownerAddress.Length == 0)
                {
                    throw new Exception("Address is required for dropoff orders");
                }

                Dictionary<string, object> newOrder = new Dictionary<string, object>
                {
                    { "make", make },
                    { "vehicleName", vehicleName },
                    { "vehicleRegistrationNumber", vehicleRegistrationNumber },
                    { "ownerContact", ownerContact },
                    { "ownerEmail", ownerEmail },
                    { "requestType", requestType },
                    { "ownerAddress", ownerAddress },
                    { "totalCost", totalCostCasted },
                    { "services", buildServiceList() },
                };
                CollectionReference orderCollection = db.Collection("orders");
                await orderCollection.AddAsync(newOrder);

                MessageBox.Show("Order Added");
                clearFields();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }
    }
}
