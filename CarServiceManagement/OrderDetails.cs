using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarServiceManagement
{
    public partial class OrderDetails : Form
    {
        public Dictionary<string, object> order;

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
            this.order_id_lb.Text = order["orderID"].ToString();
            this.make_tb.Text = order["make"].ToString();
            this.vehicle_model_tb.Text = order["vehicleModel"].ToString();
            this.vehicle_name_tb.Text = order["vehicleName"].ToString();
            this.vehicle_reg_tb.Text = order["vehicleRegistrationNumber"].ToString();
            this.owner_address_tb.Text = order["ownerAddress"].ToString();
            this.owner_contact_tb.Text = order["ownerContact"].ToString();
            this.owner_email_tb.Text = order["ownerEmail"].ToString();
            this.owner_name_tb.Text = order["ownerName"].ToString();
            this.request_type_tb.Text = order["requestType"].ToString();
            this.total_cost_lb.Text = order["totalCost"].ToString();

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
    }
}
