using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{

    // AL RAYCASTEAR LA ZONA, SE ELEVAN PARA VERSE
    // TIENEN UNA ACCIÓN PRINCIPAL QUE AFECTA AL JUGADOR, AL PJ PROTAGONISTA O A LOS ENEMIGOS O VARIOS
    // HABRIA QUE IDENTIFICAR CLARAMENTE EL RADIO DE ACCIÓN, AL IGUAL QUE EN EL SPAWN
    // Se quita el selected principal, y el proximo targeted es el selected sobre el que se hace la acción
    // Si cancelas la carta, el selected vuelve a ser el anterior (hay que almacenar)
    

    // Start is called before the first frame update
    void Start()
    {
       // InstantiateCards();
    }

    // void InstantiateCards()
    
    // {
    //     CardType bonusEnergy2 = new CardType("skill_player", "BonusEnergy2", "normal");
    //     CardType playerCardScount = new CardType("player", "Scout", "normal");

    //     // Instantiate 54 cards on canvas base go

    //     for (int i = 1; i = 4; i++) {
    //         Instantiate(GameObject.Find("cardS"), GameObject.Find("cardS").transform.position, Quaternion,identity);
    //     }
        



    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
