using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public int mostrarProxFicha;

    public Transform[] creaFichas;
    public List<GameObject> mostrarFichas;

    void Start()
    {
        mostrarProxFicha = Random.Range(0, 6);
        proximaFicha();
    }

    public void proximaFicha()
    {
        Instantiate(creaFichas[mostrarProxFicha], transform.position, Quaternion.identity);

        mostrarProxFicha = Random.Range(0, 6);

        for (int i = 0; i < mostrarFichas.Count; i++)
        {
            mostrarFichas[i].SetActive(false);
        }

        mostrarFichas[mostrarProxFicha].SetActive(true);

    }
}



