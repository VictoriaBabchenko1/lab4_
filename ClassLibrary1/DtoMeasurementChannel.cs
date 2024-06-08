using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DtoMeasurementChannel
    {
        public static int TotalChannels { get; set; }

        [Required(ErrorMessage = "Channel number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Channel number must be a positive integer.")]
        public int ChannelNumber { get; set; }

        public List<DtoDevice> Devices { get; set; }
    }
}
