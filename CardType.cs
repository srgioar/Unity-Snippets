using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardType : CardBase
{
    public string type;
    public string cardName;

    public CardType(string type, string cardName, string rarity){

        this.type = type;
        this.cardName = cardName;
        this.rarity = rarity;

    }

    void StartCard(){
        if (this.rarity == "normal") {
            this.cost = 5;
        }

        else if (this.rarity == "rare") {
            this.cost = 10;
        }

        else if (this.rarity == "epic") {
            this.cost = 15;
        }
    }

    void Action(GameObject target){




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
