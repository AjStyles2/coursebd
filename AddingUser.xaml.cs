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
    /// Логика взаимодействия для AddingUser.xaml
    /// </summary>
    public partial class AddingUser : Page
    {
        public Frame frame;

        public AddingUser()
        {
            InitializeComponent();

        }

        private void AddUserBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
                int uniqueID = 0;
                if (MainWindow.studInfo.Count > 0)
                {
                    uniqueID = MainWindow.studInfo[MainWindow.studInfo.Count - 1].Id + 1;
                }
                string name = txname.Text;
                string surname = txsurname.Text;
                string dadname = txdadname.Text;
                string group = groupBox.Text + "-" + txgroup.Text;
                //int mphone = Convert.ToInt32(txmob.Text);
                //int hphone = Convert.ToInt32(txhom.Text);
                string adress = txadress.Text;

                string sqlExpression = "INSERT INTO studTables (Id, Name, Surname, Dadname, [Group], Adress) VALUES (" + uniqueID + ", '" + name + "', '" + surname + "', '" + dadname + "', '" + group + "', '" + adress + "')";
                MessageBox.Show(sqlExpression);

                
                if (groupBox.Text != "")
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        int number = command.ExecuteNonQuery();
                        MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                        MainWindow.StudentInfo fileInfo = new MainWindow.StudentInfo();
                        fileInfo.Id = uniqueID;
                        fileInfo.Name = name;
                        fileInfo.Surname = surname;
                        fileInfo.Dadname = dadname;
                        fileInfo.Group = group;
                        //fileInfo.Mphone = mphone;
                        //fileInfo.Hphone = hphone;
                        fileInfo.Adress = adress;
                        MainWindow.studInfo.Add(fileInfo);

                        Page3Numbers phonePage = new Page3Numbers { frame = this.frame }; //Открываем добавление номерков
                        Page3Numbers.studId = uniqueID; // студентик                   
                        if (MainWindow.phoneInfo.Count > 0)
                        {
                            Page3Numbers.uniqueID = MainWindow.phoneInfo[MainWindow.phoneInfo.Count - 1].Id + 1; ; // мобилка 
                        }
                        else
                        {
                            Page3Numbers.uniqueID = uniqueID;
                        }

                        frame.Content = phonePage; //открываем мобилку страничную добавку
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана специальность", "ОШИБКА");
                }

            } 
            catch(FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");
            }

        }
    }
}
