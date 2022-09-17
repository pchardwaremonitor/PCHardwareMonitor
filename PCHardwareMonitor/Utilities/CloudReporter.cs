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
using System.Diagnostics;

namespace PCHardwareMonitor.Utilities
{
    internal class CloudReporter
    {
        private string _fileName;
        private double _nextReportTime = DateTime.MinValue.TimeOfDay.TotalMilliseconds;
        private double _lastReportTime = DateTime.MaxValue.TimeOfDay.TotalMilliseconds;

        public string APIToken;

        public TimeSpan ReportingInterval { get; set; }

        public CloudReporter(string apiToken)
        {
            APIToken = apiToken;
            _fileName = GetFileName();
        }

        public async Task CloudReportAsync(Node root, System.Windows.Forms.ToolStripStatusLabel statusBarTextLabel)
        {
            // Next report time has not been reached
            if (_nextReportTime > DateTime.Now.TimeOfDay.TotalMilliseconds)
            {
                return;
            }

            if (!APIToken.Equals(""))
            {
                statusBarTextLabel.Text = "[" + DateTime.Now + "] Sending Cloud Report.";
            }

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

            WriteReportFile(reportDataFile);

            string cloudJsonData = "{\"Data\": \"" + compressedData + "\"}";

            _nextReportTime = DateTime.MaxValue.TimeOfDay.TotalMilliseconds;

            using (var client = new HttpClient())
            {
                try
                {
                    string api_server = "";
#if DEBUG
                    api_server = "http://127.0.0.1:8000";
#else
                    api_server = "https://api.pchwmonitor.com";
#endif
                    double start_time = DateTime.Now.TimeOfDay.TotalMilliseconds;
                    HttpResponseMessage response = await client.PostAsync(
                    api_server + "/monitor/",
                     new StringContent(cloudJsonData, Encoding.UTF8, "application/json"));

                    string response_status = response.Content.ReadAsStringAsync().Result.Replace("\"", "");
                    double elapsed_time = DateTime.Now.TimeOfDay.TotalMilliseconds - start_time;

                    if (response_status.Equals("OK"))
                    {
                        if (elapsed_time < 1000)
                        {
                            statusBarTextLabel.Text = "[" + DateTime.Now + "] Cloud Reporting " + response_status + ". Time: " + (int)elapsed_time + " ms";
                        }
                        else
                        {
                            statusBarTextLabel.Text = "[" + DateTime.Now + "] Cloud Reporting " + response_status + ". Time: " + Math.Round(elapsed_time / 1000.0, 1) + " s";
                        }
                    }

                    double nextExpectedReportTime = _lastReportTime + ReportingInterval.TotalMilliseconds;
                    if (nextExpectedReportTime >= DateTime.Now.TimeOfDay.TotalMilliseconds)
                    {
                        _nextReportTime = DateTime.MinValue.TimeOfDay.TotalMilliseconds;
                    }
                    else
                    {
                        _nextReportTime = DateTime.Now.TimeOfDay.TotalMilliseconds + (nextExpectedReportTime - DateTime.Now.TimeOfDay.TotalMilliseconds);
                    }

                    _lastReportTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
                }
                catch (Exception e) {
                    statusBarTextLabel.Text = "Cloud Reporting Error.";

                    _nextReportTime = DateTime.MinValue.TimeOfDay.TotalMilliseconds;
                    _lastReportTime = DateTime.Now.TimeOfDay.TotalMilliseconds;
                }
            }
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
                jsonNode["Value"] = sensorNode.Value;

                // Only add RawValue for Throughput of Network
                if (jsonNode["Type"].ToString().Equals("Throughput") && (sensorNode.Parent is TypeNode) &&
                    (sensorNode.Parent.Parent is HardwareNode) &&
                    (((HardwareNode)sensorNode.Parent.Parent).Hardware.HardwareType is HardwareType.Network))
                {
                    jsonNode["RawValue"] = sensorNode.RawValue;
                }
                // Ony add RawValue for Data of Memory
                else if(jsonNode["Type"].ToString().Equals("Data") && (sensorNode.Parent is TypeNode) &&
                    (sensorNode.Parent.Parent is HardwareNode) &&
                    (((HardwareNode)sensorNode.Parent.Parent).Hardware.HardwareType is HardwareType.Memory))
                {
                    jsonNode["RawValue"] = sensorNode.RawValue;
                }
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

            if (children.Count > 0)
            {
                jsonNode["Children"] = children;
            }

            return jsonNode;
        }
    }
}
