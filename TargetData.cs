using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData
{
    public GameObject[] positions;
    public int typeInteraction;
    public bool isMovementLegal;
    public bool isAttackLegal;
    public bool isInteractionLegal;

    public bool isOnRange, isOnSamePlace;
    public int movRange; // Cantidad de posiciones desde el clic actual

    public TargetData(GameObject[] positions, int typeInteraction, bool isMovementLegal){
        this.positions = positions;
        this.typeInteraction = typeInteraction;
        this.isMovementLegal = isMovementLegal;
    }

    public bool CheckRango(GameObject inicial, GameObject final, int rango){

        TileIdentifier tileInicial = inicial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = final.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();


        bool estaEnRango = ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rango);
        return estaEnRango;

    }

    // Por chequear que funcione:

    public bool CheckArea(GameObject inicial, GameObject final, int rango){

        TileIdentifier tileInicial = inicial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = final.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();


        bool estaEnArea = ValuesChecker.EstaEnArea(iRow, dRow, iCol, dCol);
        return estaEnArea;

    }

    public bool CheckEstaEnCruz(GameObject inicial, GameObject final){

        TileIdentifier tileInicial = inicial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = final.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();


        bool estaEnCruz = ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol);
        return estaEnCruz;

    }

    public bool CheckEstaEnRecta(GameObject inicial, GameObject final){

        TileIdentifier tileInicial = inicial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = final.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();


        bool estaEnRecta = ValuesChecker.EstaEnRecta(iRow, dRow, iCol, dCol);
        return estaEnRecta;

    }

    public bool CheckDiagonal(GameObject inicial, GameObject final){

        TileIdentifier tileInicial = inicial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = final.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();


        bool estaEnDiagonal = ValuesChecker.EstaEnDiagonal(iRow, dRow, iCol, dCol);
        return estaEnDiagonal;

    }

    public bool CheckIfAttackValid(GameObject final){
        bool f = final.GetComponent<TileIdentifier>().tileHasEnemy;
        return f;
    }

    public bool CheckifMovValid(GameObject final){
        bool f = final.GetComponent<TileIdentifier>().tileIsEmpty;
        return f;
    }
    

    public GameObject[] SenderTargetTiles(GameObject[,] board, GameObject initial, GameObject target, int rango) {

        // Se importará el script de pj relativo al rango
        // REFINAR RANGO!

        GameObject[] tilesPacket = new GameObject[5];
        
        TileIdentifier tileInicial = initial.transform.gameObject.GetComponent<TileIdentifier>();
        TileIdentifier tileFinal = target.transform.gameObject.GetComponent<TileIdentifier>();

        int iRow = tileInicial.GetMainArray_Row();
        int iCol = tileInicial.GetSecArray_Col();

        int dRow = tileFinal.GetMainArray_Row();
        int dCol = tileFinal.GetSecArray_Col();

        string so = ValuesChecker.ReturnNextTileDirection(iRow, dRow, iCol, dCol);
        Debug.Log(so);
        //ReturnNextTileDirection(int iCol, int dCol, int iRow, int dRow, int rango)

        if (so == "vertical_arriba") {

        }

        else if (so == "vertical_abajo") {

            //Debug.Log("Llega aqui");
            int rangoInterior = dRow - iRow;
            tilesPacket = new GameObject[rangoInterior+1];

            for (int a = 0; a <= rangoInterior; a++) {
                tilesPacket[a] = board[iRow+a, iCol];
                Debug.Log("tp: " + tilesPacket[a]);
            }

            //Debug.Log("TP Test " + tilesPacket[0]);
            Debug.Log("Tras bucle..." + tilesPacket[0].ToString());
            return tilesPacket;
            
        }

        else if (so == "horizontal_izquierda") {

        }

        else if (so == "horizontal_derecha") {

        }

        else if (so == "diagonal_arriba_izquierda") {

        }

        else if (so == "diagonal_abajo_izquierda") {

        }

        else if (so == "diagonal_arriba_izquierda") {

        }

        else if (so == "diagonal_abajo_derecha") {

        }


        return tilesPacket;
    }

}
