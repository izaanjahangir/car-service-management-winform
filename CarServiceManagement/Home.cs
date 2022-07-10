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
            fetchPastOrders();
        }

        private async void fetchTodayOrders() {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                startOfDay = DateTime.SpecifyKind(startOfDay, DateTimeKind.Utc);
                CollectionReference orderCollection = db.Collection("orders");
                QuerySnapshot orderSnapshot = await orderCollection.WhereGreaterThanOrEqualTo("addedOn", startOfDay).OrderBy("addedOn").GetSnapshotAsync();

                this.today_grid_view.Columns.Add("Vehicle Make", "Vehicle Make");
                this.today_grid_view.Columns.Add("Vehicle Name", "Vehicle Name");
                this.today_grid_view.Columns.Add("Owner Name", "Owner Name");
                this.today_grid_view.Columns.Add("Owner Contact", "Owner Contact");
                this.today_grid_view.Columns.Add("Request Type", "Request Type");
                this.today_grid_view.Columns.Add("Total Cost", "Total Cost");

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
                    this.today_grid_view.Rows.Add(order["make"], order["vehicleName"], order["ownerName"], order["ownerContact"], order["requestType"], order["totalCost"]);

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private async void fetchPastOrders() {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                startOfDay = DateTime.SpecifyKind(startOfDay, DateTimeKind.Utc);
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

        private void add_order_btn_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            this.Hide();
            addOrder.Show();
        }
    }
}
