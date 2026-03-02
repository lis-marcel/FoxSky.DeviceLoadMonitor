using System.IO.Ports;
using System.Reflection;

namespace FoxSky.DeviceLoadMonitor.Master.Service
{
    internal class SerialWriter
    {
        private readonly SerialPort _serialPort;

        public SerialWriter()
        {
            _serialPort = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(WriteReceivedData);
            _serialPort.Open();
        }

        public void WriteData(string data)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write(data);

                }
                else
                {
                    throw new InvalidOperationException("Serial port is not open.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public void WriteReceivedData(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    string data = _serialPort.ReadExisting();
                    Console.WriteLine($"{data}");
                }
                else
                {
                    throw new InvalidOperationException("Serial port is not open.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
