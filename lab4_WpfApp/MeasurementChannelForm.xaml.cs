using ClassLibrary1;
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

namespace lab4_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MeasurementChannelForm.xaml
    /// </summary>
    public partial class MeasurementChannelForm : Window
    {
        private MeasurementChannel channel;

        public MeasurementChannelForm(MeasurementChannel channel)
        {
            InitializeComponent();
            this.channel = channel;
            ChannelNumberTextBox.Text = channel.ToDtoMeasurementChannel().ChannelNumber.ToString();
            UpdateDeviceList();
        }

        private void UpdateDeviceList()
        {
            DevicesListBox.ItemsSource = channel.ToDtoMeasurementChannel().Devices.Select(d => Device.FromDtoDevice(d).ToString()).ToList();
        }

        private void AddDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            NewDeviceForm form = new NewDeviceForm();
            if (form.ShowDialog() == true)
            {
                Device newDevice = form.device;
                channel.AddDevice(newDevice);
                UpdateDeviceList();
            }
        }

        private void EditDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedIndex >= 0)
            {
                DtoDevice selectedDevice = channel.ToDtoMeasurementChannel().Devices[DevicesListBox.SelectedIndex];
                NewDeviceForm form = new NewDeviceForm(Device.FromDtoDevice(selectedDevice));
                channel.RemoveDevice(form.device);

                if (form.ShowDialog() == true)
                {
                    Device newDevice = form.device;
                    channel.AddDevice(newDevice);
                    UpdateDeviceList();
                }
            }
            else
            {
                MessageBox.Show("Please select a device to edit.");
            }
        }

        private void DeleteDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedIndex >= 0)
            {
                DtoDevice selectedDevice = channel.ToDtoMeasurementChannel().Devices[DevicesListBox.SelectedIndex];
                if (selectedDevice != null)
                {
                    channel.RemoveDevice(Device.FromDtoDevice(selectedDevice));
                    UpdateDeviceList();
                }
            }
            else
            {
                MessageBox.Show("Please select a device to delete.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ChannelNumberTextBox.Text, out int channelNumber))
            {
                channel.ToDtoMeasurementChannel().ChannelNumber = channelNumber;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Invalid Channel Number");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
