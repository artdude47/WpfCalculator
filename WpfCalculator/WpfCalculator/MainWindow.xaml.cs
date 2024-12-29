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

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _currentValue = 0;
        private string _currentOperator = string.Empty;
        private bool _isOperatorClicked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (System.Windows.Controls.Button)sender;
            string numberOrDot = button.Content.ToString();

            if (txtDisplay.Text == "0" || _isOperatorClicked)
            {
                txtDisplay.Text = numberOrDot;
                _isOperatorClicked = false;
            }
            else
            {
                txtDisplay.Text += numberOrDot;
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (System.Windows.Controls.Button)sender;
            string newOperator = button.Content.ToString();

            if (double.TryParse(txtDisplay.Text, out double currentInput))
            {
                if (!string.IsNullOrEmpty(_currentOperator))
                {
                    Calculate(currentInput);
                    txtDisplay.Text = _currentValue.ToString();
                }
                else
                {
                    _currentValue = currentInput;
                }
            }

            _currentOperator = newOperator;
            _isOperatorClicked = true;
        }

        private void Calculate(double secondValue)
        {
            switch (_currentOperator)
            {
                case "+":
                    _currentValue += secondValue;
                    break;
                case "-":
                    _currentValue -= secondValue;
                    break;
                case "×":
                    _currentValue *= secondValue;
                    break;
                case "÷":
                    if (secondValue != 0)
                    {
                        _currentValue /= secondValue;
                    }
                    else
                    {
                        MessageBox.Show("Error: Division by zero is not allowed!");
                    }
                    break;
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double currentInput))
            {
                Calculate(currentInput);
                txtDisplay.Text = _currentValue.ToString();

                _currentOperator = string.Empty;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear all fields
            _currentValue = 0;
            _currentOperator = string.Empty;
            _isOperatorClicked = false;

            // Clear the text display
            txtDisplay.Text = "0";
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            //Only reset display
            txtDisplay.Text = "0";
            _isOperatorClicked = false;
        }
    }
}
