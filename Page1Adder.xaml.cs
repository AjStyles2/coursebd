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

namespace PhoneBDAutumn
{
    /// <summary>
    /// Логика взаимодействия для Page1Adder.xaml
    /// </summary>
    public partial class Page1Adder : Page
    {
        public Frame frame;

        public Page1Adder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2Find adding = new Page2Find { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddingUser adding = new AddingUser { frame = this.frame }; //Передача в presenter чтобы не был пустым 
            frame.Content = adding;
        }
    }
}
