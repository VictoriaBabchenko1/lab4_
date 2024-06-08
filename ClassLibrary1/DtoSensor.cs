using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DtoSensor
    {
        [Required(ErrorMessage = "Measurement type is required.")]
        public MeasurementType Type { get; set; }

        [Range(-100.0, 1000.0, ErrorMessage = "Min value must be between -100 and 100.")]
        public double MinValue { get; set; }


        [Range(-100.0, 1000.0, ErrorMessage = "Max value must be between -100 and 100.")]
        public double MaxValue { get; set; }


        [Range(-100.0, 1000.0, ErrorMessage = "Current value must be between -100 and 100.")]
        public double CurrentValue { get; set; }
    }
}
