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
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {

        public Frame frame;

        public static int index;
        public static class EditorInfo
        {
            public static int Id { get; set; }
            public static string Name { get; set; }
            public static string Surname { get; set; }
            public static string Dadname { get; set; }
            public static string Group { get; set; }
            public static int Mphone { get; set; }
            public static int Hphone { get; set; }
            public static string Adress { get; set; }
        }

        public Editor()
        {

            InitializeComponent();
            txname.Text = EditorInfo.Name;
            txsurname.Text = EditorInfo.Surname;
            txdadname.Text = EditorInfo.Dadname;
            txgroup.Text = Convert.ToString(EditorInfo.Group);
            txmob.Text = Convert.ToString(EditorInfo.Mphone);
            txhom.Text = Convert.ToString(EditorInfo.Hphone);
            txadress.Text = EditorInfo.Adress;

        }

        private void SavingButton(object sender, RoutedEventArgs e)
        {
            MainWindow.studInfo[index].Name = txname.Text;
            MainWindow.studInfo[index].Surname = txsurname.Text;
            MainWindow.studInfo[index].Dadname = txdadname.Text;
            MainWindow.studInfo[index].Group = txgroup.Text;
            MainWindow.studInfo[index].Mphone = Convert.ToInt32(txmob.Text);
            MainWindow.studInfo[index].Hphone = Convert.ToInt32(txhom.Text);
            MainWindow.studInfo[index].Adress = txadress.Text;

            int uniqueID = MainWindow.studInfo[index].Id;
            string name = txname.Text;
            string surname = txsurname.Text;
            string dadname = txdadname.Text;
            int group = Convert.ToInt32(txgroup.Text);
            int mphone = Convert.ToInt32(txmob.Text);
            int hphone = Convert.ToInt32(txhom.Text);
            string adress = txadress.Text;
            
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            string sqlExpression = "UPDATE studTables SET Name='" + name + "', Surname='" + surname + "', Dadname='" + dadname + "', [Group]=" + group + ", Mphone=" + mphone + ", Hphone=" + hphone + ", Adress='" + adress + "'WHERE Id=" + uniqueID;
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов: " + number);
            }
            Page2Find adding = new Page2Find { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
            MessageBox.Show(Convert.ToString(index));
            // MainWindow.studInfo[FileInfoView.SelectedIndex].Surname
            //MainWindow.studInfo[FileInfoView.SelectedIndex].Dadname
            //  Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Group)
            //  Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Mphone)
            //  Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Hphone)
            // MainWindow.studInfo[FileInfoView.SelectedIndex].Adress
        }

        private void RemoveFirstNumberPhonesAll()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            string sqlExpression = "DELETE  FROM phoneTable WHERE Id_Stud=" + EditorInfo.Id; // удаляем по айди студентика
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
                }
            }

        }

        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            SaveBtn.IsEnabled = false;
            MessageBox.Show(Convert.ToString(EditorInfo.Id));
            RemoveFirstNumberPhonesAll();
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            string sqlExpression = "DELETE  FROM studTables WHERE Id=" + EditorInfo.Id;
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
                    MainWindow.studInfo.RemoveAt(index);
                }
            }
        }

        private void NumEditorBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}
