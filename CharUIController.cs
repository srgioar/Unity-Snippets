using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUIController : MonoBehaviour
{

    Text PlayerLifeText, PlayerEnergyText;
    Image PlayerLifeImage, PlayerEnergyImage;

    CharIdentifier charIdentifier;

    GameObject goo;
    GameObject energyGo, lifeGo;
    
    // Start is called before the first frame update
    void Start()
    {
        goo = transform.GetChild(1).gameObject;
        energyGo = goo.transform.GetChild(0).gameObject;
        lifeGo = goo.transform.GetChild(1).gameObject;
        Debug.Log(goo.name);

        PlayerLifeText = lifeGo.GetComponentInChildren<Text>();
        PlayerLifeImage = lifeGo.GetComponentInChildren<Image>();

        PlayerEnergyText = energyGo.GetComponentInChildren<Text>();
        PlayerEnergyImage = energyGo.GetComponentInChildren<Image>();

        charIdentifier = GetComponent<CharIdentifier>();

        PlayerLifeText.text = charIdentifier.Life.ToString();
        PlayerEnergyText.text = charIdentifier.Energy.ToString();

        RelativeBar();
    }

    // Update is called once per frame
    void Update()
    {
        goo.transform.rotation = (Camera.main.transform.rotation);
        //transform.rotation = new Vector3(0, 0, 0);
        PlayerLifeImage.fillAmount = charIdentifier.Life * 0.33f;
        PlayerEnergyImage.fillAmount = charIdentifier.Energy * 0.33f;

        PlayerLifeText.text = charIdentifier.Life.ToString();
        PlayerEnergyText.text = charIdentifier.Energy.ToString();
        
    }


    // Dependiendo de la vida máxima la cantidad de barras de vida cambia
    // Resultado de dividir el nº vida máxima / 1
    // 1 -> 100%
    // 2 -> 50%
    // 3 -> 33%
    void RelativeBar(){

        int maxLife = 5;

        float ratio = 1f / (float)maxLife;

        Debug.Log("Ratio: " + ratio.ToString());

        GameObject obj = GameObject.Find("lifeChunk");
        GameObject baseObj = GameObject.Find("lifeBase");
        GameObject holder = GameObject.Find("lifeBaseHolder");
        GameObject iObj;

        for (int i = 1; i <= maxLife; i++) {

            // LA BASE DE ANCHURA ES 40px, 3 altura

            // Instanciar y dar nombre
            iObj = Instantiate(obj);
            iObj.name = "iObj " + i.ToString();
            // Cuadrar parent y cargar RectTransform
            iObj.transform.SetParent(holder.transform, false);
            RectTransform rt = iObj.GetComponent<RectTransform>();
            // Setear posición y escala según ratio
            rt.sizeDelta = new Vector2(40/maxLife, 3);

            
            Vector3 pos = new Vector3(((i-1) * rt.rect.width) + (0.5f * i-1), 0, 0);

            //Vector3 pos = new Vector3(-rt.rect.width + (4*i) + ((i-1) * rt.rect.width + (0.5f * i-1)), 0, 0);
            
            rt.localPosition = pos;
            Debug.Log("data" + i + " :" + (0.5f * i-1));
            Debug.Log("x: " + pos.x);
            //Debug.Log(pos + "\n");


            //rt.transform.localScale = new Vector3(ratio, 1, 1);

        }

        holder.transform.localPosition = baseObj.transform.position;



        //iObj




    }


}
