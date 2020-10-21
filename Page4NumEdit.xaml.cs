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
    /// Логика взаимодействия для Page4NumEdit.xaml
    /// </summary>
    public partial class Page4NumEdit : Page
    {
        public Frame frame;

        public static int idOfStud; //по какому айдишнику студака работаем (переданного из поисковика)

        public class PhoneOwnInfo
        {
            public int Id { get; set; }
            public int Id_Stud { get; set; }
            public int Id_Type { get; set; }
            public long Phone { get; set; }

        }

        public static ObservableCollection<PhoneOwnInfo> phoneOwnInfo = new ObservableCollection<PhoneOwnInfo>();//коллекция личной мобилки

        private void LoadPrivateInfo()
        {
            phoneOwnInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM phoneTable WHERE Id_Stud = " + idOfStud;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов

                    while (reader.Read()) // построчно считываем данные
                    {
                        PhoneOwnInfo phonerInfo = new PhoneOwnInfo();
                        phonerInfo.Id = Convert.ToInt32(reader.GetValue(0));
                        phonerInfo.Id_Stud = Convert.ToInt32(reader.GetValue(1));
                        phonerInfo.Id_Type = Convert.ToInt32(reader.GetValue(2));
                        phonerInfo.Phone = Convert.ToInt64(reader.GetValue(3));

                        phoneOwnInfo.Add(phonerInfo);
                    }

                    FileNumEdView.ItemsSource = phoneOwnInfo;
                
                }

                reader.Close();

            }
        }


        public Page4NumEdit()
        {
            
            InitializeComponent();
            LoadPrivateInfo(); // IMPORTANT THING TO LOAD THIS AFTER INIT

        }

        private void RemoveNumBtn(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            string sqlExpression = "DELETE  FROM phoneTable WHERE Id=" + phoneOwnInfo[Convert.ToInt32(FileNumEdView.SelectedIndex)].Id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Объект не найден");
                }
                else
                {
                    MessageBox.Show("Удалено объектов: " + number);
                    phoneOwnInfo.RemoveAt(FileNumEdView.SelectedIndex);
                }
            }

            btnsave.IsEnabled = false; //отрубаем для избежания бажочков
            btnrem.IsEnabled = false; //отрубаем для избежания бажочков


        }

        private void SelectionChangeSimple(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnsave.IsEnabled = true; // задействуем кнопку, дабы избежать багов
                btnrem.IsEnabled = true; // задействуем кнопку, дабы избежать багов
                txnumchange.IsEnabled = true; // задействуем текстбокс тоже
                txnumchange.Text = Convert.ToString(phoneOwnInfo[Convert.ToInt32(FileNumEdView.SelectedIndex)].Phone); // меняем номерок пафосно
                typeBox.SelectedIndex = phoneOwnInfo[Convert.ToInt32(FileNumEdView.SelectedIndex)].Id_Type - 1; //меняем комбуху пафосно
            }
            catch(ArgumentOutOfRangeException)
            {
                //MessageBox.Show("Something went wrong"); 
            }
        }

        private void UpdateNumBtn(object sender, RoutedEventArgs e)
        {


            int idtypetostr = typeBox.SelectedIndex + 1; // с нуля + 1, т.к в бдхе с 1 тип начинается
            
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";

            string sqlExpression = "UPDATE phoneTable SET Phone=" + txnumchange.Text + ", Id_Type=" + idtypetostr + " WHERE Id=" + phoneOwnInfo[Convert.ToInt32(FileNumEdView.SelectedIndex)].Id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов:" + number);
            }
        }

        private void AddNewNumBtn(object sender, RoutedEventArgs e)
        {

            int uniqueID = 0;
            if (MainWindow.studInfo.Count > 0)
            {
                uniqueID = MainWindow.phoneInfo[MainWindow.phoneInfo.Count - 1].Id + 1;
            }

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            string sqlExpression = "INSERT INTO phoneTable(Id, Id_Stud, Id_Type, Phone ) VALUES ("+ uniqueID +", "+ idOfStud + ", 1, 0)";
            MessageBox.Show(sqlExpression);


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                MainWindow.PhoneInfo phonik = new MainWindow.PhoneInfo();
                phonik.Id = uniqueID;
                phonik.Id_Stud = idOfStud;
                phonik.Id_Type = 1;
                phonik.Phone = 0;
                MainWindow.phoneInfo.Add(phonik);
                //InitializeComponent();
                LoadPrivateInfo(); // IMPORTANT THING TO LOAD THIS AFTER INIT and nice kostilino lol
            }

        }
    }
}
