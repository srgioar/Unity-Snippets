using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileIdentifier : MonoBehaviour
{

    public Image InnerCursor, OuterCursor;

    public GameObject actorHolder;
    public bool isHover;
    public bool isAvailable;

    public bool tileHasCharacter = false;
    public bool tileHasEnemy = false;
    public bool tileHasObject = false;
    public bool tileIsEmpty = true;
    //Busy -> Tiene un personaje
    public bool isSelected;
    public bool isTargeted;
    
    public int mainarray_row;
    public int secondaryarray_col;
    // Start is called before the first frame update

    void Start()
    {
        isAvailable = true;
        
        
    }

        // Update is called once per frame
    void Update()
    {
        CheckHolder();
        CheckColors();
    }

    void CheckColors(){

        OuterCursor = transform.parent.GetChild(1).GetChild(1).gameObject.GetComponent<Image>();
        InnerCursor = transform.parent.GetChild(1).GetChild(2).gameObject.GetComponent<Image>();

        if (this.isHover){
            //InnerCursor.enabled = true;
            InnerCursor.color = Color.white;
        }

        if (this.isTargeted) {
            //InnerCursor.enabled = true;
            InnerCursor.color = Color.blue;
        }

        if (this.isHover || this.isTargeted) {
            InnerCursor.enabled = true;
        }

        else {
            InnerCursor.enabled = false;
        }



        if (this.isSelected) {
            OuterCursor.enabled = true;
        }

        else{
            OuterCursor.enabled = false;
        }
    }


    // Indica el tipo de enemigo que tiene albergado
    public void CheckHolder(){

        if (tileHasCharacter || tileHasEnemy || tileHasObject) {
            tileIsEmpty = false;
        }

        if (actorHolder == null) {
            tileIsEmpty = true;
            tileHasCharacter = false;
            tileHasEnemy = false;
            tileHasObject = false;
        }

        else if (actorHolder.tag == "Enemy") {
            tileHasEnemy = true;
        }

        else if (actorHolder.tag == "PlayableChar"){
            tileHasCharacter = true;
        }

    }

    public void SetActorHolder(GameObject actor){
        this.actorHolder = actor;
    }

    public GameObject GetActorHolder(){
        return actorHolder;
    }

    public bool GetTileHasCharacter(){
        return tileHasCharacter;
    }

    public bool GetTileHasEnemy(){
        return tileHasEnemy;
    }

    public bool GetSelected(){
        return isSelected;
    }

    public void SetSelected(bool selected){
        this.isSelected = selected;
    }

    public bool GetIsHover(){
        return isHover;
    }

    public void SetIsHover(bool hover){
        this.isHover = hover;
    }

    public void SetTargeted(bool targeted){
        this.isTargeted = targeted;
    }

    public bool GetTargeted(){
        return isTargeted;
    }

    public bool GetAvailable(){
        return isAvailable;
    }

    public void SetAvailable(bool available){
        this.isAvailable = available;
    }

    public void SetPosition(int row, int col){
        this.mainarray_row = row;
        this.secondaryarray_col = col;
    }

    public string GetPositionInfo(){
        return "MainArray/Fila: " + (mainarray_row+1).ToString() + " SecArray/Columna: " + (secondaryarray_col+1).ToString();
    }

    public int GetMainArray_Row(){
        return mainarray_row;
    }

    public int GetSecArray_Col(){
        return secondaryarray_col;
    }


}
