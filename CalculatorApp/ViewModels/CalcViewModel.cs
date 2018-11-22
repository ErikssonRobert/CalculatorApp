using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalculatorApp
{
    public class CalcViewModel :INotifyPropertyChanged
    {
        private string result;
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                if (result != value)
                {
                    SetProperty(ref result, value);
                }
            }
        }

        private string firstNumber;
        public string FirstNumber
        {
            get
            {
                return firstNumber;
            }
            set
            {
                firstNumber = value;
            }
        }

        private string operatorSymbol;
        public string OperatorSymbol
        {
            get
            {
                return operatorSymbol;
            }
            set
            {
                operatorSymbol = value;
            }
        }

        public int FontSize { get; set; }

        public ICommand NumberCommand { private set; get; }

        public ICommand OperatorCommand { private set; get; }

        public ICommand ClearCommand { private set; get; }

        public ICommand SumCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CalcViewModel()
        {
            Result = "";
            FontSize = 50;

            OperatorCommand = new Command<string>(
                OperatorButton, 
                obj => { /*Can execute?*/ return true; });

            NumberCommand = new Command<string>(
                NumberButton,
                obj => { 
                    if (obj.Equals("0") && Result.Length == 1 && Result.Equals("0"))
                    {
                        return false;
                    } else if (obj.Equals(",") && Result.Contains(","))
                    {
                        return false;
                    } else
                    {
                        return true;
                    }
                }
            );

            ClearCommand = new Command(
                obj => { 
                Result = ""; 
                FirstNumber = ""; 
                OperatorSymbol = ""; 
                RefreshCanExecute(); },
                obj => { return true; });

            SumCommand = new Command(
                obj => { SumButton(); },
                obj => { return true; });
        }

        private void NumberButton(string obj)
        {
            if (obj.Equals(",") && Result.Length == 1 && Result.Equals("0"))
            {
                Result += obj;
            } else if (Result.Length == 1 && Result.Equals("0"))
            {
                Result = obj;
            } else
                Result += obj;

            RefreshCanExecute();
        }

        private void OperatorButton(string obj)
        {
            FirstNumber = Result;
            Result = "";
            OperatorSymbol = obj;
            if (OperatorSymbol.Equals("%"))
            {
                Result = string.Format("{0}", double.Parse(FirstNumber) * 100);
            } else if (OperatorSymbol.Equals("+/-"))
            {
                Result = string.Format("{0}", double.Parse(FirstNumber) * -1);
            }
            RefreshCanExecute();
        }

        private void SumButton()
        {
            switch (OperatorSymbol)
            {
                case "+":
                    Result = string.Format("{0}", double.Parse(FirstNumber) + double.Parse(Result));
                    break;
                case "-":
                    Result = string.Format("{0}", double.Parse(FirstNumber) - double.Parse(Result));
                    break;
                case "/":
                    Result = string.Format("{0}", double.Parse(FirstNumber) / double.Parse(Result));
                    break;
                case "x":
                    Result = string.Format("{0}", double.Parse(FirstNumber) * double.Parse(Result));
                    break;
                default:
                    //Unknown
                    break;
            }
            //Result = string.Format("{0}", int.Parse(FirstNumber));
        }

        //Tell all buttons to check their canexecute status again
        private void RefreshCanExecute()
        {
            (NumberCommand as Command).ChangeCanExecute();
            (OperatorCommand as Command).ChangeCanExecute();
            (ClearCommand as Command).ChangeCanExecute();
            (SumCommand as Command).ChangeCanExecute();
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
