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
    /// Логика взаимодействия для ReportLog.xaml
    /// </summary>
    public partial class ReportLog : Page
    {
        public Frame frame;
        public class RepStudInfo
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Dadname { get; set; }
            public string Group { get; set; }
            public int Mphone { get; set; }
            public int Hphone { get; set; }
            public string Adress { get; set; }
        }

        public static ObservableCollection<RepStudInfo> reportInfo = new ObservableCollection<RepStudInfo>();
        
        public ReportLog()
        {
            InitializeComponent();
            LoadReport(); // Загружаем без дубликатов инфу отсортированную

        }
        private void LoadReport() 
        {
            reportInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=phones;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "Select * FROM studTables Where Id in (select min(Id) as MinRowID FROM studTables group by Surname) Order by [Group], Surname";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        RepStudInfo logString = new RepStudInfo();
                        logString.Name = Convert.ToString(reader.GetValue(1));
                        logString.Surname = Convert.ToString(reader.GetValue(2));
                        logString.Dadname = Convert.ToString(reader.GetValue(3));
                        logString.Group = Convert.ToString(reader.GetValue(4));
                        logString.Mphone = Convert.ToInt32(reader.GetValue(5));
                        logString.Hphone = Convert.ToInt32(reader.GetValue(6));
                        logString.Adress = Convert.ToString(reader.GetValue(7));
                        FileInfoView.ItemsSource = reportInfo;
                        reportInfo.Add(logString);
                    }
                }

                reader.Close();

            }
        }

    }
}
