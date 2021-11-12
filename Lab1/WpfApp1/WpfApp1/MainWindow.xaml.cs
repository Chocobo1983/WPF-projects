using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            
        }
        Window1 abt;
        Process tmp;
        private void timerTick(object sender, EventArgs e)
        {
            proc_lb.ItemsSource = Process.GetProcesses();            
        }

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void proc_lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (proc_lb.SelectedIndex != -1)
            {
                mods_lv.ItemsSource = null;
                tmp = (Process)proc_lb.SelectedItem;
                proc_tb.Text = tmp.ProcessName;
                thread_lv.ItemsSource = tmp.Threads;
                try //для тех случаев, когда доступ закрыт
                {
                    mods_lv.ItemsSource = tmp.Modules;
                }
                catch (Exception) { }   
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void abt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (abt == null)
            {
                abt = new Window1();
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
    }
}
