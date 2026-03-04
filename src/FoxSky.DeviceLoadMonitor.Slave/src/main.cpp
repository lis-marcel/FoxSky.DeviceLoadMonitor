#include <Arduino.h>
#include <iostream>
#include <string>
#include <vector>
#include <sstream>

#define DAC_PIN 25 

int flag = 1;
String incomingString;
String temp;
long receivedValue = 0;
long dacValue = 0;
int deviceLoad = 0;
std::string segment;
std::vector<std::string> seglist;

void setup() {
  Serial.begin(9600); 
  
  Serial.setTimeout(50); 
}

void loop() {
  if (Serial.available() > 0) {
    seglist.clear();
    
    incomingString = Serial.readStringUntil('\n');

    std::istringstream iss(incomingString.c_str());
    while (std::getline(iss, segment, ','))
    {
      seglist.push_back(segment);
    }

    // 0-> CPU Load, 1-> RAM Load, 2-> GPU Load
    if (flag == 0) {
      temp = seglist[0].c_str();
    }

    if (flag == 1) {
      temp = seglist[1].c_str();
    }

    if (flag == 2) {
      temp = seglist[2].c_str();
    }

    receivedValue = temp.toInt();

    deviceLoad = constrain(receivedValue, 0, 100);

    dacValue = map(deviceLoad, 0, 100, 0, 255);

    analogWrite(DAC_PIN, dacValue);
  }
}