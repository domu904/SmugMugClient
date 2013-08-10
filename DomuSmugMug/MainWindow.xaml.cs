using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace DomuSmugMug
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CallSmugMug()
        {            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = new User()
                           {
                               UserName = UserNameTextBox.SelectedText,
                               Password = PasswordTextBox.SelectedText
                           };

        }

    }

    public class User
    {
        private string myKey;
        private string myPvtKey;

        public User()
        {
            myKey = ConfigurationManager.AppSettings["myKey"];
            myPvtKey = ConfigurationManager.AppSettings["myPvtKey"];
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
