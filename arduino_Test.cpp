//NASA SUITS 2024
//TEAM COSMIC- Jordan Thomas Acampado
//Include Libraries so arduino has streaming capabilities
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_HMC5883_U.h>
#include <ESP8266WiFi.h>
#include <WiFiClient.h>

// Replace these with your WiFi username and password, good for any secure network
const char* ssid = "your-ssid";
const char* password = "your-password";

// When you connect to the TSS Stream just plug in the ip address
const char* serverIPAddress = "xxx.xxx.xxx.xxx";  // Replace with the actual IP address
const int serverPort = 80;  // The server port should be given when you connect

Adafruit_HMC5883_Unified mag = Adafruit_HMC5883_Unified(12345);

//program to connect
void setup() {
  Serial.begin(115200);
  delay(10);

  // Connect to WiFi
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");
//Error for connecting
  if (!mag.begin()) {
    Serial.println("Could not find a valid magnetometer, check wiring!");
    while (1);
  }
}

//Magnometer set up can use anything as long as it stream data this is just a test
void loop() {
  sensors_event_t event;
  mag.getEvent(&event);

  // Create JSON string
  String jsonData = "{\"x\": " + String(event.magnetic.x) +
                    ", \"y\": " + String(event.magnetic.y) +
                    ", \"z\": " + String(event.magnetic.z) + "}";

  // Send data to TSS server
  sendToServer(jsonData);

  delay(1000);  // Adjust the delay as needed
}

//program to send to the TSS server for further use, not sure how to acquire new data
void sendToServer(String data) {
  WiFiClient client;

  if (client.connect(serverIPAddress, serverPort)) {
    // Send data to TSS server
    client.print(data);

    Serial.println("Data sent to TSS server:\n" + data);

//Error handling
    client.stop();
  } else {
    Serial.println("Unable to connect to TSS server");
  }
}
