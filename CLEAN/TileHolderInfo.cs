using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolderInfo : MonoBehaviour
{
    public GameObject InsertedObject;

    void InsertObject(GameObject obj){
        this.InsertedObject = obj;
    }

    void RemoveObject(){
        this.InsertedObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
