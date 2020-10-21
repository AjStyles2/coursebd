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

namespace PhoneBDAutumn
{
    /// <summary>
    /// Логика взаимодействия для Page2Find.xaml
    /// </summary>
    /// 

    public partial class Page2Find : Page
    {
        public Frame frame;
       
        public Page2Find()
        {
            InitializeComponent();
            //Refresh();
            FileInfoView.ItemsSource = MainWindow.studInfo;
        }

        private void Refresh() // Обновление вывода
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

                    while (reader.Read()) // построчно считываем данные
                    {
                        MainWindow.StudentInfo fileInfo = new MainWindow.StudentInfo();
                        fileInfo.Id = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.Name = Convert.ToString(reader.GetValue(1));
                        fileInfo.Surname = Convert.ToString(reader.GetValue(2));
                        fileInfo.Dadname = Convert.ToString(reader.GetValue(3));
                        fileInfo.Group = Convert.ToString(reader.GetValue(4));
                        fileInfo.Mphone = Convert.ToInt32(reader.GetValue(5));
                        fileInfo.Hphone = Convert.ToInt32(reader.GetValue(6));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(7));
                        FileInfoView.ItemsSource = MainWindow.studInfo;
                        MainWindow.studInfo.Add(fileInfo);
                    }

                }

                reader.Close();

            }
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
                        MainWindow.StudentInfo fileInfo = new MainWindow.StudentInfo();
                        fileInfo.Id = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.Name = Convert.ToString(reader.GetValue(1));
                        fileInfo.Surname = Convert.ToString(reader.GetValue(2));
                        fileInfo.Dadname = Convert.ToString(reader.GetValue(3));
                        fileInfo.Group = Convert.ToString(reader.GetValue(4));
                        fileInfo.Mphone = Convert.ToInt32(reader.GetValue(5));
                        fileInfo.Hphone = Convert.ToInt32(reader.GetValue(6));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(7));
                        FileInfoView.ItemsSource = MainWindow.studInfo;
                        MainWindow.studInfo.Add(fileInfo);
                    }

                }

                reader.Close();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Convert.ToString(FileInfoView.SelectedItem));
            MessageBox.Show(Convert.ToString(FileInfoView.SelectedIndex));
            MessageBox.Show(MainWindow.studInfo[FileInfoView.SelectedIndex].Name);
            MainWindow.studInfo.RemoveAt(FileInfoView.SelectedIndex);
            
        }

        private void Refreshing(object sender, RoutedEventArgs e) //Кнопка обновления
        {
            Refresh();
            //FileInfoView.ItemsSource = MainWindow.studInfo;
            //MessageBox.Show(MainWindow.studInfo[1].Name);
            //Page2Find editor = new Page2Find { frame = this.frame }; //Перевызываем кек
            //frame.Content = editor;
        }

        private void SearchForNumBtn(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEditor(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                Editor editor = new Editor { frame = this.frame }; //Передача в presenter чтобы не был пустым 
                
                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                editor.txname.Text = MainWindow.studInfo[FileInfoView.SelectedIndex].Name;
                editor.txsurname.Text = MainWindow.studInfo[FileInfoView.SelectedIndex].Surname;
                editor.txdadname.Text = MainWindow.studInfo[FileInfoView.SelectedIndex].Dadname;
                editor.txgroup.Text = Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Group);
                editor.txmob.Text = Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Mphone);
                editor.txhom.Text = Convert.ToString(MainWindow.studInfo[FileInfoView.SelectedIndex].Hphone);
                editor.txadress.Text = MainWindow.studInfo[FileInfoView.SelectedIndex].Adress;
                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                Editor.index = FileInfoView.SelectedIndex;
                Editor.EditorInfo.Id = MainWindow.studInfo[FileInfoView.SelectedIndex].Id;
                Editor.EditorInfo.Name = MainWindow.studInfo[FileInfoView.SelectedIndex].Name;
                Editor.EditorInfo.Surname = MainWindow.studInfo[FileInfoView.SelectedIndex].Surname;
                Editor.EditorInfo.Dadname = MainWindow.studInfo[FileInfoView.SelectedIndex].Dadname;
                Editor.EditorInfo.Group = MainWindow.studInfo[FileInfoView.SelectedIndex].Group;
                Editor.EditorInfo.Mphone = MainWindow.studInfo[FileInfoView.SelectedIndex].Mphone;
                Editor.EditorInfo.Hphone = MainWindow.studInfo[FileInfoView.SelectedIndex].Hphone;
                Editor.EditorInfo.Adress = MainWindow.studInfo[FileInfoView.SelectedIndex].Adress;
                frame.Content = editor;
            } 
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void InputCheck(object sender, KeyEventArgs e)
        {
           // MessageBox.Show(Convert.ToString(e.Key));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //surnameBox.SelectionStart = 0;
            //surnameBox.SelectionLength = 10;
            surnameBox.Select(0, 10);
        }

        private void surnameBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            surnameBox.Select(0, 10);
        }

        private void FindByFam(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(surnameBox.Text);
            //FindLike(surnameBox.Text);
            //phoneBox.Text = ""; // to avoid user`s mind bump
            FindLike("SELECT * FROM studTables WHERE Surname LIKE '" + surnameBox.Text + "%'");

        }

        private void FindByPhone(object sender, TextChangedEventArgs e)
        {
            //surnameBox.Text = ""; // to avoid user`s mind bump
            FindLike("SELECT * FROM studTables WHERE Mphone LIKE '" + phoneBox.Text + "%' OR Hphone LIKE '" + phoneBox.Text + "%'");

        }

        private void CreateReportBtn(object sender, RoutedEventArgs e)
        {
            ReportLog reporter = new ReportLog { frame = this.frame }; //Открываем отчёт
            frame.Content = reporter;
        }

        private void NumEditorBtn(object sender, RoutedEventArgs e)
        {
            Page4NumEdit numeditor = new Page4NumEdit { frame = this.frame }; //Открываем редактор номерка
            frame.Content = numeditor;
        }

        private void FileInfoView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //trydo
            try
            {
                Page4NumEdit.idOfStud = MainWindow.studInfo[FileInfoView.SelectedIndex].Id; // передаём айдиху студака, чтоб его номерки вывести, но заранее, т.к перемка поздно апдейтится и листвью багует
            }
            catch
            {
                MessageBox.Show("Something went wrong");
            }
        }
    }
}
