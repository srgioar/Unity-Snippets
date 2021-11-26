using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCreator : MonoBehaviour
{

    public GameObject[,] TestTable;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateBoard();
        InitBoardTestActors();
        DropValues();
    }

    // Update is called once per frame
    void Update()
    {
        //slayerGO = GameObject.FindGameObjectWithTag("PlayableChar");
    }

     

    void DropValues(){

        //GameObject TileBase = GameObject.Find("TILEBASE");
        //Debug.Log("Bounds x: " + TileBase.GetComponent<Renderer>().bounds.size.x + "\n" + TileBase.GetComponent<Renderer>().bounds.size.y);

    }

    void InitBoardTestActors(){
        GameObject playerGO = GameObject.FindGameObjectWithTag("PlayableChar");
        playerGO.transform.position = TestTable[5,3].transform.position;
        TileHolderInfo thi = TestTable[5,3].GetComponentInChildren<TileHolderInfo>();
        thi.InsertedObject = playerGO;

        GameObject enemyGO = GameObject.FindGameObjectWithTag("Enemy");
        enemyGO.transform.position = TestTable[7,3].transform.position;
        TileHolderInfo thie = TestTable[7,3].GetComponentInChildren<TileHolderInfo>();
        thie.InsertedObject = enemyGO;

    }

    void InstantiateBoard(){

          // Offset horizontal para colocar las piezas entre ´si
        //float hOffset = 1.35764f;
        //float wOffset = 3.1003f;
        GameObject TileBase = GameObject.Find("TILEBASE");
        GameObject TileModel = TileBase.transform.GetChild(0).gameObject;
        Collider TileBounds = TileModel.GetComponent<Collider>();

        // FIX : DESHARDCODEAR

        // float vOffset = TileBounds.bounds.size.y * 2;
        // float hOffset = TileBounds.bounds.size.x * 2;

        // IMPLEMENTAR MATH RANDOM DE 4
        // ROTAR SOBRE SÍ MISMO EL MODELO PARA DAR SENSACIÓN DE MUCHOS
        // IMPLEMENTAR CICLAR ENTRE VARIOS MODELOS CON TEXTURA DIFERENTE

        float vOffset = 1f;
        float hOffset = 1f;

        int a = 14;
        int b = 14;

        TestTable = new GameObject[a, b];
        

        for (int f = 0; f < a; f++){

            for (int i = 0; i < b; i++){

                TestTable[f, i] = Instantiate(
                TileBase,
                new Vector3( (0 + i * hOffset), 0, (0-f * vOffset) ),
                Quaternion.identity,
                GameObject.Find("TILE_CONTAINER").transform
                );

                // Instanciar el script de cada uno de los tiles para almacenar su información (row/col)
                TileIdentifierNew tileIdentifier = TestTable[f, i].GetComponentInChildren<TileIdentifierNew>();
                tileIdentifier.SetPosition(f, i);

                // Darles nombres a los clones
                TestTable[f, i].name = "TileBase" + f.ToString() + "-" + i.ToString();

                // Pintar id de row/col en el tile (Canvas individual -> Text)
                Text ttt = TestTable[f,i].GetComponentInChildren<Text>();
                ttt.text = "a" + f.ToString()  + "-p" + i.ToString();
            
            } // FIN PRIMER FOR
        } // FIN SEGUNDO FOR

        // COLOCA EL TABLERO EN EL CENTRO
        GameObject.Find("TILE_CONTAINER").transform.position = new Vector3(-6.35f, 0, 6.73f);

    }


}
