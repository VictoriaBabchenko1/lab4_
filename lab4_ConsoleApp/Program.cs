using ClassLibrary1;
using Newtonsoft.Json;
using System;
using System.IO;

namespace lab4_ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            // Generate random objects and serialize to JSON
            MeasurementChannel channel = GenerateRandomChannel();
            SerializeToJson(channel, "measurement_channel1.json");

            MeasurementChannel channel2 = GenerateRandomChannel();
            SerializeToJson(channel2, "measurement_channel2.json");

            MeasurementChannel channel3 = GenerateRandomChannel();
            SerializeToJson(channel3, "measurement_channel3.json");

            // Deserialize from JSON and display on the screen
            MeasurementChannel deserializedChannel = DeserializeFromJson("measurement_channel1.json");
            Console.WriteLine(deserializedChannel);

            MeasurementChannel deserializedChannel2 = DeserializeFromJson("measurement_channel2.json");
            Console.WriteLine(deserializedChannel2);

            MeasurementChannel deserializedChannel3 = DeserializeFromJson("measurement_channel3.json");
            Console.WriteLine(deserializedChannel3);
        }

        private static MeasurementChannel GenerateRandomChannel()
        {
            MeasurementChannel channel = new MeasurementChannel();
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                DateTime calibrationDate = DateTime.Now.AddDays(-random.Next(1, 365));
                Sensor sensor = new Sensor((MeasurementType)random.Next(5), random.NextDouble() * 100, random.NextDouble() * 1000, 1);
                Device device = new Device(sensor, i, calibrationDate);
                channel.AddDevice(device);
            }

            return channel;
        }

        private static void SerializeToJson(MeasurementChannel channel, string fileName)
        {
            string json = JsonConvert.SerializeObject(channel.ToDtoMeasurementChannel(), Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        private static MeasurementChannel DeserializeFromJson(string fileName)
        {
            string jsonContent = File.ReadAllText(fileName);
            DtoMeasurementChannel dtoChannel = JsonConvert.DeserializeObject<DtoMeasurementChannel>(jsonContent);
            return MeasurementChannel.FromDtoMeasurementChannel(dtoChannel);
        }
    }
}
