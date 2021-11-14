using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    Button SpawnPlayerButton, SpawnEnemyButton, SpawnDebugItem;
    GameObject[,,] TestTable;
    BoardCreator3D boardCreator;

    GameObject PlayerChar, EnemyChar;

    Vector3 vOffset;

    // Controla el spawn automático del flow del juego y los spawns manuales
    // Start is called before the first frame update
    void Start()
    {
        //PlayerChar = GameObject.Find("PlayerChar");
        PlayerChar = Resources.Load("Chars/PlayerChar") as GameObject;
        EnemyChar = Resources.Load("Chars/EnemyChar") as GameObject;
        boardCreator = GameObject.Find("SceneController").GetComponent<BoardCreator3D>();
        vOffset = new Vector3(0, 0.05f, 0);
    }

    public void SpawnEnemyTestFixed() {

        TestTable = boardCreator.TestTable;

        // Checkear: INEXPLICABLEMENTE HA DE SER >= a 0 EL VALOR PARA INST ENEMIGO Y QUE NO SE QUEDE SIN ULTIMA POSICION IZQ

        for (int x = 0; x < 3; x++) {

            for (int f = 0; f < 8; f++) {

                for (int i = 7; i >= 0; i--) {

                    TileInfo tileInfo = TestTable[f,i,x].GetComponent<TileInfo>();

                    if (tileInfo.IsAvailable()) {
                        // Spawn player
                        Instantiate(
                        EnemyChar,
                        TestTable[f,i,x].transform.position - vOffset,
                        Quaternion.identity,
                        GameObject.Find("LiveCharsContainer").transform
                        );
                        Debug.Log("Instanciado en: " + TestTable[f,i,x].name);
                        tileInfo.SetAsNotWalkable();
                        return;
                    }
                }
            }
        }
    }

    public void SpawnPlayerTestFixed() {

        TestTable = boardCreator.TestTable;

        // Checkear: INEXPLICABLEMENTE HA DE SER -1 EL VALOR PARA INST ALIADO Y QUE NO DÉ OUT OF BOUNDS
        for (int x = 0; x < 3; x++) {

            for (int f = 7; f > 0; f--) {

                for (int i = 0; i < 8; i++) {

                    TileInfo tileInfo = TestTable[f,i,x].GetComponent<TileInfo>();

                    if (tileInfo.IsAvailable()) {
                        // Spawn player
                        Instantiate(
                        PlayerChar,
                        TestTable[f,i,x].transform.position - vOffset,
                        Quaternion.identity,
                        GameObject.Find("LiveCharsContainer").transform
                        );
                        Debug.Log("Instanciado en: " + TestTable[f,i,x].name);
                        tileInfo.SetAsNotWalkable();
                        return;
                    }
                }
            }
        }
    }

    public void SpawnPlayerTest(){

        boardCreator = GameObject.Find("SceneController").GetComponent<BoardCreator3D>();
        TestTable = boardCreator.TestTable;

        // F, I, X
        // 8 8 3
        for (int f = 0; f < 8; f++){

            for (int i = 0; i < 8; i++){

                for (int x = 0; x < 3; x++) {

                    TileInfo tileInfo = TestTable[f,i,x].GetComponent<TileInfo>();

                    if (tileInfo.IsAvailable()) {
                        // Spawn player
                        Instantiate(
                        PlayerChar,
                        TestTable[f,i,x].transform.position + new Vector3(0, 0.2f, 0),
                        Quaternion.identity,
                        GameObject.Find("LiveCharsContainer").transform
                        );
                        Debug.Log("Instanciado en: " + TestTable[f,i,x].name);
                        tileInfo.SetAsNotWalkable();
                        return;
                    }
                } // FIN TERCER FOR

            } // FIN PRIMER FOR
        } // FIN SEGUNDO FOR

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
