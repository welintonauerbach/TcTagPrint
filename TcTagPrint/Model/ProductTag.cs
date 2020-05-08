using System.ComponentModel;

namespace TcTagPrint.Model
{
    /// <summary>
    /// Classe Model da TAG
    /// </summary>
    public class ProductTag : INotifyPropertyChanged
    {
        private bool _print;
        private bool _printed;
        private string _position;
        private string _item;
        private string _description;
        private string _of;
        private string _orderNumber;
        private string _code;
        private string _drawingCodeName;
        private int _quantity;

        public bool Print
        {
            get => _print;
            set
            {
                _print = value;
                OnPropertyChanged("Print");
            }
        }

        public bool Printed
        {
            get => _printed;
            set
            {
                _printed = value;
                OnPropertyChanged("Printed");
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Of
        {
            get => _of;
            set
            {
                _of = value;
                OnPropertyChanged("Of");
            }
        }

        public string OrderNumber
        {
            get => _orderNumber;
            set
            {
                _orderNumber = value;
                OnPropertyChanged("OrderNumber");
            }
        }

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        public string DrawingCodeName
        {
            get => _drawingCodeName;
            set
            {
                _drawingCodeName = value;
                OnPropertyChanged("DrawingCodeName");
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
