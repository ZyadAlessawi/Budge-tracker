using Essential_Lib.API;
using Essential_Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Data;

namespace Calculatour
{
    public partial class Math_Model : ObservableObject
    {
         

        [ObservableProperty]
        public string calcDisplay = string.Empty;

        [ObservableProperty]
        public decimal resultDisplay;

        [ObservableProperty]
        private int cursorPosition;

        [RelayCommand]
        public async Task<Task> HandleButtonPressAsync(string buttonText)
        {
            if (buttonText == "( )")
            {
                buttonText = CalcDisplay.ToCharArray().Count(x => x == '(' || x == ')') % 2 == 0 ? "(" : ")";
            }

            if (buttonText == "C")
            {
                CalcDisplay = string.Empty;
                ResultDisplay = 0;
                CursorPosition = 0;
            }
            else if (int.TryParse(buttonText, out var _) || buttonText == "%" || buttonText == ".")
            {
                var ch = buttonText[0];
                if (CursorPosition <= CalcDisplay.Length)
                {
                    CalcDisplay = CalcDisplay.Insert(CursorPosition, ch.ToString());
                    CursorPosition += 1;
                }

                UpdateResultDisplayAsync();
            }
            else if (buttonText == "=")
            {
                try
                {

                   

                    decimal result = Convert.ToDecimal(new DataTable().Compute(GenerateExpression(), null));
                    ResultDisplay = result;

                    Keys key = new Keys() { calchistory = CalcDisplay, resulthistory = ResultDisplay };
                    await LocalDatabaseAPIs.AddItemAsync(key);

                    CalcDisplay = ResultDisplay.ToString("N");
                    CursorPosition = CalcDisplay.Length;

            

                }
                catch (Exception ex)
                {
                    //ResultDisplay = "Format error";
                    await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                }

            }
            else if (buttonText == "✕")
            {
                if (!string.IsNullOrEmpty(CalcDisplay) && CursorPosition > 0)
                {
                    CalcDisplay = CalcDisplay.Remove(CursorPosition - 1, 1);
                    CursorPosition -= 1;
                }
            }
            else
            {
                var ch = buttonText[0];
                if (CursorPosition <= CalcDisplay.Length)
                {
                    CalcDisplay = CalcDisplay.Insert(CursorPosition, ch.ToString());
                    CursorPosition += 1;
                }
            }

            return Task.CompletedTask;
        }



        private async void UpdateResultDisplayAsync()
        {
            try
            {
                if (!decimal.TryParse(CalcDisplay, out var _) && ResultDisplay <= decimal.MaxValue && ResultDisplay >= decimal.MinValue)
                {
                    var datatable = new DataTable();
                    decimal result = Convert.ToDecimal(datatable.Compute(GenerateExpression(), null));
                    ResultDisplay = result;
                }
                else
                {
                    ResultDisplay = 0;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                //ResultDisplay = "Format error";
            }
        }

        private string GenerateExpression() =>
            CalcDisplay
            .Replace("×", "*")
            .Replace("÷", "/")
            .Replace("%", "*0.01")
            .Replace("(", "*(")
            .Replace(")", ")*");

    }
}
