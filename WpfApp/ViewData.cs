using System;
using System.Windows;
using System.ComponentModel;
using ClassLibrary;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;


namespace WpfApp
{
    public class ViewData : INotifyPropertyChanged
    {
        public VMBenchmark Bench { get; set; }

        public bool change;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public bool change_property
        {
            get
            {
                return change;
            }
            set
            {
                if (value != change)
                {
                    change = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(change_property)));
                }
            }
        }
        private void Coll_changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            change_property = true;
        }

        public ViewData()
        {
            Bench = new();
            change = false;
            Bench.time.CollectionChanged += Coll_changed;
            Bench.accurasy.CollectionChanged += Coll_changed;
        }
        
        public void AddVMTime(VMGrid grid)
        {
            Bench.AddVMTime(grid);
        }

        public void AddVMAccuracy(VMGrid grid)
        {
            Bench.AddVMAccuracy(grid);
        }

        public bool Save(string file)
        {
            FileStream f = null;
            try
            {
                using (f = new FileStream(file, FileMode.OpenOrCreate))
                {
                    StreamWriter writer = new(f);

                    writer.WriteLine(Bench.time.Count);
                    for (int i = 0; i < Bench.time.Count; i++)
                    {
                        writer.WriteLine((int)Bench.time[i].grid.func);
                        writer.WriteLine(Bench.time[i].grid.first);
                        writer.WriteLine(Bench.time[i].grid.last);
                        writer.WriteLine(Bench.time[i].grid.n);
                    }
                    writer.WriteLine(Bench.accurasy.Count);
                    for (int i = 0; i < Bench.accurasy.Count; i++)
                    {
                        writer.WriteLine((int)Bench.accurasy[i].grid.func);
                        writer.WriteLine(Bench.accurasy[i].grid.first);
                        writer.WriteLine(Bench.accurasy[i].grid.last);
                        writer.WriteLine(Bench.accurasy[i].grid.n);
                    }
                    writer.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Bench.time.Clear();
                Bench.accurasy.Clear();
                MessageBox.Show("File saving error\n", ex.Message);
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (f != null) f.Close();
            }
        }

        public bool Load(string file)
        {
            FileStream f = null;
            try
            {
                Bench.time.Clear();
                Bench.accurasy.Clear();
                using (f = new FileStream(file, FileMode.OpenOrCreate))
                {
                    StreamReader reader = new(f);

                    int len_time_list = Convert.ToInt32(reader.ReadLine());
                    for (int i = 0; i < len_time_list; i++)
                    {
                        VMf func = (VMf)Convert.ToInt32(reader.ReadLine());
                        double first = Convert.ToDouble(reader.ReadLine());
                        double last = Convert.ToDouble(reader.ReadLine());
                        int n = Convert.ToInt32(reader.ReadLine());
                        VMGrid grid = new(n, first, last, func);
                        Bench.AddVMTime(grid);
                    }

                    int len_acc_list = Convert.ToInt32(reader.ReadLine());
                    for (int i = 0; i < len_acc_list; i++)
                    {
                        VMf func = (VMf)Convert.ToInt32(reader.ReadLine());
                        double first = Convert.ToDouble(reader.ReadLine());
                        double last = Convert.ToDouble(reader.ReadLine());
                        int n = Convert.ToInt32(reader.ReadLine());
                        VMGrid grid = new(n, first, last, func);
                        Bench.AddVMAccuracy(grid);
                    }
                    reader.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Bench.time.Clear();
                Bench.accurasy.Clear();
                MessageBox.Show("Data loading error\n", ex.Message);
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (f != null) f.Close();
            }
        }
    }
}
