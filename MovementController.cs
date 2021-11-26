using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Métodos para girar y mover los objetos en base a recibir un array de posiciones

    GameObject currentActivePlayer;
    TargetDataHolder tdh;

    void Move(GameObject[] positions){

        // Mover a la primera posición del gameobject
        // Montar un wait for seconds
        // Uno adicional interno para trampas

        // Si no se interrumpe el movimiento mediante trampa o enchant, sigue a la siguiente posición
        // Programar Vector3 distance entre ambas, activando animación

        for (int i = 0; i < positions.Length-1; i++){
            


        }




    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
