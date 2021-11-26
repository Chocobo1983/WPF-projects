using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace asyncMVVM.Model
{
    internal class MathModule
    {
        static double Factorial(int num)
        {
            double factorial = 1;
            for (int i = 1; i <= num; i++) factorial *= i; 
            return factorial;
        }
        public static double FactorialAsync(int n) => Task.Run(() => Factorial(n)).Result; 
        public static double PowAsync(int x, int y) => Task.Run(() => Math.Pow(x, y)).Result;
        public static int VowelsAsync(string str) => Task.Run(() => Regex.Matches(str, @"[уеыаоэяиёюAEIOU]", RegexOptions.IgnoreCase).Count).Result;
        public static int ConsonantsAsync(string str) => Task.Run(() => Regex.Matches(str, @"[цкнгшщзхфвпрлджчсмтбBCDFGHJKLMNPQRSTVWXYZ]", RegexOptions.IgnoreCase).Count).Result;
        public static int SymbolsAsync(string str) => Task.Run(() => Regex.Matches(str, "[~`!@№#$%^&*()_+=\\-?:;,./|''{}\"[]", RegexOptions.IgnoreCase).Count).Result;
        public static int NumbersAsync(string str) => Task.Run(() => Regex.Matches(str, @"[0123456789]", RegexOptions.IgnoreCase).Count).Result;
    }
}
