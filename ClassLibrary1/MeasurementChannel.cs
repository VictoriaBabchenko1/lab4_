using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Serializable]
    public class MeasurementChannel : IComparable, ICloneable
    {
        private static int totalChannels = 0;
        private int channelNumber;

        private List<Device> devices;

        public MeasurementChannel()
        {
            channelNumber = ++totalChannels;
            devices = new List<Device>();
        }

        public void AddDevice(Device device)
        {
            devices.Add(device);
        }

        public void RemoveDevice(Device device)
        {
            if (devices.Contains(device))
            {
                devices.Remove(device);
            }
        }

        public override string ToString()
        {
            return $"Measurement Channel {channelNumber}";
        }

        public DtoMeasurementChannel ToDtoMeasurementChannel()
        {
            return new DtoMeasurementChannel
            {
                ChannelNumber = channelNumber,
                Devices = devices.Select(d => d.ToDtoDevice()).ToList()
            };
        }

        public static MeasurementChannel FromDtoMeasurementChannel(DtoMeasurementChannel dtoMeasurementChannel)
        {
            MeasurementChannel channel = new MeasurementChannel
            {
                channelNumber = dtoMeasurementChannel.ChannelNumber
            };

            foreach (DtoDevice dtoDevice in dtoMeasurementChannel.Devices)
            {
                channel.AddDevice(Device.FromDtoDevice(dtoDevice));
            }

            return channel;
        }

        public int CompareTo(object obj)
        {
            if (obj is MeasurementChannel channel)
            {
                return devices.Count.CompareTo(channel.devices.Count);
            }
            else
            {
                throw new ArgumentException("Object must be of type MeasurementChannel.");
            }
        }

        public object Clone()
        {
            MeasurementChannel channel = new MeasurementChannel();

            foreach (Device device in devices)
            {
                channel.AddDevice(device.Clone() as Device);
            }

            return channel;
        }

        public override bool Equals(object obj)
        {
            if (obj is MeasurementChannel channel)
            {
                return channelNumber == channel.channelNumber &&
                       devices.TrueForAll(d => channel.devices.Exists(cd => cd.Equals(d)));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + channelNumber.GetHashCode();
                foreach (var device in devices)
                {
                    hash = hash * 23 + (device != null ? device.GetHashCode() : 0);
                }
                return hash;
            }
        }
    }
}
