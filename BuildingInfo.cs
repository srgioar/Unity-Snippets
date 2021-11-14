using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    // El tile al que está atado esta pieza
    public GameObject MasterTile;
    public Text BuildingText;
    public MeshRenderer meshRenderer;

    public GameObject BuildingCreation, Building1, Building2, WatchTower, EnergyUnit;

    public enum BuildingType
    {
        Creation, Wood, Stone, Building1, Building2, Building3
    }

    public BuildingType buildingType = BuildingType.Creation;

    // Start is called before the first frame update

    // Tipos básicos

    // Si es edificio compuesto o normal (Vida compartida o individual como bloques)


    // Establece lo visual del edificio
    // void SetBulidingVisuals(BuildingType type){
    //     if (type == BuildingType.Wood) {
    //         meshRenderer.material = WoodMaterial;
    //     }
    // }

    public void SetMasterTile(GameObject tile){

    }

    // public void SetAsTestMaterial(){
    //     meshRenderer.material = WoodMaterial;
    // }

    public void SetMaterial(Material material){
        if (meshRenderer != null) {
            meshRenderer.material = material;
        }

        else{
            meshRenderer = this.GetComponent<MeshRenderer>();
        } 
        
    }

    public void SetBuildingType(BuildingType buildingType) {

        // switch(buildingType) {
        //     buildingType == BuildingType.Building1:
        //     break;
        //     buildingType == BuildingType.Building2:
        //     break;
        //     buildingType = BuildingType.Building3:
        //     break;
        // }

    }

    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        BuildingText = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
