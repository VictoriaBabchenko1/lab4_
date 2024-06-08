using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Serializable]
    public class Sensor : ICloneable, IComparable
    {
        private MeasurementType type;
        private double minValue;
        private double maxValue;
        private double currentValue;

        public Sensor(MeasurementType type, double minValue, double maxValue, double currentValue)
        {
            this.type = type;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }

        public override string ToString()
        {
            return $"Value Type: {type}, Min Value: {minValue}, Max Value: {maxValue}, Current Value: {currentValue}";
        }

        public DtoSensor ToDtoSensor()
        {
            return new DtoSensor
            {
                Type = type,
                MinValue = minValue,
                MaxValue = maxValue,
                CurrentValue = currentValue
            };
        }

        public static Sensor FromDtoSensor(DtoSensor dtoSensor)
        {
            return new Sensor(dtoSensor.Type, dtoSensor.MinValue, dtoSensor.MaxValue, dtoSensor.CurrentValue);
        }

        public object Clone()
        {
            return new Sensor(type, minValue, maxValue, currentValue);
        }

        public int CompareTo(object obj)
        {
            if (obj is Sensor sensor)
            {
                return currentValue.CompareTo(sensor.currentValue);
            }
            else
            {
                throw new ArgumentException("Object must be of type Sensor.");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Sensor sensor)
            {
                return type == sensor.type &&
                       minValue == sensor.minValue &&
                       maxValue == sensor.maxValue &&
                       currentValue.Equals(sensor.currentValue);
            }
            else
            {
                return false;
            }
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(type, minValue, maxValue, currentValue);
        //}

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + type.GetHashCode();
                hash = hash * 23 + minValue.GetHashCode();
                hash = hash * 23 + maxValue.GetHashCode();
                hash = hash * 23 + currentValue.GetHashCode();
                return hash;
            }
        }
    }
}
