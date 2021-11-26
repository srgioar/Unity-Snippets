using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FlowControllerNew : MonoBehaviour
{
    GameObject Targeted, Selected, Hovered;
    public List<GameObject> ButtonsList;
    bool InspectEnabled = false;
    bool SelectEnabled = false;
    bool AttackEnabled = false;
    bool MoveEnabled = false;
    bool SkillEnabled = false;

    GameObject ContextMenu;

    void TargetTile(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && !TouchingUI()){

            Transform TileHit = hit.transform;

            if ((TileHit.tag == "tilecube")) {

                if (Targeted != null) {
                    TileIdentifierNew ti = Targeted.GetComponentInChildren<TileIdentifierNew>();
                    ti.SetTargeted(false);
                    Targeted = null;
                    Destroy(GameObject.Find("ContextMenu"));
                }

                TileIdentifierNew tid = TileHit.gameObject.GetComponent<TileIdentifierNew>();
                tid.SetTargeted(true);
                Targeted = TileHit.parent.gameObject;
                
            }

        }
    }

    public void SelectTile(){

    // Si el tile targeteado tiene un personaje, el seleccionado equivale al personaje
    // Mejorar: Chequear que la posición inicial tiene un punto de interés (personaje)
    // Que la posición a elegir de destino NO está como no disponible
    // Establecer que la segunda posición seleccionada es posible si se usa
    // FIX: DESELECCIONAR Y SELECCIONAR NUEVA POSICIÓN CORRECTAMENTE

        TileIdentifierNew lt = Targeted.GetComponentInChildren<TileIdentifierNew>();
        Debug.Log("HASCHAR: " + lt.containedType.ToString());
        
        if (lt.containedType == TileIdentifierNew.ContainedType.Ally) {
            if (Selected != null) {
                TileIdentifierNew ls = Selected.GetComponentInChildren<TileIdentifierNew>();
                ls.SetSelected(false);
            }

            else {

            }

            Selected = Targeted;
            Selected.GetComponentInChildren<TileIdentifierNew>().SetSelected(true);
            Destroy(GameObject.Find("ContextMenu"));
        }

    }

    void HoverTile(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && !TouchingUI()){

            Transform TileHit = hit.transform;

            if ((TileHit.tag == "tilecube")) {

                if (Hovered != null) {
                    TileIdentifierNew ti = Hovered.GetComponentInChildren<TileIdentifierNew>();
                    ti.SetIsHover(false);
                }

                TileIdentifierNew tid = TileHit.gameObject.GetComponent<TileIdentifierNew>();
                tid.SetIsHover(true);
                Hovered = TileHit.parent.gameObject;

            }

        }
    }

    public void RefreshInteractions(){

        // Si no hay nada seleccionado la interacción es inspeccionar
        if (Targeted != null) {
            // Enable inspect
            InspectEnabled = true;
        }

        if (Targeted == null) {
            InspectEnabled = true;
        }

        if (Selected == null && Targeted != null) {

            if (Targeted.GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Ally) {
                SelectEnabled = true;
            }

            else {
                SelectEnabled = false;
            }

        }

        if (Selected != null) {
            SelectEnabled = false;
        }

        if ((Selected != null) && (Targeted != null)) {
            //TargetDataHolder tdh = new TargetDataHolder(CurrentSelected, CurrentTargeted);
            TileInteractionScanner tis = new TileInteractionScanner(Selected, Targeted);
            //int interaction = tdh.GetTypeOfInteraction(1, 2, 2, 1);
            tis.CheckInteraction();
            AttackEnabled = tis.IsAttackPossible;
            MoveEnabled = tis.IsMovementPossible;
        }

    }

    bool TouchingUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Versión nueva
    void GenerateContextMenu(){

    }

    void OpenContextMenu(){

        Camera cam = Camera.main;
        GameObject inst = GameObject.Find("ButtonBackground");
        GameObject projecter = Targeted;
        Vector3 pos1 = cam.WorldToScreenPoint(projecter.transform.position);

        GameObject obj;
        obj = Instantiate(inst);
        obj.transform.SetParent(GameObject.Find("MainCanvas").transform, false);
        //RectTransform rt = obj.GetComponent<RectTransform>();
        // Setear posición y escala según ratio
        //rt.localPosition = screenPoint;
        obj.transform.position = pos1 + new Vector3(50, 36, 0);
        obj.name = "ContextMenu";
        this.ContextMenu = obj;
        
        
        ButtonInstancier();
        ButtonListInstancier();
        //ButtonInstancierNew();

    }

    // void ButtonInstancierNew(){

    //     GameObject MainCan = GameObject.Find("MainCanvas");
    //     if (MainCan == null) {
    //         Debug.Log("Canvas null");
    //     }
        
    //     transform.SetParent(MainCan.transform, false);
    //     //Debug.Log("InspectEnabled: "+ InspectEnabled);
        
    //     // GameObject ButtonsHolder = new GameObject();
    //     // ButtonsHolder.transform.SetParent(Popup);
    //     Debug.Log("Count= " + ButtonsList.Count);
    //     for (int i = 0; i < ButtonsList.Count; i++){
    //         GameObject item;
    //         item = Instantiate(ButtonsList[i]);
    //         item.transform.SetParent(MainCan.transform, false);
    //         item.transform.position = MainCan.transform.position + new Vector3(0, i*26, 0);
    //         //item.transform.rotation = Quaternion.identity;
    //         item.name = "TestButton";
    //     }
    // }

    void ButtonInstancier(){

        GameObject btnInspect = GameObject.Find("BTN_INSPECT");
        GameObject btnSelect = GameObject.Find("BTN_SELECT");

        GameObject btnMove = GameObject.Find("BTN_MOVE");
        GameObject btnAttack = GameObject.Find("BTN_ATTACK");
        GameObject btnSkill = GameObject.Find("BTN_SKILL");
        GameObject btnPick = GameObject.Find("BTN_PICK");

        ButtonsList = new List<GameObject>();

        if (InspectEnabled){
            ButtonsList.Add(btnInspect);
            Debug.Log("DBG: Inspect Enabled!");
        }

        if (AttackEnabled){
            ButtonsList.Add(btnAttack);
            Debug.Log("DBG: Attack Enabled!");
        }

        if (MoveEnabled){
            ButtonsList.Add(btnMove);
            Debug.Log("DBG: Movement Enabled!");
        }

        if (SelectEnabled){
            ButtonsList.Add(btnSelect);
            Debug.Log("DBG: Select Enabled!");
        }

        // TESTEO

    }

    void ButtonListInstancier2(){

        GameObject holder = new GameObject();
        holder.name = "Holder";
        GameObject Popup = ContextMenu;
        holder.transform.SetParent(Popup.transform, false);

    }

    void ButtonListInstancier(){

        GameObject holder = new GameObject();
        holder.name = "Holder";
        GameObject Popup = ContextMenu;
        holder.transform.SetParent(Popup.transform, false);
        

        //GameObject MainCan = GameObject.Find("MainCanvas");
        Debug.Log("InspectEnabled: "+ InspectEnabled);
        
        // GameObject ButtonsHolder = new GameObject();
        // ButtonsHolder.transform.SetParent(Popup);
        Debug.Log("Count= " + ButtonsList.Count);

        for (int i = 0; i < ButtonsList.Count; i++){
            GameObject item;
            item = Instantiate(ButtonsList[i]);
            item.transform.SetParent(holder.transform, false);
            item.transform.position = holder.transform.position + new Vector3(0, i*26, 0);
            //item.transform.rotation = Quaternion.identity;
            item.name = "TestBut";
        }

        //Text DebugButtons = GameObject.Find("debugtext_buttonlist").GetComponent<Text>();
        //DebugButtons.text = "Count: " + ButtonsList.Count;

        //Debug.Log("Popup existe?: " + Popup.name);
        //Debug.Log("Llega al instancier");
        //Debug.Log(GameObject.Find("popup"));

        Debug.Log("Targeteado: " + Targeted.name);
        // Debug.Log("LLEGA A LA INSTANCIACIÓN DE LOS SUBOBJETOS DEL MENU CONTEXT");
        //Debug.Log(Targeted.name);

        RectTransform PopupRect = Popup.GetComponent<RectTransform>();

        if (ButtonsList.Count == 1) {
            PopupRect.sizeDelta = new Vector2(100, 40);
        }

        else if (ButtonsList.Count == 2) {
            PopupRect.sizeDelta = new Vector2(100, 80);
            holder.transform.localPosition = new Vector3(0, -14, 0);
        }

        else if (ButtonsList.Count == 3) {
            PopupRect.sizeDelta = new Vector2(100, 120);
        }

        else {
            //DebugButtons.text += "Algún problema con popuprect...";
            Debug.Log("Problema con popu");
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text DebugTextButtons = GameObject.Find("debugtext_buttonlist").GetComponent<Text>();
        //DebugTextButtons.text = ButtonListInstancier[]

        HoverTile();

        if (Input.GetMouseButtonDown(0)) {
            
            TargetTile();
            RefreshInteractions();
        }

        if (Input.GetMouseButtonDown(1)) {

            TargetTile();
            RefreshInteractions();
            Debug.Log("Count: " + ButtonsList.Count);
            OpenContextMenu();
        }

        if (Input.GetMouseButtonDown(2)){

        }
        
        
    }
}
