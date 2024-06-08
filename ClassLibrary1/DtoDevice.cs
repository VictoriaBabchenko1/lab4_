using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DtoDevice
    {
        [Required(ErrorMessage = "Sensor is required.")]
        public DtoSensor Sensor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Mounting place must be a positive integer.")]
        public int MountingPlace { get; set; }

        [Required(ErrorMessage = "Calibration date is required.")]
        public DateTime CalibrationDate { get; set; }
    }
}
