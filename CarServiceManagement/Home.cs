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
    public partial class Home : Form
    {
        private FirestoreDb db;
        private string selectedOrderID;
        private IReadOnlyList<DocumentSnapshot> orders;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");

            fetchTodayOrders();
        }

        private async void fetchTodayOrders() {
            try
            {
                CollectionReference orderCollection = db.Collection("orders");
                QuerySnapshot orderSnapshot = await orderCollection.OrderBy("addedOn").GetSnapshotAsync();

                this.today_grid_view.Columns.Add("Order ID", "Order ID");
                this.today_grid_view.Columns.Add("Vehicle Make", "Vehicle Make");
                this.today_grid_view.Columns.Add("Vehicle Name", "Vehicle Name");
                this.today_grid_view.Columns.Add("Owner Name", "Owner Name");
                this.today_grid_view.Columns.Add("Owner Contact", "Owner Contact");
                this.today_grid_view.Columns.Add("Request Type", "Request Type");
                this.today_grid_view.Columns.Add("Total Cost", "Total Cost");

                orders = orderSnapshot.Documents;

                foreach (DocumentSnapshot documentSnapshot in orderSnapshot.Documents)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    Console.WriteLine(data);
                    Dictionary<string, object> order = new Dictionary<string, object>()
                    {
                        {"orderID", documentSnapshot.Id },
                        {"vehicleName", data.ContainsKey("vehicleName") ? data["vehicleName"]: "" },
                        {"make", data.ContainsKey("make") ? data["make"]: "" },
                        {"ownerContact", data.ContainsKey("ownerContact") ? data["ownerContact"]: "" },
                        {"ownerName", data.ContainsKey("ownerName") ? data["ownerName"]: "" },
                        {"requestType", data.ContainsKey("requestType") ? data["requestType"]: "" },
                        {"totalCost", data.ContainsKey("totalCost") ? data["totalCost"].ToString(): "" },
                    };
                    Console.WriteLine("HERE");
                    this.today_grid_view.Rows.Add(order["orderID"], order["make"], order["vehicleName"], order["ownerName"], order["ownerContact"], order["requestType"], order["totalCost"]);
                   
                    Console.WriteLine("End of Loop");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        /*
        private async void fetchPastOrders() {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                startOfDay = DateTime.SpecifyKind(startOfDay, DateTimeKind.Utc);

                MessageBox.Show(startOfDay.ToString());

                CollectionReference orderCollection = db.Collection("orders");
                QuerySnapshot orderSnapshot = await orderCollection.WhereLessThan("addedOn", startOfDay).OrderBy("addedOn").GetSnapshotAsync();

                this.past_grid_view.Columns.Add("Vehicle Make", "Vehicle Make");
                this.past_grid_view.Columns.Add("Vehicle Name", "Vehicle Name");
                this.past_grid_view.Columns.Add("Owner Name", "Owner Name");
                this.past_grid_view.Columns.Add("Owner Contact", "Owner Contact");
                this.past_grid_view.Columns.Add("Request Type", "Request Type");
                this.past_grid_view.Columns.Add("Total Cost", "Total Cost");

                foreach (DocumentSnapshot documentSnapshot in orderSnapshot.Documents)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    Dictionary<string, object> order = new Dictionary<string, object>()
                    {
                        {"vehicleName", data.ContainsKey("vehicleName") ? data["vehicleName"]: "" },
                        {"make", data.ContainsKey("make") ? data["make"]: "" },
                        {"ownerContact", data.ContainsKey("ownerContact") ? data["ownerContact"]: "" },
                        {"ownerName", data.ContainsKey("ownerName") ? data["ownerName"]: "" },
                        {"requestType", data.ContainsKey("requestType") ? data["requestType"]: "" },
                        {"totalCost", data.ContainsKey("totalCost") ? data["totalCost"]: "" },
                    };
                    this.past_grid_view.Rows.Add(order["make"], order["vehicleName"], order["ownerName"], order["ownerContact"],  order["requestType"], order["totalCost"]);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        */

        private void add_order_btn_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            this.Hide();
            addOrder.Show();
        }

        private void today_grid_view_selection_changed(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if(dgv != null && dgv.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgv.SelectedRows[0];

                if (row != null)
                {
                    selectedOrderID = row.Cells[0].Value.ToString();
                    this.details_btn.Enabled = true;
                }
            }
            else
            {
                selectedOrderID = null;
                this.details_btn.Enabled = false;
            }
        }

        private void details_btn_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> foundOrder= null;

            foreach (DocumentSnapshot documentSnapshot in orders)
            {
                Dictionary<string, object> data = documentSnapshot.ToDictionary();
                Dictionary<string, object> newOrder = new Dictionary<string, object>()
                {
                    {"orderID", documentSnapshot.Id.ToString() },
                    {"vehicleName", data.ContainsKey("vehicleName") ? data["vehicleName"]: "" },
                    {"make", data.ContainsKey("make") ? data["make"]: "" },
                    {"ownerContact", data.ContainsKey("ownerContact") ? data["ownerContact"]: "" },
                    {"ownerName", data.ContainsKey("ownerName") ? data["ownerName"]: "" },
                    {"requestType", data.ContainsKey("requestType") ? data["requestType"]: "" },
                    {"totalCost", data.ContainsKey("totalCost") ? data["totalCost"].ToString(): "" },
                    {"addedOn", data["addedOn"] },
                    {"ownerAddress", data.ContainsKey("ownerAddress") ? data["ownerAddress"].ToString(): "" },
                    {"ownerEmail", data.ContainsKey("ownerEmail") ? data["ownerEmail"].ToString(): "" },
                    {"services", data.ContainsKey("services") ? data["services"]: new List<string>() },
                    {"vehicleModel", data.ContainsKey("vehicleModel") ? data["vehicleModel"]: "" },
                    {"vehicleRegistrationNumber", data.ContainsKey("vehicleRegistrationNumber") ? data["vehicleRegistrationNumber"]: "" },

                };
                if (documentSnapshot.Id == selectedOrderID)
                {
                    foundOrder = newOrder;
                }
            }

            if(foundOrder == null)
            {
                MessageBox.Show("Something went wrong!");
            }
            else
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.order = foundOrder;
                this.Hide();
                orderDetails.Show();
            }
        }
    }
}
