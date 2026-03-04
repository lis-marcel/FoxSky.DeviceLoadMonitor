#pragma once
#include "Arduino.h"

enum DisplayMode {
    CPU_LOAD,
    RAM_LOAD,
    GPU_LOAD
};

class MeterDriver {
    private:
        int _dacPin;
        int _adcPin;

        int _currentCpu;
        int _currentRam;
        int _currentGpu;

    public:
        MeterDriver(int dacPin, int adcPin);

        void begin();
        DisplayMode getDisplayMode();
        void parseIncomingData(const String& data);
        void updateAnalogMeter(DisplayMode mode);
};