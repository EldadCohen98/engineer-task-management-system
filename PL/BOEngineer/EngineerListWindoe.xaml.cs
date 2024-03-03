using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.BOEngineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public IEnumerable<BO.EngineerInTask> EngineerList
    {
        get { return (IEnumerable<BO.EngineerInTask>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }
    public static readonly DependencyProperty EngineerListProperty =
    DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.EngineerInTask>), typeof(EngineerListWindow), new PropertyMetadata(null));

    public BO.EngineerLevels? levelFilterList { get; set; } = null;


    //constractor
    public EngineerListWindow()
    {
        InitializeComponent();

        List<BO.BOEngineer> EngineerTempList = new(s_bl?.BOEngineer.ReadList()!)!;
        
        List <BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();
        
        foreach (var engineer in EngineerTempList)
        {
            BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
            {
                EngineerId = engineer.EngineerId,
                EngineerName = engineer.EngineerName,
            };
            EngineerInTaskTempList.Add(EngineerInTaskTempItem);
        }
        EngineerList = EngineerInTaskTempList;
        this.Show();
    }

    private void ChangedLevelFilter(object sender, SelectionChangedEventArgs e)
    {
        ComboBox? comboBoxSender = sender as ComboBox;

        //comboBoxSender.SelectedItem == BO.EngineerLevels.Beginner
        if ((BO.EngineerLevels)comboBoxSender.SelectedItem == BO.EngineerLevels.Beginner)
        {
            List<BO.BOEngineer> engineersList = new(s_bl.BOEngineer.ReadList(item => item.LeverOfEngineer!.Value == BO.EngineerLevels.Beginner));

            List<BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();

            foreach (var engineer in engineersList)
            {
                BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
                {
                    EngineerId = engineer.EngineerId,
                    EngineerName = engineer.EngineerName,
                };
                EngineerInTaskTempList.Add(EngineerInTaskTempItem);
            }
            EngineerList = EngineerInTaskTempList;
            this.Show();
        }

        //comboBoxSender.SelectedItem == BO.EngineerLevels.AdvancedBeginner
        if ((BO.EngineerLevels)comboBoxSender.SelectedItem == BO.EngineerLevels.AdvancedBeginner)
        {
            List<BO.BOEngineer> engineersList = new(s_bl.BOEngineer.ReadList(item => item.LeverOfEngineer!.Value == BO.EngineerLevels.AdvancedBeginner));

            List<BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();

            foreach (var engineer in engineersList)
            {
                BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
                {
                    EngineerId = engineer.EngineerId,
                    EngineerName = engineer.EngineerName,
                };
                EngineerInTaskTempList.Add(EngineerInTaskTempItem);
            }
            EngineerList = EngineerInTaskTempList;
            this.Show();
        }

        //comboBoxSender.SelectedItem == BO.EngineerLevels.Advanced
        if ((BO.EngineerLevels)comboBoxSender.SelectedItem == BO.EngineerLevels.Advanced)
        {
            List<BO.BOEngineer> engineersList = new(s_bl.BOEngineer.ReadList(item => item.LeverOfEngineer!.Value == BO.EngineerLevels.Advanced));

            List<BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();

            foreach (var engineer in engineersList)
            {
                BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
                {
                    EngineerId = engineer.EngineerId,
                    EngineerName = engineer.EngineerName,
                };
                EngineerInTaskTempList.Add(EngineerInTaskTempItem);
            }
            EngineerList = EngineerInTaskTempList;
            this.Show();
        }

        //comboBoxSender.SelectedItem == BO.EngineerLevels.Intermediate
        if ((BO.EngineerLevels)comboBoxSender.SelectedItem == BO.EngineerLevels.Intermediate)
        {
            List<BO.BOEngineer> engineersList = new(s_bl.BOEngineer.ReadList(item => item.LeverOfEngineer!.Value == BO.EngineerLevels.Intermediate));

            List<BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();

            foreach (var engineer in engineersList)
            {
                BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
                {
                    EngineerId = engineer.EngineerId,
                    EngineerName = engineer.EngineerName,
                };
                EngineerInTaskTempList.Add(EngineerInTaskTempItem);
            }
            EngineerList = EngineerInTaskTempList;
            this.Show();
        }

        //comboBoxSender.SelectedItem == BO.EngineerLevels.Expert
        if ((BO.EngineerLevels)comboBoxSender.SelectedItem == BO.EngineerLevels.Expert)
        {
            List<BO.BOEngineer> engineersList = new(s_bl.BOEngineer.ReadList(item => item.LeverOfEngineer!.Value == BO.EngineerLevels.Expert));

            List<BO.EngineerInTask> EngineerInTaskTempList = new List<BO.EngineerInTask>();

            foreach (var engineer in engineersList)
            {
                BO.EngineerInTask EngineerInTaskTempItem = new BO.EngineerInTask()
                {
                    EngineerId = engineer.EngineerId,
                    EngineerName = engineer.EngineerName,
                };
                EngineerInTaskTempList.Add(EngineerInTaskTempItem);
            }
            EngineerList = EngineerInTaskTempList;
            this.Show();
        }
    }

    private void ComboBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        List<BO.EngineerLevels> levels = new();
        Array enumValues = Enum.GetValues(typeof(BO.EngineerLevels));

        foreach (BO.EngineerLevels level in enumValues)
        {
            levels.Add(level);
        }
        List<BO.EngineerLevels> staticListLevels = new(levels);
        levelList.ItemsSource = staticListLevels;
    }

    private void AddButton(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().Show();
    }


    private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.EngineerInTask? EngineerInList = (sender as ListView)?.SelectedItem as BO.EngineerInTask;
        new EngineerWindow(EngineerInList!.EngineerId).Show();
    }
}


