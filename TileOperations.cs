using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOperations : MonoBehaviour
{
    public GameObject[] TilesSelected;
    GameObject origin;
    GameObject destination;
    int iCol, dCol, iRow, dRow;
    GameObject[] CursorTiles;

    public string InteractionLegal = "No Data";

    void Start()
    {

        //boardStatus = GameObject.Find("SceneController").GetComponent<BoardStatus>();


    }

    
    public bool EstaEnCruz(GameObject origin, GameObject destination){

                TileInfo originTi = origin.GetComponent<TileInfo>();
        TileInfo destiTi = destination.GetComponent<TileInfo>();

        int iRow = originTi.GetMainArray_Row();
        int iCol = originTi.GetSecArray_Col();

        int dRow = destiTi.GetMainArray_Row();
        int dCol = destiTi.GetSecArray_Col();

        bool estaEnCruz = ( (iRow - dRow == 1) && (iCol - dCol == 0) ||
                            (iRow - dRow == -1) && (iCol - dCol == 0) ||
                            (iRow - dRow == 0) && (iCol - dCol == 1) ||
                            (iRow - dRow == 0) && (iCol - dCol == -1)
                            );
        return estaEnCruz;
    }



    public bool EstaEnRecta(GameObject origin, GameObject destination){

        TileInfo originTi = origin.GetComponent<TileInfo>();
        TileInfo destiTi = destination.GetComponent<TileInfo>();

        int iRow = originTi.GetMainArray_Row();
        int iCol = originTi.GetSecArray_Col();

        int dRow = destiTi.GetMainArray_Row();
        int dCol = destiTi.GetSecArray_Col();

        bool estaEnRecta = false;
            //bool estaDerecha = iCol - dCol <= rango || iCol - dCol <= -rango;
        bool estaDerecha = iCol < dCol && (iRow == dRow);
        bool estaIzquierda = iCol > dCol && (iRow == dRow);
        bool estaArriba = iRow > dRow && (iCol == dCol);
        bool estaAbajo = iRow < dRow && (iCol == dCol);

        if (estaDerecha || estaIzquierda || estaArriba || estaAbajo) {
            estaEnRecta = true;
        }

        else {
            estaEnRecta = false;
        }
        return estaEnRecta;

    }

    // Comprueba si el tile es distinto y válido para construir
    public bool IsBuildingLegal(GameObject origin, GameObject destination){

        TileInfo originTi = origin.GetComponent<TileInfo>();
        TileInfo destiTi = destination.GetComponent<TileInfo>();

        int iRow = originTi.GetMainArray_Row();
        int iCol = originTi.GetSecArray_Col();

        int dRow = destiTi.GetMainArray_Row();
        int dCol = destiTi.GetSecArray_Col();

        bool estaEnRecta = false;
        //bool estaDerecha = iCol - dCol <= rango || iCol - dCol <= -rango;
        bool estaDerecha = iCol < dCol && (iRow == dRow);
        bool estaIzquierda = iCol > dCol && (iRow == dRow);
        bool estaArriba = iRow > dRow && (iCol == dCol);
        bool estaAbajo = iRow < dRow && (iCol == dCol);

        if (estaDerecha || estaIzquierda || estaArriba || estaAbajo) {
            
            estaEnRecta = true;
            InteractionLegal = "Int Legal";
        }

        else {
            estaEnRecta = false;
            InteractionLegal = "Int Legal";
        }

        InteractionLegal = "Int Legal";
        return estaEnRecta;


    }



    // Start is called before the first frame update



    // Comprueba si los edificios se pueden construir
    void ArrayChecker(){

        // Checkea si el tile clicado distinto al original y su relación
        // Si hay un tile seleccionado
        // Si el siguiente está en cruz, se añade al array
        // Máximo 4

    }

    // Update is called once per frame
    void Update()
    {
    }
}
