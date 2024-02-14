#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_HMC5883_U.h>
#include <ESP8266WiFi.h>
#include <WiFiClient.h>

// Replace these with your WiFi credentials
const char* ssid = "your-ssid";
const char* password = "your-password";

// Replace this with your Thingspeak server address and API key
const char* serverAddress = "api.thingspeak.com";
const char* apiKey = "your-api-key";

Adafruit_HMC5883_Unified mag = Adafruit_HMC5883_Unified(12345);

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

  if (!mag.begin()) {
    Serial.println("Could not find a valid magnetometer, check wiring!");
    while (1);
  }
}

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

void sendToServer(String data) {
  WiFiClient client;

  if (client.connect(serverAddress, 80)) {
    // Make an HTTP request
    client.println("POST /update.json HTTP/1.1");
    client.println("Host: " + String(serverAddress));
    client.println("Connection: close");
    client.println("Content-Type: application/json");
    client.print("Content-Length: ");
    client.println(data.length());
    client.println();
    client.println(data);

    Serial.println("Data sent to server:\n" + data);

    // Wait for the server to close the connection
    while (client.connected()) {
      if (client.available()) {
        String line = client.readStringUntil('\r');
        Serial.print(line);
      }
    }

    client.stop();
  } else {
    Serial.println("Unable to connect to server");
  }
}
