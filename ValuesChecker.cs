using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValuesChecker
{

    public static bool EstaEnRango(int iCol, int dCol, int iRow, int dRow, int rango){
        bool estaEnRango = estaEnRango = (iRow - dRow <= rango && iRow - dRow >= -rango) && (iCol - dCol <= rango && iCol - dCol >= -rango);
        return estaEnRango;
    }

    public static bool EstaQuieto(int iCol, int dCol, int iRow, int dRow){
        bool estaQuieto = iCol == dCol && iRow == dRow;
        return estaQuieto;
    }

    public static bool EstaEnRecta(int iCol, int dCol, int iRow, int dRow){
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

    public static bool EstaEnDiagonal(int iCol, int dCol, int iRow, int dRow){

        bool estaEnDiagonal = false;

        bool estaDiagonalAbajoDer = (iCol < dCol && iRow < dRow) && (iCol - dCol == iRow - dRow); 
        bool estaDiagonalArribaDer = (iCol < dCol && iRow > dRow) && (iRow - dRow == -(iCol - dCol));
        //Debug.Log((iRow - dRow == +(iCol - dCol)));
        bool estaDiagonalAbajoIzq = (iCol > dCol && iRow < dRow)  && (iRow - dRow == -(iCol - dCol));
        bool estaDiagonalArribaIzq = (iCol > dCol && iRow > dRow) && (iCol - dCol == iRow - dRow);

        estaEnDiagonal = (estaDiagonalArribaIzq || estaDiagonalArribaDer || estaDiagonalAbajoIzq || estaDiagonalAbajoDer);

        return estaEnDiagonal;

    }

    public static bool EstaEnArea(int iCol, int dCol, int iRow, int dRow){
            bool estaEnArea = (iCol != dCol && iRow != dRow);
            return estaEnArea;

    }

    public static bool EstaEnCruz(int iCol, int dCol, int iRow, int dRow){
        bool estaEnCruz = ( (iRow - dRow == 1) && (iCol - dCol == 0) ||
                            (iRow - dRow == -1) && (iCol - dCol == 0) ||
                            (iRow - dRow == 0) && (iCol - dCol == 1) ||
                            (iRow - dRow == 0) && (iCol - dCol == -1)
                            );
        return estaEnCruz;
    }

    //, int dRow, int iCol, int dCol, int rango;

     // No comprueba realmente si está en diagonal, comprueba la dirección general

    public static string ReturnNextTileDirection(int iRow, int dRow, int iCol, int dCol){

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
   
    


    public static void BoolCheckerTester(){

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
