using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoController : MonoBehaviour
{
    SerialPort puerto = new SerialPort("COM3", 9600);

    private int btnD;
    private int btnI;
    public int ponten;
    private int pontenVel;

    public bool Rotar, act;

    public bool caida;
    public bool right;
    public bool left;

    void Start()
    {
        puerto.Open();
        puerto.ReadTimeout = 1;
    }

    private void FixedUpdate()
    {
        if (puerto.IsOpen)
        {
            try
            {
                mover(puerto.ReadLine());
            }
            catch (System.Exception)
            {

            }
            Debug.Log("Datos Arduino: BtnD=" + right + ", BtnI=" + left + ", Giro=" + Rotar + ", Vel=" + caida);
        }
    }

    void mover(string datoArduino)
    {
        string[] datosArray = datoArduino.Split(char.Parse(","));

        if (datosArray.Length == 4)
        {
            btnD = int.Parse(datosArray[0]);
            btnI = int.Parse(datosArray[1]);
            ponten = int.Parse(datosArray[2]);
            pontenVel = int.Parse(datosArray[3]);
        }

        if (btnD == 1 && btnI == 0) right = true;
        else if (btnI == 1 && btnD == 0) left = true;
        else {
            right = false;
            left = false;
        }
        if (pontenVel == 1) caida = true;
        else caida = false;


        if (ponten == 1)
        {
            if(!Rotar && !act)
            StartCoroutine(Esperetantico());
        }
        else Rotar = false;

    }

    IEnumerator Esperetantico()
    {
        Rotar = true;
        act = true;
        yield return new WaitForSeconds(0.1f);
        Rotar = false;
        yield return new WaitForSeconds(1);
        act = false;
        StopAllCoroutines();
    }
}

