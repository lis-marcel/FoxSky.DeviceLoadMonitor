#include <Arduino.h>

#define DAC_PIN 25 

String incomingString; 
int cpuLoad = 0;
int dacValue = 0;

void setup() {
  Serial.begin(9600); 
  
  Serial.setTimeout(50); 
}

void loop() {
  if (Serial.available() > 0) {
    
    incomingString = Serial.readStringUntil('\n');

    cpuLoad = incomingString.toInt();

    cpuLoad = constrain(cpuLoad, 0, 100);

    dacValue = map(cpuLoad, 0, 100, 0, 255);

    dacWrite(DAC_PIN, dacValue);
    
    // Debugging: Print back to Serial Monitor to verify (Optional)
    // Serial.print("CPU: ");
    // Serial.print(cpuLoad);
    // Serial.print("% -> DAC: ");
    // Serial.println(dacValue);
  }
}