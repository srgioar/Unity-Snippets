using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;


public class BoardCreator3D : MonoBehaviour
{

    Object[] TilesGreenTextures;
    Object[] TilesBlueTextures;

    float vOffset = 1f;
    float hOffset = 1f;
    float zOffset = 0.22f;
    float zOffset2 = 0.75f;
    int a, b, c;
    public GameObject[,,] TestTable;
    GameObject TileBase;
    GameObject BigTile;
    Collider TileBounds;

    // LISTA DE TEXTURAS - 64 
    public List<Texture> TextureList;

    // Tablero en miniatura de tres dimensiones para testeo
    // Tres dimensiones habituales, piezas de profundidad nivel 3 tiles
    // Contenido en un gameobject insertado en TileInfo
    // public void InstantiateBasic3Board(){

    //     a = 8; b = 8; c = 3;

    //     TestTable = new GameObject[a, b, c];

    //     if (GameObject.Find("TileContainer") != null) {
    //         GameObject dgo = GameObject.Find("TileContainer");
    //         Destroy(dgo);
    //     }

    //     GameObject TileContainer = new GameObject();
    //     TileContainer.name = "TileContainer";
    //     TileContainer.transform.SetParent(GameObject.Find("BoardPivot").transform);
    //     TileContainer.transform.localPosition = new Vector3(-2.4f, 0f, 10f);

    //     // F, I, X
    //     for (int f = 0; f < a; f++){

    //         for (int i = 0; i < b; i++){

    //             for (int x = 0; x < c; x++) {

    //             TestTable[f, i, x] = Instantiate(
    //             TileBase,
    //             new Vector3( (0 + i * hOffset), (0 + x * zOffset) , (0-f * vOffset) ),
    //             Quaternion.identity,
    //             TileContainer.transform

    //             );

    //             NavMeshSurface navMeshSurface = TestTable[f,i,x].transform.GetChild(2).gameObject.GetComponent<NavMeshSurface>();

    //             // Instanciar el script de cada uno de los tiles para almacenar su información (row/col)
    //             TileInfo tileIdentifier = TestTable[f, i, x].GetComponent<TileInfo>();
    //             tileIdentifier.SetPosition3D(f, i, x);

    //             // SI ES EL TILE PILAR BASE
    //             if (x == 0) {

    //                 int num = Random.Range(2, 8);

    //             if (num <= 4) {

    //                 tileIdentifier.SetAsElement(TileInfo.ElementType.Water);
    //                 tileIdentifier.SetAsNotWalkable();
    //                 //navMeshSurface.enabled = false;
    //                 //tileIdentifier.GetComponent<NavMeshSurface>().enabled = false;
    //             }

    //                 else if (num > 4) {

    //                 tileIdentifier.SetAsElement(TileInfo.ElementType.ClearWater);
    //                 tileIdentifier.SetAsWalkable();
                   
    //                 }

    //             }

    //             else if (x == 1) {


    //                 if (TestTable[f, i, 0].GetComponent<TileInfo>().IsAvailable()){

    //                     tileIdentifier.SetAsElement(TileInfo.ElementType.Sand);
                        

    //                 }

    //                 else if (!TestTable[f, i, 0].GetComponent<TileInfo>().IsAvailable()) {

    //                     tileIdentifier.SetAsElement(TileInfo.ElementType.Sand);
    //                     tileIdentifier.SetAsNotWalkable();
    //                     //navMeshSurface.enabled = false;

    //                 }

    //             }

    //             else if (x == 2) {

    //                 if (TestTable[f, i, 0].GetComponent<TileInfo>().IsAvailable()){

    //                     tileIdentifier.SetAsElement(TileInfo.ElementType.Grass);
    //                     navMeshSurface.enabled = true;

    //                 }

    //                 else if (!TestTable[f, i, 0].GetComponent<TileInfo>().IsAvailable()) {

    //                     tileIdentifier.SetAsElement(TileInfo.ElementType.Stone);
    //                     tileIdentifier.SetAsNotWalkable();
                        

    //                 }

    //             }
            
    //             // Darles nombres a los clones
    //             TestTable[f, i, x].name = "Tile" + f.ToString()  + "-" + i.ToString() + "-l" + x.ToString();

