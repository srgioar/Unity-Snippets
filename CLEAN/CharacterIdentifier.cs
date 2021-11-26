using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdentifier : MonoBehaviour
{

    public int Movement;
    public int Diagonal;
    public bool IsDistanceChar;
    public int AttackDistance;
    public int SkillDistance;

    public enum CharType
    {
        PlayerChar, EnemyChar
    }

    public CharType charType;


    // Start is called before the first frame update
    void Start()
    {
        Movement = 2;
        Diagonal = 1;
        IsDistanceChar = true;
        AttackDistance = 2;
        SkillDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Colorize(){

        if (this.charType == CharType.EnemyChar) {
            Material mat = Resources.Load<Material>("Materials/EnemyMaterial");
            GameObject model = transform.GetChild(0).gameObject;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            model.GetComponent<Renderer>().sharedMaterial = mat;
            this.gameObject.tag = "Enemy";
        }

        else if (this.charType == CharType.PlayerChar) {
            this.gameObject.tag = "PlayableChar";
        }
    }


}
