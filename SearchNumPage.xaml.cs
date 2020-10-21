using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PhoneBDAutumn
{
    /// <summary>
    /// Логика взаимодействия для SearchNumPage.xaml
    /// </summary>
    public partial class SearchNumPage : Page
    {
        public class PhoneSearchInfo
        {
            public long Phone { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }

        }

        //public static ObservableCollection<phoneSrchInfo> phoneSrchInfo = new ObservableCollection<phoneSrchInfo>();//коллекция поисковика

        public SearchNumPage()
        {
            InitializeComponent();
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            MainWindow.studInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = execcomm;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        PhoneSearchInfo searchInfo = new PhoneSearchInfo();
                        searchInfo.Phone = Convert.ToInt32(reader.GetValue(0));
                        searchInfo.Name = Convert.ToString(reader.GetValue(1));
                        searchInfo.Surname = Convert.ToString(reader.GetValue(2));
                        FileInfoView.ItemsSource = MainWindow.studInfo;
                        //MainWindow.studInfo.Add(searchInfo);
                    }

                }

                reader.Close();

            }
        }

        private void FindByPhone(object sender, TextChangedEventArgs e)
        {
            FindLike("SELECT phoneTable.Phone, studTables.Name, studTables.Surname FROM phoneTable INNER JOIN studTables ON phoneTable.Id_Stud = studTables.Id WHERE phoneTable.Phone LIKE '"+ phoneBox.Text +"%'");
        }
    }
}
