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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;                
                switch (btn.Content.ToString())
                {
                    case "1":
                        textBoxExpression.Text += "1";
                        break;
                    case "2":
                        textBoxExpression.Text += "2";
                        break;
                    case "3":
                        textBoxExpression.Text += "3";
                        break;
                    case "4":
                        textBoxExpression.Text += "4";
                        break;
                    case "5":
                        textBoxExpression.Text += "5";
                        break;
                    case "6":
                        textBoxExpression.Text += "6";
                        break;
                    case "7":
                        textBoxExpression.Text += "7";
                        break;
                    case "8":
                        textBoxExpression.Text += "8";
                        break;
                    case "9":
                        textBoxExpression.Text += "9";
                        break;
                    case "0":
                        textBoxExpression.Text += "0";
                        break;
                    case "(":
                        textBoxExpression.Text += "(";
                        break;
                    case ")":
                        textBoxExpression.Text += ")";
                        break;
                    case "*":
                        textBoxExpression.Text += "*";
                        break;
                    case "/":
                        textBoxExpression.Text += "/";
                        break;
                    case "+":
                        textBoxExpression.Text += "+";
                        break;
                    case "-":
                        textBoxExpression.Text += "-";
                        break;
                    case "C":
                        textBoxExpression.Text = "0";
                        break;
                    case "Backspace":
                        if(textBoxExpression.Text.Length>0)
                        textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1);
                        break;
                    case "+/-":
                        if (textBoxExpression.Text.Length > 0)
                        {
                            if (textBoxExpression.Text.Length > 0 && char.IsDigit(textBoxExpression.Text[0]) || textBoxExpression.Text[0] == '(')
                            {
                                textBoxExpression.Text = "-" + textBoxExpression.Text;
                            }
                            else
                            {
                                if (textBoxExpression.Text.Length > 0 && textBoxExpression.Text[0] == '-')
                                {
                                    string str = textBoxExpression.Text;
                                    textBoxExpression.Text = str.Remove(0, 1);
                                }
                            }
                        }                            
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry for the inconvenience, Unexpected error occured. Details: " +
                    ex.Message);
            }
        }

    }
}
