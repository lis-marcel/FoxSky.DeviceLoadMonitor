#include <Arduino.h>

void setup() {
  pinMode(GPIO26, OUTPUT); 
}

void loop() {
  digitalWrite(LED_BUILTIN, HIGH);   
  delay(1000);                       
  digitalWrite(LED_BUILTIN, LOW);    
  delay(1000);                       
}