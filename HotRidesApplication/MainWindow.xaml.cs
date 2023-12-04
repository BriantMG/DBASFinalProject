using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace HotRidesApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentUser = null;

        private static string GetConnectionString()
        {
            return Properties.Resources.ConnectionString;
        }
        public MainWindow()
        {
            InitializeComponent();
            filEventsCombo();
            fillRegCombo();

            dgvVehicle.ItemsSource = MyDatabase.GetVehicles();
            dgvReg.ItemsSource = MyDatabase.GetRegistries();

            
        }

        void filEventsCombo() {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM events";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string location = dr.GetString(1);
                    cmbEvent.Items.Add(location);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fillRegCombo()
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM registrations";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string name = dr.GetString(1);
                    cmbRegId.Items.Add(name) ;
                    
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ClearRegEntry()
        {
            tbFName.Text = string.Empty;
            tbLName.Text = string.Empty;
            tbRegId.Text = string.Empty;    
            tbClub.Text = string.Empty;
            tbPayment.Text = string.Empty;
            rdoSmall.IsChecked = false;
            rdoMedium.IsChecked = false;
            rdoLarge.IsChecked = false;

        }

        void ClearVEntries()
        {
            tbVID.Text = string.Empty;
            tbVMake.Text = string.Empty;
            tbVModel.Text = string.Empty;
            tbVYear.Text = string.Empty;

        }


        private void btnCreateReg_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"INSERT INTO registrations (firstname, lastname, payment, payment_type, eventid, shirt_size, club ) " +
                "VALUES (@firstname, @lastname, @payment, @payment_type, @eventid, @shirt_size, @club)";
            command.Parameters.AddWithValue("@firstname", tbFName.Text);
            command.Parameters.AddWithValue("@lastname", tbLName.Text);
            command.Parameters.AddWithValue("@payment", tbPayment.Text);
            command.Parameters.AddWithValue("@payment_type", cmbPaymentType.Text);
            command.Parameters.AddWithValue("@eventid", getEventId(cmbEvent.SelectedItem.ToString()));
            if (rdoSmall.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Small");
            }
            if (rdoMedium.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Medium");
            }
            if (rdoLarge.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Large");
            }
            command.Parameters.AddWithValue("@club", tbClub.Text);
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);
            ClearRegEntry();
        }

        private void main_Loaded(object sender, RoutedEventArgs e)
        {
            
            dgvReg.ItemsSource = MyDatabase.GetRegistries();
            dgvVehicle.ItemsSource = MyDatabase.GetVehicles();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            tbFName.Text = "Zach";
            tbLName.Text = "Elhayek";
            tbPayment.Text = "2400.00";
            cmbPaymentType.SelectedIndex = 0;
            cmbEvent.SelectedIndex = 0;
            rdoLarge.IsChecked = true;
            tbClub.Text = "TestClub";
        }

        private void dgvReg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                tbRegId.Text = dr["Reg ID"].ToString();
                tbFName.Text = dr["First Name"].ToString();
                tbLName.Text = dr["Last Name"].ToString();
                tbPayment.Text = dr["Payment"].ToString();
                cmbPaymentType.Text = dr["Payment Type"].ToString();
                cmbEvent.Text = getEventLocation(Int32.Parse(dr["Event ID"].ToString()));
                if (dr["Shirt Size"].ToString() == "Small")
                {
                    rdoSmall.IsChecked = true;
                }
                if (dr["Shirt Size"].ToString() == "Medium")
                {
                    rdoMedium.IsChecked = true;
                }
                if (dr["Shirt Size"].ToString() == "Large")
                {
                    rdoLarge.IsChecked = true;
                }
                tbClub.Text = dr["Club"].ToString();
            }
        }

        private string getEventLocation(int id) {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM events";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetInt32(0) == id)
                    {
                        return dr.GetString(1);
                    }
                   
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private int getEventId(string location)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM events";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(1) == location)
                    {
                        return dr.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }


        private void lblUpdateReg_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"UPDATE registrations SET firstname=@firstname, lastname=@lastname, payment=@payment, payment_type=@payment_type, eventid=@eventid, shirt_size=@shirt_size, club=@club" +
                " WHERE regid=@regid";
            command.Parameters.AddWithValue("@regid", tbRegId.Text);
            command.Parameters.AddWithValue("@firstname", tbFName.Text);
            command.Parameters.AddWithValue("@lastname", tbLName.Text);
            command.Parameters.AddWithValue("@payment", tbPayment.Text);
            command.Parameters.AddWithValue("@payment_type", cmbPaymentType.Text);
            command.Parameters.AddWithValue("@eventid", getEventId(cmbEvent.SelectedItem.ToString()));
            if (rdoSmall.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Small");
            }
            if (rdoMedium.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Medium");
            }
            if (rdoLarge.IsChecked == true)
            {
                command.Parameters.AddWithValue("@shirt_size", "Large");
            }
            command.Parameters.AddWithValue("@club", tbClub.Text);
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);
            ClearRegEntry();
        }

        private void lblDeleteReg_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"DELETE FROM registrations WHERE regid=@regid";
            command.Parameters.AddWithValue("@regid", tbRegId.Text);

            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);

        }

        private void dgvVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                tbVID.Text = dr["Vehicle ID"].ToString();
                tbVMake.Text = dr["Make"].ToString();
                tbVModel.Text = dr["Model"].ToString();
                tbVYear.Text = dr["Year"].ToString();
                cmbRegId.Text = getRegName(Int32.Parse(dr["Attendee ID"].ToString()));
            }
        }

        private void btnCreateV_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"INSERT INTO vehicle (make, model, year, regid) " +
                "VALUES (@make, @model, @year, @regid)";
            command.Parameters.AddWithValue("@make", tbVMake.Text);
            command.Parameters.AddWithValue("@model", tbVModel.Text);
            command.Parameters.AddWithValue("@year", tbVYear.Text);
            command.Parameters.AddWithValue("@regid", getRegId(cmbRegId.SelectedItem.ToString()));
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);
            ClearVEntries();
        }

        private void lblUpdateV_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"UPDATE vehicle SET make=@make, model=@model, year=@year, regid=@regid" +
                " WHERE vehicleId=@vehicleId";
            command.Parameters.AddWithValue("@vehicleId", tbVID.Text);
            command.Parameters.AddWithValue("@make", tbVMake.Text);
            command.Parameters.AddWithValue("@model", tbVModel.Text);
            command.Parameters.AddWithValue("@year", tbVYear.Text);
            command.Parameters.AddWithValue("@regid", getRegId(cmbRegId.SelectedItem.ToString()));
            
            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);
            ClearVEntries();
        }

        private void lblDeleteV_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection dbConnection = new SqlConnection(Properties.Settings.Default.DB_Connect);
            SqlCommand command = dbConnection.CreateCommand();

            command.CommandText = @"DELETE FROM vehicle WHERE vehicleId=@vehicleId";
            command.Parameters.AddWithValue("@vehicleId", tbVID.Text);

            dbConnection.Open();
            command.ExecuteNonQuery();
            dbConnection.Close();
            main_Loaded(sender, e);
        }



        private string getRegName(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM registrations";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetInt32(0) == id)
                    {
                        return dr.GetString(1);
                    }

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private int getRegId(string name)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM registrations";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(1) == name)
                    {
                        return dr.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        #region SignIn
        /// <summary>
        /// when the sign in button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(btnLogin.Content == "Login")
            {
                if (tbxUser.Text != "" && tbxPass.Password != "")
                {
                    string user = getUser(tbxUser.Text, tbxPass.Password);
                    if (user != null)
                    {
                        CurrentUser = user;
                        tabSign.Header = user;
                        lblUser.Visibility = Visibility.Hidden;
                        lblPass.Visibility = Visibility.Hidden;
                        tbxUser.Visibility = Visibility.Hidden;
                        tbxPass.Visibility = Visibility.Hidden;
                        btnLogin.Content = "Logout";
                        tabController.SelectedIndex = 1;
                    }
                }
            }
            else
            {
                CurrentUser = null;
                tabSign.Header = "Sign In";
                lblUser.Visibility = Visibility.Visible;
                lblPass.Visibility = Visibility.Visible;
                tbxUser.Visibility = Visibility.Visible;
                tbxPass.Visibility = Visibility.Visible;
                btnLogin.Content = "Login";
            }
        }

        private string getUser(string username, string password)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

            try
            {
                sqlConnection.Open();
                string query = "SELECT * FROM users";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(1).ToLower() == username.ToLower())
                    {
                        if(dr.GetString(2) == password)
                        {
                            return dr.GetString(1);
                        }
                    }

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }


        #endregion

        private void tabController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentUser == null && tabController.SelectedIndex != 1)
            {
                tabController.SelectedIndex = 0;
            }
        }
    }
}
