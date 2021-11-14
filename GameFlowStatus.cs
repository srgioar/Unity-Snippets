using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStatus : MonoBehaviour
{
    // control de estado
    public bool SelectModeEnabled;
    public bool TileNavigationEnabled;
    public bool IsBuildingModalActive;

    CursorConfig cursorConfig;

    public List<GameObject> TilesSelected;

    // Start is called before the first frame update
    void Start()
    {
        SelectModeEnabled = false;
        TileNavigationEnabled = true;
        //IsModalActive = false;
    }

    void CheckSelectModeEnabled(){

        if (TilesSelected.Count > 0) {
            SelectModeEnabled = true;
        }

        else if (TilesSelected.Count <= 0) {
            SelectModeEnabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // TilesSelected = cursorConfig.tilesSelected;
        // CheckSelectModeEnabled();
    }
}
