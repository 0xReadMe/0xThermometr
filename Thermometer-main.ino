#include <LiquidCrystal_I2C.h>
#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>

#define _LCD_TYPE 1
#include <LCD_1602_RUS_ALL.h>
#include <font_LCD_1602_RUS.h>

#define DHTPIN 10     
#define DHTTYPE DHT22

DHT_Unified dht(DHTPIN, DHTTYPE);
LCD_1602_RUS lcdRUS(0x27, 16, 2);
LiquidCrystal_I2C lcd(0x27, 16, 2);

void setup() {
  Serial.begin(9600);
  dht.begin();
  sensor_t sensor;
  dht.temperature().getSensor(&sensor);
  dht.humidity().getSensor(&sensor);
  lcd.begin();
  lcd.backlight();
}

void loop() {
  sensors_event_t event;
  dht.temperature().getEvent(&event);
  Serial.print(event.temperature);
  lcd.clear();
  lcdRUS.setCursor(0, 0);
  lcdRUS.print("Температура:");
  lcd.setCursor(12, 0);
  lcd.print(event.temperature);

  Serial.print(" ");
  dht.humidity().getEvent(&event);
  Serial.println(event.relative_humidity);
  lcdRUS.setCursor(0, 1);
  lcdRUS.print("Влажность:");
  lcd.setCursor(12, 1);
  lcd.print(event.relative_humidity);
  delay(1000);
}
