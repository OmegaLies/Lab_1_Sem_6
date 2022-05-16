using System;
using System.Windows;
using System.Windows.Controls;
using ClassLibrary;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public ViewData Vdata { get; set; }
        public VMGrid grid { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Vdata = new();
            grid = new();
            DataContext = this;
        }

        public void Click_New(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Vdata.change_property)
                {
                    MessageBoxResult result = MessageBox.Show("Save changes?", "Warning", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        Click_Save(sender, e);
                    }
                    else if (result == MessageBoxResult.Cancel) return;
                }
                Vdata.Bench.time.Clear();
                Vdata.Bench.accurasy.Clear();
                Vdata.change_property = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Click_Open(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Vdata.change_property)
                {
                    MessageBoxResult result = MessageBox.Show("Save changes?", "Warning", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        Click_Save(sender, e);
                    }
                    else if (result == MessageBoxResult.Cancel) return;
                }

                Microsoft.Win32.OpenFileDialog file = new();
                if (file.ShowDialog() == true)
                {
                    Vdata.Load(file.FileName);
                    Vdata.change_property = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog file = new();
                if (file.ShowDialog() == true)
                {
                    Vdata.Save(file.FileName);
                }
                Vdata.change_property = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Click_Time(object sender, RoutedEventArgs e)
        {
            try
            {
                Vdata.AddVMTime(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Click_Accuracy(object sender, RoutedEventArgs e)
        {
            try
            {
                Vdata.AddVMAccuracy(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NodesTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                grid.n = int.Parse(Nodes.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FirstTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                grid.first = Convert.ToDouble(First.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LastTextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                grid.last = Convert.ToDouble(Last.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
