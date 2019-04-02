using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Data;

namespace CustomerDataBase
{
    public static class WorkWithData
    {
        public static void InsertData(SqlConnection connection,string name,string lastName,DateTime dt, string passportSeries,int phoneNumber1,string passportSeries2 = null,int? phonenumber2 =null)
        {
            string sql = $"insert into Customer Values('{name}','{lastName}','{dt.ToShortTimeString()}','{passportSeries}','{passportSeries2}','{phoneNumber1}','{phonenumber2}')";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                System.Windows.MessageBox.Show("Տվյալը հաջողությամբ մուտքագրվել է");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Բազայի հետ հաղորդակցվելիս տեղի է ունեցել սխալ", "ուշադրություն");
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        public static void ShowDB(SqlConnection connection, DataGrid dg)
        {
            string sql = "select * from Customer";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dg.ItemsSource = dt.DefaultView;
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Բազայի հետ հաղորդակցվելիս տեղի է ունեցել սխալ","ուշադրություն" );
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteFromDB(SqlConnection connection,TextBox tx)
        {
            string sql = $"delete from Customer where PassportID = '{tx.Text}'";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                System.Windows.MessageBox.Show("Տվյալը հաջողությամբ Հեռացվել է");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Բազայի հետ հաղորդակցվելիս տեղի է ունեցել սխալ", "ուշադրություն");
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void UpdateFromDB(SqlConnection connection, ComboBox field,TextBox passID,TextBox newValue)
        {
            string sql = $"update Customer set  {field.SelectedItem.ToString()} = ('{newValue.Text}') where PassportID = '{passID.Text}'";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                System.Windows.MessageBox.Show("Տվյալը հաջողությամբ Թարմացվել է");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Բազայի հետ հաղորդակցվելիս տեղի է ունեցել սխալ", "ուշադրություն");
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void SelectWhere(SqlConnection connection,ComboBox field , TextBox search,DataGrid dg)
        {
            string sql = $"select * from Customer where {field.SelectedItem.ToString()} = ('{search.Text}')";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dg.ItemsSource = dt.DefaultView;
                connection.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Բազայի հետ հաղորդակցվելիս տեղի է ունեցել սխալ", "ուշադրություն");
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
