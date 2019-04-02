using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace CustomerDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum Fields
    {
        FirstName,
        LastName,
        Birthday,
        PassportID,
        Passport2ID,
        PhoneNumber1,
        PhoneNumber2
    }

    public partial class MainWindow : Window
    {
        SqlConnectionStringBuilder cnStringBuilder = new SqlConnectionStringBuilder
        {
            InitialCatalog = "BankAccounts",
            DataSource = @"RICHTOSHIBA",
            ConnectTimeout = 30,
            IntegratedSecurity = true
        };

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 7; i++)
            {
                FieldsComboBox.Items.Add((Fields)i);
                FieldsSearchComboBox.Items.Add((Fields)i);
            }
            
        }

        private void InsertIntoDBButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text == "" || SurnameTextBox.Text == "" 
                || Phone1TextBox.Text == "" || Passport1TextBox.Text == "" || Birthday.Text == "")
            {
                MessageBox.Show("'*'-ով նշված դաշտերը չեն կարող լինել դատարկ", "Ուշադրություն");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = cnStringBuilder.ConnectionString;
                    //connection.Open();
                    WorkWithData.InsertData(connection, NameTextBox.Text, SurnameTextBox.Text, Birthday.DisplayDate, Passport1TextBox.Text,int.Parse(Phone1TextBox.Text),Phone2TextBox.Text);
                    NameTextBox.Text = "";
                    SurnameTextBox.Text = "";
                    Birthday.Text = "";
                    Passport1TextBox.Text = "";
                    Passport2TextBox.Text = "";
                    Phone1TextBox.Text = "";
                    Phone2TextBox.Text = "";
                }
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = cnStringBuilder.ConnectionString;
                //connection.Open();
                WorkWithData.ShowDB(connection,DataTable);
            }
        }

        private void RemoveFromDBButton_Click(object sender, RoutedEventArgs e)
        {
            if(RemoveTextBox.Text != "" && !(RemoveTextBox.Text.StartsWith(" ")))
            {
                using(SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = cnStringBuilder.ConnectionString;
                    WorkWithData.DeleteFromDB(connection, RemoveTextBox);
                    RemoveTextBox.Text = "";
                }
            }
            else
            {
                RemoveTextBox.Text = "";
                MessageBox.Show("Մուտքագրեք Անձնգրի համարը", "Ուշադրություն");
            }
        }

        private void UpdateDBButton_Click(object sender, RoutedEventArgs e)
        {
            if(UpdateTextBox.Text != "" && NewDataInUpdateTextBox.Text != "" &&
                !(UpdateTextBox.Text.StartsWith(" ")) && !(NewDataInUpdateTextBox.Text.StartsWith(" "))
                && FieldsComboBox.SelectedIndex> -1)
            {
                using(SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = cnStringBuilder.ConnectionString;
                    WorkWithData.UpdateFromDB(connection, FieldsComboBox, UpdateTextBox, NewDataInUpdateTextBox);
                    UpdateTextBox.Text = "";
                    NewDataInUpdateTextBox.Text = "";
                    FieldsComboBox.Text = "";
                }
            }
            else
            {
                UpdateTextBox.Text = "";
                NewDataInUpdateTextBox.Text = "";
                MessageBox.Show("Մուտքագրեք բոլոր դաշտերը","Ուղադրություն");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsSearchComboBox.SelectedIndex > -1 && SearchTextBox.Text != "" 
                && !(SearchTextBox.Text.StartsWith(" ")))
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = cnStringBuilder.ConnectionString;
                    WorkWithData.SelectWhere(connection, FieldsSearchComboBox, SearchTextBox, DataTable);
                }
            }
            else
            {
                SearchTextBox.Text = "";
                MessageBox.Show("մուտքագրեք բոլոր դաշտերը", "ուղադրություն");
            }
        }

        private void Multy_Checked(object sender, RoutedEventArgs e)
        {
            Passport2TextBox.IsEnabled = true;                
        }

        private void Multy_Unchecked(object sender, RoutedEventArgs e)
        {
            Passport2TextBox.IsEnabled = false;
        }

        private void Phone1TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key > (Key)33 && e.Key < (Key)44) {}
            else if (e.Key > (Key)73 && e.Key < (Key)84) {}
            else
            {
                Phone1TextBox.Text = "";
                MessageBox.Show("Մուտքագրեք միայն թվեր", "Ուղադրություն");
            }
        }

        private void Phone2TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key > (Key)33 && e.Key < (Key)44) { }
            else if (e.Key > (Key)73 && e.Key < (Key)84) { }
            else
            {
                Phone2TextBox.Text = "";
                MessageBox.Show("Մուտքագրեք միայն թվեր", "Ուղադրություն");
            }
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(FieldsSearchComboBox.Text == "PhoneNumber1" || FieldsSearchComboBox.Text == "PhoneNumber2")
            {
                if (e.Key > (Key)33 && e.Key < (Key)44) { }
                else if (e.Key > (Key)73 && e.Key < (Key)84) { }
                else
                {
                    SearchTextBox.Text = "";
                    MessageBox.Show("Մուտքագրեք միայն թվեր", "Ուղադրություն");
                }
            }
        }

        private void NewDataInUpdateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (FieldsComboBox.Text == "PhoneNumber1" || FieldsComboBox.Text == "PhoneNumber2")
            {
                if (e.Key > (Key)33 && e.Key < (Key)44) { }
                else if (e.Key > (Key)73 && e.Key < (Key)84) { }
                else
                {
                    NewDataInUpdateTextBox.Text = "";
                    MessageBox.Show("Մուտքագրեք միայն թվեր", "Ուղադրություն");
                }
            }
        }
    }
}
