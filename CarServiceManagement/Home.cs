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
            DateTime now = DateTime.Now;
            DateTime startOfDay = new DateTime(now.Year, now.Month,now.Day, 0, 0, 0, 0);

            CollectionReference orderCollection = db.Collection("orders");
            QuerySnapshot orderSnapshot = await orderCollection.WhereGreaterThanOrEqualTo("addedOn", startOfDay).OrderBy("addedOn").GetSnapshotAsync();
        }

        private void fetchPastOrders() { }

        private void add_order_btn_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder();
            this.Hide();
            addOrder.Show();
        }
    }
}
