using DO;
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

namespace PL.BOEngineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{

    private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.BOEngineer CurrentEngineer
    {
        get { return (BO.BOEngineer)GetValue(CurrentEngineerPropertys); }
        set { SetValue(CurrentEngineerPropertys, value); }
    }

    public static readonly DependencyProperty CurrentEngineerPropertys =
    DependencyProperty.Register("CurrentEngineer", typeof(BO.BOEngineer), typeof(EngineerWindow), new PropertyMetadata(null));

    public EngineerWindow(int IdEngineer = 0)
    {
        InitializeComponent();
        if (IdEngineer == 0)
        {
            BO.BOEngineer bOEngineer = new BO.BOEngineer();
            CurrentEngineer = bOEngineer;
            this.Show();
        }

        else
        {
            CurrentEngineer = s_bl.BOEngineer.Read(IdEngineer);
            this.Show();
        }
    }



    private void AddNewEngineer(object sender, RoutedEventArgs e)
    {
        if (s_bl.BOEngineer.Read(CurrentEngineer.EngineerId) == null)
        {
            BO.BOEngineer newEngineerToList = new BO.BOEngineer()
            {
                EngineerId = CurrentEngineer.EngineerId,
                EngineerName = CurrentEngineer.EngineerName,
                EngineerEmail = CurrentEngineer.EngineerEmail,
                LeverOfEngineer = CurrentEngineer.LeverOfEngineer,
                SalaryPerHour = CurrentEngineer.SalaryPerHour,
            };

            s_bl.BOEngineer.Add(newEngineerToList);
            MessageBox.Show("The engineer has been added");
        }
        else
        {
            BO.BOEngineer newEngineerToList = new BO.BOEngineer()
            {
                EngineerId = CurrentEngineer.EngineerId,
                EngineerName = CurrentEngineer.EngineerName,
                EngineerEmail = CurrentEngineer.EngineerEmail,
                LeverOfEngineer = CurrentEngineer.LeverOfEngineer,
                SalaryPerHour = CurrentEngineer.SalaryPerHour,
            };

            s_bl.BOEngineer.Update(newEngineerToList);
            MessageBox.Show("The engineer has been updeted");
        }
    }

}
