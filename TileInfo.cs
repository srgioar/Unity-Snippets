using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour
{
    Transform TileCanvas;
    Collider TileCollider;
    public GameObject ContainedBuilding;

    Image HoverCursor, SelectCursor, TargetCursor;

    GameObject Building1, Building2, Building3;

    public bool hasBuilding;

    // Personaje o item que contiene el tile
    //public GameObject HoldingChar;
    public enum FieldStatus
    {
        Disabled, Walkable, NotWalkable, Empty
    }

    public enum ContainedType {
        Ally, Enemy, Item, Obstacle, Building1F, Building2F, Building3F, None
    }

    // Types con colores. Hay PRINCIPAL, SECUNDARIO, TERCIARIO (arriba a abajo en array 3d)
    // Prnicipales: GRASS, ICE, STONE
    // Secundarios: Ground, , ClearGround, DarkGround
    // Terciarios: Water, darkwater, lava
    // Empty

    public enum ElementType {
        Grass, Ice, Stone, Ground, DarkGround, Water, DarkWater, Lava, None
    }

    public ElementType elementType = ElementType.None;
    public FieldStatus fieldStatus = FieldStatus.Walkable;
    public ContainedType containedType;
    public bool isSelected;
    public bool isTargeted;
    public bool isHover;

    public int mainarray_row;
    public int secondaryarray_col;
    public int thirdarray_lvl;

    public void SetAsWalkable(){
        this.fieldStatus = FieldStatus.Walkable;
    }

    public void SetAsNotWalkable(){
        this.fieldStatus = FieldStatus.NotWalkable;
    }

    public void SetAsElement(ElementType element) {
        this.elementType = element;
    }

    public void SetAsSelected() {
        this.isSelected = true;
        this.EnableSelectCursor();
    }

    public void SetAsNotSelected(){
        this.isSelected = false;
        this.DisableSelectCursor();
    }

    public bool GetSelected(){
        return isSelected;
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
        if ((fieldStatus == FieldStatus.Walkable))
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

    public void SetPosition3D(int row, int col, int lvl) {
        this.mainarray_row = row;
        this.secondaryarray_col = col;
        this.thirdarray_lvl = lvl;
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

        MeshRenderer mr = GetComponent<MeshRenderer>();

        // if (fieldStatus == FieldStatus.Disabled) {
        //     mr.enabled = false;
        // }

        switch (elementType)
        {

        case ElementType.Grass:
            mr.material.color = new Color32(79, 175, 31, 255);
            break;
        case ElementType.Ground:
            mr.material.color = new Color32(141, 89, 13, 255);
            break;
        case ElementType.Water:
            mr.material.color = new Color32(46, 116, 255, 255);
            break;
        case ElementType.None:
            mr.enabled = false;
            DeactivateTileCanvas();
            break;
        }

        // if (elementType == ElementType.Sand) {
        //     mr.material.color = Color.yellow;
        // }

        // else if (elementType == ElementType.Grass) {
        //     mr.material.color = Color.green;
        // }

        // else if (elementType == ElementType.Water){
        //     mr.material.color = Color.blue;
        // }

        // else if (elementType == ElementType.ClearWater){
        //     mr.material.color = Color.cyan;
        // }

        // else if (elementType == ElementType.Stone){
        //     //mr.material.color = Color.gray;
        //     mr.enabled = false;
        //     DeactivateTileCanvas();
        // }

        // else if (elementType == ElementType.Void){
        //     mr.material.color = Color.black;
        // }

        // else if (elementType == ElementType.Ground){
        //     mr.material.color = Color.magenta;
        // }

    }

    public void EnableHoverCursor(){
        HoverCursor.enabled = true;
    }

    public void DisableHoverCursor(){
        HoverCursor.enabled = false;
    }

    public void EnableTargetCursor(){
        TargetCursor.enabled = true;
    }

    public void DisableTargetCursor(){
        TargetCursor.enabled = false;
    }

    public void EnableSelectCursor(){
        SelectCursor.enabled = true;
    }

    public void DisableSelectCursor(){
        SelectCursor.enabled = false;
    }

    public void SetAsNormalCursor(){
        SelectCursor.color = Color.white;
    }

    public void SetAsGuideCursor(){
        SelectCursor.color = Color.red;
    }

    // Changes number of apiled tiles depending on the tile height

    // void CheckStatusType(){
    //     // Dependiendo del holder, seteamos datos
    //     TileHolderInfo thi = GetComponent<TileHolderInfo>();
    //     GameObject go = thi.InsertedObject;
    //     //CharacterIdentifier ci = go.GetComponent<CharacterIdentifier>();

    //     if (go.tag == "PlayerChar") {
    //         containedType = ContainedType.Ally;
    //     }

    //     else if (go.tag == "EnemyChar") {
    //         containedType = ContainedType.Enemy;
    //     }

    //     else if (go.tag == "Item") {
    //         containedType = ContainedType.Item;
    //     }
    //     }

    //     else {
    //          containedType = ContainedType.Null;
    //     }

    // }

    // Start is called before the first frame update
    void Start()
    {   
        // Inicializar los cursores
        TileCanvas = transform.GetChild(0);
        HoverCursor = TileCanvas.GetChild(1).GetComponent<Image>();
        TargetCursor = TileCanvas.GetChild(0).GetComponent<Image>();
        SelectCursor = TileCanvas.GetChild(2).GetComponent<Image>();
        TileCollider = GetComponent<Collider>();

        Building1 = GameObject.Find("bd_SMALL");
        Building2 = GameObject.Find("bd_MEDIUM");
        
    }

    void DeactivateTileCanvas(){
        TileCanvas.gameObject.GetComponent<Canvas>().enabled = false;
        TileCollider.enabled = false;
    }

    public void InsertBuilding(int height, Material material){

        GameObject building = null;
        // Offset vertical para ajustar el tamaño según edificio
        float vOffset = 0f;

        if (height == 1) {
            building = Building1;
            vOffset = 0.35f;
        }

        if (height == 2) {
            building = Building2;
            vOffset = 0.5f;
        }

        GameObject createdBuilding;
        createdBuilding = Instantiate(
                building,
                transform.position + new Vector3(0, vOffset, 0),
                Quaternion.identity);

        createdBuilding.transform.SetParent(transform.GetChild(1));
        createdBuilding.name = "Building Lvl " + height.ToString();
        BuildingInfo buildingInfo = createdBuilding.GetComponent<BuildingInfo>();
        buildingInfo.BuildingText.text = "B " + height.ToString();
        buildingInfo.SetMaterial(material);
        this.ContainedBuilding = createdBuilding;
        this.SetAsNotWalkable();

    }

    public void RemoveBuilding(){
        this.ContainedBuilding = null;
        this.SetAsWalkable();
        Destroy(this.transform.GetChild(1).GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SetTileProperties();
        //CheckStatusType();
        //CheckColors();
    }
}
