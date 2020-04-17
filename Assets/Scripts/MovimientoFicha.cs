using UnityEngine;
using System.Collections;

public class MovimientoFicha : MonoBehaviour
{
    public bool Rotar;
    public bool Rotar360;

    public float caida;

    public float velocidad;
    public float tiempo;

    Manager gManager;
    Spawner gSpawner;

    private ArduinoController Arduino;

    private void Awake()
    {
        Arduino = GameObject.Find("ArduinoController").GetComponent<ArduinoController>();
    }

    void Start()
    {
        tiempo = velocidad;

        gManager = GameObject.FindObjectOfType<Manager>();

        gSpawner = GameObject.FindObjectOfType<Spawner>();
    }

    void Update()
    {

        if (gManager.puntoDificultad > 1000)
        {
            gManager.puntoDificultad -= 1000;
            gManager.dificultad += .1f;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow)
            || Arduino.left || Arduino.right)
            tiempo = velocidad;

        //Movimiento derecha
        if (Input.GetKey(KeyCode.RightArrow) || Arduino.right)
        {
            tiempo += Time.deltaTime;

            if (tiempo > velocidad)
            {
                transform.position += new Vector3(1, 0, 0);
                tiempo = 0;
            }

            if (posicionValida())
            {
                gManager.actualizarCuadricula(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        // Movimiento izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Arduino.left)
        {
            tiempo += Time.deltaTime;

            if (tiempo > velocidad)
            {
                transform.position += new Vector3(-1, 0, 0);
                tiempo = 0;
            }

            if (posicionValida())
            {
                gManager.actualizarCuadricula(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        // Caida
        if (Input.GetKey(KeyCode.DownArrow) || Arduino.caida) //|| Time.time - caida >= 1)
        {
            tiempo += Time.deltaTime;

            if (tiempo > velocidad)
            {
                transform.position += new Vector3(0, -1, 0);
                tiempo = 0;
            }

            if (posicionValida())
            {
                gManager.actualizarCuadricula(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                gManager.actualizarLinea();

                if (gManager.sobreCuadricula(this))
                {
                    gManager.perder();
                }

                gManager.puntaje += 10;
                gManager.puntoDificultad += 10;
                enabled = false;
                gSpawner.proximaFicha();
            }
            //caida = Time.time;
        }

        if (Time.time - caida >= (1 / gManager.dificultad) && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0);

            if (posicionValida())
            {
                gManager.actualizarCuadricula(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                gManager.actualizarLinea();

                if (gManager.sobreCuadricula(this))
                {
                    gManager.perder();
                }

                gManager.puntaje += 10;
                gManager.puntoDificultad += 10;
                enabled = false;
                gSpawner.proximaFicha();
            }

            caida = Time.time;
        }

        // Rotacion
        if (Input.GetKeyDown(KeyCode.UpArrow) || Arduino.Rotar)
        {
            if (Rotar)
            {
                if (!Rotar360)
                {
                    if (transform.rotation.z < 0)
                    {
                        transform.Rotate(0, 0, 90);

                        if (posicionValida())
                        {
                            gManager.actualizarCuadricula(this);
                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);
                        }

                    }
                    else
                    {
                        transform.Rotate(0, 0, -90);

                        if (posicionValida())
                        {
                            gManager.actualizarCuadricula(this);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }

                    }
                }
                else if(Rotar360)
                {
                    transform.Rotate(0, 0, -90);

                    if (posicionValida())
                    {
                        gManager.actualizarCuadricula(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
            }


        }
    }

    bool posicionValida()
    {
        foreach (Transform child in transform)
        {
            Vector2 posBlock = gManager.redondear(child.position);

            if (gManager.dentroCuadricula(posBlock) == false)
            {
                return false;
            }

            if (gManager.posicionTransformada(posBlock) != null && gManager.posicionTransformada(posBlock).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}