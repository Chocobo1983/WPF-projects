using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_MVVM.Model
{
    internal class Calculation
    {
        static NumberFormatInfo numberFormatInfo = NumberFormatInfo.InvariantInfo;
        static string action = "", past = "", self = "", bloodyHell = "";
        static bool again = false;
        static bool gimp = true;
        static bool point = true;
        static bool feather = true;
        static double dec1, dec2;
        static char act;

        public static void Click(string parameter, ref string lower_txt, ref string upper_txt)
        {
            if (lower_txt == "0." && upper_txt == "")
            {
                lower_txt += parameter;
                gimp = true;
            }
            else
            {
                if (lower_txt == "0" || !gimp)
                {
                    lower_txt = "";
                    gimp = true;
                }
                else if (upper_txt != "" && lower_txt != "")
                {
                    char tmp = upper_txt[upper_txt.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        upper_txt = "";
                        lower_txt = "";
                        past = self = bloodyHell = "";
                        feather = false;
                    }
                }
                lower_txt += parameter;
            }
            
        }
        public static void Action(string parameter, ref string lower_txt, ref string upper_txt)
        {
            if (!gimp)
            {
                gimp = true;
                lower_txt = "0";
            }
            if (!point) point = true;
            if (!feather) feather = true;
            action = parameter;
            if (upper_txt != "" && lower_txt == "")
            {
                char tmp = upper_txt[upper_txt.Length - 1];
                if (tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/')
                {
                    upper_txt.Remove(upper_txt.Length - 1);
                    upper_txt = self + action;
                }
                lower_txt = "";
            }
            else if (upper_txt != "" && lower_txt != "" && !again)
            {

                act = upper_txt[upper_txt.Length - 1];
                dec1 = double.Parse(upper_txt.Remove(upper_txt.Length - 1), numberFormatInfo);
                dec2 = double.Parse(lower_txt, numberFormatInfo);
                bloodyHell = Calculate(ref dec1, ref dec2, ref act, ref upper_txt);
                if(bloodyHell=="Деление на ноль")
                {
                    lower_txt = "Деление на ноль";
                    return;
                } 
                else lower_txt = bloodyHell;
                upper_txt = bloodyHell + action;
                self = lower_txt;
                lower_txt = "";
            }
            else
            {
                if (lower_txt[lower_txt.Length - 1] == '.') lower_txt = lower_txt.Remove(lower_txt.Length - 1);
                upper_txt = lower_txt + action;
                self = lower_txt;
                lower_txt = "";
            }
            again = false;
        }

        private static string Calculate(ref double dec1, ref double dec2, ref char act, ref string upper_txt)
        {
            switch (act)
            {
                case '+': return (dec1 + dec2).ToString(numberFormatInfo);
                case '-': return (dec1 - dec2).ToString(numberFormatInfo);
                case '*': return (dec1 * dec2).ToString(numberFormatInfo);
                case '/':
                    {
                        if (dec2 == 0)
                        {
                            upper_txt = "";
                            dec1 = dec2 = 0;
                            act = ' ';
                            gimp = false;
                            return "Деление на ноль";
                        }
                        else return (dec1 / dec2).ToString(numberFormatInfo);
                    }
                default: return "0";

            }
        }
        public static void Point(ref string lower_txt, ref string upper_txt)
        {
            if (point)
            {
                point = false;
                if (upper_txt != "" && lower_txt != "")
                {
                    char tmp = upper_txt[upper_txt.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        upper_txt = "";
                        past = self = bloodyHell = "";
                        feather = false;
                        lower_txt = "0";
                    }
                }
                else if (upper_txt != "" && lower_txt == "") lower_txt = "0";
                else if (lower_txt == "Деление на ноль") lower_txt = "0";
                lower_txt += ".";
            }
        }
        public static void Delete(ref string lower_txt, ref string upper_txt)
        {
            if (lower_txt.Length > 1 && lower_txt != "0")
            {
                if (lower_txt[lower_txt.Length - 1] == '.') point = true;
                lower_txt = lower_txt.Remove(lower_txt.Length - 1);
            }
            else if (lower_txt.Length == 1) lower_txt = "0";
        }

        public static void Equal(ref string lower_txt, ref string upper_txt)
        {
            if (!point && upper_txt != "") point = true;
            if (lower_txt != "" && !again && upper_txt != "")
            {
                act = upper_txt[upper_txt.Length - 1];
                dec1 = double.Parse(upper_txt.Remove(upper_txt.Length - 1), numberFormatInfo);
                dec2 = double.Parse(lower_txt, numberFormatInfo);
                upper_txt += lower_txt;
                past = lower_txt;
                lower_txt = Calculate(ref dec1, ref dec2, ref act, ref upper_txt);
                again = true;
            }
            else if (lower_txt != "" && again && gimp && feather)
            {
                dec1 = double.Parse(lower_txt, numberFormatInfo);
                dec2 = double.Parse(past, numberFormatInfo);
                upper_txt = lower_txt + action + past;
                lower_txt = Calculate(ref dec1, ref dec2, ref act, ref upper_txt);
            }
            else if (lower_txt == "")
            {
                past = self;
                dec1 = double.Parse(self, numberFormatInfo);
                dec2 = double.Parse(past, numberFormatInfo);
                act = action[0];
                upper_txt = self + action + past;
                lower_txt = Calculate(ref dec1, ref dec2, ref act, ref upper_txt);
                again = true;
            }
        }

        public static void C_Click(ref string lower_txt, ref string upper_txt)
        {
            upper_txt = "";
            lower_txt = "";
            gimp = false;
            lower_txt = "0";
            if (!point) point = true;
            dec1 = dec2 = 0;
            act = ' ';
            feather = false;
        }

        public static void CE_Click(ref string lower_txt)
        {
            lower_txt = "0";
        }

    }
}
