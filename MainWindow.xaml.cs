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
using System.Collections.ObjectModel;

namespace PhoneBDAutumn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class StudentInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Dadname { get; set; }
            public string Group { get; set; }
            public int Mphone { get; set; }
            public int Hphone { get; set; }
            public string Adress { get; set; }
        }
        public class PhoneInfo
        {
            public int Id { get; set; }
            public int Id_Stud { get; set; }
            public int Id_Type { get; set; }
            public long Phone { get; set; }

        }

        public static ObservableCollection<StudentInfo> studInfo = new ObservableCollection<StudentInfo>();// коллекция студака
        public static ObservableCollection<PhoneInfo> phoneInfo = new ObservableCollection<PhoneInfo>();//коллекция мобилок

        private void Refresh()
        {
            MainWindow.studInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM studTables";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов

                    while (reader.Read()) // построчно считываем данные
                    {
                        StudentInfo fileInfo = new StudentInfo();
                        fileInfo.Id = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.Name = Convert.ToString(reader.GetValue(1));
                        fileInfo.Surname = Convert.ToString(reader.GetValue(2));
                        fileInfo.Dadname = Convert.ToString(reader.GetValue(3));
                        fileInfo.Group = Convert.ToString(reader.GetValue(4));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(5));
                        //FileInfoView.ItemsSource = MainWindow.studInfo;
                        studInfo.Add(fileInfo);
                    }

                }

                reader.Close();


            }
        }

        private void RefreshPhones()
        {
            MainWindow.phoneInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM phoneTable";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        PhoneInfo phonik = new PhoneInfo();
                        phonik.Id = Convert.ToInt32(reader.GetValue(0));
                        phonik.Id_Stud = Convert.ToInt32(reader.GetValue(0));
                        phonik.Id_Type = Convert.ToInt32(reader.GetValue(0));
                        phonik.Phone = Convert.ToInt64(reader.GetValue(0));

                        phoneInfo.Add(phonik);
                    }

                }

                reader.Close();

            }
        }

        public MainWindow()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";

            // Создание подключения
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                //MessageBox.Show("Подключение открыто");
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
               // MessageBox.Show("Подключение закрыто...");
            }
            Refresh();
            RefreshPhones();
            InitializeComponent();
            Page1Adder page1 = new Page1Adder();
            page1.frame = MainFrame;
            MainFrame.Content = page1;

        }
    }
}
