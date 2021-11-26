using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileIdentifierNew : MonoBehaviour
{

    public enum FieldStatus
    {
        Disabled, Walkable, NotWalkable
    }

    public enum ContainedType {

        Ally, Enemy, Item, Obstacle, Null
    }

    public FieldStatus fieldStatus = FieldStatus.Walkable;
    public ContainedType containedType;

    public bool isSelected;
    public bool isTargeted;
    public bool isHover;

    public Image InnerCursor, OuterCursor;

    public int mainarray_row;
    public int secondaryarray_col;

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

    public bool IsAvailable(){
        if ((fieldStatus != FieldStatus.Walkable))
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void SetAsAvailable(){
        this.fieldStatus = FieldStatus.Walkable;
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

    void SetTileProperties(){
        if (fieldStatus == FieldStatus.Disabled) {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
    }


    void CheckStatusType(){
        // Dependiendo del holder, seteamos datos
        TileHolderInfo thi = GetComponent<TileHolderInfo>();
        GameObject go = thi.InsertedObject;
        //CharacterIdentifier ci = go.GetComponent<CharacterIdentifier>();

        if (go != null) {

        if (go.tag == "Ally") {
            containedType = ContainedType.Ally;
        }

        else if (go.tag == "Enemy") {
            containedType = ContainedType.Enemy;
        }

        else if (go.tag == "Item") {
            containedType = ContainedType.Item;
        }
        }

        else {
             containedType = ContainedType.Null;
        }

    }

    // Start is called before the first frame update
    void Start()
    {   
        fieldStatus = FieldStatus.Walkable;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTileProperties();
        CheckStatusType();
        CheckColors();
    }
}
