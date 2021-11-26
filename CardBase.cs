using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase
{

    // el juego tiene una moneda principal, cristales
    // LA SECUNDARIA ES ENERGÍA

    // Matar enemigos, conseguir objetos en los tiles da CRISTALES
    // Los cristales sirven para usar cartas
    // Las cartas pueden dar: BONUS, PERSONAJES, ENERGÍA 
    // personajes -> LEVEL 1 / LEVEL 2 / LEVEL 3

    // las BONUS como las SKILLS TIENEN TARGET -> PLAYER O ENEMY

    // ENERGÍA AZUL, AMARILLA, ROJA
    // BONUS -> PLAYER O ENEMY -> AUMENTAR O REDUCIR PUNTOS DE ACCIÓN
    // EQUIPAR -> OBJETOS EQUIPADOS
    public string rarity;
    public int cost;
    
    // CONTROLAR TIPO (PERSONAJE, BONUS, PASIVA)
    // HOLDER ES EL TIPO DE OBJETO QUE CONTIENE
    // Start is called before the first frame update

}
