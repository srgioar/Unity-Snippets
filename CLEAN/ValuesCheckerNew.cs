using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesCheckerNew
{

    GameObject origin;
    GameObject destination;

    GameObject[] positions;

    public int iCol, dCol, iRow, dRow;

    public ValuesCheckerNew(GameObject origin, GameObject destination){
        
        this.origin = origin;
        this.destination = destination;
        this.iCol = origin.GetComponentInChildren<TileIdentifierNew>().secondaryarray_col;
        this.dCol = destination.GetComponentInChildren<TileIdentifierNew>().secondaryarray_col;
        this.iRow = origin.GetComponentInChildren<TileIdentifierNew>().mainarray_row;
        this.dRow = destination.GetComponentInChildren<TileIdentifierNew>().mainarray_row;
        //this.rango = origin.GetComponent<CharIdentifier>().ran
    }

    public bool EstaEnRango(int rango){
        bool estaEnRango = (iRow - dRow <= rango && iRow - dRow >= -rango) && (iCol - dCol <= rango && iCol - dCol >= -rango);
        return estaEnRango;
    }

    public bool SonIguales(){

        bool estaQuieto = iCol == dCol && iRow == dRow;
        return estaQuieto;
    }

    public bool EstaEnRecta(){

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

    public bool EstaEnDiagonal(){

        bool estaEnDiagonal = false;
        bool estaDiagonalAbajoDer = (iCol < dCol && iRow < dRow) && (iCol - dCol == iRow - dRow); 
        bool estaDiagonalArribaDer = (iCol < dCol && iRow > dRow) && (iRow - dRow == -(iCol - dCol));
        //Debug.Log((iRow - dRow == +(iCol - dCol)));
        bool estaDiagonalAbajoIzq = (iCol > dCol && iRow < dRow)  && (iRow - dRow == -(iCol - dCol));
        bool estaDiagonalArribaIzq = (iCol > dCol && iRow > dRow) && (iCol - dCol == iRow - dRow);

        estaEnDiagonal = (estaDiagonalArribaIzq || estaDiagonalArribaDer || estaDiagonalAbajoIzq || estaDiagonalAbajoDer);

        return estaEnDiagonal;

    }

    public bool EstaEnArea(int iCol, int dCol, int iRow, int dRow){
            bool estaEnArea = (iCol != dCol && iRow != dRow);
            return estaEnArea;
    }

    public bool EstaEnCruz(){
        bool estaEnCruz = ( (iRow - dRow == 1) && (iCol - dCol == 0) ||
                            (iRow - dRow == -1) && (iCol - dCol == 0) ||
                            (iRow - dRow == 0) && (iCol - dCol == 1) ||
                            (iRow - dRow == 0) && (iCol - dCol == -1)
                            );
        return estaEnCruz;
    }

    //, int dRow, int iCol, int dCol, int rango;

     // No comprueba realmente si está en diagonal, comprueba la dirección general

    public string ReturnNextTileDirection(){

    string relativePosition = "";

    // Modificarse para mejorar pathfinding!
    //bool estaEnDiagonal = (iCol != dCol && iRow != dRow);
    bool estaEnArea = (iCol != dCol && iRow != dRow);

    //bool estaDerecha = iCol - dCol <= rango || iCol - dCol <= -rango;
    bool estaDerecha = iCol < dCol && (iRow == dRow);
    bool estaIzquierda = iCol > dCol && (iRow == dRow);
    bool estaArriba = iRow > dRow && (iCol == dCol);
    bool estaAbajo = iRow < dRow && (iCol == dCol);
    // Nota: La segnda comparación compara si los dígitos son iguales o son iguales con signo distinto (o diferentes)
    // Las posiciones en diagonal comparten dígito (Col y Row es igual) pero tienen distinto signo
    // Es diagonal si mueven la misma cantidad de tiles (uno ancho uno alto);

    bool estaDiagonalAbajoDer = (iCol < dCol && iRow < dRow) && (iCol - dCol == iRow - dRow); 
    bool estaDiagonalArribaDer = (iCol < dCol && iRow > dRow) && (-(iRow - dRow) == (iCol - dCol));
    //Debug.Log((iRow - dRow == +(iCol - dCol)));
    bool estaDiagonalAbajoIzq = (iCol > dCol && iRow < dRow)  && (iRow - dRow == -(iCol - dCol));
    bool estaDiagonalArribaIzq = (iCol > dCol && iRow > dRow) && (iCol - dCol == iRow - dRow);

    //estaEnRango = ((iRow - dRow <= rango || iRow - dRow <= -rango) && (iCol - dCol <= rango || iCol - dCol >= -rango));
    //bool estaEnRango = ((iRow - dRow <= rango) || (iRow - dRow == rango) || (iRow - dRow <= -rango));
    bool estaQuieto = iCol == dCol && iRow == dRow;
    //bool estaEnRango = (iRow - dRow <= rango && iRow - dRow >= -rango) && (iCol - dCol <= rango && iCol - dCol >= -rango);
    bool estaEnCruz = ((iRow - dRow == 1 || iRow - dRow == -1) && (iCol - dCol == 1 || iCol - dCol == -1));
    
    
    if (estaDerecha) {
        return "horizontal_derecha";
    }

    else if (estaIzquierda) {
        return "horizontal_izquierda";
    }

    else if (estaArriba){
        return "vertical_arriba";
    }

    else if (estaAbajo){
        return "vertical_abajo";
    }

    else if (estaDiagonalArribaIzq) {
        return "diagonal_arriba_izquierda";
    }

    else if (estaDiagonalAbajoIzq){
        return "diagonal_abajo_izquierda";
    }

    else if (estaDiagonalArribaDer){
        return "diagonal_arriba_derecha";
    }

    else if (estaDiagonalAbajoDer){
        return "diagonal_abajo_derecha";

    }

    // else{
    //     return "Ninguna dirección";
    // }
     
     // HAY QUE DIFERENCIAR ENTRE POSICIONES TARGETEABLES Y POSICIONES MOVIBLES
        // P.E SE PUEDE CASTEAR DENTRO DE RANGO AUNQUE NO HAYA LINEA RECTA O DIAGONAL DE XPOSICIONES (MOV. EN ELE)

        //p.e estaEnRangoMovimiento estaEnRangoCasteo!!
        /*
        if (!estaEnDiagonal) {
            estaEnRango = (iRow - dRow <= rango && iRow - dRow >= -rango) && (iCol - dCol <= rango && iCol - dCol >= -rango);
        }

        else {
            estaEnRango = (iRow - dRow <= rango-1 && iRow - dRow >= -rango+1) && (iCol - dCol <= rango-1 && iCol - dCol >= -rango+1);
        }
        */

         return relativePosition;

     }

     // Hacer un loop entre todos los tiles objetivos y devolver si la posición es posible
    public bool CheckIfPathToTileIsFree(){

        // Comprobación necesaria solo para movimientos y ataques. Las skills se puden tirar en cualquier tile vacío.

        TileIdentifierNew selectedTileInfo = origin.GetComponentInChildren<TileIdentifierNew>();
        TileIdentifierNew targetTileInfo = destination.GetComponentInChildren<TileIdentifierNew>();
        GameObject[,] TestTable = GameObject.Find("TILE_CONTAINER").GetComponent<BoardCreator>().TestTable;

        // Calcular
        ValuesCheckerNew vc = new ValuesCheckerNew(origin, destination);
        string direction = vc.ReturnNextTileDirection();
        bool controlEmpty = false;

        if (direction == "vertical_abajo") {

            int rangoInterior = dRow-iRow;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow+1, iCol];
                controlEmpty = true;
            }

            else {

                for (int a = 1; a < rangoInterior; a++) {
                    if (TestTable[iRow + a, iCol].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                        arrayTiles[a-1] = TestTable[iRow+a, iCol];
                        controlEmpty = true;
                    }

                    else {
                        controlEmpty = false;
                    }
                } // Fin del for

            }

            if (controlEmpty) {
                this.positions = arrayTiles;
                //Debug.Log(positions.Length.ToString() + this.positions[0]);
                //Text debugArraytext = GameObject.Find("POSITIONS0").GetComponent<Text>();
                //debugArraytext.text = this.positions[0].ToString();
                
            }

        }

        else if (direction == "vertical_arriba") {

            int rangoInterior = iRow - dRow;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow-1, iCol];
                controlEmpty = true;
            }

            else {

                for (int a = 1; a < rangoInterior+1; a++) {
                if (TestTable[iRow-a, iCol].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow-a, iCol];
                    controlEmpty = true;
                }

                else {
                    controlEmpty = false;
                }
                } // Fin del for

            }

            if (controlEmpty) {
                this.positions = arrayTiles;
                Debug.Log(positions.Length.ToString() + this.positions[0]);
                //Text debugArraytext = GameObject.Find("POSITIONS0").GetComponent<Text>();
                //debugArraytext.text = this.positions[1].ToString();

            }

        }

        else if (direction == "horizontal_izquierda") {

            int rangoInterior = iCol - dCol;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow, iCol-1];
                controlEmpty = true;
            }

            else {

                for (int a = 1; a < rangoInterior; a++) {
                    if (TestTable[iRow, iCol-a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow, iCol-a];
                    controlEmpty = true;
                    }

                    else {
                            controlEmpty = false;
                        }
                    } // Fin del for

            }

            if (controlEmpty) {
            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[0]);
            }
        }

        else if (direction == "horizontal_derecha") {

            int rangoInterior = dCol - iCol;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow, iCol+1];
                controlEmpty = true;
            }

            else {

                for (int a = 1; a < rangoInterior; a++) {
                if (TestTable[iRow, iCol+a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow, iCol+a];
                    controlEmpty = true;
                }

                else {
                    controlEmpty = false;
                }
            } // Fin del for

            }

            if (controlEmpty) {
            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[0]);

            }

        }


        else if (direction == "diagonal_arriba_izquierda") {

            int tilesDiferencia = iRow - dRow;
            GameObject[] arrayTiles = new GameObject[tilesDiferencia+1];

            if (tilesDiferencia < 2 && tilesDiferencia > 0) {
                arrayTiles[0] = TestTable[iRow-1, iCol-1];
                controlEmpty = true;
            }

            else {
    
                for (int a = 1; a < tilesDiferencia+1; a++) {
                    if (TestTable[iRow-a, iCol-a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                        arrayTiles[a] = TestTable[iRow-a, iCol-a];
                        controlEmpty = true;
                    }

                    else {
                        controlEmpty = false;
                    }
                } // Fin del for

            }

            if (controlEmpty) {

            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[0]);
            }

        }

        else if (direction == "diagonal_abajo_izquierda") {

            int rangoInterior = iCol - dCol;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow+1, iCol-1];
                controlEmpty = true;
            }

            else{

                for (int a = 1; a < rangoInterior; a++) {
                if (TestTable[iRow+a, iCol-a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow+a, iCol-a];
                    controlEmpty = true;
                }

                else {
                    controlEmpty = false;
                }
            } // Fin del for

            }

            if (controlEmpty) {
            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[0]);

            }

        }

        else if (direction == "diagonal_arriba_derecha") {

            int rangoInterior = iRow - dRow;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow-1, iCol+1];
                controlEmpty = true;
            }

            else{

                for (int a = 1; a < rangoInterior; a++) {
                if (TestTable[iRow-a, iCol+a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow-a, iCol+a];
                    controlEmpty = true;
                }

                else {
                    controlEmpty = false;
                }
            } // Fin del for

            }

            if (controlEmpty) {
            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[0]);

            }

        }

        else if (direction == "diagonal_abajo_derecha") {

            int rangoInterior = -(iCol - dCol);
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow+1, iCol+1];
                controlEmpty = true;
            }

            else {

                for (int a = 1; a < rangoInterior; a++) {
                if (TestTable[iRow+a, iCol+a].GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null) {
                    arrayTiles[a] = TestTable[iRow+a, iCol+a];
                    controlEmpty = true;
                }

                else {
                    controlEmpty = false;
                }
            } // Fin del for

            }

            if (controlEmpty) {
            this.positions = arrayTiles;
            //Debug.Log(positions.Length.ToString() + this.positions[1].ToString());
            }

        }

            Debug.Log("DIR: " + direction);
            Debug.Log("Control empty: " + controlEmpty.ToString());
            return controlEmpty;
            
    }

    public GameObject[] ReturnTargetTiles(){
        if (positions != null) {
            Debug.Log("Positions" + positions.Length);
        }

        else {
            Debug.Log("Positions array null!!");
        }
        
        return this.positions;
        
    }


   
    
    public void BoolCheckerTester(){

        //testeoARRIBA_IZQUIERDA = ()

        int arribaDer = (1) + (-1);
        int abajoDer = (-1) + (-1);

        int arribaIzq = (-1) + (1);
        int abajoIzq = (1) + (1);

        Debug.Log("Arriba Derecha: " + arribaDer.ToString());
                Debug.Log("Abajo Derecha: " + abajoDer.ToString());
                        Debug.Log("Arriba Izquierda: " + arribaIzq.ToString());
                                Debug.Log("Abajo Izquierda: " + abajoIzq.ToString());


        // (-1) + (-1)

        // (-1) + (1)

        // (1) + (1)
    }


}
