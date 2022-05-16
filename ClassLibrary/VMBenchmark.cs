using System;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Specialized;


namespace ClassLibrary
{
    [Serializable]
    public class VMBenchmark: INotifyPropertyChanged
    {
        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll_Lib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_sin(int n, double[] a, double[] y, char c);

        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll_Lib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_cos(int n, double[] a, double[] y, char c);

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<VMTime> time { get; set; }
        public ObservableCollection<VMAccuracy> accurasy { get; set; }

        public VMBenchmark()
        {
            time = new ObservableCollection<VMTime>();
            accurasy = new ObservableCollection<VMAccuracy>();
            time.CollectionChanged += Coll_Change;
            accurasy.CollectionChanged += Coll_Change;
        }

        public bool AddVMTime(VMGrid grid)
        {
            try
            {
                int n = grid.n;
                double[] a = new double[n];
                double[] y_HA = new double[n];
                double[] y_LA = new double[n];
                double[] y_EP = new double[n];
                if (n != 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        a[i] = grid.first + grid.step * i;
                    }
                }
                else
                {
                    a[0] = (grid.first - grid.first) / 2;
                }

                VMTime t;
                if (grid.func == VMf.vmdSin)
                {
                    t = new(grid, a, ref y_HA, ref y_HA, ref y_HA, mkl_sin);
                    time.Add(t);
                }
                else if (grid.func == VMf.vmdCos)
                {
                    t = new(grid, a, ref y_HA, ref y_HA, ref y_HA, mkl_cos);
                    time.Add(t);
                }
                else if (grid.func == VMf.vmdSinCos)
                {
                    t = new(grid, a, ref y_HA, ref y_HA, ref y_HA, mkl_sin);
                    time.Add(t);
                    t = new(grid, a, ref y_HA, ref y_HA, ref y_HA, mkl_cos);
                    time.Add(t);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
        }

        public bool AddVMAccuracy(VMGrid grid)
        {
            try
            {
                VMAccuracy acc = new(grid);
                int n = grid.n;
                double[] a = new double[n];
                double[] y_HA = new double[n];
                double[] y_LA = new double[n];
                double[] y_EP = new double[n];
                if (n != 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        a[i] = grid.first + grid.step * i;
                    }
                }
                else
                {
                    a[0] = (grid.first - grid.first) / 2;
                }

                VMTime t;
                if (grid.func == VMf.vmdSin)
                {
                    t = new(grid, a, ref y_HA, ref y_LA, ref y_EP, mkl_sin);
                }
                else if (grid.func == VMf.vmdCos)
                {
                    t = new(grid, a, ref y_HA, ref y_LA, ref y_EP, mkl_cos);
                }
                else if (grid.func == VMf.vmdSinCos)
                {
                    t = new(grid, a, ref y_HA, ref y_LA, ref y_EP, mkl_sin);
                    acc.MaxAbs(a, y_HA, y_EP);
                    accurasy.Add(acc);
                    t = new(grid, a, ref y_HA, ref y_LA, ref y_EP, mkl_cos);
                }
                acc.MaxAbs(a, y_HA, y_EP);
                accurasy.Add(acc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
            return true;
        }

        public double min_HA
        {
            get
            {
                double min = -1;
                if (time.Count != 0)
                {
                    min = time[0].time_HA;
                    for (int i = 1; i < time.Count; i++)
                    {
                        if (time[i].time_HA < min)
                        {
                            min = time[i].time_HA;
                        }
                    }
                }
                return min;
            }
        }

        public double min_EP
        {
            get
            {
                double min = -1;
                if (time.Count != 0)
                {
                    min = time[0].time_EP;
                    for (int i = 1; i < time.Count; i++)
                    {
                        if (time[i].time_EP < min)
                        {
                            min = time[i].time_EP;
                        }
                    }
                }
                return min;
            }
        }

        private void Coll_Change(object sender, NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(min_HA)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(min_EP)));
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
