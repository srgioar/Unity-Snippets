using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BuildingCreatorManager : MonoBehaviour
{
    public Material DefaultMaterial, WoodMaterial, StoneMaterial, BuildingMaterial;
    public Material CurrentMaterial;
    public bool BuildingModeEnabled;
    // Datos de la lista de tiles
    CursorConfig cf;
    CameraController camControl;
    List<GameObject> currentTilesSelected;
    GameObject BuildingsContainer;

    // Elementos UI del manager de creación
    GameObject BuildingOptionsPanel;
    Button sizeUp, sizeDown, typeUp, typeDown, buttonOk, buttonCancel;
    public Text currentSizeNumText, currentTypeText;
    Image currentSizeNumImg;

    // Datos actuales de estado
    public int sizeNum, lastSizeNum, typeNum, lastTypeNum;
    string typeString = "Select Material";
    BuildingInfo.BuildingType buildingType;

    // Start is called before the first frame update
    void Start()
    {
        CurrentMaterial = DefaultMaterial;
        camControl = GameObject.Find("SceneController").GetComponent<CameraController>();
        cf = GameObject.Find("SceneController").GetComponent<CursorConfig>();

        BuildingsContainer = GameObject.Find("BuildingsContainer");
        BuildingOptionsPanel = GameObject.Find("BulidingCreationPanel");

        currentSizeNumImg = GameObject.Find("sizeNum").GetComponent<Image>();
        currentSizeNumText = GameObject.Find("sizeNum").GetComponentInChildren<Text>();

        currentTypeText = GameObject.Find("typeName").GetComponentInChildren<Text>();

        sizeNum = 1;
        lastSizeNum = 1;
        // Typenum empieza a 0 porque hay un material default (sin escoger edificio)
        typeNum = 0;
        lastTypeNum = 0;

        currentSizeNumText.text = sizeNum.ToString();
        currentTypeText.text = typeString.ToString();
        //sizeUp
    }

    public void ResetNums(){
        typeNum = 0;
        lastTypeNum = 0;
        sizeNum = 1;
        lastSizeNum = 1;
        CurrentMaterial = DefaultMaterial;
        typeString = "Select";
    }

    public void UppingSize(){
        sizeNum++;
        if (sizeNum > 2) {
            sizeNum = 1;
        }
    }

    public void UppingType(){
        typeNum++;
        if (typeNum > 3) {
            typeNum = 1;
        }
    }

    public void DowningSize(){
        sizeNum--;
        if (sizeNum < 1) {
            sizeNum = 2;
        }
    }

    public void DowningType(){
        typeNum--;
        if (typeNum < 1){
            typeNum = 3;
        }
    }

    void UpdatePanelsText(){

        if (GameObject.Find("DetailsMenu")!=null) {

            GameObject.Find("DetailsMenu").transform.GetChild(1).GetChild(3).GetComponentInChildren<Text>().text = sizeNum.ToString();
            GameObject.Find("DetailsMenu").transform.GetChild(2).GetChild(3).GetComponentInChildren<Text>().text = typeString; 

        }
  
    }

    public void CreationWindowLoop(){

        // SI NUESTRA VENTANA DE BULIDING ESTÁ ACTIVADA
        // DESACTIVAMOS LA NAVEGACIÓN DE HOVER EN CURSORCONFIG

        OpenBuildingCreationWindow();

        if (GameObject.Find("DetailsMenu") != null) {
            cf.TileNavigationEnabled = false;
        }

    }


    // checkea si el valor cambia, el resto de botones cambian el valor.
    // Permite pulsar ACCEPT o CANCEL
    void CheckChangeInValue(){
        
        if (sizeNum != lastSizeNum) {
            // LLamar a instancier
            RemoveBuildings();
            InstantiateBuildings(sizeNum);
            lastSizeNum = sizeNum;
            //currentSizeNumText.text = sizeNum.ToString();
        }
    }

    void CheckChangeInType(){

        if (typeNum != lastTypeNum) {

            if (typeNum == 1) {
                CurrentMaterial = WoodMaterial;
                typeString = "Wood";
            }

            else if (typeNum == 2) {
                CurrentMaterial = StoneMaterial;
                typeString = "Stone";
            }

            else if (typeNum == 3) {
                CurrentMaterial = BuildingMaterial;
                typeString = "Building";
            }

            else if (typeNum == 0) {
                 CurrentMaterial = DefaultMaterial;
                 typeString = "Select";
            }

            ChangeBuildingMaterial(CurrentMaterial);
            lastTypeNum = typeNum;
        }

    }

    public void ChangeBuildingMaterial(Material material){

        currentTilesSelected = cf.tilesSelected;

        for (int i = 0; i < currentTilesSelected.Count; i++) {
                BuildingInfo buildingInfo = currentTilesSelected[i].GetComponent<TileInfo>().ContainedBuilding.GetComponent<BuildingInfo>();
                buildingInfo.SetMaterial(material);
            }
    }

    public void OpenBuildingCreationWindow(){

        currentTilesSelected = cf.tilesSelected;
        GameObject inst = GameObject.Find("CreateBuildingMenu");
        GameObject obb;
        obb = Instantiate(inst);
        obb.transform.SetParent(GameObject.Find("MainCanvas").transform, false);

        // 0 a 7
        // 0 a 7
        // Si es esquina superior izq, arriba izq
        // Si es esquina superior derecha, 
        Vector3 pos1 = Camera.main.WorldToScreenPoint(currentTilesSelected.Last().transform.position);
        camControl.FixateCameraOnBuilding(currentTilesSelected.Last());
        obb.transform.position = pos1 + new Vector3(100, 0, 0);
        obb.name = "DetailsMenu";


        InstantiateBuildings(1);
        Destroy(GameObject.Find("ContextMenu"));
        ResetNums();

    }

    public void CloseBuildingCreationWindow(int type){

        // type 0 -> Accepting
        // type 1 -> Closing cancelling

        if (GameObject.Find("DetailsMenu") != null) {

            Destroy(GameObject.Find("DetailsMenu"));

            currentTilesSelected = cf.tilesSelected;

            if (type == 0) {

                for (int i = 0; i < currentTilesSelected.Count; i++) {
                currentTilesSelected[i].GetComponent<TileInfo>().RemoveBuilding();
                }
            }

            camControl.RestartCamera();
            ResetNums();

        }
    }

    public void RemoveBuildings(){

        currentTilesSelected = cf.tilesSelected;

        if (currentTilesSelected.Count > 0) {
            GameObject[] newBuilding = new GameObject[currentTilesSelected.Count];
            for (int i = 0; i < currentTilesSelected.Count; i++) {
                currentTilesSelected[i].GetComponent<TileInfo>().RemoveBuilding();
            }

            }

    }

    public void InstantiateBuildings(int height){

        currentTilesSelected = cf.tilesSelected;

        if (currentTilesSelected.Count > 0) {
            //GameObject[] newBuilding = new GameObject[currentTilesSelected.Count];
            for (int i = 0; i < currentTilesSelected.Count; i++) {
                currentTilesSelected[i].GetComponent<TileInfo>().InsertBuilding(height, CurrentMaterial);
                // BuildingInfo buildingInfo = currentTilesSelected[i].GetComponent<TileInfo>().ContainedBuilding.GetComponent<BuildingInfo>();
                // buildingInfo.SetMaterial(CurrentMaterial);
            }

            currentTilesSelected = cf.tilesSelected;

            }
    }

    public void AcceptBuildling(){

        //currentTilesSelected = cf.tilesSelected;
        GameObject newBuilding = new GameObject();
        ComplexBuildingInfo complexInfo = newBuilding.AddComponent<ComplexBuildingInfo>() as ComplexBuildingInfo;
        newBuilding.transform.SetParent(BuildingsContainer.transform);
        newBuilding.name = typeString + " " + "Building n";
        complexInfo.complexBuildingName = newBuilding.name;
        CloseBuildingCreationWindow(1);
        cf.TileNavigationEnabled = true;
        complexInfo.FillTiles(currentTilesSelected);
        cf.tilesSelected.Clear();

    }

    // Update is called once per frame
    void Update()
    {
        CheckChangeInValue();
        CheckChangeInType();
        UpdatePanelsText();
        
    }
}
