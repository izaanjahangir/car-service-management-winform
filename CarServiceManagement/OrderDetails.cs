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
    public partial class OrderDetails : Form
    {
        public Dictionary<string, object> order;
        private FirestoreDb db;

        public OrderDetails()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void OrderDetails_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");

            this.order_id_lb.Text = order["orderID"].ToString();
            this.make_cb.Text = order["make"].ToString();
            this.vehicle_model_tb.Text = order["vehicleModel"].ToString();
            this.vehicle_name_tb.Text = order["vehicleName"].ToString();
            this.vehicle_reg_tb.Text = order["vehicleRegistrationNumber"].ToString();
            this.owner_address_tb.Text = order["ownerAddress"].ToString();
            this.owner_contact_tb.Text = order["ownerContact"].ToString();
            this.owner_email_tb.Text = order["ownerEmail"].ToString();
            this.owner_name_tb.Text = order["ownerName"].ToString();
            this.request_type_cb.Text = order["requestType"].ToString();
            this.total_cost_tb.Text = order["totalCost"].ToString();

            List<object> services = (List<object>)order["services"];
            
            
            if (services.Contains("Oil Change")) {
                this.oil_change_cb.Checked = true;
            }

            if (services.Contains("Engine Tune up"))
            {
                this.engine_tuneup_cb.Checked = true;
            }

            if (services.Contains("Denting"))
            {
                this.denting_cb.Checked = true;
            }

            if (services.Contains("Paint Work"))
            {
                this.paint_work_cb.Checked = true;
            }

            if (services.Contains("Tire Replacement"))
            {
                this.tire_replacement_cb.Checked = true;
            }
        }

        private bool isServicesSelect()
        {
            return buildServiceList().Count > 0;
        }

        private List<string> buildServiceList()
        {
            List<string> services = new List<string>();
            bool oilChange = this.oil_change_cb.Checked;
            bool engineTuneUp = this.engine_tuneup_cb.Checked;
            bool denting = this.denting_cb.Checked;
            bool paintWork = this.paint_work_cb.Checked;
            bool tireReplacement = this.tire_replacement_cb.Checked;

            if (oilChange)
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

        private async void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string make = this.make_cb.Text;
                string vehicleName = this.vehicle_name_tb.Text;
                string vehicleRegistrationNumber = this.vehicle_reg_tb.Text;
                string vehicleModel = this.vehicle_model_tb.Text;
                string ownerName = this.owner_name_tb.Text;
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

                if (make.Length == 0 || vehicleModel.Length == 0 || ownerName.Length == 0 || vehicleName.Length == 0 || vehicleRegistrationNumber.Length == 0 || ownerContact.Length == 0 || requestType.Length == 0 || totalCost.Length == 0 || !isServicesSelect())
                {
                    throw new Exception("Please fill all required fields");
                }

                int totalCostCasted;
                if (!int.TryParse(totalCost, out totalCostCasted))
                {
                    throw new Exception("Please enter only number in price field");
                }

                if (requestType == "Dropoff" && ownerAddress.Length == 0)
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
                    { "vehicleModel", vehicleModel},
                    { "totalCost", totalCostCasted },
                    { "ownerName", ownerName },
                    { "services", buildServiceList() },
                };
                DocumentReference orderDocument = db.Collection("orders").Document(order["orderID"].ToString());
                await orderDocument.UpdateAsync(newOrder);

                MessageBox.Show("Order Edited");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private async void delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                await db.Collection("orders").Document(order["orderID"].ToString()).DeleteAsync();
                MessageBox.Show("Order Deleted");

                this.Hide();
                Home home = new Home();
                home.Show();
            }
            catch (Exception error) {
                MessageBox.Show(error.Message);
            }

        }
    }
}
