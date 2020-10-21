using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Page3Numbers.xaml
    /// </summary>
    public partial class Page3Numbers : Page
    {
        public Frame frame;

        public static int studId = 0; // переменная для получения инфы какому студентику номерочек добавить

        public static int uniqueID = 0; // ID именно таблицы PhoneTable, т.к studId увеличивается, а телефонов не может быть без студентика

        long oldPhoneNumber = 0;

        public Page3Numbers()
        {
            InitializeComponent();
        }

        private void AddNumBtn(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";

            try
            {
                long phoneNumber = Convert.ToInt64(txphonenum.Text); //номерок

                if (oldPhoneNumber == phoneNumber)
                {
                    MessageBox.Show("Этот номер уже занят!", "ПРЕДУПРЕЖДЕНИЕ");
                }

                int typePhone = 1; //тип телефона

                if (typeBox.Text == "Домашний")
                {
                    typePhone = 2;
                }


                string sqlExpression = "INSERT INTO dbo.phoneTable(Id, Id_Stud, Id_Type, Phone ) VALUES (" + uniqueID + ", " + studId + ", " + typePhone + ", " + phoneNumber + ")";

                MessageBox.Show(sqlExpression);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    MainWindow.PhoneInfo phonik = new MainWindow.PhoneInfo();
                    phonik.Id = studId;
                    phonik.Id_Stud = studId;
                    phonik.Id_Type = 1;
                    phonik.Phone = 1488;

                    uniqueID = uniqueID + 1; // увеличиваем айдишник, для уникальности
                    oldPhoneNumber = phoneNumber;                                                                                                               //фейк проверка, можно просто запросиком чекать, но МНЕ ЛЕНЬ МНЕ ЛЕНЬ, ну а че могу себе позволить
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Введите цифры!", "ОШИБКА");
            }
        }
    }
}
