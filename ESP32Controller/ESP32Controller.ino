// --- bluetooth ---

#include "BluetoothSerial.h"

// --- end bluetooth ---

// --- MPU6050 ---

#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>

// --- end MPU6050 ---

// --- joystick ---

#define PIN_JOYSTICK_X 14
#define PIN_JOYSTICK_Y 12
#define PIN_JOYSTICK_SEL 27

// --- end joystick ---

// --- buttons ---

#define PIN_BUTTON_1 33
#define PIN_BUTTON_2 32

// --- end buttons ---

// --- bluetooth ---

BluetoothSerial SerialBT;

// --- end bluetooth ---

// --- MPU6050 ---

Adafruit_MPU6050 mpu;
sensors_event_t a, g, temp;

// --- end MPU6050 ---

void setup() {
  Serial.begin(115200);

  // --- bluetooth ---

  SerialBT.begin("ESP32Controller");
  Serial.println("Bluetooth Device is Ready to Pair");

  // --- end bluetooth ---

  // --- joystick ---

  pinMode(PIN_JOYSTICK_X, INPUT);
  pinMode(PIN_JOYSTICK_Y, INPUT);
  pinMode(PIN_JOYSTICK_SEL, INPUT_PULLUP);

  Serial.println("Finished joystick pinMode setup");

  // --- end joystick ---

  // --- buttons ---

  pinMode(PIN_BUTTON_1, INPUT_PULLUP);
  pinMode(PIN_BUTTON_2, INPUT_PULLUP);

  Serial.println("Finished buttons pinMode setup");

  // --- end buttons ---

  // --- MPU6050 ---

  while (!mpu.begin()) {
    Serial.println("MPU6050 not connected!");
    delay(1000);
  }
  Serial.println("MPU6050 ready!");

  // --- end MPU6050 ---
}

void loop() {
  // Read joystick values
  int valueX = analogRead(PIN_JOYSTICK_X);
  int valueY = analogRead(PIN_JOYSTICK_Y);
  int valueSEL = digitalRead(PIN_JOYSTICK_SEL);

  // Read buttons values
  int valueBUTTON1 = digitalRead(PIN_BUTTON_1);
  int valueBUTTON2 = digitalRead(PIN_BUTTON_2);

  // Read MPU6050 values
  mpu.getAccelerometerSensor()->getEvent(&a);
  mpu.getGyroSensor()->getEvent(&g);
  mpu.getTemperatureSensor()->getEvent(&temp);

  // Create a buffer to hold the data
  byte dataBuffer[35];

  // Fill the buffer with data
  dataBuffer[0] = valueX & 0xFF;    // Low byte of valueX
  dataBuffer[1] = valueX >> 8;      // High byte of valueX
  dataBuffer[2] = valueY & 0xFF;    // Low byte of valueY
  dataBuffer[3] = valueY >> 8;      // High byte of valueY
  dataBuffer[4] = valueSEL & 0xFF;  // Low byte of valueSEL
  dataBuffer[5] = valueSEL >> 8;    // High byte of valueSEL

  // Fill the buffer with MPU6050 data (updated indices)
  memcpy(&dataBuffer[6], &a.acceleration.x, sizeof(float));   // 4 bytes
  memcpy(&dataBuffer[10], &a.acceleration.y, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[14], &a.acceleration.z, sizeof(float));  // 4 bytes

  memcpy(&dataBuffer[18], &g.gyro.x, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[22], &g.gyro.y, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[26], &g.gyro.z, sizeof(float));  // 4 bytes

  memcpy(&dataBuffer[30], &temp.temperature, sizeof(float));  // 4 bytes

  // Compute a simple checksum
  byte checksum = 0;
  for (int i = 0; i < 34; i++) {
    checksum ^= dataBuffer[i];
  }
  dataBuffer[34] = checksum;  // Store checksum in the last byte of the buffer

  // Send the buffer over BluetoothSerial
  size_t bytesWritten = SerialBT.write(dataBuffer, sizeof(dataBuffer));
  
  // Check if the write was successful
  if (bytesWritten == sizeof(dataBuffer)) {
    Serial.println("Data sent successfully over BluetoothSerial.");
  } else {
    Serial.println("Failed to send data over BluetoothSerial.");
  }

  // Log the values being sent
  Serial.print("Sent Joystick X: ");
  Serial.print(valueX);
  Serial.print(", Y: ");
  Serial.print(valueY);
  Serial.print(", SEL: ");
  Serial.print(valueSEL);
  Serial.print(", BUTTON1: ");
  Serial.print(valueBUTTON1);
  Serial.print(", BUTTON2: ");
  Serial.print(valueBUTTON2);
  Serial.print(", Accel X: ");
  Serial.print(a.acceleration.x);
  Serial.print(", Y: ");
  Serial.print(a.acceleration.y);
  Serial.print(", Z: ");
  Serial.print(a.acceleration.z);
  Serial.print(", Gyro X: ");
  Serial.print(g.gyro.x);
  Serial.print(", Y: ");
  Serial.print(g.gyro.y);
  Serial.print(", Z: ");
  Serial.print(g.gyro.z);
  Serial.print(", Temp: ");
  Serial.println(temp.temperature);
}
