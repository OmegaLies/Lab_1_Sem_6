using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary
{
    [Serializable]
    public class VMGrid: INotifyPropertyChanged
    {
        public int n { get; set; }
        public double first { get; set; }
        public double last { get; set; }
        public double step
        {
            get
            {
                return Math.Abs(last - first) / (n - 1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public VMf func { get; set; }

        public VMGrid(int N = 10, double F = 0, double L = 5, VMf f = VMf.vmdSin)
        { 
            n = N;
            first = F;
            last = L;
            func = f;
        }
        public VMGrid(VMGrid grid)
        {
            n = grid.n;
            first = grid.first;
            last = grid.last;
            func = grid.func;
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
