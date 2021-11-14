using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanelManager : MonoBehaviour
{
    public Image DebugPanel;
    public GameObject MainScript;
    public string TileInteracted;
    public Text CursorOptionsPanel;
    public Text CompatibilityPanel;

    CursorConfig cf;
    //BoardStatus bs;


    // Start is called before the first frame update
    void Start()
    {
        DebugPanel = GameObject.Find("Debug").transform.GetChild(0).gameObject.GetComponent<Image>();
        CursorOptionsPanel = DebugPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
        MainScript = GameObject.Find("SceneController");
        cf = MainScript.GetComponent<CursorConfig>();
        //bs = MainScript.GetComponent<BoardStatus>();

    }

    // Update is called once per frame
    void Update()
    {
        WritePanel();
        WriteCompatibilityPanel();
    }

    void WritePanel(){
    //     DebugPanelText.text = "Tile Interacted: " + cf.TileInteracted.ToString() + "\n"
    //     + "Tile Available: " + cf.TileInteracted.GetComponent<TileInfo>().IsAvailable().ToString() + "\n";
    //    // + "Tile Selected:" + bs.Selected.ToString();
    }

    void WriteCompatibilityPanel(){

        if (cf.Targeted != null && cf.Hovered != null) {
            CompatibilityPanel.text = "Hovered: " + cf.Hovered.name + " Targeted: " + cf.Targeted.name;
            CompatibilityPanel.text += "\n" + "Interaccion legal: " + cf.LiveCheck();
        }

        else {
            CompatibilityPanel.text = "No data";
        }

        CursorOptionsPanel.text = "Sel Mode Enabled: " + cf.SelectModeEnabled + "\n" + "Tile Nav Enabled: " + cf.TileNavigationEnabled;

    }
}
