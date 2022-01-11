using ICSharpCode.SharpZipLib.BZip2;
using PCHardwareMonitor.Hardware;
using PCHardwareMonitor.Hardware.Motherboard;
using PCHardwareMonitor.UI;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Node = PCHardwareMonitor.UI.Node;

namespace PCHardwareMonitor.Utilities
{
    internal class CloudReporter
    {
        private string _fileName;
        private DateTime _lastLoggedTime = DateTime.MinValue;

        public string APIToken;

        public TimeSpan ReportingInterval { get; set; }

        public CloudReporter(string apiToken)
        {
            APIToken = apiToken;
            _fileName = GetFileName();
        }

        public async Task CloudReportAsync(Node root)
        {
            DateTime now = DateTime.Now;

            if (_lastLoggedTime + ReportingInterval - new TimeSpan(5000000) > now)
                return;

            JObject json = new JObject();

            int nodeIndex = 0;

            json["id"] = nodeIndex++;
            json["Text"] = "PCHardwareMonitor";
            json["APIToken"] = APIToken;

            JArray children = new JArray { GenerateJsonForNode(root, ref nodeIndex) };
            json["Children"] = children;

            string reportDataFile = json.ToString(Newtonsoft.Json.Formatting.Indented);
            string reportData = json.ToString(Newtonsoft.Json.Formatting.None);

            string compressedData = string.Empty;
            using (MemoryStream source = new MemoryStream(Encoding.UTF8.GetBytes(reportData)))
            {
                using (MemoryStream target = new MemoryStream())
                {
                    BZip2.Compress(source, target, true, 4096);
                    byte[] targetByteArray = target.ToArray();
                    compressedData = Convert.ToBase64String(targetByteArray);
                }
            }

            string cloudJsonData = "{\"Data\": \"" + compressedData + "\"}";

            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(4) })
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(
                    "http://127.0.0.1:8000/monitor/",
                     new StringContent(cloudJsonData, Encoding.UTF8, "application/json"));
                }
                catch (Exception e) { }
            }
            WriteReportFile(reportDataFile);

            _lastLoggedTime = now;
        }

        private void WriteReportFile(string reportData)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                {
                    writer.Write(reportData);
                }
            }
            catch (IOException) { }
        }

        private static string GetFileName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "PCHardwareMonitorCloudReport.txt";
        }

        private JObject GenerateJsonForNode(Node n, ref int nodeIndex)
        {
            JObject jsonNode = new JObject
            {
                ["id"] = nodeIndex++,
                ["Text"] = n.Text,
            };

            if (n is SensorNode sensorNode)
            {
                jsonNode["SensorId"] = sensorNode.Sensor.Identifier.ToString();
                jsonNode["Type"] = sensorNode.Sensor.SensorType.ToString();
                jsonNode["Min"] = sensorNode.Min;
                jsonNode["Value"] = sensorNode.Value;
                jsonNode["Max"] = sensorNode.Max;
            }
            else if (n is HardwareNode hardwareNode)
            {
                jsonNode["Type"] = hardwareNode.Hardware.HardwareType.ToString();
                if (hardwareNode.Hardware.HardwareType == HardwareType.Motherboard)
                {
                    jsonNode["Serial"] = ((Motherboard)hardwareNode.Hardware).SMBios.Board.SerialNumber;
                }
            }
            else if (n is TypeNode typeNode)
            {
                jsonNode["Type"] = typeNode.SensorType.ToString();
            }
            else
            {
                jsonNode["Type"] = "Computer";
            }

            JArray children = new JArray();
            foreach (Node child in n.Nodes)
            {
                children.Add(GenerateJsonForNode(child, ref nodeIndex));
            }

            jsonNode["Children"] = children;

            return jsonNode;
        }
    }
}
