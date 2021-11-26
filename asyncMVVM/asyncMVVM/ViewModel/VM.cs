using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asyncMVVM.ViewModel
{
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _num;
        public int Num { get { return _num; } set { _num = value; OnPropertyChanged("Factorial"); } }
        public double Factorial => Model.MathModule.FactorialAsync(Num);
        private int _num1;
        public int Num1 { get { return _num1; } set { _num1 = value; OnPropertyChanged("Pow"); } }
        private int _num2;
        public int Num2 { get { return _num2; } set { _num2 = value; OnPropertyChanged("Pow"); } }
        public double Pow => Model.MathModule.PowAsync(Num1, Num2);
        private string? _str1;
        public string Str1 { get { return _str1 ?? ""; } set { _str1 = value; OnPropertyChanged("Vowels"); } }
        public string Vowels => $"Vowels: {Model.MathModule.VowelsAsync(Str1)}\tConsonants: {Model.MathModule.ConsonantsAsync(Str1)}\nSymbols: {Model.MathModule.SymbolsAsync(Str1)}\tNumbers: {Model.MathModule.NumbersAsync(Str1)}";
        

    }
}