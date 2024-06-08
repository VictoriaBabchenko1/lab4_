using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace lab4_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для NewDeviceForm.xaml
    /// </summary>
    public partial class NewDeviceForm : Window
    {
        public Device device;

        public NewDeviceForm()
        {
            InitializeComponent();
            SensorTypeComboBox.ItemsSource = Enum.GetValues(typeof(MeasurementType));
        }

        public NewDeviceForm(Device device) : this()
        {
            this.device = device;
            LoadDeviceData();
        }

        private void LoadDeviceData()
        {
            if (device != null)
            {
                var dtoDevice = device.ToDtoDevice();
                var dtoSensor = dtoDevice.Sensor;

                SensorTypeComboBox.SelectedItem = dtoSensor.Type;
                MinValueTextBox.Text = dtoSensor.MinValue.ToString();
                MaxValueTextBox.Text = dtoSensor.MaxValue.ToString();
                CurrentValueTextBox.Text = dtoSensor.CurrentValue.ToString();
                MountingPlaceTextBox.Text = dtoDevice.MountingPlace.ToString();
                CalibrationDatePicker.SelectedDate = dtoDevice.CalibrationDate;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(MinValueTextBox.Text, out double minValue) &&
                double.TryParse(MaxValueTextBox.Text, out double maxValue) &&
                double.TryParse(CurrentValueTextBox.Text, out double currentValue) &&
                int.TryParse(MountingPlaceTextBox.Text, out int mountingPlace) &&
                CalibrationDatePicker.SelectedDate.HasValue &&
                SensorTypeComboBox.SelectedItem is MeasurementType type)
            {
                if (currentValue >= minValue && currentValue <= maxValue)
                {
                    Sensor sensor = new Sensor(type, minValue, maxValue, currentValue);
                    device = new Device(sensor, mountingPlace, CalibrationDatePicker.SelectedDate.Value);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Current value must be between minimum and maximum values.");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please check the values.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
