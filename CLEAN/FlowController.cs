using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlowController : MonoBehaviour
{

    bool InspectEnabled;
    bool AttackEnabled;
    bool MoveEnabled;
    bool SelectEnabled;

    Text DebugTextContainers, DebugTextInteractions;

    bool IsContextualActive = false;

    GameObject LastHovered;
    GameObject CurrentHovered;
    GameObject LastTargeted;
    GameObject CurrentTargeted;
    GameObject LastSelected;
    GameObject CurrentSelected;
    GameObject CurrentDestination;

    GameObject Hovered;
    GameObject Targeted;
    GameObject Selected;

    bool LeftMouse;
    bool RightMouse;


    // InnerCursor es cuando hay algo agarrado, Outer es el cursor normal para todo
    Image InnerCursor, OuterCursor;

    // SELECTED
    // DESTINATION
    // TARGETED
    // HOVERED
    bool StopControl = false;
    // Move y atacar trabajarán con el booleano anterior

    // Recalcular compatibilidad entre targets solo si el last target cambia


    // DEFINE EL CONTROL EN LA PARTE PRINCIPAL DE LA PARTIDA
    // CONTORLA EL MAIN LOOP Y ACTIVA DEACTIVA EVENTOS
    // TRABAJA JUNTO A OBJETO CURSOR



    // Start is called before the first frame update
    void Start()
    {
        DebugTextContainers = GameObject.Find("debugtext_containers").GetComponent<Text>();
        DebugTextInteractions = GameObject.Find("debugtext_interactions").GetComponent<Text>();

    }

    void ShowDebugText(){


        DebugTextContainers.text = ("Current Hovered:" + CurrentHovered);
        DebugTextContainers.text += ("\n Last Hovered:" + LastHovered);
        DebugTextContainers.text += ("\n Current Selected:" + CurrentSelected);
        DebugTextContainers.text += ("\n Last Selected:" + LastSelected);
        DebugTextContainers.text += ("\n Current Targeted:" + CurrentTargeted);
        DebugTextContainers.text += ("\n Last Targeted: " + LastTargeted);

        DebugTextInteractions.text = ("INSPECT:" + InspectEnabled);
        DebugTextInteractions.text += ("\nSELECT: " + SelectEnabled);
         DebugTextInteractions.text += ("\nATTACK: " + AttackEnabled);
          DebugTextInteractions.text += ("\nMOVE: " + MoveEnabled);


    }

    void ContextMenuNew(){

            Camera cam = Camera.main;
            GameObject inst = GameObject.Find("ButtonBackground");
            GameObject projecter = LastHovered;
            Vector3 pos1 = cam.WorldToScreenPoint(projecter.transform.position);
            Vector3 pos = Input.mousePosition;

            GameObject obj;
            obj = Instantiate(inst);
            obj.transform.SetParent(GameObject.Find("MainCanvas").transform, false);
            //RectTransform rt = obj.GetComponent<RectTransform>();
            // Setear posición y escala según ratio
            //rt.localPosition = screenPoint;
            obj.transform.position = pos1 + new Vector3(50, 40, 0);
            obj.name = "popup";
            //Debug.Log("Is context active: " + IsContextualActive);
            ButtonListInstancier();
            //IsContextualActive = true;

        

    }

    void ButtonListInstancier(){

        GameObject btnInspect = GameObject.Find("BTN_INSPECT");
        GameObject btnSelect = GameObject.Find("BTN_SELECT");

        GameObject btnMove = GameObject.Find("BTN_MOVE");
        GameObject btnAttack = GameObject.Find("BTN_ATTACK");
        GameObject btnSkill = GameObject.Find("BTN_SKILL");
        GameObject btnPick = GameObject.Find("BTN_PICK");

        List<GameObject> ButtonsList = new List<GameObject>();

        if (InspectEnabled){
            ButtonsList.Add(btnInspect);
        }

        if (AttackEnabled){
            ButtonsList.Add(btnAttack);
        }

        if (MoveEnabled){
            ButtonsList.Add(btnMove);
        }

        if (SelectEnabled){
            ButtonsList.Add(btnSelect);
        }

        GameObject Popup = GameObject.Find("popup");
        GameObject MainCan = GameObject.Find("MainCanvas");
        Debug.Log("InspectEnabled: "+ InspectEnabled);
        Debug.Log("Count: " + ButtonsList.Count);
        // GameObject ButtonsHolder = new GameObject();
        // ButtonsHolder.transform.SetParent(Popup);

        for (int i = 0; i < ButtonsList.Count; i++){
            GameObject item;
            item = Instantiate(ButtonsList[i]);
            item.transform.SetParent(Popup.transform, false);
            item.transform.position = Popup.transform.position + new Vector3(0, i*20, 0);
            item.transform.rotation = Quaternion.identity;
            item.name = "TestBut";
        }

        Text DebugButtons = GameObject.Find("debugtext_buttonlist").GetComponent<Text>();
        DebugButtons.text = "Count: " + ButtonsList.Count;

        //Debug.Log("Popup existe?: " + Popup.name);
        Debug.Log("Llega al instancier");
        //Debug.Log(GameObject.Find("popup"));
        RectTransform PopupRect = Popup.GetComponent<RectTransform>();

        if (ButtonsList.Count == 1) {
            PopupRect.sizeDelta = new Vector2(140, 40);
        }

        else if (ButtonsList.Count == 2) {
            PopupRect.sizeDelta = new Vector2(140, 80);
        }

        else if (ButtonsList.Count == 3) {
            PopupRect.sizeDelta = new Vector2(140, 120);
        }

        else {
            DebugButtons.text += "Algún problema con popuprect...";
            Debug.Log("Problema con popu");
        }

        //Debug.Log("Instancia correcto");

    }

    public void DeselectNew(){
        // SI current targeted tiene el mismo aliado que el selected
        if (Input.GetMouseButton(2) && CurrentSelected != null) {
            CurrentSelected.GetComponentInChildren<TileIdentifierNew>().SetSelected(false);
            CurrentSelected = null;
        }
    }   
        
    public void RefreshInteractions(){

        // Si no hay nada seleccionado la interacción es inspeccionar
        if (CurrentTargeted != null) {
            // Enable inspect
            InspectEnabled = true;
        }

        else {
            InspectEnabled = false;
        }

        if (CurrentSelected == null && CurrentTargeted != null) {

            if (CurrentTargeted.GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Ally) {
                SelectEnabled = true;
            }

            else {
                SelectEnabled = false;
            }

        }

        if (CurrentSelected != null) {
            SelectEnabled = false;
        }

        if ((CurrentSelected != null) && (CurrentTargeted != null)) {

            //TargetDataHolder tdh = new TargetDataHolder(CurrentSelected, CurrentTargeted);
            TileInteractionScanner tis = new TileInteractionScanner(CurrentSelected, CurrentTargeted);
            //int interaction = tdh.GetTypeOfInteraction(1, 2, 2, 1);
            tis.CheckInteraction();
            AttackEnabled = tis.IsAttackPossible;
            MoveEnabled = tis.IsMovementPossible;

        }

        //return str_interaction;

    }

    public void Select(){

    // Si el tile targeteado tiene un personaje, el seleccionado equivale al personaje
    // Mejorar: Chequear que la posición inicial tiene un punto de interés (personaje)
    // Que la posición a elegir de destino NO está como no disponible
    // Establecer que la segunda posición seleccionada es posible si se usa
    // FIX: DESELECCIONAR Y SELECCIONAR NUEVA POSICIÓN CORRECTAMENTE

        TileIdentifierNew lt = CurrentTargeted.GetComponentInChildren<TileIdentifierNew>();
        Debug.Log("HASCHAR: " + lt.containedType.ToString());
        
        if (lt.containedType == TileIdentifierNew.ContainedType.Ally) {
            if (LastSelected != null) {
                TileIdentifierNew ls = LastSelected.GetComponentInChildren<TileIdentifierNew>();
                ls.SetSelected(false);
            }

            else {

            }

            CurrentSelected = CurrentTargeted;
            CurrentSelected.GetComponentInChildren<TileIdentifierNew>().SetSelected(true);
            Destroy(GameObject.Find("popup"));
            //Debug.Log("Llega a marcar como true!");
            LastSelected = CurrentSelected;
        }

    }

    bool TouchingUI(){

    //     bool IsTouchingUI = false;
    //     GraphicRaycaster gr = FindObjectOfType<GraphicRaycaster>();
    //     EventSystem eventSystem = FindObjectOfType<EventSystem>();
    //     PointerEventData pointerEventData = new PointerEventData(eventSystem);
    //     pointerEventData.position = Input.mousePosition;
    //     List<RaycastResult> results = new List<RaycastResult>();
    //     gr.Raycast(pointerEventData, results);
    //     IsTouchingUI = results.Count > 0;
    //     Debug.Log("Touching: " + IsTouchingUI);
    //     }
        return EventSystem.current.IsPointerOverGameObject();
    }

    void Targeting3(){

        if (RightMouse) {
                ContextMenuNew();
        }

        if (LeftMouse || RightMouse) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            
            if (Physics.Raycast(ray, out hit) && !TouchingUI()) {

                Transform objHit;
                objHit = hit.transform;
                if ((objHit.tag == "tilecube")) {
                    //RefreshInteractions();
                    Destroy(GameObject.Find("popup"));

                    // Borrar antiguo target antes de nada...
                if (Targeted != null) {
                    if (Targeted != objHit.parent.gameObject) {
                        TileIdentifierNew lh = Targeted.GetComponentInChildren<TileIdentifierNew>();
                        lh.SetTargeted(false);
                        
                    }
                }

                TileIdentifierNew ti = objHit.parent.gameObject.GetComponentInChildren<TileIdentifierNew>();
                ti.SetTargeted(true);
                Targeted = objHit.parent.gameObject;

                }
            }
        }

    }

    void TargetingNew(){

        if (LeftMouse || RightMouse) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            
            if (Physics.Raycast(ray, out hit) && !TouchingUI()) {

                Transform objHit;
                objHit = hit.transform;
                if ((objHit.tag == "tilecube")) {
                    //RefreshInteractions();
                    Destroy(GameObject.Find("popup"));
                    //IsContextualActive = false;

                    // Borrar antiguo target antes de nada...
                    if (LastTargeted != null) {
                    if (LastTargeted != objHit.gameObject) {
                        TileIdentifierNew lh = LastTargeted.GetComponentInChildren<TileIdentifierNew>();
                        lh.SetTargeted(false);
                        
                    }
                }
                TileIdentifierNew ti = objHit.gameObject.GetComponent<TileIdentifierNew>();
                ti.SetTargeted(true);
                LastTargeted = objHit.parent.gameObject;
                CurrentTargeted = objHit.parent.gameObject;

                if (RightMouse) {
                ContextMenuNew();
                }


                }
            }
        }



    }


    void Hover(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && !TouchingUI()) {

            Transform tileHit = hit.transform;

            if ((tileHit.tag == "tilecube")) {

            //InnerCursor = tileHit.parent.GetChild(1).GetChild(2).gameObject.GetComponent<Image>();

            // Borrar antiguo hover
                if (LastHovered != null) {
                    if (LastHovered != tileHit.gameObject) {
                        TileIdentifierNew lh = LastHovered.GetComponentInChildren<TileIdentifierNew>();
                        lh.SetIsHover(false);
                        //LastHovered.transform.parent.GetChild(1).GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                    }

                    else {
                        // DO nothing
                    }
                }

                TileIdentifierNew ti = tileHit.GetComponent<TileIdentifierNew>();
                ti.SetIsHover(true);
                //InnerCursor.enabled = true;
                LastHovered = tileHit.parent.gameObject;

            }

        }

        else {

            if (LastHovered != null) {
                TileIdentifierNew lh = LastHovered.GetComponentInChildren<TileIdentifierNew>();
                lh.SetIsHover(false);
            }

        }

        }

    // Update is called once per frame
    void Update()
    {   
        
        Hover();
        TargetingNew();
        RefreshInteractions();
        ShowDebugText();
        DeselectNew();
        LeftMouse = Input.GetMouseButtonDown(0);
        RightMouse = Input.GetMouseButtonDown(1);

        
        //Debug.Log("Contextual Active: " + IsContextualActive);
    }

    
}


