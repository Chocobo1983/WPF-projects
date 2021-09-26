using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string action = "", past = "", self = "", bloodyHell = "";
        bool again = false;
        bool gimp = true;
        bool point = true;
        bool feather = true;
        About abt;
        private void btn_1_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_2_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_3_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_4_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_5_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_6_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_7_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_8_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_9_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_0_Click(object sender, RoutedEventArgs e) { Click(sender, e); }
        private void btn_point_Click(object sender, RoutedEventArgs e)
        {
            if (point)
            {
                point = false;
                if (upper_txt.Text != "" && lower_txt.Text != "")
                {
                    char tmp = upper_txt.Text[upper_txt.Text.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        upper_txt.Clear();
                        lower_txt.Clear();
                        past = self = bloodyHell = "";
                        feather = false;
                        lower_txt.Text = "0";
                    }
                }
                else if (upper_txt.Text != "" && lower_txt.Text == "") lower_txt.Text = "0";
                lower_txt.Text += ",";
            }
        }
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (lower_txt.Text.Length > 1 && lower_txt.Text != "0")
            {
                if (lower_txt.Text[lower_txt.Text.Length - 1] == ',') point = true;    
                lower_txt.Text = lower_txt.Text.Remove(lower_txt.Text.Length - 1);
            }
            else if (lower_txt.Text.Length == 1) lower_txt.Text = "0"; 
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e) { this.Close(); }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) { DragMove(); }
        private void Click(object sender, RoutedEventArgs e) 
        {
            if (lower_txt.Text == "0" || !gimp)
            {
                lower_txt.Clear();
                gimp = true;
            }
            else if (upper_txt.Text != "" && lower_txt.Text != "")
            {
                char tmp = upper_txt.Text[upper_txt.Text.Length - 1];
                if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                {
                    upper_txt.Clear();
                    lower_txt.Clear();
                    past = self = bloodyHell = "";
                    feather = false;
                }
            }
            lower_txt.Text += ((Button)e.OriginalSource).Content.ToString();
        }
        private void VJUH(object sender, RoutedEventArgs e)
        {
            if(!gimp)
            {
                lower_txt.Clear();
                gimp = true;
                lower_txt.Text = "0";
            }
            if (!point) point = true;
            if (!feather) feather = true;
            action = ((Button)e.OriginalSource).Content.ToString();
            if (upper_txt.Text != "" && lower_txt.Text == "")
            {
                char tmp = upper_txt.Text[upper_txt.Text.Length - 1];
                if (tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/')
                {
                    upper_txt.Text.Remove(upper_txt.Text.Length - 1);
                    upper_txt.Text = self + action;
                }
                lower_txt.Clear();
            }
            else if(upper_txt.Text != "" && lower_txt.Text != "" && !again)
            {

                act = upper_txt.Text[upper_txt.Text.Length - 1];
                dec1 = double.Parse(upper_txt.Text.Remove(upper_txt.Text.Length - 1));
                dec2 = double.Parse(lower_txt.Text);
                bloodyHell = Calculation(ref dec1, ref dec2, ref act);
                lower_txt.Text = bloodyHell;
                upper_txt.Text = bloodyHell + action;
                self = lower_txt.Text;
                lower_txt.Clear();
            }
            else
            {
                if (lower_txt.Text[lower_txt.Text.Length - 1] == ',') lower_txt.Text = lower_txt.Text.Remove(lower_txt.Text.Length - 1);
                upper_txt.Text = lower_txt.Text + action;
                self = lower_txt.Text;
                lower_txt.Clear();
            }
            again = false;
        }
        private void btn_equal_Click(object sender, RoutedEventArgs e)
        {
            if (!point && upper_txt.Text!="") point = true;
            if (lower_txt.Text != "" && !again && upper_txt.Text != "") 
            {
                act = upper_txt.Text[upper_txt.Text.Length - 1];
                dec1 = double.Parse(upper_txt.Text.Remove(upper_txt.Text.Length - 1));
                dec2 = double.Parse(lower_txt.Text);
                upper_txt.Text += lower_txt.Text;
                past = lower_txt.Text;
                lower_txt.Text = Calculation(ref dec1, ref dec2, ref act);
                again = true;
            }
            else if(lower_txt.Text != "" && again && gimp && feather) 
            {
                dec1 = double.Parse(lower_txt.Text);
                dec2 = double.Parse(past);
                upper_txt.Text = lower_txt.Text + action + past;
                lower_txt.Text = Calculation(ref dec1, ref dec2, ref act);
            }
            else if(lower_txt.Text == "")
            {
                past = self;
                dec1 = double.Parse(self);
                dec2 = double.Parse(past);
                act = action[0];
                upper_txt.Text = self + action + past;
                lower_txt.Text = Calculation(ref dec1, ref dec2, ref act);
                again = true;
            }
        }
        private void btn_plus_Click(object sender, RoutedEventArgs e) { VJUH(sender, e); }
        private void btn_minus_Click(object sender, RoutedEventArgs e) { VJUH(sender, e); }
        private void btn_mult_Click(object sender, RoutedEventArgs e) { VJUH(sender, e); }
        double dec1, dec2;
        char act;

        private void btn_restore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_logo_Click(object sender, RoutedEventArgs e)
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

        private void btn_divide_Click(object sender, RoutedEventArgs e) { VJUH(sender, e); }
        private void btn_C_Click(object sender, RoutedEventArgs e) 
        {   upper_txt.Clear();        
            lower_txt.Clear();
            gimp = false;
            lower_txt.Text = "0";
            if (!point) point = true;
            dec1 = dec2 = 0;
            act = ' ';
            feather = false;
        }
        private void btn_CE_Click(object sender, RoutedEventArgs e)
        {
            lower_txt.Clear();
            lower_txt.Text = "0";
        }
        private string Calculation(ref double dec1, ref double dec2, ref char act)
        {
            switch (act)
            {
                case '+': return (dec1 + dec2).ToString();
                case '-': return (dec1 - dec2).ToString();
                case '*': return (dec1 * dec2).ToString();
                case '/':
                    {
                        if (dec2 == 0)
                        {
                            upper_txt.Text = "";
                            dec1 = dec2 = 0;
                            act = ' ';
                            gimp = false;
                            return "Деление на ноль";
                        }
                        else return (dec1 / dec2).ToString(); 
                    }
                default: return "0";
                    
            }
        }

    }
}
