using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharIdentifier : MonoBehaviour
{
    public int MeleeRange = 1, DistanceRange = 2, MovementRange = 2, DiagonalRange = 1;
    public string CharName;

    public enum CharType
    {
        PlayerChar, EnemyChar
    }

    public CharType charType;

    public int Life = 3, Energy = 3, Atk = 1, Def = 1;
    //public List<ActorAbility> AbilitiesList;

    // public CharIdentifier(string CharName, CharType charType){

    //     this.CharName = CharName;
    //     this.charType = charType;

    //     this.Life = 3;
    //     this.Mana = 3;
    //     this.Energy = 3;
    //     this.Level = 1;
    //     this.MeleeRange = 1; this.DistanceRange = 2; this.MovementRange = 2; this.DiagonalRange = 1;

    // }

    public GameObject[] ItemHolders;

    void Colorize(){

        if (this.charType == CharType.EnemyChar) {
            Material mat = Resources.Load<Material>("Materials/EnemyMaterial");
            GameObject model = transform.GetChild(0).gameObject;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            this.gameObject.tag = "Enemy";
        }

        else {
            this.gameObject.tag = "PlayableChar";
        }
    }

    void Start(){

        Colorize();

    }

    void Update(){

    }

}




// public class CharAtkBase : CharIdentifier{

//     public CharAtkBase(int MeleeRange, int DistanceRange, int MovementRange, int DiagonalRange, int ItemHoldersNum, string StageId, string TaxonomyType) {

//         this.TaxonomyType  = "atkbase";
//         this.StageId = "base2";
//         this.MeleeRange = 1;
//         this.DistanceRange = 0;
//         this.MovementRange = 1;
//         this.DiagonalRange = 0;
//         this.ItemHoldersNum = 1;

//     }

// }

// public class CharDefBase : CharIdentifier{
    
// }
