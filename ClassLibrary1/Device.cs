using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Serializable]
    public class Device : IComparable, ICloneable
    {
        private Sensor sensor;
        private int mountingPlace;
        private DateTime calibrationDate;

        public Device(Sensor sensor, int mountingPlace, DateTime calibrationDate)
        {
            this.sensor = sensor;
            this.mountingPlace = mountingPlace;
            this.calibrationDate = calibrationDate;
        }

        public override string ToString()
        {
            return $"Mounting Place: {mountingPlace}, Calibration Date: {calibrationDate}, Sensor Info: {sensor}";
        }

        public static Device FromDtoDevice(DtoDevice dtoDevice)
        {
            return new Device(Sensor.FromDtoSensor(dtoDevice.Sensor), dtoDevice.MountingPlace, dtoDevice.CalibrationDate);
        }

        public DtoDevice ToDtoDevice()
        {
            return new DtoDevice
            {
                MountingPlace = mountingPlace,
                CalibrationDate = calibrationDate,
                Sensor = sensor.ToDtoSensor()
            };
        }
        public object Clone()
        {
            return new Device(sensor.Clone() as Sensor, mountingPlace, calibrationDate);
        }

        public int CompareTo(object obj)
        {
            if (obj is Device otherDevice)
            {
                return calibrationDate.CompareTo(otherDevice.calibrationDate);
            }
            else
            {
                throw new ArgumentException("Object must be of type Device.");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Device device)
            {
                return mountingPlace == device.mountingPlace &&
                       calibrationDate.Equals(device.calibrationDate) &&
                       sensor.Equals(device.sensor);
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
                hash = hash * 23 + mountingPlace.GetHashCode();
                hash = hash * 23 + calibrationDate.GetHashCode();
                hash = hash * 23 + (sensor != null ? sensor.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
