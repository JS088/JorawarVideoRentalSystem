using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JorawarVideoRentalSystem
{
    public partial class Form1 : Form
    {

        // object of the clases that are used in the database connection of the c# to sql server
        SqlConnection sqlConn;
        String connection_String = "Data Source=DESKTOP-HKD1BEO\\SQLEXPRESS;Initial Catalog=JVideoDatabase;Integrated Security=True";
        SqlCommand sqlCmd;
        SqlDataReader sqlDatareader;
        fetch sqlSelect = new fetch();

        int RentID = 0;

        int videoData = 0, CustomerData = 0, RentData = 0;

        int copy = 0, cost = 0;

        public void empty() {

            Jtitle.Text = "";
            JRatting.Text = "";
            JPlot.Text = "";
            JGenre.Text = "";
            JYear.Value = 0;
            JCopies.Value = 0;
            JCost.Value = 0;

            JCustomerID.Text = "";
            JName.Text = "";
            JLName.Text = "";
            JMobileNo.Value = 0;
            JAddress.Text = "";

            JCustomerID.Text = "";
            JMovieID.Text = "";
            RentID = 0;

            videoData = 0; CustomerData = 0; RentData = 0;

            copy = 0; cost = 0;
            DateTime Current_date = DateTime.Now;

            jIssueDate.Text = Current_date.ToString("MM/dd/yyyy");
            JReturnDate.Text = Current_date.ToString("MM/dd/yyyy");
        }
        public Form1()
        {
            InitializeComponent();
            DateTime Current_date = DateTime.Now;

            jIssueDate.Text =Current_date.ToString("MM/dd/yyyy");
            JReturnDate.Text = Current_date.ToString("MM/dd/yyyy");
        }
        public int checkVideoData() {
            if (Jtitle.Text.ToString().Equals("") && JRatting.Text.ToString().Equals("") && JYear.Value == 0 && JCost.Value == 0 && JCopies.Value == 0 && JPlot.Text.ToString().Equals("") && JGenre.Text.ToString().Equals(""))
            {
                return 0;
            }
            else {
                return 1;
             }
        }
        public int checkCustomerData() {
            if (JName.Text.ToString().Equals("") && JLName.Text.ToString().Equals("") && JMobileNo.Value == 0 && JAddress.Text.ToString().Equals(""))
            {
                return 0;
            }
            else {
                return 1;
            }
        }

        public int checkRentData() {
            if (JCustomerID.Text.ToString().Equals("") && JMovieID.Text.ToString().Equals(""))
            {
                return 0;
            }
            else {
                return 1;
            }
        }
        private void JAddVideo_Click(object sender, EventArgs e)
        {
            if (checkVideoData() == 1)
            {
                String qry = "insert into JVideoData(Title,Ratting,Year,Copies,Cost,Plot,Genre)values(@JTitle,@JRatting,@JYear,@JCopies,@JCost,@JPlot,@JGenre)";
                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);

                sqlCmd.Parameters.Add("@JTitle", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JRatting", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JYear", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JCopies", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JCost", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JPlot", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JGenre", System.Data.SqlDbType.VarChar);

                sqlCmd.Parameters["@JTitle"].Value = Jtitle.Text.ToString();
                sqlCmd.Parameters["@JRatting"].Value = JRatting.Text.ToString();
                sqlCmd.Parameters["@JYear"].Value = Convert.ToInt32(JYear.Text.ToString());
                sqlCmd.Parameters["@JCopies"].Value = Convert.ToInt32(JCopies.Text.ToString());
                sqlCmd.Parameters["@JCost"].Value = Convert.ToInt32(JCost.Text.ToString());
                sqlCmd.Parameters["@JPlot"].Value = JPlot.Text.ToString();
                sqlCmd.Parameters["@JGenre"].Value = JGenre.Text.ToString();

                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record");
                }
                else
                {
                    MessageBox.Show("Video data in stored in the database");
                }

            }
            else {
                MessageBox.Show("Fill all the Details related to the Video ");
            }
            empty();

        }

        private void JdelVideoCD_Click(object sender, EventArgs e)
        {
            if (JMovieID.Text.ToString().Equals(""))
            {
                MessageBox.Show("First of all select the record from the Data to delete");
            }
            else {

                DataTable tblData = new DataTable();
                tblData = sqlSelect.SelectQuery("select Count(*) from JRentData where VideoId=" + Convert.ToInt32(JMovieID.Text.ToString()) + " && ReturnDate='rent'");
                if (tblData.Rows.Count == 0)
                {
                     String qry = "delete from JVideoData where VideoId=@JMovieId";
                    sqlConn = new SqlConnection(connection_String);
                    sqlConn.Open();
                    sqlCmd = new SqlCommand(qry, sqlConn);

                    sqlCmd.Parameters.Add("@JMovieId", System.Data.SqlDbType.Int);

                    sqlCmd.Parameters["@JMovieId"].Value = Convert.ToInt32(JMovieID.Text.ToString());

                    int rowsAffected = sqlCmd.ExecuteNonQuery();
                    sqlCmd.Dispose();
                    sqlConn.Close();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Check the Record there is an error in the record");
                    }
                    else
                    {
                        MessageBox.Show("Video data in Deleted from the database");
                    }
                }
                else {
                    MessageBox.Show("Movie is already on rent so cannot delete");
                }
            }
            empty();
        }

        private void JupdateVideoCD_Click(object sender, EventArgs e)
        {
            if (checkVideoData() == 1)
            {

                String qry = "Update JVideoData set Title=@JTitle,Ratting=@JRatting,Year=@JYear,Copies=@JCopies,Cost=@JCost,Plot=@JPlot,Genre=@JGenre where VideoId=@JMovieId";

                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);

                sqlCmd.Parameters.Add("@JTitle", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JRatting", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JYear", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JCopies", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JCost", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JPlot", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JGenre", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JMovieId", System.Data.SqlDbType.Int);



                sqlCmd.Parameters["@JMovieId"].Value = Convert.ToInt32(JMovieID.Text.ToString());
                sqlCmd.Parameters["@JTitle"].Value = Jtitle.Text.ToString();
                sqlCmd.Parameters["@JRatting"].Value = JRatting.Text.ToString();
                sqlCmd.Parameters["@JYear"].Value = Convert.ToInt32(JYear.Text.ToString());
                sqlCmd.Parameters["@JCopies"].Value = Convert.ToInt32(JCopies.Text.ToString());
                sqlCmd.Parameters["@JCost"].Value = Convert.ToInt32(JCost.Text.ToString());
                sqlCmd.Parameters["@JPlot"].Value = JPlot.Text.ToString();
                sqlCmd.Parameters["@JGenre"].Value = JGenre.Text.ToString();

                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record");
                }
                else
                {
                    MessageBox.Show("Video data in Updated in the database");
                }
  
            }
            else {
                MessageBox.Show("Fill all the Details to delete the record ");
            }
            empty();

        }

        private void JaddCustomer_Click(object sender, EventArgs e)
        {
            if (checkCustomerData()==1) {

                String qry = "insert into JCustomerData(FirstName,LastName,Mobile,Address)values(@JFName,@JLName,@JMobile,@JAddress)";
                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);

                sqlCmd.Parameters.Add("@JFName", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JLName", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JMobile", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JAddress", System.Data.SqlDbType.VarChar);
                
                sqlCmd.Parameters["@JFName"].Value = JName.Text.ToString();
                sqlCmd.Parameters["@JLName"].Value =JLName.Text.ToString();
                sqlCmd.Parameters["@JMobile"].Value = JMobileNo.Value.ToString();
                sqlCmd.Parameters["@JAddress"].Value = JAddress.Text.ToString();
                
                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record");
                }
                else
                {
                    MessageBox.Show("Customer data in stored in the database ");
                }

            }

            empty();
        }

        private void JdelCustomer_Click(object sender, EventArgs e)
        {
            if (JCustomerID.Text.ToString().Equals(""))
            {
                MessageBox.Show("Select the Customer Data to delete ");
            }
            else {

                DataTable tblData = new DataTable();
                tblData = sqlSelect.SelectQuery("select Count(*) from JRentData where CustomerId=" + Convert.ToInt32(JCustomerID.Text.ToString()) + " && ReturnDate='rent'");
                if (tblData.Rows.Count == 0)
                {


                    String qry = "delete from JCustomerData where CustomerId=@JCustomerId";
                    sqlConn = new SqlConnection(connection_String);
                    sqlConn.Open();
                    sqlCmd = new SqlCommand(qry, sqlConn);

                    sqlCmd.Parameters.Add("@JCustomerId", System.Data.SqlDbType.Int);

                    sqlCmd.Parameters["@JCustomerId"].Value = Convert.ToInt32(JCustomerID.Text.ToString());

                    int rowsAffected = sqlCmd.ExecuteNonQuery();
                    sqlCmd.Dispose();
                    sqlConn.Close();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Check the Record there is an error in the record ");
                    }
                    else
                    {
                        MessageBox.Show("Customer  data is deleted in the database ");
                    }
                }
                else {
                    MessageBox.Show("Customer already have a Movie on rent so we can not delete");
                 }
            }

            empty();
        }

        private void JupdateCustomer_Click(object sender, EventArgs e)
        {
            if (checkCustomerData() == 1)
            {

                String qry = "update  JCustomerData set FirstName=@JFName,LastName=@JLName,Mobile=@JMobile,Address=@JAddress where CustomerID=@JCustomerId";
                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);

                sqlCmd.Parameters.Add("@JFName", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JLName", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JMobile", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JAddress", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JCustomerId", System.Data.SqlDbType.Int);


                sqlCmd.Parameters["@JCustomerId"].Value = Convert.ToInt32(JCustomerID.Text.ToString());
                sqlCmd.Parameters["@JFName"].Value = JName.Text.ToString();
                sqlCmd.Parameters["@JLName"].Value = JLName.Text.ToString();
                sqlCmd.Parameters["@JMobile"].Value = JMobileNo.Value.ToString();
                sqlCmd.Parameters["@JAddress"].Value = JAddress.Text.ToString();

                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record");
                }
                else
                {
                    MessageBox.Show("Customer data in stored in the database ");
                }
            }
            else {
                MessageBox.Show("selec the customer to update his  record ");
            }
            empty();
        }

        private void JBooking_Click(object sender, EventArgs e)
        {
            if (checkRentData() == 1)
            {

                DataTable tblData = new DataTable();
                tblData = sqlSelect.SelectQuery("select Count(*) from JRentData where VideoId=" + Convert.ToInt32(JMovieID.Text.ToString()) + " && ReturnDate='rent'");

                DataTable tblData1 = new DataTable();
                tblData1 = sqlSelect.SelectQuery("select Count(*) from JRentData where CustomerId=" + Convert.ToInt32(JCustomerID.Text.ToString()) + " && ReturnDate='rent'");

                if (tblData.Rows.Count < copy && tblData1.Rows.Count < 2)
                {



                    String qry = "insert into JRentData(CustomerId,VideoId,IssueDate,ReturnDate)values(@JCustomerId,@JMovieId,@JIssue,@JReturn)";
                    sqlConn = new SqlConnection(connection_String);
                    sqlConn.Open();
                    sqlCmd = new SqlCommand(qry, sqlConn);

                    sqlCmd.Parameters.Add("@JCustomerId", System.Data.SqlDbType.Int);

                    sqlCmd.Parameters.Add("@JMovieId", System.Data.SqlDbType.Int);
                    sqlCmd.Parameters.Add("@JIssue", System.Data.SqlDbType.VarChar);
                    sqlCmd.Parameters.Add("@JReturn", System.Data.SqlDbType.VarChar);


                    sqlCmd.Parameters["@JCustomerId"].Value = Convert.ToInt32(JCustomerID.Text.ToString());
                    sqlCmd.Parameters["@JMovieId"].Value = Convert.ToInt32(JMovieID.Text.ToString());

                    sqlCmd.Parameters["@JIssue"].Value = jIssueDate.Text.ToString();
                    sqlCmd.Parameters["@JReturn"].Value = "rent";

                    int rowsAffected = sqlCmd.ExecuteNonQuery();
                    sqlCmd.Dispose();
                    sqlConn.Close();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("Check the Record there is an error in the record");
                    }
                    else
                    {
                        MessageBox.Show("Rent data in stored in the database ");
                    }
                }
                else
                {
                    MessageBox.Show("you have one of two issue like all video cd are booked or may be the customer have 2 cd on rent ");
                }


            }
            else {
                MessageBox.Show("select the video for rent ");
            }
            empty();
        }

        private void JDelete_Click(object sender, EventArgs e)
        {
            if (RentID > 0)
            {
                String qry = "delete from JRentData where RentId=@JRentId";
                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);

                sqlCmd.Parameters.Add("@JRentId", System.Data.SqlDbType.Int);

                sqlCmd.Parameters["@JRentId"].Value = RentID;

                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record ");
                }
                else
                {
                    MessageBox.Show("Rental data is deleted in the database ");
                }

            }
            else {
                MessageBox.Show("Select the Record to delete ");
            }

            empty();
        }

        private void JReturn_Click(object sender, EventArgs e)
        {
            if (checkRentData() == 1)
            {
                DataTable tblData = new DataTable();
                tblData = sqlSelect.SelectQuery("select * from JVideoData where VideoId=" + Convert.ToInt32(JMovieID.Text.ToString()) + "");
                cost = Convert.ToInt32(tblData.Rows[0]["Cost"].ToString());

                DateTime start = Convert.ToDateTime(jIssueDate.Text.ToString());
                DateTime end = DateTime.Now;



                TimeSpan difference = end - start; //create TimeSpan object
                int days = difference.Days;
                if (difference.Days == 0)
                {
                    days = 1;
                }




                String qry = "update JRentData set CustomerId=@JCustomerId,VideoId=@JMovieId,IssueDate=@JIssue,ReturnDate=@JReturn where RentId=@JRentId";
                sqlConn = new SqlConnection(connection_String);
                sqlConn.Open();
                sqlCmd = new SqlCommand(qry, sqlConn);


                sqlCmd.Parameters.Add("@JCustomerId", System.Data.SqlDbType.Int);

                sqlCmd.Parameters.Add("@JMovieId", System.Data.SqlDbType.Int);
                sqlCmd.Parameters.Add("@JIssue", System.Data.SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@JReturn", System.Data.SqlDbType.VarChar);

                sqlCmd.Parameters.Add("@JRentId", System.Data.SqlDbType.Int);

                sqlCmd.Parameters["@JRentId"].Value = RentID;

                sqlCmd.Parameters["@JCustomerId"].Value = Convert.ToInt32(JCustomerID.Text.ToString());
                sqlCmd.Parameters["@JMovieId"].Value = Convert.ToInt32(JMovieID.Text.ToString());

                sqlCmd.Parameters["@JIssue"].Value = jIssueDate.Text.ToString();
                sqlCmd.Parameters["@JReturn"].Value = JReturn.Text.ToString();

                int rowsAffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                sqlConn.Close();
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Check the Record there is an error in the record");
                }
                else
                {
                    MessageBox.Show("Video is returned to the database and your Charges are =$" + cost * days);
                }


            }
            else {
                MessageBox.Show("select the rental record to return ");
            }
            empty();
        }

        private void JVideo_Click(object sender, EventArgs e)
        {
            DataTable tblData = new DataTable();
            tblData=sqlSelect.SelectQuery("select * from JVideoData");
            database.DataSource = tblData;
            videoData = 1;
        }

        private void JCopies_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;

                int year = date.Year;

                int yr =year-Convert.ToInt32(JYear.Value);
                if (yr >= 5) {
                    JCost.Value = 2;
                }
                else if (yr>=0 && yr<5) {
                    JCost.Value = 5;
                }
            }
            catch (Exception ex) {
                    
            }
        }

        private void JTopMovie_Click(object sender, EventArgs e)
        {

            DataTable tblData = new DataTable();
            tblData = sqlSelect.SelectQuery("select * from JVideoData ");
            int x = 0, y = 0,cunt=0;
            String Title = "";
            for (x=0;x<tblData.Rows.Count;x++) {
                DataTable tblData1 = new DataTable();
                tblData1 = sqlSelect.SelectQuery("select count(*) from JRentData where VideoId="+Convert.ToInt32(tblData.Rows[x]["VideoId"].ToString())+"");

                if (tblData1.Rows.Count>cunt) {
                    Title = tblData.Rows[x]["Title"].ToString();
                    cunt = tblData1.Rows.Count;
                }

            }
            MessageBox.Show(" the Most favourite Video is =" + Title);
            empty();
        }

        private void JTopCustomer_Click(object sender, EventArgs e)
        {
            DataTable tblData = new DataTable();
            tblData = sqlSelect.SelectQuery("select * from JCustomerData ");
            int x = 0, y = 0, cunt = 0;
            String Name= "";
            for (x = 0; x < tblData.Rows.Count; x++)
            {
                DataTable tblData1 = new DataTable();
                tblData1 = sqlSelect.SelectQuery("select count(*) from JRentData where CustomerId=" + Convert.ToInt32(tblData.Rows[x]["CustomerId"].ToString()) + "");
           
                if(tblData1.Rows.Count > cunt)
                {
                    Name = tblData.Rows[x]["FirstName"].ToString()+" "+ tblData.Rows[x]["LastName"].ToString();
                    cunt = tblData1.Rows.Count;
                }
            }
            MessageBox.Show(" the Top Customer Name is =" + Name);
            empty();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void database_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CustomerData==1) {
                JCustomerID.Text= database.CurrentRow.Cells[0].Value.ToString();
                JName.Text = database.CurrentRow.Cells[1].Value.ToString();
                JLName.Text = database.CurrentRow.Cells[2].Value.ToString();
                JMobileNo.Value = Convert.ToInt32(database.CurrentRow.Cells[3].Value.ToString());
                JAddress.Text = database.CurrentRow.Cells[4].Value.ToString();
                CustomerData = 0;
            }

           else if (videoData==1) {
                JMovieID.Text= database.CurrentRow.Cells[0].Value.ToString();
                Jtitle.Text = database.CurrentRow.Cells[1].Value.ToString();
                JRatting.Text = database.CurrentRow.Cells[2].Value.ToString();
                JYear.Value = Convert.ToInt32(database.CurrentRow.Cells[3].Value.ToString());

                JCost.Value = Convert.ToInt32(database.CurrentRow.Cells[4].Value.ToString());
                cost = Convert.ToInt32(database.CurrentRow.Cells[4].Value.ToString());

                JCopies.Value = Convert.ToInt32(database.CurrentRow.Cells[5].Value.ToString());
                copy = Convert.ToInt32(database.CurrentRow.Cells[5].Value.ToString());

                JPlot.Text= database.CurrentRow.Cells[6].Value.ToString();
                JGenre.Text= database.CurrentRow.Cells[7].Value.ToString();
                videoData = 0;
            }
           else if (RentData==1) {

                RentID = Convert.ToInt32(database.CurrentRow.Cells[0].Value.ToString());
                JCustomerID.Text= database.CurrentRow.Cells[1].Value.ToString();
                JMovieID.Text= database.CurrentRow.Cells[2].Value.ToString();
                jIssueDate.Text= database.CurrentRow.Cells[3].Value.ToString();
                JReturnDate.Text= database.CurrentRow.Cells[4].Value.ToString();
                RentData = 0;
            }



        }

        private void JCustomer_Click(object sender, EventArgs e)
        {

            DataTable tblData = new DataTable();
            tblData = sqlSelect.SelectQuery("select * from JCustomerData");
            database.DataSource = tblData;
            CustomerData = 1;
        }

        private void JRent_Click(object sender, EventArgs e)
        {

            DataTable tblData = new DataTable();
            tblData = sqlSelect.SelectQuery("select * from JRentData");
            database.DataSource = tblData;
            RentData = 1;
        }
    }
}
