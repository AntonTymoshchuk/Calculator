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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBlock.Text = string.Empty;
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayTextBlock.Text.Length > 0)
            {
                bool ResultFound = DisplayTextBlock.Text.Contains("=");
                if (ResultFound == true)
                {
                    DisplayTextBlock.Text = DisplayTextBlock.Text.Split('=')[0];
                    DisplayTextBlock.Text = DisplayTextBlock.Text.Remove(DisplayTextBlock.Text.Length - 1, 1);
                }
                else
                {
                    char LastSymbol = DisplayTextBlock.Text.ToCharArray()[DisplayTextBlock.Text.Length - 1];
                    if (LastSymbol == ' ')
                        DisplayTextBlock.Text = DisplayTextBlock.Text.Remove(DisplayTextBlock.Text.Length - 3, 3);
                    else
                        DisplayTextBlock.Text = DisplayTextBlock.Text.Remove(DisplayTextBlock.Text.Length - 1, 1);
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            char[] Symbols = DisplayTextBlock.Text.ToCharArray();
            bool ResultFound = DisplayTextBlock.Text.Contains("=");
            if (Symbols[Symbols.Length - 1] == ' ' || Symbols[Symbols.Length - 1] == ',' || ResultFound == true)
                return;

            double RealResult = Calculate();
            string TextualResult = Convert.ToString(RealResult);
            if (RealResult < 0)
                TextualResult = "−" + Convert.ToString(Math.Abs(RealResult));

            DisplayTextBlock.Text += " = " + TextualResult;
        }

        private void DataButton_Click(object sender,RoutedEventArgs e)
        {
            Button DataButton = sender as Button;
            string Data = Convert.ToString(DataButton?.Content);
            char LastSymbol = '\0';
            if (DisplayTextBlock.Text.Length > 0)
                LastSymbol = DisplayTextBlock.Text.ToCharArray()[DisplayTextBlock.Text.Length - 1];
            bool ResultFound = DisplayTextBlock.Text.Contains("=");
            if (ResultFound == false)
            {
                switch (Data)
                {
                    case "+":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += " " + Data + " ";
                        break;
                    case "−":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += " " + Data + " ";
                        break;
                    case "×":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += " " + Data + " ";
                        break;
                    case "÷":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += " " + Data + " ";
                        break;
                    case "√":
                        if (LastSymbol == ' ' || DisplayTextBlock.Text.Length == 0)
                            DisplayTextBlock.Text += Data;
                        break;
                    case "xⁿ":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += " ^ ";
                        break;
                    case ",":
                        if (LastSymbol != ' ' && LastSymbol != ',' && DisplayTextBlock.Text.Length > 0)
                            DisplayTextBlock.Text += ",";
                        break;
                    default:
                        DisplayTextBlock.Text += Data;
                        break;
                }
            }
        }

        private double Calculate()
        {
            List<string> TextualNumbers = new List<string>();
            List<string> Functions = new List<string>();
            List<double> RealNumbers = new List<double>();

            string[] UnsortedValues = DisplayTextBlock.Text.Split(' ');
            for (int i = 0; i < UnsortedValues.Length; i++)
            {
                switch (UnsortedValues[i])
                {
                    case "+":
                        Functions.Add(UnsortedValues[i]);
                        break;
                    case "−":
                        Functions.Add(UnsortedValues[i]);
                        break;
                    case "×":
                        Functions.Add(UnsortedValues[i]);
                        break;
                    case "÷":
                        Functions.Add(UnsortedValues[i]);
                        break;
                    case "^":
                        Functions.Add(UnsortedValues[i]);
                        break;
                    default:
                        TextualNumbers.Add(UnsortedValues[i]);
                        break;
                }
            }

            for (int i = 0; i < TextualNumbers.Count; i++)
            {
                if (TextualNumbers[i].StartsWith("√") == true)
                    RealNumbers.Add(Math.Sqrt(Convert.ToDouble(TextualNumbers[i].TrimStart('√'))));
                else
                    RealNumbers.Add(Convert.ToDouble(TextualNumbers[i]));
            }

            for (int i = 0; i < Functions.Count; i++)
            {
                switch (Functions[i])
                {
                    case "^":
                        Functions.Remove(Functions[i]);
                        RealNumbers[i] = Math.Pow(RealNumbers[i], RealNumbers[i + 1]);
                        RealNumbers.Remove(RealNumbers[i + 1]);
                        i--;
                        break;
                }
            }

            for (int i = 0; i < Functions.Count; i++)
            {
                switch (Functions[i])
                {
                    case "×":
                        Functions.Remove(Functions[i]);
                        RealNumbers[i] *= RealNumbers[i + 1];
                        RealNumbers.Remove(RealNumbers[i + 1]);
                        i--;
                        break;
                    case "÷":
                        Functions.Remove(Functions[i]);
                        RealNumbers[i] /= RealNumbers[i + 1];
                        RealNumbers.Remove(RealNumbers[i + 1]);
                        i--;
                        break;
                }
            }

            for (int i = 0; i < Functions.Count; i++)
            {
                switch (Functions[i])
                {
                    case "+":
                        Functions.Remove(Functions[i]);
                        RealNumbers[i] += RealNumbers[i + 1];
                        RealNumbers.Remove(RealNumbers[i + 1]);
                        i--;
                        break;
                    case "−":
                        Functions.Remove(Functions[i]);
                        RealNumbers[i] -= RealNumbers[i + 1];
                        RealNumbers.Remove(RealNumbers[i + 1]);
                        i--;
                        break;
                }
            }

            double Result = RealNumbers[0];
            return Result;
        }
    }
}
