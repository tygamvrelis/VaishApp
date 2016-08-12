using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;
using System.Threading;

namespace Vaish
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties and fields
        public Timer CurrentTimer { get; set; }
        #endregion

        #region Constructor(s)
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this.DataContext;
        }
        #endregion

        #region Images
        private void VaishImage_MouseEnter(object sender, MouseEventArgs e)
        {
            string packUri = @"D:\Users\Tyler\Documents\Adobe\Photoshop\Projects\2016\Vaish\VaishSwagger.png";
            VaishImage.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
        }
        private void VaishImage_MouseLeave(object sender, MouseEventArgs e)
        {
            string packUri = @"D:\Users\Tyler\Documents\Adobe\Photoshop\Projects\2016\Vaish\VaishSwag.png";
            VaishImage.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
        }
        #endregion

        #region Button-related events
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTimer != null)
            {
                if (SaveName.Text != "")
                {
                    int flag = 0;
                    using (StreamReader sr = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\" + "MySavedTimers.txt"))
                    {
                        string line;
                        string[] split;
                        while((line = sr.ReadLine()) != null)
                        {
                            if((split = line.Split('\t'))[0] == SaveName.Text)
                            {
                                flag = 1;
                            }
                        }
                        if (flag == 1) { MessageBox.Show("Timer with same name already exists"); }
                    }
                    if(flag == 0)
                    {
                        SaveFile.SaveBoxContent = SaveName.Text;
                        SaveFile.Save(CurrentTimer.MyDate);
                        
                    }
                }
                else
                {
                    Status.Text = "Please enter a name for the timer";
                    DispatcherTimer dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
                    dispatcherTimer.Tick += new EventHandler(Save_Click_NoSaveName);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                    dispatcherTimer.Start();
                }
            }
            else
            {
                Status.Text = "Please choose a date first";
                DispatcherTimer dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);
                dispatcherTimer.Tick += new EventHandler(Save_Click_NoDate);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
                dispatcherTimer.Start();
            }
        }
        private void Save_Click_NoDate(object sender, EventArgs e)
        {
            if (Status.Text.ToString() == "Please choose a date first")
            {
                    Status.Text = "";
            }
        }
        private void Save_Click_NoSaveName(object sender, EventArgs e)
        {
            if (Status.Text.ToString() == "Please enter a name for the timer")
            {
                    Status.Text = "";
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                Timer MyTimer = new Timer(date.Value);
                CurrentTimer = MyTimer;

                Binding myBinding = new Binding("MyWaitString");
                myBinding.Source = MyTimer;
                TimeRemaining.SetBinding(TextBlock.TextProperty, myBinding);

                this.Title = "Time until " + date.Value.ToShortDateString();
            }
        }
        #endregion

        #region Tooltips
        private void Save_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            Button b = sender as Button;
            b.ToolTip = "Saves the current timer date and name";
        }
        private void Button_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            ComboBox b = sender as ComboBox;
            b.ToolTip = "Select a saved timer to load"; 
        }
        #endregion

        private void ComboBoxLoad_Loaded(object sender, RoutedEventArgs e)
        {
            Load mySaves = new Load();
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Load.TimerNames;
        }

        private void ComboBoxLoad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string selection = comboBox.SelectedItem as string;

            Timer MyTimer = new Timer(Load.Timers[selection]);
            CurrentTimer = MyTimer;

            Binding myBinding = new Binding("MyWaitString");
            myBinding.Source = MyTimer;
            TimeRemaining.SetBinding(TextBlock.TextProperty, myBinding);

            this.Title = "Time until " + selection;
        }
    }
}
