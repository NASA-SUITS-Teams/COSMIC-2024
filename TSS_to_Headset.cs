using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoCommunication : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM3", 115200); // Adjust port and baud rate

    void Start()
    {
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                ParseData(data);
            }
            catch (System.Exception)
            {
                // Handle exception if any
            }
        }
    }

    void ParseData(string data)
    {
        // Parse and use the data as needed
        // Example: If your Arduino sends JSON, you might use a JSON parser here
        Debug.Log("Received data: " + data);
    }

    void OnDestroy()
    {
        serialPort.Close();
    }
}
