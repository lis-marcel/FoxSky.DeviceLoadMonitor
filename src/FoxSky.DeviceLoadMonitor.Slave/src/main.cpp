#include <Arduino.h>

#define DAC_PIN 25 

String incomingString; 
long cpuLoad = 0;
long dacValue = 0;

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

    analogWrite(DAC_PIN, dacValue);
    
    // Odsyłanie logów z powrotem do PC
    // Serial.print("Odebrano: '");
    // Serial.print(incomingString);
    // Serial.print("' -> CPU: ");
    // Serial.print(cpuLoad);
    // Serial.print("% -> DAC: ");
    // Serial.println(dacValue);
  }
}