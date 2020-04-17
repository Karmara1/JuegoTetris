int BotonD = 7;  // boton Derecha
int BotonI = 8;  // boton Izquierda

int Poten = A0; // potenciometro
int Poten2 = A1; // potenciometro

int datoPoten1;
int datoPoten2;

int dato;
int dato2;
int dato3;
int dato4;

void setup()
{
  Serial.begin(9600);
  pinMode(BotonD, INPUT);
  pinMode(BotonI, INPUT);
  pinMode(Poten, INPUT);
  pinMode(Poten2, INPUT);

  
}

void loop()
{
  if (digitalRead(BotonD) == HIGH)
  {
    dato = 1;
      
  }
  else if (digitalRead(BotonD) == LOW){
    dato = 0;    
    }
  
  if (digitalRead(BotonI) == HIGH) // if the switch is pressed
  {
    dato2 = 1;
    
  }
  else if (digitalRead(BotonI) == LOW){
    dato2 = 0;    
  }
    

  datoPoten1 = analogRead(Poten);
  datoPoten2 = analogRead(Poten2);


  dato3 = map(datoPoten1, 0, 1023, 0, 1);
  dato4 = map(datoPoten2, 0, 1023, 0, 1);



  Serial.print(dato);
  Serial.print(",");
  Serial.print(dato2);
  Serial.print(",");
  Serial.print(dato3);
  Serial.print(",");
  Serial.println(dato4);
  Serial.flush();
  delay(20);
  
}
