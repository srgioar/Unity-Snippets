using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexBuildingInfo : MonoBehaviour
{
    public List<GameObject> BuildingTiles;
    public string complexBuildingName;
    int complexBuildingHealth;
    // Comportamiento de edificios completos
    // Start is called before the first frame update
    void Start()
    {
        BuildingTiles = new List<GameObject>();
    }

    public void FillTiles(List<GameObject> list){
        // Llena la lista con todos los tiles de los que se compone el objeto
        this.BuildingTiles = list;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
