using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableChecker : MonoBehaviour
{

    // Posición inicial del ply object
    Vector3 initialPosition;

    // Tile desde el que se empieza
    GameObject initialTile;

    // Último tile seleccionado
    GameObject lastSelectedTile;

    // ?????
    GameObject finalTile;

    public int a = 6;
    public int b = 6;

    public Text currentSelectedInfoText;
    public Text currentInspectingInfoText;

    public Material clickMaterial;

    public Material selectedMaterial;

    //public Material 
    public Material hoverMaterial;

    public Material busyMaterial;

    public Material basicMaterial;
    public GameObject playerObj;

     /* 
            0   1   2   3   4   5

        0   X   X   X   X   X   X

        1   X   X   X   X   X   X

        2   X   X   X   X   X   X

        3   X   X   X   X   X   X

        4   X   X   X   X   X   X

        5   X   X   X   X   X   X

     */

    [SerializeField]
   // public GameObject[] MainTable;

    public int[,] VirtualTable = new int[6,6];

    [SerializeField]
    public GameObject[,] TestTable;

    // Start is called before the first frame update
    void Start()
    {
        Instanciador();
    }

    void Girador(){
    }

    void Detarget(GameObject tile){
        tile.GetComponent<TileIdentifier>().SetTargeted(false);
    }

    void MarkAsTargeted(GameObject tile){
        tile.GetComponent<TileIdentifier>().SetTargeted(true);
    }

    void CalculadorCaminoDisponible(GameObject initial, GameObject target){

        TileIdentifier tileInicial = initial.transform.gameObject.GetComponent<TileIdentifier>();
        Debug.Log("TILE INICIAL POS: " + tileInicial.GetMainArray_Row() + "-" + tileInicial.GetSecArray_Col());

        TileIdentifier tileFinal = target.transform.gameObject.GetComponent<TileIdentifier>();
        Debug.Log("TILE DESTINO POS: " + tileFinal.GetMainArray_Row() + "-" + tileFinal.GetSecArray_Col());
        bool tileLibre = tileFinal.GetAvailable();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();
        
        Debug.Log(iRow - dRow);
        Debug.Log(iCol - dCol);
        Debug.Log("¿Tile Libre? " + tileLibre);

        bool movValido = (iRow - dRow == 1 || iRow - dRow == -1 || iRow - dRow == 0)  &&  (iCol - dCol == 1 || iCol - dCol == -1 || iCol - dCol == 0);

        bool movAbajo = iRow - dRow == -1;
        bool movArriba = iRow - dRow == 1;
        bool movIzquierda = iCol - dCol == 1;
        bool movDerecha = iCol - dCol == -1;

        bool movDiaIzqArriba = movArriba & movIzquierda;
        bool movDiaIzqAbajo = movAbajo & movIzquierda;
        bool movDiaDerArriba = movArriba & movDerecha;
        bool movDiaDerAbajo = movAbajo & movDerecha;

        if (movValido & tileLibre)  {
            Debug.Log("Movimiento VALIDO");

            if (movIzquierda) {

                playerObj.transform.eulerAngles = new Vector3(0, 180, 0);
                playerObj.transform.position = target.transform.position + new Vector3(0, 0.474f, 0);
                initialTile.GetComponent<TileIdentifier>().SetSelected(false);
                initialTile = TestTable[dRow, dCol];
                initialTile.GetComponent<TileIdentifier>().SetSelected(true);
                Detarget(target);
                Detarget(finalTile);
                

            }

            if (movDerecha) {

                playerObj.transform.eulerAngles = new Vector3(0, 0, 0);
                playerObj.transform.position = target.transform.position + new Vector3(0, 0.474f, 0);
                initialTile.GetComponent<TileIdentifier>().SetSelected(false);
                initialTile = TestTable[dRow, dCol];
                initialTile.GetComponent<TileIdentifier>().SetSelected(true);
                Detarget(target);
                Detarget(finalTile);
                                

            }

            if (movArriba) {

                playerObj.transform.eulerAngles = new Vector3(0, -90, 0);
                playerObj.transform.position = target.transform.position + new Vector3(0, 0.474f, 0);
                initialTile.GetComponent<TileIdentifier>().SetSelected(false);
                initialTile = TestTable[dRow, dCol];
                initialTile.GetComponent<TileIdentifier>().SetSelected(true);
                Detarget(target);
                Detarget(finalTile);
                                

            }

            if (movAbajo) {

                playerObj.transform.eulerAngles = new Vector3(0, 90, 0);
                playerObj.transform.position = target.transform.position + new Vector3(0, 0.474f, 0);
                initialTile.GetComponent<TileIdentifier>().SetSelected(false);
                initialTile = TestTable[dRow, dCol];
                initialTile.GetComponent<TileIdentifier>().SetSelected(true);
                Detarget(target);
                                Detarget(finalTile);
                                

            }

        }

        else {
            Debug.Log("Movimiento INVALIDO");
        }

        if (movAbajo) {
            Debug.Log("Moviendo abajo");
        }

        else if (movArriba) {
            Debug.Log("Moviendo arriba");
        }

        else if (movIzquierda) {
            Debug.Log("Moviendo izquierda");
        }

        else if (movDerecha) {
            Debug.Log("Moviendo derecha");
        }

        if (movDiaIzqArriba) {
            Debug.Log("Moviendo movDiaIzqArriba");
        }


        // Si entre la pos destino y origen las dos posiciones no tienen uno de diferencia entre ellos, movimiento valido
        // comprobar tambien si el cuadro está libre



        // DE COLUMNA 6 FILA 4    A     COLUMNA 4 FILA 5

        // MOVIMIENTO HIPERSIMPLE
        // SI HAY MENOS DE DOS TILES DE DIFERENCIA
        // MOVIMIENTO PERMITIDO


    }

    void CheckColorearTiles(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        for (int f = 0; f < a; f++){

        for (int i = 0; i < b; i++){
            
        TileIdentifier ti = TestTable[f, i].GetComponent<TileIdentifier>();
        bool tileStatus = ti.GetAvailable();
        if (!tileStatus){
            TestTable[f, i].GetComponent<MeshRenderer>().material = busyMaterial; 
        }

        else if (ti.GetSelected()) {
            TestTable[f, i].GetComponent<MeshRenderer>().material = selectedMaterial;
        }

        else if (ti.GetTargeted()) {
            TestTable[f, i].GetComponent<MeshRenderer>().material = clickMaterial;
        }

        else if (Physics.Raycast(ray, out hit)) {
        Transform objectHit = hit.transform;
        if (objectHit.tag == "basecube") {
                MeshRenderer meshRenderer = objectHit.GetComponent<MeshRenderer>();
                meshRenderer.material = hoverMaterial;
        }
        }

        else {
            TestTable[f, i].GetComponent<MeshRenderer>().material = basicMaterial; 
        }    
         
        } // Final primer for

        } // Final segundo for

    }

    void InteractuarConTiles(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //GameObject tagged;
        
        if (Physics.Raycast(ray, out hit)) {
        Transform objectHit = hit.transform;
        if (objectHit.tag == "basecube") {
                MeshRenderer meshRenderer = objectHit.GetComponent<MeshRenderer>();
                meshRenderer.material = hoverMaterial;
        }
        }

        else {
                //hit.transform.GetComponent<MeshRenderer>().material = basicMaterial;

        }

        if (Input.GetMouseButtonDown(0)) {

            if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "basecube") {
                //Detarget(lastSelectedTile);
                //Debug.Log(objectHit.GetComponentInChildren<Text>().text);
                finalTile = hit.transform.gameObject;
                Detarget(finalTile);
                TileIdentifier tile = hit.transform.gameObject.GetComponent<TileIdentifier>();
                //Debug.Log(tile.GetPositionInfo());
                CalculadorCaminoDisponible(initialTile, finalTile);
                //objectHit.GetComponent<MeshRenderer>().material = clickMaterial;
                tile.SetTargeted(true);
                lastSelectedTile = objectHit.gameObject;
               
        }
            }
        }


        if (Input.GetMouseButton(0)) {

            if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "basecube") {
                
                finalTile = hit.transform.gameObject;
                Vector3 playerPosition = playerObj.transform.position;
                Vector3 targetPosition = objectHit.transform.position;
                
                float dist = Vector3.Distance(playerPosition, targetPosition);
                TileIdentifier tile = hit.transform.gameObject.GetComponent<TileIdentifier>();
                
                //playerObj.transform.position += dist * Time.deltaTime;     
            }
            
            
            // Do something with the object that was hit by the raycast.
            }
        }

        if (Input.GetMouseButton(1)) {
            //Detarget(tile.target);
        }
       
    }

    void Instanciador(){

        TestTable = new GameObject[a, b];

        for (int f = 0; f < a; f++){

        for (int i = 0; i < b; i++){

            TestTable[f, i] = Instantiate(GameObject.FindGameObjectWithTag("basecube"), new Vector3((0 + i),0,(0-f)), Quaternion.identity, GameObject.Find("CONTAINER").transform);
            TileIdentifier tileIdentifier = TestTable[f, i].GetComponentInChildren<TileIdentifier>();
            tileIdentifier.SetPosition(f, i);
            //GameObject test = Instantiate(GameObject.FindGameObjectWithTag("basecube"), new Vector3((0 + i),0,(0+f)), Quaternion.identity);
            Text ttt = TestTable[f,i].GetComponentInChildren<Text>();
            TestTable[f, i].name = "TileBase" + f.ToString() + "-" + i.ToString();
            ttt.text = "a" + f.ToString()  + "-p" + i.ToString();
            
        }
        }

        GameObject.Find("CONTAINER").transform.position = new Vector3(-3.5f, 0, 3.5f);
        playerObj.transform.position = TestTable[5,3].transform.position + new Vector3(0, 0.5f, 0);
        initialPosition = TestTable[5,3].transform.position;
        initialTile = TestTable[5,3];
        initialTile.GetComponent<TileIdentifier>().SetSelected(true);
        finalTile = initialTile;
        lastSelectedTile = null;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InteractuarConTiles();
        CheckColorearTiles();
    }

    void Update(){

    }
}
