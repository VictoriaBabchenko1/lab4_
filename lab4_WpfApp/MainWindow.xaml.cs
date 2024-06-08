using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
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
using Newtonsoft.Json;

namespace lab4_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<MeasurementChannel> channels = new List<MeasurementChannel>();

        public MainWindow()
        {
            InitializeComponent();
            LoadChannels();
        }

        private void LoadChannels()
        {
            try
            {
                if (File.Exists("channels.json"))
                {
                    string json = File.ReadAllText("channels.json");
                    List<DtoMeasurementChannel> dtoChannels = JsonConvert.DeserializeObject<List<DtoMeasurementChannel>>(json);
                    channels = dtoChannels.Select(dtoChannel => MeasurementChannel.FromDtoMeasurementChannel(dtoChannel)).ToList();
                }
                else
                {
                    channels = new List<MeasurementChannel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load channels: {ex.Message}");
                channels = new List<MeasurementChannel>();
            }
            UpdateChannelList();
        }

        private void SaveChannels()
        {
            try
            {
                List<DtoMeasurementChannel> dtoChannels = channels.Select(channel => channel.ToDtoMeasurementChannel()).ToList();
                string json = JsonConvert.SerializeObject(dtoChannels, Formatting.Indented);
                File.WriteAllText("channels.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save channels: {ex.Message}");
            }
        }

        private void AddChannelButton_Click(object sender, RoutedEventArgs e)
        {
            MeasurementChannel channel = new MeasurementChannel();
            MeasurementChannelForm form = new MeasurementChannelForm(channel);
            if (form.ShowDialog() == true)
            {
                channels.Add(channel);
                UpdateChannelList();
                SaveChannels();
            }
        }

        private void EditChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChannelListBox.SelectedIndex >= 0)
            {
                MeasurementChannel channel = channels[ChannelListBox.SelectedIndex];
                MeasurementChannelForm form = new MeasurementChannelForm(channel);
                if (form.ShowDialog() == true)
                {
                    UpdateChannelList();
                    SaveChannels();
                }
            }
            else
            {
                MessageBox.Show("Please select a channel to edit.");
            }
        }

        private void DeleteChannelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChannelListBox.SelectedIndex >= 0)
            {
                channels.RemoveAt(ChannelListBox.SelectedIndex);
                UpdateChannelList();
                SaveChannels();
            }
            else
            {
                MessageBox.Show("Please select a channel to delete.");
            }
        }

        private void UpdateChannelList()
        {
            ChannelListBox.ItemsSource = null;
            ChannelListBox.ItemsSource = channels.Select(c => c.ToString()).ToList();
        }
    }
}
