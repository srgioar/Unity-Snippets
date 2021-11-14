using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class NavigationTest : MonoBehaviour
{
    Vector3 destination;
    GameObject TargetGameObject;
    Transform target;
    NavMeshAgent navAgent;
    Vector3 NavMeshSize = new Vector3(20, 20, 20);

    GameObject RayGuide;

    public bool IAIsActive;

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        TargetGameObject = GameObject.Find("DUMMYTARGET");
        target = TargetGameObject.transform;
        IAIsActive = false;
        RayGuide = transform.GetChild(5).gameObject;
    }

    public void ActivarIA(){
        IAIsActive = true;
    }

    void ManualNavigation(){
        // El go rastrea todo el rato hacia adelante, en caso de que el trigger dé ok sigu eandando
        // Si no da ok, el raycast rota hasta entontrar la posición correcta
        // El modelo hace un movetorwards - lookrotation y corrige la posición

        // MOVE / FIX TARGET / DEFIX TARGET
        if (Physics.Raycast(RayGuide.transform.position, Vector3.forward)) {
            
        }
    }

    void SetDestination(){
        // I
        // Update destination if the target moves one unit
        if (Vector3.Distance(target.position, destination ) > 0.02f)
        {
            destination = target.position;
            navAgent.destination = destination;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if (IAIsActive) {
            SetDestination();
        }

        else {

        }
    }
}
