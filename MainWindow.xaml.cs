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
using System.Net;
using System.IO;
using System.Diagnostics;

namespace FTPClearer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get details from textboxes for the address, username and password
            string FTPAddress = "ftp://" + tbFTPAddress.Text + "/BurrowsAssets/resources/locale/nl-nl"; ;
            string FTPUsername = tbFTPUsername.Text.Normalize();
            string FTPPassword = tbFTPPassword.Text.Normalize();

            string FtpDelTime = "";

            if (rbMonth.IsChecked == true)
            {
                FtpDelTime = "Month";
            }
            else if (rbWeek.IsChecked == true)
            {
                FtpDelTime = "Week";
            }

            //Run the loadFTPDetails function with the given address, username and password
            loadFTPDetails(FTPAddress, FTPUsername, FTPPassword, FtpDelTime);
        }

        public void loadFTPDetails(string ftpaddress, string ftpusername, string ftppassword, string ftpdeltime)
        {

            FtpWebRequest ftprequest = (FtpWebRequest)WebRequest.Create(ftpaddress);
            ftprequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftprequest.Credentials = new NetworkCredential(ftpusername, ftppassword);
            ftprequest.UsePassive = false;
            ftprequest.Timeout = 10000;
            FtpWebResponse response = (FtpWebResponse)ftprequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string details = reader.ReadToEnd();

            reader.Close();
            response.Close();


        }
    }


}