    //             // Pintar id de row/col en el tile (Canvas individual -> Text)
    //             Text ttt = TestTable[f,i,x].GetComponentInChildren<Text>();
    //             ttt.text = "a" + f.ToString()  + "-p" + i.ToString() + "-l" + x.ToString();


    //             } // FIN TERCER FOR

 
            
    //         } // FIN PRIMER FOR
    //     } // FIN SEGUNDO FOR

    //     GameObject.Find("BoardPivot").transform.position = new Vector3(-3.87f,0f,3.11f);

    // }

    public void InstantiateBoardImproved(){

        a = 8; b = 8; c = 3;

        TestTable = new GameObject[a, b, c];

        if (GameObject.Find("TileContainer") != null) {
            GameObject dgo = GameObject.Find("TileContainer");
            Destroy(dgo);
        }

        GameObject TileContainer = new GameObject();
        TileContainer.name = "TileContainer";
        TileContainer.transform.SetParent(GameObject.Find("BoardPivot").transform);
        TileContainer.transform.localPosition = new Vector3(-2.4f, 0f, 10f);

        // F, I, X
        for (int f = 0; f < a; f++){

            for (int i = 0; i < b; i++){

                for (int x = 0; x < c; x++) {

                TestTable[f, i, x] = Instantiate(
                Resources.Load("Tiles/BaseTile") as GameObject,
                new Vector3( (0 + i * hOffset), (0 + x * zOffset) , (0-f * vOffset) ),
                Quaternion.identity,
                TileContainer.transform

                );

                NavMeshSurface navMeshSurface = TestTable[f,i,x].transform.GetChild(2).gameObject.GetComponent<NavMeshSurface>();

                // Instanciar el script de cada uno de los tiles para almacenar su información (row/col)
                TileInfo tileIdentifier = TestTable[f, i, x].GetComponent<TileInfo>();
                tileIdentifier.SetPosition3D(f, i, x);

                if (x == 0) {

                    tileIdentifier.SetAsElement(TileInfo.ElementType.Water);
                    tileIdentifier.SetAsNotWalkable();


                }

                else if (x == 1) {

                    tileIdentifier.SetAsElement(TileInfo.ElementType.Ground);
                    tileIdentifier.SetAsNotWalkable();


                }

                else if (x == 2) {

                    int num = Random.Range(2, 8);

                if (num <= 2) {
                    tileIdentifier.SetAsElement(TileInfo.ElementType.None);
                    tileIdentifier.SetAsNotWalkable();
                }

                    else if (num > 2) {
                    tileIdentifier.SetAsElement(TileInfo.ElementType.Grass);
                    tileIdentifier.SetAsWalkable();
                    navMeshSurface.enabled = true;
                    }

                }

                // Darles nombres a los clones
                TestTable[f, i, x].name = "Tile" + f.ToString()  + "-" + i.ToString() + "-l" + x.ToString();

                // Pintar id de row/col en el tile (Canvas individual -> Text)
                Text ttt = TestTable[f,i,x].GetComponentInChildren<Text>();
                ttt.text = "a" + f.ToString()  + "-p" + i.ToString() + "-l" + x.ToString();


                } // FIN TERCER FOR

 
            
            } // FIN PRIMER FOR
        } // FIN SEGUNDO FOR

        GameObject.Find("BoardPivot").transform.position = new Vector3(-3.87f,0f,3.11f);

    }

    void RandomizeTileTextures(GameObject go){
        // CARGAR LAS 64 TEXTURAS POSIBLES
        // MATH RANDOM 64

        //Object[] textures;

        Texture texture = (Texture)TilesGreenTextures[Random.Range(0, TilesGreenTextures.Length)];
        go.GetComponent<Renderer>().material.mainTexture = texture;

    }

    // Experimento con array de tres dimensiones
    // Start is called before the first frame update
    void Start()
    {
        TileBase = Resources.Load("Tiles/BaseTile") as GameObject;
        BigTile = GameObject.Find("BigTile");
        TileBounds = TileBase.GetComponent<Collider>();
        TilesGreenTextures = Resources.LoadAll("Textures/Tiles/testslice-green", typeof(Texture));

        //InstantiateBasic3Board();
        InstantiateBoardImproved();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
