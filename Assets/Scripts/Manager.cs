using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public static int alto = 20;
    public static int ancho = 10;

    public int puntaje = 0;

    public Text textoPuntaje;

    public int puntoDificultad;
    public float dificultad = 1;

    public static Transform[,] cuadricula = new Transform[ancho, alto];

    private void Update()
    {

        textoPuntaje.text = puntaje.ToString();
    }

    public bool dentroCuadricula(Vector2 posicion)
    {
        return ((int)posicion.x >= 0 && (int)posicion.x < ancho && (int)posicion.y >= 0);
    }

    public Vector2 redondear(Vector2 numRedondo)
    {
        return new Vector2(Mathf.Round(numRedondo.x), Mathf.Round(numRedondo.y));
    }

    public void actualizarCuadricula(MovimientoFicha fichaTetris)
    {
        for (int y = 0; y < alto; y++)
        {
            for (int x = 0; x < ancho; x++)
            {
                if (cuadricula[x, y] != null)
                {
                    if (cuadricula[x, y].parent == fichaTetris.transform)
                    {
                        cuadricula[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform ficha in fichaTetris.transform)
        {
            Vector2 posicion = redondear(ficha.position);

            if (posicion.y < alto)
            {
                cuadricula[(int)posicion.x, (int)posicion.y] = ficha;
            }
        }
    }

    public Transform posicionTransformada(Vector2 posicion)
    {
        if (posicion.y > alto - 1)
        {
            return null;
        }
        else
        {
            return cuadricula[(int)posicion.x, (int)posicion.y];
        }
    }

    public bool lineaCompleta(int y)
    {
        for (int x = 0; x < ancho; x++)
        {
            if (cuadricula[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void eliminarLinea(int y)
    {
        for (int x = 0; x < ancho; x++)
        {
            Destroy(cuadricula[x, y].gameObject);

            cuadricula[x, y] = null;
        }
    }

    public void bajarLinea(int y)
    {
        for (int x = 0; x < ancho; x++)
        {
            if (cuadricula[x, y] != null)
            {
                cuadricula[x, y - 1] = cuadricula[x, y];
                cuadricula[x, y] = null;
                cuadricula[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void bajarLineas(int y)
    {
        for (int i = y; i < alto; i++)
        {
            bajarLinea(i);
        }
    }

    public void actualizarLinea()
    {
        for (int y = 0; y < alto; y++)
        {
            if (lineaCompleta(y))
            {
                eliminarLinea(y);
                bajarLineas(y + 1);
                y--;
                puntaje += 100;
                puntoDificultad += 100;
            }
        }
    }




    public bool sobreCuadricula (MovimientoFicha fichaMovimiento)
    {
        for (int x = 0; x < ancho; x++)
        {
            foreach (Transform cuadrado in fichaMovimiento.transform)
            {
                Vector2 posicion = redondear(cuadrado.position);

                if (posicion.y > alto - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }
    

    public void perder()
    {
        SceneManager.LoadScene(2);
    }

}

