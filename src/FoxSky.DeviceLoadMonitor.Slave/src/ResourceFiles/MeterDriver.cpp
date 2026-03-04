#include "HeaderFiles/MeterDriver.h"
#include <Arduino.h>
#include <iostream>

MeterDriver::MeterDriver(int dacPin, int adcPin) : _dacPin(dacPin), _adcPin(adcPin), _currentCpu(0), _currentRam(0), _currentGpu(0) {}

void MeterDriver::begin() {
    pinMode(_dacPin, OUTPUT);
    pinMode(_adcPin, INPUT);
}

DisplayMode MeterDriver::getDisplayMode() {
    int adcValue = analogRead(_adcPin);

    if (adcValue < 1300) {
        return CPU_LOAD;
    } else if (adcValue > 2800) {
        return RAM_LOAD;
    } else {
        return GPU_LOAD;
    }
}

void MeterDriver::parseIncomingData(const String& data) {
    int matched = sscanf(data.c_str(), "%d,%d,%d", &_currentCpu, &_currentRam, &_currentGpu);
    Serial.println("Parsed values - CPU: " + String(_currentCpu) + ", RAM: " + String(_currentRam) + ", GPU: " + String(_currentGpu));

    if (matched != 3) {
        Serial.println("Error parsing data: " + data);
    }
}

void MeterDriver::updateAnalogMeter(DisplayMode mode) {
    int loadValue = 0;

    switch (mode) {
        case CPU_LOAD:
            loadValue = _currentCpu;
            break;
        case RAM_LOAD:
            loadValue = _currentRam;
            break;
        case GPU_LOAD:
            loadValue = _currentGpu;
            break;
    }

    loadValue = constrain(loadValue, 0, 100);
    int dacValue = map(loadValue, 0, 100, 0, 255);

    analogWrite(_dacPin, dacValue);
}