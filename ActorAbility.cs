using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorAbility
{

    // Clase de la que heredan todos los tipos de habilidades
    public string name;
    public int type; // 1-> Melee atack | 2-> Ranged attack | 3-> Ranged spell | 4-> Utility
    // Coeficiente
    public int base_number;
    public Sprite icon;

}

public class HybridAbility : ActorAbility{

    public string ratio; // Coeficiente. Son habilidades que hacen daño y curan vida al que lo usa, entonces se necesita un ratio para establecer la diferencia


}
