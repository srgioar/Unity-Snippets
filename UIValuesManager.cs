using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValuesManager : MonoBehaviour
{

    // Almacenamos referencia al script principal con el status de TODO
    TableController MainGameController;
    TargetDataHolder CurrentCursorData;
    GameObject firstSelected, lastTargeted; // Referencias importadas del script principal

    // PANEL DE MUESTRA SELECT - TARGET (IZQ SUPERIOR)
    Text SelectedTileTitle, SelectedActorName;
    Text SelectedStatMelee, SelectedStatMovement, SelectedStatDiagonal, SelectedStatDistance;
    Image SelectedLife, SelectedEnergy;
    Text SelectedVarStats;

    Text TargetTileTitle, TargetActorTitle;
    
    Text DebugInfo;
    public Text CurrentHolderInfo;

    GameObject SelectedTile, TargetTile;
    CharIdentifier SelectedActorIdentifier, TargetActorIdentifier;
    TileIdentifier SelectedTileIdentifier, TargetTileIdentifier;

    // BLOQUE INFORMACIÓN RELATIVA DE POSICIÓN (INICIO / FINAL)

    Text CurrentSelectedDirection;
    Text DifferenceRows, DifferenceCols;
    Text InteractionType;
    Text IsOnRange;

    // BLOQUE TARGETEO / HOVER + GAMEOBJECT CONTENIDO

    Text CurrentTargetedInfo, CurrentHoldingInfo;


    // Instancia los chunks de energía dependiendo según la cantidad del pj
    void InstantiateEnergy(){
        int friendlyActorEnergy, enemyActorEnergy;
        // instanciar en UI

    }

    // void CursorInfo(){
    //     auxInfo.text = "First selected: " + firstSelected + "\n" + "Second selected: " + lastSelected + "\n" + 
    //     "Last targeted: " + lastTargeted + "\n" + "Last selected: " + lastSelected;
    // }

    // SELECTED Y TARGETED
    void ChargeCurrentCursorUIInfo(){
        SelectedTileTitle.text = firstSelected.name;
        TargetTileTitle.text = lastTargeted.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        // CARGAR EL SCRIPT CONTENEDOR PRINCIPAL, TABLE CONTROL ->
        MainGameController = GameObject.Find("CONTAINER").GetComponent<TableController>();

        // comprobar y actualizar valores vida/maná/energía
        //SelectedTileTitle = GameObject.Find("PRIMARY/tile_name").GetComponent<Text>();
        SelectedActorName = GameObject.Find("PRIMARY/ACTOR_NAME").GetComponent<Text>();
        SelectedVarStats = GameObject.Find("PRIMARY/ACTORDATA/VAR_STATS").GetComponent<Text>();

        SelectedStatMelee = GameObject.Find("PRIMARY/ACTORDATA/RANGE_MELEE").GetComponent<Text>();
        SelectedStatDistance = GameObject.Find("PRIMARY/ACTORDATA/RANGE_DIST").GetComponent<Text>();
        SelectedStatMovement = GameObject.Find("PRIMARY/ACTORDATA/RANGE_MOV").GetComponent<Text>();
        SelectedStatDiagonal = GameObject.Find("PRIMARY/ACTORDATA/RANGE_DIAG").GetComponent<Text>();

        DebugInfo = GameObject.Find("UI_DEBUG_INFO/Image/debugInfo").GetComponent<Text>();

        //TargetTileTitle = GameObject.Find("")

    }

    void ShowBasicUIValues(){

        //MainGameController

        DebugInfo.text = CurrentCursorData.GetTypeOfInteraction(1, 2, 2, 1).ToString();

        if (!SelectedTileIdentifier.tileIsEmpty) {

            GameObject holding = SelectedTileIdentifier.GetActorHolder();
            CharIdentifier ci = holding.GetComponent<CharIdentifier>();

            CurrentHolderInfo.text = holding.name;

            SelectedActorName.text = holding.name;
            
            SelectedStatMelee.text = ci.MeleeRange.ToString();
            SelectedStatMovement.text = ci.MovementRange.ToString();
            SelectedStatDistance.text = ci.DistanceRange.ToString();
            SelectedStatDiagonal.text = ci.DiagonalRange.ToString();

            SelectedVarStats.text = ci.Atk.ToString() + "/" + ci.Def.ToString();

            for (int i = 1; i <= ci.Life; i++) {

                // GameObject newx =
                // Instantiate(
                // GameObject.Find("life_bar"),
                // GameObject.Find("life_bar").transform.position + new Vector3(0.2f, 0f, 0f), transform.rotation,
                // GameObject.Find("PRIMARY/ACTORDATA").transform
                // );

                // newx.name = "Copyx+" + i.ToString();
            }

        }

        else {
                CurrentHolderInfo.text = "Nothing-Empty";
        }

        

        // CARGAR DATOS PRIMARY


        

        // DebugInfo.text +=

        //     "\nEn recta: " + ValuesChecker.EstaEnRecta(iRow, dRow, iCol, dCol).ToString()
        //     + "\nEn Cruz: " + ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol).ToString()
        //     + "\nEn Distancia: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDistancia).ToString()
        //     + "\nEn R movimiento: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMovimiento).ToString()
        //     + "\nEn R Melee: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMelee).ToString()
        // ;

    }

    // Update is called once per frame
    void Update()
    {

        // Update en principio asigna los valores de la variable que pilla el TableController y actualiza las variables en consecuencia

        // Tendría que dividirse entre Load Values y Show Values

        // Han de actualizarse constantemente
        // Obtener el TargetDataHolder actual y asignar sus valores:
        firstSelected = MainGameController.firstSelected;
        lastTargeted = MainGameController.lastTargeted;

        if (MainGameController.currentCursorData != null) {
            CurrentCursorData = MainGameController.currentCursorData;
            ShowBasicUIValues();
        }
        

        // Cargar los scripts internos de ambos pjs
        if (lastTargeted != null) {
            SelectedTileIdentifier = lastTargeted.GetComponent<TileIdentifier>();
            //SelectedActorIdentifier = SelectedTileIdentifier.actorHolder.GetComponent<CharIdentifier>();
        }
        

        

        
    }
}
