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
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace Background_Changer
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
            //Open Windows Explorer window for file selection
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp, *.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.FilterIndex = 1;
            
            //Set selected file name as text of the label with tag = "Source"
            if (openFileDialog.ShowDialog() == true && source != null)
            {
                source.Content = openFileDialog.FileName;
                Preview.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                ChangeButton.IsEnabled = true;
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            //Pop a dialogue box informing the user that the program is about to change the background
            MessageBoxResult result = MessageBox.Show("Are you sure you want to change the background?", "Background Changer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes && (source.Content != null))
            {
                //Get the selected file name from the label with tag = "Source"
                string fileName = source.Content.ToString();


                WallpaperStyle style = WallpaperStyle.Fill;

                //Set the wallpaper of Windows as the selected image

                Wallpaper.Set(source.Content.ToString(), style);
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Open Windows Explorer window for file selection
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp, *.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true && source != null)
            {
                source_Default.Content = openFileDialog.FileName;

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (source != null)
            {
                source.Content = source_Default.Content;
                Preview.Source = new BitmapImage(new Uri((string)source.Content));
                ChangeButton.IsEnabled = true;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

    public static class Wallpaper
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

            private const int SPI_SETDESKWALLPAPER = 0x0014;
            private const int SPIF_UPDATEINIFILE = 0x01;
            private const int SPIF_SENDCHANGE = 0x02;

            public static void Set(string imagePath, WallpaperStyle style)
            {
            int styleValue = 0;

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, styleValue);
            SystemParametersInfo(SPIF_UPDATEINIFILE | SPIF_SENDCHANGE, 0, null, styleValue);
        }
    }

    public enum WallpaperStyle
        {
            Fill,
            Fit,
            Stretch,
            Tile,
            Center,
            Span
    }

}
