using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class CursorConfig : MonoBehaviour
{
    // control de estado
    public bool SelectModeEnabled;
    public bool TileNavigationEnabled;
    public bool IsModalActive;

    // Gameflowstatus
    GameFlowStatus gameFlowStatus;
    GameObject BuildingsContainer;

    // Llamadas a scripts ajenos
    BuildingCreatorManager buildingCreatorManager;
    TileOperations tileOperations;

    // Apertura de context menu
    GameObject ContextMenu;

    // Lista de tiles seleccionados
    public List<GameObject> tilesSelected;
    //BoardStatus boardStatus;

    // Contiene los GO actuales hovered o targeteados
    public GameObject Hovered, Targeted;

    // Cambio de cursor
    public Texture2D cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        gameFlowStatus = GameObject.Find("SceneController").GetComponent<GameFlowStatus>();
        tileOperations = GameObject.Find("SceneController").GetComponent<TileOperations>();
        buildingCreatorManager = GameObject.Find("SceneController").GetComponent<BuildingCreatorManager>();
        SetCustomCursor();
        BuildingsContainer = GameObject.Find("BuildingsContainer");
        SelectModeEnabled = false;
        TileNavigationEnabled = true;

    }

    bool TouchingUI(){
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Hover & Select
    void Hover(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Si tocamos algo y no es la UI...
        if (Physics.Raycast(ray, out hit)){

            //Debug.Log("Tile Hit!: " + hit.transform.gameObject.name);

            Transform ObjectHit = hit.transform;

            // En caso de que sea un tile
            if ((ObjectHit.tag == "Tile")) {

                if (Hovered != null) {
                    TileInfo LastHoveredInfo = Hovered.GetComponent<TileInfo>();
                    LastHoveredInfo.DisableHoverCursor();
                }

                Hovered = ObjectHit.gameObject;
                TileInfo HoveredInfo = Hovered.GetComponent<TileInfo>();
                HoveredInfo.EnableHoverCursor();
             

            // Al pulsar para seleccionar (Click izq)


            } // FIN DE EVENTOS SI TOCAS TILE
        }

    }

    void Target(){

            if (Input.GetMouseButton(0) && Hovered != null) {

                if (Targeted != null) {
                    TileInfo LastTargetedInfo = Targeted.GetComponent<TileInfo>();
                    LastTargetedInfo.DisableTargetCursor();
                }

                Targeted = Hovered;
                TileInfo TargetedInfo = Targeted.GetComponent<TileInfo>();
                TargetedInfo.EnableTargetCursor();

            } // FIN DEL INPUT MOUSEBUTTON 

    }

    public void Cancel(){

        // GameObject CurrentContextMenu = new GameObject();
        // CurrentContextMenu = GameObject.Find("ContextMenu");

        // if (CurrentContextMenu != null) {
        //     Destroy(CurrentContextMenu);
        // }

        for (int i = 0; i < tilesSelected.Count; i++) {
            tilesSelected[i].GetComponent<TileInfo>().SetAsNotSelected();
        }

        TileNavigationEnabled = true;
        SelectModeEnabled = false;

        buildingCreatorManager.CloseBuildingCreationWindow(0);
        
        if (tilesSelected.Count > 0) {
            tilesSelected.Clear();
        }

        

    }

    void Select() {

        if (Hovered != null) {

            if (Input.GetKeyDown(KeyCode.LeftShift)) {

                if (Hovered != null) {

                    TileInfo TargetedInfo = Targeted.GetComponent<TileInfo>();
                
                    if (TargetedInfo.IsAvailable() && tilesSelected.Count == 0) {
                        tilesSelected.Add(Targeted);
                        TargetedInfo.EnableSelectCursor();
                    }

                    else if (TargetedInfo.IsAvailable() && tilesSelected.Count > 0 && tilesSelected.Count < 4) {

                        bool movValid = tileOperations.EstaEnCruz(tilesSelected.Last(), Targeted);

                        if (movValid) {
                            tilesSelected.Add(Targeted);
                            TargetedInfo.EnableSelectCursor();
                            int cuenta = tilesSelected.Count;
                            tilesSelected[cuenta-2].GetComponent<TileInfo>().SetAsNormalCursor();
                            tilesSelected.Last().GetComponent<TileInfo>().SetAsGuideCursor();
                        }

                    }

                }
            }
        }

    }


    void Build(){

        // Boton derecho y saca el contextual de construir
        // En caso de estar pulsando encima del tile guía
        // En caso contrario habilita la navegación de nuevo

        if (Input.GetMouseButtonDown(1) && tilesSelected.Count > 0) {

            Destroy(GameObject.Find("ContextMenu"));
            //TileNavigationEnabled = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Si tocamos algo...
            if (Physics.Raycast(ray, out hit)){

                if (hit.transform.gameObject == tilesSelected.Last()) {

                    Camera cam = Camera.main;
                    GameObject inst = GameObject.Find("MainBuildPanel");
                    GameObject projecter = tilesSelected.Last();
                    Vector3 pos1 = cam.WorldToScreenPoint(projecter.transform.position);

                    GameObject obb;
                    obb = Instantiate(inst);
                    obb.transform.SetParent(GameObject.Find("MainCanvas").transform, false);
                    obb.transform.position = pos1 + new Vector3(55, 26, 0);
                    obb.name = "ContextMenu";
                    this.ContextMenu = obb;
                    TileNavigationEnabled = false;

                    Button ButtonBuild = GameObject.Find("ButtonInteract").GetComponent<Button>();
                    
                    // Spawn BUILD OPTIONS

                }

                // Si lo que tocamos no es el last tile...
                else {

                    Cancel();

                }
            }

    }

    }

    public string LiveCheck(){

        if (Targeted != null && Hovered != null) {

            return tileOperations.EstaEnCruz(Targeted, Hovered).ToString();
        // if () {
        //     Debug.Log("Es mov valido...");
        // }

        // else {
        //     Debug.Log("No es");
        // }
    }

    else {
        return "Sin datos";
    }

    }

    void SetCustomCursor(){
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void CheckSelectModeEnabled(){

        if (tilesSelected.Count > 0) {
            SelectModeEnabled = true;
        }

        else if (tilesSelected.Count <= 0) {
            SelectModeEnabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        tilesSelected = gameFlowStatus.TilesSelected;

        if (TileNavigationEnabled) {
                Hover();
                Target();
        }

        Select();
        CheckSelectModeEnabled();
        Build();

    }
}
