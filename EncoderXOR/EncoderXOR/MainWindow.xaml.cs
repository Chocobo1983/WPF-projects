using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace EncoderXOR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /*
     * 'Вызывающий поток не может получить доступ к данному объекту, так как владельцем этого объекта является другой поток.'
        Чтобы обойти эту ошибку я использовал костыли в виде прямых ссылок на переменные-индикаторы для UI элементов в основном потоке
        Буду благодарен если покажете как сделать без использования костылей
        Также несовсем понятно зачем нужны радиобаттоны в UI, если сама процедура XOR шифровки\дешифровки одинакова
        В папке Debug лежат два файла для тестирования программы, test.txt и test2.txt, один маленький, второй большой
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            x = new XOR();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pbar.Value = counter;
            pbar.Maximum = end;
        }

        string path;
        string password;
        readonly XOR x;
        int counter, end = 100;
        Thread thread;
        About abt;

        private void file_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)| *.txt";
            if (ofd.ShowDialog() == false) return;
            path_tb.Text = path = ofd.FileName;

        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            if (password_tb.Text != "" && File.Exists(path_tb.Text))
            {
                thread = new Thread(() => Encrypt());
                thread.Start();
            }
        }
        private void Encrypt()
        {
            string message = "";
            using (StreamReader sr = new StreamReader(path))
            {
                message = x.Encrypt(sr.ReadToEnd(), password, ref counter, ref end);
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(message);
            }
            MessageBox.Show("Success!");
            counter = 0;
            
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
                counter = 0;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_expand_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (abt == null)
            {
                abt = new About();
                abt.Owner = this;
                abt.Closed += Abt_Closed;
                abt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                abt.ShowDialog();
            }
        }

        private void Abt_Closed(object sender, EventArgs e)
        {
            abt = null;
        }

        private void password_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (password_tb.Text !="") password = password_tb.Text;
        }
    }
}
