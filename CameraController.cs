using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Distintos métodos para gestionar la cámara
    GameObject CameraIsoHolder, Camera3DHolder;
    Vector3 CameraIsoNormalPosition, Camera3DNormalPosition;

    float InputH;


    // Start is called before the first frame update
    void Start()
    {
        CameraIsoHolder = GameObject.Find("CameraIsoCrane");
        Camera3DHolder = GameObject.Find("Camera3DCrane");
        CameraIsoNormalPosition = CameraIsoHolder.transform.position;
        Camera3DNormalPosition = Camera3DHolder.transform.position;
    }

    void EnableCamera3DTurn(){
        InputH = Input.GetAxis("Horizontal");
        Camera3DHolder.transform.Rotate(0, InputH, 0, Space.Self);
    }

    // Fijar la cámara en el edificio al sacar modo crear
    public void FixateCameraOnBuilding(GameObject building){
        CameraIsoHolder.transform.position = building.transform.position;
    }

    public void Set3DCamAsActive(){
        CameraIsoHolder.GetComponentInChildren<Camera>().enabled = false;
        Camera3DHolder.GetComponentInChildren<Camera>().enabled = true;
    }

    public void SetIsoCamAsActive(){

        CameraIsoHolder.GetComponentInChildren<Camera>().enabled = true;
        Camera3DHolder.GetComponentInChildren<Camera>().enabled = false;

    }

    public void RestartCamera(){
        CameraIsoHolder.transform.position = CameraIsoNormalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        EnableCamera3DTurn();
    }
}
