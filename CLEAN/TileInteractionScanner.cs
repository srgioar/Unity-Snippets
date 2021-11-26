using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInteractionScanner{

    public GameObject Selected;
    public GameObject Targeted;

    public bool IsSelectPossible;
    public bool IsInspectPossible;
    public bool IsAttackPossible;
    public bool IsMovementPossible;
    public bool IsSkillPossible;
    public bool IsPickupPossible;

    public int Movement;
    public int Diagonal;
    public int Melee;
    public bool CapableDistance;
    public int AttackDistance;
    public int SkillDistance;

    public TileInteractionScanner(GameObject Selected, GameObject Targeted){

        this.Selected = Selected;
        this.Targeted = Targeted;
        this.Movement = 0;
        this.Diagonal = 0;
        this.Melee = 0;
        this.AttackDistance = 0;
        this.SkillDistance = 0;

    }

    // Carga los valores del personaje en SELECTED

    void LoadValuesFromChar(){
        
        if (Selected != null) {
        TileHolderInfo thi = Selected.GetComponentInChildren<TileHolderInfo>();
        GameObject go = thi.InsertedObject;
        CharacterIdentifier ci = go.GetComponent<CharacterIdentifier>();
        Movement = ci.Movement;
        Diagonal = ci.Diagonal;
        CapableDistance = ci.IsDistanceChar;
        AttackDistance = ci.AttackDistance;
        SkillDistance = ci.SkillDistance;
        Melee = 1;

        }
        
    }

    public void CheckInteraction(){

        LoadValuesFromChar();

        ValuesCheckerNew vc = new ValuesCheckerNew(Selected, Targeted);

        Debug.Log("En cruz?" + vc.EstaEnCruz());
        //Debug.Log("En rango melee?" + vc.EstaEnRango(Melee));
        Debug.Log("En diagonal? " + vc.EstaEnDiagonal());
        Debug.Log("En recta? " + vc.EstaEnRecta());
        Debug.Log("Camino disponible: " + vc.CheckIfPathToTileIsFree());

        vc.ReturnTargetTiles();

        if ((vc.EstaEnRango(Diagonal) && vc.EstaEnDiagonal() && vc.CheckIfPathToTileIsFree()) || (vc.EstaEnRango(Movement) && vc.EstaEnRecta() && vc.CheckIfPathToTileIsFree() && Targeted.GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Null)) {
            Debug.Log("APTO PARA MOVEMENT");
            IsMovementPossible = true;
        }

        if ((vc.EstaEnCruz() && vc.CheckIfPathToTileIsFree() && vc.EstaEnRango(Melee) && (Targeted.GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Enemy))  || ((vc.EstaEnRango(AttackDistance) && CapableDistance && Targeted.GetComponentInChildren<TileIdentifierNew>().containedType == TileIdentifierNew.ContainedType.Enemy && vc.CheckIfPathToTileIsFree()))) {
            Debug.Log("APTO PARA ATACAR");
            IsAttackPossible = true;
        }

    }
    
}



