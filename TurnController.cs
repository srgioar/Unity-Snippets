using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{

    public string PlayerTurn, EnemyTurn;

    GameObject[] PlayerChars = new GameObject[0];

    GameObject[] EnemyChars = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        // Comprobar y volcar aquí chars y enemigos
        // COMPROBAR si la energía de todos los players está a 0 -> Cambiar turno automáticamente
        // comprobar si la energía de enemigos está a 0 -> Ceder turno al jugador
        // Cambiar turno -> Cambiar string turno, wait de los eventos
        // Añadir +2 de energía por turno
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
