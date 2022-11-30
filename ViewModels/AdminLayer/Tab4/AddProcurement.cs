using NazarTunes.ViewModels.Notifiers;
using System;
using System.Windows;

namespace NazarTunes.ViewModels.AdminLayer.Tab4
{
    public class AddProcurement : Notifier
    {
        private int _selectedRecordIndex;
        public int SelectedRecordIndex
        {
            get => _selectedRecordIndex;
            set
            {
                SetField(ref _selectedRecordIndex, value);
                RefreshStates?.Invoke();
            }
        }

        private int _selectedSupplierIndex;
        public int SelectedSupplierIndex
        {
            get => _selectedSupplierIndex;
            set
            {
                SetField(ref _selectedSupplierIndex, value);
                RefreshStates?.Invoke();
            }
        }

        private string? _amount;
        public string? Amount
        {
            get => _amount;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _amount, value);
                RefreshStates?.Invoke();
            }
        }

        private string? _price1;
        public string? Price1
        {
            get => _price1;
            set
            {
                RemoveLettersOrSymbols(ref value!);
                SetField(ref _price1, value);
                RefreshStates?.Invoke();
            }
        }

        private string? _price2;
        public string? Price2
        {
            get => _price2;
            set
            {
                RemoveLettersOrSymbols(ref value!, "cut");
                SetField(ref _price2, value);
                RefreshStates?.Invoke();
            }
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get => _isVisible;
            set => SetField(ref _isVisible, value);
        }

        public Action? RefreshStates { get; set; }

        public AddProcurement()
        {
            IsVisible = Visibility.Collapsed;
            SelectedRecordIndex = -1;
            SelectedSupplierIndex = -1;
        }
        private void RemoveLettersOrSymbols(ref string str, string? key=null)
        { 
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (!char.IsDigit(str[i])) str = str.Remove(i, 1);
                }
            }
            if (key is not null)
            {
                if (str.Length > 2) str = str.Remove(2);
                else return;
            }
        }

        public bool RefreshFieldsState()
        {
            return !string.IsNullOrWhiteSpace(Amount) && !string.IsNullOrWhiteSpace(Price1) && SelectedRecordIndex != -1 && SelectedSupplierIndex != -1;
        }

        public (int recordIndex, int supplierIndex, DateTime date, int amount, double price) GetFields()
        {
            var date = DateTime.Now;
            if (string.IsNullOrWhiteSpace(Price2)) Price2 = "0";
            var price = double.Parse($"{Price1},{Price2}");
            var amount = int.Parse(Amount!);
            return (SelectedRecordIndex, SelectedSupplierIndex, date, amount, price);
        }

        public int OpenClose()
        {
            int height;
            if (IsVisible == Visibility.Visible)
            {
                Hide();
                height = 47;
            }
            else
            {
                Show();
                height = 120;
            }
            return height;
        }

        private void Show()
        {
            IsVisible = Visibility.Visible;
        }

        private void Hide()
        {
            Clear();
            IsVisible = Visibility.Collapsed;
        }
        public void Clear()
        {
            SelectedRecordIndex = SelectedSupplierIndex = -1;
            Amount = Price1 = Price2 = string.Empty;
        }
    }
}
