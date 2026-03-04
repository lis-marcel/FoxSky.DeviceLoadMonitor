#include <Arduino.h>
#include <iostream>
#include "HeaderFiles/MeterDriver.h"

#define DAC_PIN 25 
#define ADC_PIN 34

MeterDriver meter(DAC_PIN, ADC_PIN);

void setup() {
  Serial.begin(9600); 
  Serial.setTimeout(50);
  
  meter.begin();
}

void loop() {
  if (Serial.available() > 0) {
    String incomingData = Serial.readStringUntil('\n');
    meter.parseIncomingData(incomingData);
  }

  DisplayMode mode = meter.getDisplayMode();

  meter.updateAnalogMeter(mode);

  delay(100);
}