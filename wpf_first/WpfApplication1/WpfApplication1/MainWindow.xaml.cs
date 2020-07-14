using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;



namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            binddatagrid();
        }
        private void binddatagrid()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            //stringBuilder["Server"]     = "VODINHNGHIA\SQLEXPRESS";
            stringBuilder["Database"] = "Chatbot";
            stringBuilder["User Id"] = "sa";
            stringBuilder["Password"] = "nghia123";

            string sqlConnectStr = stringBuilder.ToString();

            using (SqlConnection connection = new SqlConnection(sqlConnectStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from [Noidung]";
                cmd.Connection = connection;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Noidung");
                da.Fill(dt);
                g1.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }
  
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbl_date.Content = DateTime.Now;
            lbl_tbox_contend.Text = tbox1.Text;
            string sqltext = "select * from [Noidung]";
            SqlConnection sqlcon = new SqlConnection(@"Data Source=VODINHNGHIA\SQLEXPRESS;Initial Catalog=Chatbot;Integrated Security=True");
            SqlCommand command = new SqlCommand(sqltext, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = new SqlCommand("INSERT INTO Noidung VALUES(@Noidung,@Ngaydang)", sqlcon);
            da.InsertCommand.Parameters.Add("@Noidung", SqlDbType.NVarChar).Value = tbox1.Text;
            da.InsertCommand.Parameters.Add("@Ngaydang", SqlDbType.DateTime).Value = DateTime.Now;
            sqlcon.Open();
            da.InsertCommand.ExecuteNonQuery();
            sqlcon.Close();
            binddatagrid();
            MessageBox.Show("Them thanh cong");
            
        }

        private void btn_sua_Click(object sender, RoutedEventArgs e)
        {
            string sqltext = "select * from [Noidung]";
            SqlConnection sqlcon = new SqlConnection(@"Data Source=VODINHNGHIA\SQLEXPRESS;Initial Catalog=Chatbot;Integrated Security=True");
            SqlCommand command = new SqlCommand(sqltext, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter();
            //string nn = tbox_contend_sua.Text;
            da.UpdateCommand = new SqlCommand("UPDATE Noidung SET Noi_dung = @noidung WHERE Noi_dung=@nd", sqlcon);
            da.UpdateCommand.Parameters.Add("@noidung", SqlDbType.NVarChar).Value = tbox1.Text;
            da.UpdateCommand.Parameters.Add("@nd", SqlDbType.NVarChar).Value = tbox_contend_sua.Text;
            //da.UpdateCommand.Parameters.Add("@ngaydang", SqlDbType.DateTime).Value = DateTime.Now;
            sqlcon.Open();
            da.UpdateCommand.ExecuteNonQuery();
            sqlcon.Close();
            binddatagrid();
            MessageBox.Show("Sua thanh cong");
        }

        private void btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            string sqltext = "select * from [Noidung]";
            SqlConnection sqlcon = new SqlConnection(@"Data Source=VODINHNGHIA\SQLEXPRESS;Initial Catalog=Chatbot;Integrated Security=True");
            SqlCommand command = new SqlCommand(sqltext, sqlcon);
            SqlDataAdapter da = new SqlDataAdapter();
            da.DeleteCommand = new SqlCommand("DELETE FROM Noidung WHERE Noi_dung = @Noidung", sqlcon);
            da.DeleteCommand.Parameters.Add("@Noidung", SqlDbType.NVarChar).Value = tbox1.Text;
            sqlcon.Open();
            da.DeleteCommand.ExecuteNonQuery();
            sqlcon.Close();
            binddatagrid();
            MessageBox.Show("Xoa thanh cong");
        }

        private void g1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if(dr!=null)
            {
                tbox_contend_sua.Text = dr["Noi_dung"].ToString();
                tbox1.Text = dr["Noi_dung"].ToString();
                lbl_date.Content = dr["Ngay_dang"].ToString();
                btn1.IsEnabled = true;
                btn_xoa.IsEnabled = true;
                btn_sua.IsEnabled = true;
            }
        }
        

    }
}
    