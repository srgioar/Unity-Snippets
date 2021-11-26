using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetDataHolder{

    public GameObject[] positions;

    public GameObject originTile, destinationTile;

    public bool isOnRange;

    public int typeOfInteraction;

    // Setear interaction legal dependiendo de si es mayor a inspect!

    // Obtener las posiciones
    // Si la posición es válida, habilitar interactuar (UI)


    // Al cambiar el cursor de posición entre origen y destino, se setean los datos nuevos del objeto

    public TargetDataHolder(GameObject originTile, GameObject destinationTile){

        this.originTile = originTile;
        this.destinationTile = destinationTile;
    }

    // Setear la interacción si es válida y si es válida cargar el array con gameobjects
    // Por defecto todos los tiles requieren interacción (excepto tiles indisponibles -rotos-)

    // Coge un gameobject y setea el tipo de interacción del target cursor
    public int GetTypeOfInteraction(int rangoMelee, int rangoDistancia, int rangoMovimiento, int rangoDiagonal){

        //Text UI_interaction = GameObject.Find("TYPEOFINTERACTION").GetComponent<Text>();

        TileIdentifier selectedTileInfo = this.originTile.GetComponent<TileIdentifier>();
        TileIdentifier targetTileInfo = this.destinationTile.GetComponent<TileIdentifier>();

        int iRow = selectedTileInfo.GetMainArray_Row();
        int iCol = selectedTileInfo.GetSecArray_Col();

        int dRow = targetTileInfo.GetMainArray_Row();
        int dCol = targetTileInfo.GetSecArray_Col();

        // IMPORTANTE DIFERENCIAR SI ESTÁ EN CRUZ O ESTÁ EN MELEE!!!

        // Tile inválido
        if (targetTileInfo.tileIsEmpty & !ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMovimiento) & !ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDistancia)) {
            this.typeOfInteraction = 0;
        }

        // Tile está a distancia pero no cumple ninguna otra condición
        if (ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDistancia)){
            this.typeOfInteraction = 6;
        }

        // Tile apto para MOVIMIENTO
        if (targetTileInfo.tileIsEmpty & ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMovimiento) & ValuesChecker.EstaEnRecta(iRow, dRow, iCol, dCol)) {
            this.typeOfInteraction = 1;
        }

        // Tile apto ATAQUE DISTANCIA
        if (targetTileInfo.tileHasEnemy & ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDistancia) & !ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol)) {
            this.typeOfInteraction = 3;
        }

        //Tile apto pickear OBJETO
        if (targetTileInfo.tileHasObject & ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMovimiento) & ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol)) {
            this.typeOfInteraction = 4;
        }

        // Tile apto movimiento tipo DIAGONAL
        if (targetTileInfo.tileIsEmpty & ValuesChecker.EstaEnDiagonal(iRow, dRow, iCol, dCol) & ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDiagonal)){
            this.typeOfInteraction = 5;
        }

        //Tile apto ATAQUE MELEE
        if (targetTileInfo.tileHasEnemy && ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMelee) && ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol)) {
            this.typeOfInteraction = 2;
        }

        if (!targetTileInfo.GetAvailable() || ValuesChecker.EstaQuieto(iRow, dRow, iCol, dCol)) {
            this.typeOfInteraction = -1;
        }

        // UI_interaction.text = this.typeOfInteraction.ToString() + "\nEn recta: " + ValuesChecker.EstaEnRecta(iRow, dRow, iCol, dCol).ToString()
        //     + "\nEn Cruz: " + ValuesChecker.EstaEnCruz(iRow, dRow, iCol, dCol).ToString()  + "\nEn Distancia: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoDistancia).ToString()
        //      + "\nEn R movimiento: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMovimiento).ToString()
        //      + "\nEn R Melee: " + ValuesChecker.EstaEnRango(iRow, dRow, iCol, dCol, rangoMelee).ToString()
        //  ;

        return this.typeOfInteraction;

    }

    // Llena con las posiciones totales de movimiento a seguir
    public GameObject[] FillArrayPositions(){

        TileIdentifier selectedTileInfo = this.originTile.GetComponent<TileIdentifier>();
        TileIdentifier targetTileInfo = this.destinationTile.GetComponent<TileIdentifier>();
        GameObject[,] TestTable = GameObject.Find("CONTAINER").GetComponent<TableController>().TestTable;
        GameObject[] arrayTiles = new GameObject[0]; //Las posiciones que se devuelven

        int iRow = selectedTileInfo.GetMainArray_Row();
        int iCol = selectedTileInfo.GetSecArray_Col();

        int dRow = targetTileInfo.GetMainArray_Row();
        int dCol = targetTileInfo.GetSecArray_Col();

        string direction = ValuesChecker.ReturnNextTileDirection(iRow, dRow, iCol, dCol);

        
        if (direction == "vertical_abajo") {

            int rangoInterior = dRow-iRow;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow+a, iCol];
                } // Fin del for
        }

        else if (direction == "vertical_arriba") {

            int rangoInterior = iRow - dRow;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow-a, iCol];
                } // Fin del for

        }

        else if (direction == "horizontal_izquierda") {

            int rangoInterior = iCol - dCol;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow, iCol-a];
            }
        }

        else if (direction == "horizontal_derecha") {

            int rangoInterior = dCol - iCol;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow, iCol+a];
            } // Fin del for

        }


        else if (direction == "diagonal_arriba_izquierda") {

            int rangoInterior = iRow - dRow;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow-a, iCol-a];
                } // Fin del for

            }


        else if (direction == "diagonal_abajo_izquierda") {

            int rangoInterior = iCol - dCol;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow+a, iCol-a];
               
            } // Fin del for

        }

        else if (direction == "diagonal_arriba_derecha") {

            int rangoInterior = iRow - dRow;
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow-a, iCol+a];
            } // Fin del for

        }

        else if (direction == "diagonal_abajo_derecha") {

            int rangoInterior = -(iCol - dCol);
            arrayTiles = new GameObject[rangoInterior];

            for (int a = 1; a <= rangoInterior; a++) {
                arrayTiles[a-1] = TestTable[iRow+a, iCol+a];

            } // Fin del for
            
        }

        // Return arrayTiles
        return arrayTiles;


    }

    // En base a la posición final, devuelve la dirección actual
    public string ReturnCurrentDirection(){

        TileIdentifier selectedTileInfo = this.originTile.GetComponent<TileIdentifier>();
        TileIdentifier targetTileInfo = this.destinationTile.GetComponent<TileIdentifier>();

        int iRow = selectedTileInfo.GetMainArray_Row();
        int iCol = selectedTileInfo.GetSecArray_Col();

        int dRow = targetTileInfo.GetMainArray_Row();
        int dCol = targetTileInfo.GetSecArray_Col();

        string direction = ValuesChecker.ReturnNextTileDirection(iRow, dRow, iCol, dCol);
        return direction;
        
    }

    // Finalmente cuando la posición es válida, cargamos todos sus gameobjects para dárselo a las funciones de Mover / Atacar

    // Hacer un loop entre todos los tiles objetivos y devolver si la posición es posible
    public bool CheckIfPathToTileIsFree(){

        TileIdentifier selectedTileInfo = this.originTile.GetComponent<TileIdentifier>();
        TileIdentifier targetTileInfo = this.destinationTile.GetComponent<TileIdentifier>();
        GameObject[,] TestTable = GameObject.Find("CONTAINER").GetComponent<TableController>().TestTable;

        int iRow = selectedTileInfo.GetMainArray_Row();
        int iCol = selectedTileInfo.GetSecArray_Col();

        int dRow = targetTileInfo.GetMainArray_Row();
        int dCol = targetTileInfo.GetSecArray_Col();

        // Calcular
        string direction = ValuesChecker.ReturnNextTileDirection(iRow, dRow, iCol, dCol);
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
                    if (TestTable[iRow+a, iCol].GetComponent<TileIdentifier>().tileIsEmpty) {
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
                if (TestTable[iRow-a, iCol].GetComponent<TileIdentifier>().tileIsEmpty) {
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
                    if (TestTable[iRow, iCol-a].GetComponent<TileIdentifier>().tileIsEmpty & TestTable[iRow, iCol-a].GetComponent<TileIdentifier>().GetAvailable()) {
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
                if (TestTable[iRow, iCol+a].GetComponent<TileIdentifier>().tileIsEmpty & TestTable[iRow, iCol+a].GetComponent<TileIdentifier>().GetAvailable()) {
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

            int rangoInterior = iRow - dRow;
            GameObject[] arrayTiles = new GameObject[rangoInterior+1];

            if (rangoInterior < 2 && rangoInterior > 0) {
                arrayTiles[0] = TestTable[iRow-1, iCol-1];
                controlEmpty = true;
            }

            else {
    
                for (int a = 1; a < rangoInterior+1; a++) {
                    if (TestTable[iRow-a, iCol-a].GetComponent<TileIdentifier>().tileIsEmpty && TestTable[iRow-a, iCol-a].GetComponent<TileIdentifier>().GetAvailable()) {
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
                if (TestTable[iRow+a, iCol-a].GetComponent<TileIdentifier>().tileIsEmpty && TestTable[iRow+a, iCol-a].GetComponent<TileIdentifier>().GetAvailable()) {
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
                if (TestTable[iRow-a, iCol+a].GetComponent<TileIdentifier>().tileIsEmpty && TestTable[iRow-a, iCol+a].GetComponent<TileIdentifier>().GetAvailable()) {
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
                if (TestTable[iRow+a, iCol+a].GetComponent<TileIdentifier>().tileIsEmpty && TestTable[iRow+a, iCol+a].GetComponent<TileIdentifier>().GetAvailable()) {
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
        return this.positions;
    }

    public bool IsInteractionLegal(){
    // Si hay más que hacer un inspect, la interactuación con este tile es legal
    // Al hacerlo cargamos el arra yde GameObjects
        int i = this.typeOfInteraction;

        if (i != 0) {
            return true;
        }

        else {
            return false;
        }
    }
    
}
