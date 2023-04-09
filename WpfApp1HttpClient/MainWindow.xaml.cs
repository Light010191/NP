using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp1HttpClient
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(adressTextBox.Text))
            {
                MessageBox.Show("Input Adress");
                return;
            }
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(adressTextBox.Text);
                responseTextBox.Text = await response.Content.ReadAsStringAsync();
                codeTextBlock.Text = response.StatusCode.ToString();
                await File.WriteAllBytesAsync("test.txt", await client.GetByteArrayAsync(adressTextBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
