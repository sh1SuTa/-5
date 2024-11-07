using Model;
using MyDLL;
using System;
using System.Net;
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

namespace ImageClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        String url = "https://localhost:44358/ImageServer.ashx";
        public MainWindow()
        { 
            InitializeComponent(); 
        }//注意不要误删构造函数
        void showMsg(String s) {
            MessageBox.Show(s, "Information", MessageBoxButton.OK);
        }
        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                ResultClass res = My.deserialize<ResultClass>(e.Result);
                if (res.state == "success")
                {
                    txtMsg.Text = res.message;
                }
                else showMsg(res.message);
            }
            else showMsg(e.Error.Message);
        }
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            String uName = My.encode(txtUser.Text.Trim());
            String uPass = My.encode(txtPass.Password.Trim());
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.DownloadStringCompleted += Client_DownloadStringCompleted;
                Uri uri = new Uri(url + "?uName=" + uName + "&uPass=" + uPass, UriKind.Absolute);
                client.DownloadStringAsync(uri);
            }
            catch (Exception exp) { showMsg(exp.Message); }
        }
    }
}
