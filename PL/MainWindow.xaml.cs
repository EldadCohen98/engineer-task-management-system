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
using System.Windows.Shapes;


namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void ViewList(object sender, RoutedEventArgs e)
    {
        new PL.BOEngineer.EngineerListWindow().Show();

    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        s_bl.BOEngineer.Initial();
        MessageBox.Show("Are you sure you want to delete everything?");
        if (PL.BOEngineer.EngineerListWindow.EngineerListProperty == null)
        {
            MessageBox.Show("The data has already been deleted");
            return;
        }
        else
            s_bl.BOEngineer.Reset();
        MessageBox.Show("The data has been deleted");

    }

    private void InitialData(object sender, RoutedEventArgs e)
    {
        s_bl.BOEngineer.Initial();
        MessageBox.Show("The data has been initialized");

    }
}