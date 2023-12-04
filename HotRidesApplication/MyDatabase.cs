using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace HotRidesApplication
{

    public partial class MyDatabase
    {
        private static string GetConnectionString()
        {
            return Properties.Resources.ConnectionString;
        }

        /// <summary>
        /// adds a registry
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="LName"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="paymentType"></param>
        /// <param name="shirtSize"></param>
        /// <param name="club"></param>
        /// <param name="eventID"></param>
        public static void AddRegistry(string FName, string LName, double paymentAmount, string paymentType, string shirtSize, string club, int eventID)
        {

            SqlConnection dbCon = new SqlConnection(GetConnectionString());
            SqlCommand command = dbCon.CreateCommand();
            command.CommandText = @"INSERT INTO registrations (firstname, lastname, payment, payment_type, eventid, shirt_size, club ) " +
                "VALUES (@firstname, @lastname, @payment, @payment_type, @eventid, @shirt_size, @club)";
            command.Parameters.AddWithValue("@firstname", FName);
            command.Parameters.AddWithValue("@lastname", LName);
            command.Parameters.AddWithValue("@payment", paymentAmount);
            command.Parameters.AddWithValue("@paymentType", paymentType);
            command.Parameters.AddWithValue("@eventid", eventID);


           
        }

        /// <summary>
        /// Gets all registries
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetRegistries()
        {
            string GetPatientsQuery = "SELECT  regid AS 'Reg ID', firstname AS 'First Name', lastname AS 'Last Name', payment AS 'Payment', payment_type AS 'Payment Type', eventid AS 'Event ID', shirt_size AS 'Shirt Size', club AS 'Club'  FROM registrations;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "registrations");
            return ds.Tables["registrations"].DefaultView;
        }

        /// <summary>
        /// deletes the registry of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteRegistry(int id)
        {
            string DeletePatientsQuery = "DELETE FROM registrations WHERE regid = "+id+";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// adds a user to the db
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        public static void AddUser(string username, string password, string type)
        {
            string UpdateNotesQuery = "INSERT INTO users (username, password, type) " +
                "VALUES ('" + username + "', '" + password + "', '" + type + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// gets a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static object?[] GetUsers(string username)
        {
            string GetPatientsQuery = "SELECT * FROM users WHERE username = '"+username+"';";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "users");
            return ds.Tables["users"].Rows[0].ItemArray;
        }

        /// <summary>
        /// adds a vehicle
        /// </summary>
        /// <param name="FName"></param>
        /// <param name="LName"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="paymentType"></param>
        /// <param name="shirtSize"></param>
        /// <param name="club"></param>
        /// <param name="eventID"></param>
        public static void AddVehicle(string make, string model, string year, int registradionID)
        {
            string UpdateNotesQuery = "INSERT INTO vehicles (vehicle_make, vehicle_model, vehicle_year, regID) " +
                "VALUES ('" + make + "', '" + model + "', '" + year + "', '" + registradionID + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets all vehicles
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetVehicles()
        {
            string GetPatientsQuery = "SELECT vehicleId as 'Vehicle ID', make AS 'Make', model AS 'Model', year AS 'Year', regid AS 'Attendee ID' FROM vehicle;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "vehicle");
            return ds.Tables["vehicle"].DefaultView;
        }

        /// <summary>
        /// deletes the vehicle of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteVehicle(int id)
        {
            string DeletePatientsQuery = "DELETE FROM vehicles WHERE vehicleID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// adds a donation
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="registradionID"></param>
        public static void AddDonation(string type, string value, int supporterID)
        {
            string UpdateNotesQuery = "INSERT INTO donations (donation_type, cash_value, supporterID) " +
                "VALUES ('" + type + "', '" + value + "', '" + supporterID + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets all donations
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetDonations()
        {
            string GetPatientsQuery = "SELECT donationID as 'Donation ID', dontaiont_type AS 'Type', cash_value AS 'Cash Value', supporterID AS 'Supporter' FROM donations;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "donations");
            return ds.Tables["donations"].DefaultView;
        }

        /// <summary>
        /// deletes the donation of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteDonation(int id)
        {
            string DeletePatientsQuery = "DELETE FROM donations WHERE donationID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// adds a supporter
        /// </summary>
        /// <param name="sponsor"></param>
        /// <param name="vendor"></param>
        /// <param name="paymentfees"></param>
        public static void AddSupporter(string sponsor, string vendor, double paymentfees)
        {
            string UpdateNotesQuery = "INSERT INTO supporters (sponsor, vendor, payment_fees) " +
                "VALUES ('" + sponsor + "', '" + vendor + "', '" + paymentfees + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets all supporters
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetSupporters()
        {
            string GetPatientsQuery = "SELECT supporterID as 'Donation ID', sponsor AS 'Sponsor', vendor AS 'Vendor', payment_fees AS 'Payment Fees' FROM supporters;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "supporters");
            return ds.Tables["supporters"].DefaultView;
        }

        /// <summary>
        /// deletes the supporter of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteSupporter(int id)
        {
            string DeletePatientsQuery = "DELETE FROM supporters WHERE supporterID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /*
        public static void AddEvent(string location, double regprice, double weekprice, double dayprice, DateTime start, DateTime end, )
        {
            string UpdateNotesQuery = "INSERT INTO supporters (sponsor, vendor, payment_fees) " +
                "VALUES ('" + sponsor + "', '" + vendor + "', '" + paymentfees + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        */

        // Not sure what we want to do about "AVailable prizes, like a list of prize id's? "

        /*
        public static DataView GetEvents()
        {
            string GetPatientsQuery = "SELECT supporterID as 'Donation ID', sponsor AS 'Sponsor', vendor AS 'Vendor', payment_fees AS 'Payment Fees' FROM supporters;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "supporters");
            return ds.Tables["supporters"].DefaultView;
        }
        */

        /// <summary>
        /// deletes the event of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteEvent(int id)
        {
            string DeletePatientsQuery = "DELETE FROM events WHERE eventID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// adds a prize
        /// </summary>
        /// <param name="category"></param>
        public static void AddPrize(string category)
        {
            string UpdateNotesQuery = "INSERT INTO prizes (category) " +
                "VALUES ('" + category + "')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets all prizes
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetPrizes()
        {
            string GetPatientsQuery = "SELECT prizeID as 'Prize ID', category AS 'Category' FROM prizes;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "prizes");
            return ds.Tables["prizes"].DefaultView;
        }

        /// <summary>
        /// deletes the prize of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeletePrize(int id)
        {
            string DeletePatientsQuery = "DELETE FROM prizes WHERE prizeID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// adds a winner
        /// </summary>
        /// <param name="category"></param>
        public static void AddWinner(int regid, string position, string classstr)
        {
            string UpdateNotesQuery = "INSERT INTO winners (regID, position, class) " +
                "VALUES ('" + regid + "', '"+position+"', '"+classstr+"')";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(UpdateNotesQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets all winners
        /// </summary>
        /// <returns>DataView for Datagrid</returns>
        public static DataView GetWinners()
        {
            string GetPatientsQuery = "SELECT winnerID as 'Winner ID', regID AS 'Registered Member', position AS 'Position', class AS 'Class' FROM winners;";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlDataAdapter da = new SqlDataAdapter(GetPatientsQuery, sqlConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "prizes");
            return ds.Tables["prizes"].DefaultView;
        }

        /// <summary>
        /// deletes the winner of a certain ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void DeleteWinner(int id)
        {
            string DeletePatientsQuery = "DELETE FROM winners WHERE winnerID = " + id + ";";
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString());
            SqlCommand command = new SqlCommand(DeletePatientsQuery, sqlConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}