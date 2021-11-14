using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuManager : MonoBehaviour
{
    List <GameObject> tilesList;
    CursorConfig ccfig;
    // TRABAJA CON LA LONGITUD DE LA LISTA DE TILES
    // SI LA LISTA ES 1 -> BUILDLINGS MODO 1 (O PATH MODO 1)
    // SI LA LISTA ES 3 MAX -> BUILDLING
    // SI LA LISTA ES +3 -> PATH

    // Start is called before the first frame update
    void Start()
    {
        tilesList = new List<GameObject>();
        ccfig = GameObject.Find("SceneController").GetComponent<CursorConfig>();
        tilesList = ccfig.tilesSelected;
    }

    // void CheckList(){

    //     int listLength = tilesList.Count;

    //     switch (listLength) {
    //         case (listLength == 1):
    //             break;
    //         }
    //     }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
