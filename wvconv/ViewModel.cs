using System.ComponentModel;

namespace wvconv {
    class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly PropertyChangedEventArgs IndexPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Index));
        private int _index = 0;
        public int Index {
            get => _index;
            set {
                if (_index == value) return;
                _index = value;
                PropertyChanged?.Invoke(this, IndexPropertyChangedEventArgs);
            }
        }


        private static readonly PropertyChangedEventArgs IdlePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Idle));
        private bool _idle = true;
        public bool Idle {
            get => _idle;
            set {
                if (_idle == value) return;
                _idle = value;
                PropertyChanged?.Invoke(this, IdlePropertyChangedEventArgs);
            }
        }

        private static readonly PropertyChangedEventArgs PvaluePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Pvalue));
        private double _pvalue;
        public double Pvalue {
            get => _pvalue;
            set {
                if (_pvalue == value) return;
                _pvalue = value;
                PropertyChanged?.Invoke(this, PvaluePropertyChangedEventArgs);
            }
        }

        private static readonly PropertyChangedEventArgs PtextPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Ptext));
        private string _ptext;
        public string Ptext {
            get => _ptext;
            set {
                if (_ptext == value) return;
                _ptext = value;
                PropertyChanged?.Invoke(this, PtextPropertyChangedEventArgs);
            }
        }

        private static readonly PropertyChangedEventArgs DeltaTextPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(DeltaText));
        private string _deltatext;
        public string DeltaText {
            get => _deltatext;
            set {
                if (_deltatext == value) return;
                _deltatext = value;
                PropertyChanged?.Invoke(this, DeltaTextPropertyChangedEventArgs);
            }
        }
    }
}
