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

namespace SPlab2._3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pbar.Maximum = max_value;
            pbar.Value = value;            
        }

        string from, to, name;
        int value = 0, max_value = 100;
        About abt;
        private void from_file_btn_Click(object sender, RoutedEventArgs e)
        {
            from = "";
            to_tb.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All Files(*.*)|*.*";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == false) return;
            from_tb.Text = from = ofd.FileName;
            name = ofd.SafeFileName;
        }
        private void to_file_btn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
                to = dialog.SelectedPath+"\\";
                to_tb.Text = to + System.IO.Path.GetFileName(from_tb.Text);
            }
        }

        private void from_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            to_tb.Text = "";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_exp_Click(object sender, RoutedEventArgs e)
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

        private void to_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) start_btn_Click(sender, e);
        }

        private void to_tb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(File.Exists(from_tb.Text)) name = to + System.IO.Path.GetFileName(from_tb.Text);
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            Task task = Task.Run(() => Copy(ref value, ref max_value));
            
        }

        private void Copy(ref int value, ref int max_value )
        {
            try
            {
                if (File.Exists(from) && Directory.Exists(to))
                {
                    if (File.Exists(name))
                    {
                        MessageBoxResult result = MessageBox.Show("A file with this name already exists.Do you want to overwrite it?", "Lowrider", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.Cancel) return;
                    }
                    using (var inputFs = new FileStream(from, FileMode.Open, FileAccess.Read))
                    using (var outputFs = new FileStream(name, FileMode.Create, FileAccess.Write))
                    {
                        var buffer = new byte[4096];
                        var fsize = new FileInfo(from).Length;
                        max_value = (int)fsize / 4096;
                        while (inputFs.Position < inputFs.Length)
                        {
                            var wasRead = inputFs.Read(buffer, 0, (int)4096);
                            outputFs.Write(buffer, 0, wasRead);
                            value++;
                        }
                    }
                    MessageBox.Show("Success!");
                    value = 0;
                    max_value = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());                
            }
            
            
        }
    }
}
