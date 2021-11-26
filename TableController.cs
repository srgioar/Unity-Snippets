using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    public bool CardSelected = false;
    public bool CharSelected = false;

    public TargetDataHolder currentCursorData;
    public GameObject[] outTargetData;
    TargetData currentTargetData;
    public Text currentTargetedInfo;
    GameObject lastHovered;
    public GameObject lastTargeted;
    GameObject lastSelected;
    GameObject[] arraySelectedTiles;

    public GameObject firstSelected;
    GameObject secondSelected;

    // Tablero
    [SerializeField]
    public GameObject[,] TestTable;

    // Posición dummy INICIAL del pj anterior...
    Vector3 initialPlayerPosition;

    public Sprite spriteBase, spriteCursorHover, spriteCursorTarget, spriteCursorSelect;

    // MATERIALES BOTONES UI
    public Button btnSelect, btnDeselect, btnMove, btnAttack, btnInspect;

    public Sprite spriteButtonActive, spriteButtonInactive;
    
    // Start is called before the first frame update
    void Start()
    {
        // Instanciador();
        // SpawnPersonajes(); -> Temporal (Incluye enemigos)

        BoardCreator(8, 8);
        BoardInit();
        PlayersInit();

        currentTargetData = new TargetData(null, 0, false);
        currentTargetData.positions = new GameObject[2+1];

        //currentCursorData = new TargetDataHolder(null, null);
        
    }

    void EvaluateInteraction(){

                if ((firstSelected != null) && (lastTargeted != null)) {

            //ListaPosicionesTileObjetivo(firstSelected, lastTargeted);

            // Creamos un objeto de tipo cursor

                TargetDataHolder tdh = new TargetDataHolder(firstSelected, lastTargeted);
                currentCursorData = tdh;
                // Definimos su tipo de interacción
                int interaction = currentCursorData.GetTypeOfInteraction(1, 2, 2, 1);
                // Checkeamos si el tipo de interacción es legal.
                // Si lo es, llenamos el array de gameobjects...

                if(currentCursorData.IsInteractionLegal()){
                    Debug.Log("Interaction legal...Llenando tiles");
                    if (currentCursorData.CheckIfPathToTileIsFree()){
                        // LLenar tiles             
                        ButtonUIEnabler();
                    };

                    
                }
                else {
                    Debug.Log("Interaccion ilegal. Solo inspección...");
                }

            }

    }

    // Update is called once per frame
    void Update()
    {
        // Habilita el hover sobre tiles
        Hover();
        // Habilita el target sobre tiles
        Targeting();
        // Chequear si dependiendo de los tiles origen / destino podemos hacer acciones:
        ButtonSelectionEnabler();
        // FIX: ARREGLAR Y COMPARTIMENTALIZAR CORRECTAMENTE LLAMADAS A LA UI
        // DEBERÍA SER: BOTONES ACCIÓN EN CONDICIÓN
        // BOTONES SELECT LIBRES (CONSTANTE UPDATE!)
        // RECORDAR CAMBIAR LAS FUNCIONES EN UPDATE A COMPROBACIONES DE CAMBIO DE TARGET / SELECT

        ColorTile();

        // Cargar un tipo cursor interaccion nuevo si selected y target no están vacíos
        EvaluateInteraction();
    
    }


    // Función aislada para girar el personaje en base a la dirección destino
    // POR AMPLIAR: TENER EN CUENTA LAS POSICIONES TAMBIÉN DE ATAQUE PARA EL GIRO
    // AMPLIAR: TENER EN CUENTA SI SE ESTÁ SIENDO -ATACADO- Y GIRAR EN CONSECUENCIA
    public void BetaTurn(GameObject targetToMove){

        Vector3 mirarIzq = new Vector3(0, 180, 0);
        Vector3 mirarDer = new Vector3(0, 0, 0);
        Vector3 mirarArr = new Vector3(0, -90, 0);
        Vector3 mirarAbj = new Vector3(0, 90, 0);

        string currentDirection = currentCursorData.ReturnCurrentDirection();

        if (currentDirection == "vertical_arriba") {
            targetToMove.transform.eulerAngles = mirarArr;
        }

        else if (currentDirection == "vertical_abajo"){
            targetToMove.transform.eulerAngles = mirarAbj;
        }

        else if (currentDirection == "horizontal_izquierda" || currentDirection == "diagonal_arriba_izquierda" || currentDirection == "diagonal_abajo_izquierda") {
            targetToMove.transform.eulerAngles = mirarIzq;
        }

        else if (currentDirection == "horizontal_derecha" || currentDirection == "diagonal_arriba_derecha" || currentDirection == "diagonal_abajo_derecha") {
            targetToMove.transform.eulerAngles = mirarDer;
        }

    }

    public void BetaMove(){

        // CUELGA DE LA FUNCIÓN SELECTED

        Vector3 verticalOffset = new Vector3(0, 0.5f, 0);

        GameObject[] localPositions = currentCursorData.FillArrayPositions();

        TileIdentifier playerTile;
        GameObject currentPlayerActor;

        int iter = localPositions.Length-1;

        for (int i = 0; i <= iter; i++) {

            playerTile = firstSelected.GetComponent<TileIdentifier>();
            currentPlayerActor = playerTile.GetActorHolder();

            playerTile.SetActorHolder(null);
            firstSelected.GetComponent<TileIdentifier>().SetSelected(false);
            currentPlayerActor.transform.position = localPositions[i].transform.position + verticalOffset;
            firstSelected = localPositions[i];
            localPositions[i].GetComponent<TileIdentifier>().SetActorHolder(currentPlayerActor);
            BetaTurn(currentPlayerActor.transform.GetChild(0).gameObject);
            firstSelected.GetComponent<TileIdentifier>().SetSelected(true);
        }

    }

    // Función hermana a la de abajo, pero para select / deselect
    // No depende de TargetDataHolder
    public void ButtonSelectionEnabler(){

    Image btnSelectImage = btnSelect.GetComponent<Image>();
    Text btnSelectText = btnSelect.GetComponentInChildren<Text>();

    Image btnAttackImage = btnAttack.GetComponent<Image>();
    Text btnAttackText = btnAttack.GetComponentInChildren<Text>();

    Image btnDeselectImage = btnDeselect.GetComponent<Image>();
    Text btnDeselectText = btnDeselect.GetComponentInChildren<Text>();

    Image btnMoveImage = btnMove.GetComponent<Image>();
    Text btnMoveText = btnMove.GetComponentInChildren<Text>();

    Image btnInspectImage = btnInspect.GetComponent<Image>();
    Text btnInspectText = btnInspect.GetComponentInChildren<Text>();

    btnMoveImage.sprite = spriteButtonInactive;
    btnMoveText.color = Color.gray;

    btnAttackImage.sprite = spriteButtonInactive;
    btnAttackText.color = Color.gray;

    btnSelectImage.sprite = spriteButtonInactive;
    btnSelectText.color = Color.gray;

    btnDeselectImage.sprite = spriteButtonInactive;
    btnDeselectText.color = Color.gray;

    btnInspectImage.sprite = spriteButtonInactive;
    btnInspectText.color = Color.gray;


    if (firstSelected == null) {

        btnDeselectImage.sprite = spriteButtonInactive;
        btnDeselectText.color = Color.gray;

        btnMoveImage.sprite = spriteButtonInactive;
        btnMoveText.color = Color.gray;

    }

    else {

        btnDeselectImage.sprite = spriteButtonActive;
        btnDeselectText.color = Color.black;

        btnMoveImage.sprite = spriteButtonInactive;
        btnMoveText.color = Color.gray;

        btnAttackImage.sprite = spriteButtonInactive;
        btnAttackText.color = Color.gray;

    }

    // habilitar select

    if (lastTargeted != null) {

        if (lastTargeted.GetComponent<TileIdentifier>().tileHasCharacter){
            // Habilitar select
            btnSelectImage.sprite = spriteButtonActive;
            btnSelectText.color = Color.black;
        }
    

        else {
            btnSelectImage.sprite = spriteButtonInactive;
            btnSelectText.color = Color.gray;
        }

    }

    // CHECKEO INSPECT

        if (lastTargeted != null) {
            btnInspectImage.sprite = spriteButtonActive;
            btnInspectText.color = Color.black;
        }

        else {
            btnInspectImage.sprite = spriteButtonInactive;
            btnInspectText.color = Color.gray;

        }

    }

    public void ButtonUIEnabler(){

        Image btnAttackImage = btnAttack.GetComponent<Image>();
        Text btnAttackText = btnAttack.GetComponentInChildren<Text>();

        Image btnMoveImage = btnMove.GetComponent<Image>();
        Text btnMoveText = btnMove.GetComponentInChildren<Text>();

        btnMoveImage.sprite = spriteButtonInactive;
        btnMoveText.color = Color.gray;

        btnAttackImage.sprite = spriteButtonInactive;
        btnAttackText.color = Color.gray;

        int interaction = currentCursorData.GetTypeOfInteraction(1, 2, 3, 1);

        if (interaction == 1 || interaction == 5) {
           // MovementAvailable;
            btnMoveImage.sprite = spriteButtonActive;
            btnMoveText.color = Color.black;
        }

        else {

            btnMoveImage.sprite = spriteButtonInactive;
            btnMoveText.color = Color.gray;

        }

        if (interaction == 2 || interaction == 3) {

            btnAttackImage.sprite = spriteButtonActive;
            btnAttackText.color = Color.black;

        }

        else {

            btnAttackImage.sprite = spriteButtonInactive;
            btnAttackText.color = Color.gray;

        }

        if (interaction == 4) {

        }

        if (interaction == 6) {

        }


    }



public void Select(){

    // Si el tile targeteado tiene un personaje, el seleccionado equivale al personaje
    // Mejorar: Chequear que la posición inicial tiene un punto de interés (personaje)
    // Que la posición a elegir de destino NO está como no disponible
    // Establecer que la segunda posición seleccionada es posible si se usa
    // FIX: DESELECCIONAR Y SELECCIONAR NUEVA POSICIÓN CORRECTAMENTE

        TileIdentifier lt = lastTargeted.GetComponent<TileIdentifier>();
        Debug.Log("HASCHAR: " + lt.tileHasCharacter.ToString());
        
        if (lt.tileHasCharacter) {
            if (lastSelected != null) {
                TileIdentifier ls = lastSelected.GetComponent<TileIdentifier>();
                ls.SetSelected(false);
            }

            else {

            }

            firstSelected = lastTargeted;
            firstSelected.GetComponent<TileIdentifier>().SetSelected(true);
            Debug.Log("Llega a marcar como true!");
            lastSelected = firstSelected;
        }

        else{
            Debug.Log("No detecta pj en tile...");
        }

    
    // if (lastSelected != null) {
    //     TileIdentifier ls = lastSelected.GetComponent<TileIdentifier>();
    //     ls.SetSelected(false);
    // }
        
    }

    public void Deselect(){

        if (firstSelected == null && secondSelected == null) {

            // DO nothing!
        }

        else if (firstSelected != null && secondSelected == null) {
            TileIdentifier fS = firstSelected.GetComponent<TileIdentifier>();
            fS.SetSelected(false);
            firstSelected = null;
            
        }

        else if (firstSelected != null && secondSelected != null) {
            TileIdentifier sS = secondSelected.GetComponent<TileIdentifier>();
            sS.SetSelected(false);
            secondSelected = null;
        }

    }


    // Colorea y marca los tiles con cursores y colores, o si cambia el tipo
     void ColorTile(){

        for (int f = 0; f < TestTable.GetLength(0); f++){

            for (int i = 0; i < TestTable.GetLength(0); i++){

            TileIdentifier ti = TestTable[f, i].GetComponent<TileIdentifier>();
            GameObject cursorCanvas = TestTable[f, i].transform.Find("canvasBase/cursor").gameObject;


        if (firstSelected && lastTargeted != null) {

            TargetDataHolder tdh = new TargetDataHolder(firstSelected, TestTable[f, i]);
            int interaction = tdh.GetTypeOfInteraction(1, 2, 2, 1);
            GameObject cursorCanvasLegal = TestTable[f, i].transform.Find("canvasBase/cursorLegal").gameObject;
            Image cursorLegalImage = cursorCanvasLegal.GetComponent<Image>();
            //
           //Material tileMaterial = TestTable[f, i].transform.Find("canvasBase/cursorTargeted").gameObject;

        if (tdh.IsInteractionLegal()){

            // Pintar tile
            cursorCanvasLegal.SetActive(true);

            if (interaction == 1 || interaction == 5) {
                cursorLegalImage.color = new Color32(102, 153, 255, 130);
            }

            else if (interaction == 6) {
                cursorLegalImage.color = new Color32(188, 132, 255, 130);
            }

            else if (interaction == 2) {
                cursorLegalImage.color = new Color32(255, 133, 79, 130);
            }

            else if (interaction == 3) {
                cursorLegalImage.color = new Color32(255, 178, 79, 130);
            }

        }

        else {
            cursorCanvasLegal.SetActive(false);

        }

        }

        else {
            GameObject cursorCanvasLegal = TestTable[f, i].transform.Find("canvasBase/cursorLegal").gameObject;
            cursorCanvasLegal.SetActive(false);

        }

        if (ti.GetIsHover()) {

                
               // Debug.Log(cursorCanvas.name + " hovered!");
                cursorCanvas.GetComponent<Image>().sprite = spriteCursorHover;
                cursorCanvas.GetComponent<Image>().color = Color.blue;

        }

        else if (!ti.GetAvailable()) {


        }

        else if (ti.GetSelected()) {

                cursorCanvas.GetComponent<Image>().sprite = spriteCursorSelect;
                cursorCanvas.GetComponent<Image>().color = new Color32(229, 130, 37, 100);

        }

        else if (ti.GetTargeted()) {

                cursorCanvas.GetComponent<Image>().sprite = spriteCursorSelect;
                cursorCanvas.GetComponent<Image>().color = new Color32(11, 191, 142, 100);


        }

        if (!ti.GetSelected() & !ti.GetTargeted() & !ti.GetIsHover()) {

               // Debug.Log(cursorCanvas.name + " hovered!");
                cursorCanvas.GetComponent<Image>().sprite = spriteBase;
                cursorCanvas.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                
                //cursorCanvas.GetComponent<Image>().color = new Color32(229, 130, 37, 100);

        }

                } // Final primer for

        } // Final segundo for


    }

    void Targeting(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // TARGETEAR!

        if (Input.GetMouseButton(0)) {

            if (Physics.Raycast(ray, out hit)) {

            Transform tileHit = hit.transform;

            if (tileHit.tag == "basecube") {

            // Borrar antiguo hover
                if (lastTargeted != null) {
                    if (lastTargeted != tileHit.gameObject) {
                        TileIdentifier lh = lastTargeted.GetComponent<TileIdentifier>();
                        lh.SetTargeted(false);
                    }

                    else {
                        // DO nothing
                    }
                }

                TileIdentifier ti = tileHit.GetComponent<TileIdentifier>();
                ti.SetTargeted(true);
                lastTargeted = tileHit.gameObject;
                currentTargetedInfo.text = lastTargeted.name;

            }

    }
       
        }

    }


    // Controla que al pasar el ratón por encima de un tile se marque en su canvas el sprite de cursor
    void Hover(){

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            Transform tileHit = hit.transform;

            if (tileHit.tag == "basecube") {

            // Borrar antiguo hover
                if (lastHovered != null) {
                    if (lastHovered != tileHit.gameObject) {
                        TileIdentifier lh = lastHovered.GetComponent<TileIdentifier>();
                        lh.SetIsHover(false);
                    }

                    else {
                        // DO nothing
                    }
                }

                TileIdentifier ti = tileHit.GetComponent<TileIdentifier>();
                ti.SetIsHover(true);
                lastHovered = tileHit.gameObject;

            }

        }
        }

    
    void PlayersInit(){

        GameObject pGo1 = TestTable[1,1];
        GameObject pG02 = TestTable[1,2];
        GameObject eGo1 = TestTable[4,1];
        GameObject eGo2 = TestTable[4,2];

        // Cuatro posiciones iniciales

        // CharIdentifier player1 = new CharIdentifier("Player1", CharIdentifier.CharType.PlayerChar);
        // CharIdentifier player2 = new CharIdentifier("Player2", CharIdentifier.CharType.PlayerChar);

        // CharIdentifier enemy1 = new CharIdentifier("Enemy1", CharIdentifier.CharType.EnemyChar);
        // CharIdentifier enemy2 = new CharIdentifier("Enemy2", CharIdentifier.CharType.EnemyChar);

        // Instantiate();
        GameObject tGo1 = Instantiate(
                GameObject.Find("actorbase"),
                TestTable[1,1].transform.position + new Vector3(0, 0.3932959f, 0), Quaternion.identity,
                GameObject.Find("CONTAINER").transform
                );

        TestTable[1,1].GetComponent<TileIdentifier>().SetActorHolder(tGo1);

                // Instantiate();
        GameObject tGoE1 = Instantiate(
                GameObject.Find("actorbase"),
                TestTable[4,1].transform.position + new Vector3(0, 0.3932959f, 0), Quaternion.identity,
                GameObject.Find("CONTAINER").transform
                );

        TestTable[4,1].GetComponent<TileIdentifier>().SetActorHolder(tGoE1);
        tGoE1.GetComponent<CharIdentifier>().charType = CharIdentifier.CharType.EnemyChar;


    }

    


    // Después de instanciar, pone en su sitio parámetros como primera posición, posición del primer pj...
    // NECESARIO PARA SETEAR TODO TRAS INSTANCIAR

    void BoardInit(){

        initialPlayerPosition = TestTable[5,3].transform.position;
        //initialTile = TestTable[5,3];
        //initialTile.GetComponent<TileIdentifier>().SetSelected(true);
        //finalTile = initialTile;
        //lastSelectedTile = null;
        arraySelectedTiles = new GameObject[2];
        // Establecer que el tile de prueba tiene un personaje e inyectar el pj gameobject
        TestTable[5,3].GetComponent<TileIdentifier>().tileHasCharacter = true;
        // TestTable[5,3].GetComponent<TileIdentifier>().SetActorHolder(playerObj);
        // TestTable[7,3].GetComponent<TileIdentifier>().SetActorHolder(enemyObj);
        // TestTable[5,4].GetComponent<TileIdentifier>().SetActorHolder(enemyObj2);
        // TestTable[2,2].GetComponent<TileIdentifier>().SetActorHolder(playerObj2);
        // enemyObj.transform.position = TestTable[7,3].transform.position + new Vector3(0, 0.5f, 0);
        // enemyObj2.transform.position = TestTable[5,4].transform.position + new Vector3(0, 0.5f, 0);
        // playerObj.transform.position = TestTable[5,3].transform.position + new Vector3(0, 0.5f, 0);
        // playerObj2.transform.position = TestTable[2,2].transform.position + new Vector3(0, 0.5f, 0);

    }

    void BoardCreator(int a, int b){

        // Offset horizontal para colocar las piezas entre ´si
        float hOffset = 1.35764f;

        TestTable = new GameObject[a, b];

        for (int f = 0; f < a; f++){

            for (int i = 0; i < b; i++){

                TestTable[f, i] = Instantiate(
                GameObject.FindGameObjectWithTag("basecube"),
                new Vector3((0 + i * hOffset),0,(0-f * 0.9529505f)), Quaternion.identity,
                GameObject.Find("CONTAINER").transform
                );

                // Instanciar el script de cada uno de los tiles para almacenar su información (row/col)
                TileIdentifier tileIdentifier = TestTable[f, i].GetComponentInChildren<TileIdentifier>();
                tileIdentifier.SetPosition(f, i);

                // Darles nombres a los clones
                TestTable[f, i].name = "TileBase" + f.ToString() + "-" + i.ToString();

                // Pintar id de row/col en el tile (Canvas individual -> Text)
                Text ttt = TestTable[f,i].GetComponentInChildren<Text>();
                ttt.text = "a" + f.ToString()  + "-p" + i.ToString();
            
            } // FIN PRIMER FOR
        } // FIN SEGUNDO FOR

        // COLOCA EL TABLERO EN EL CENTRO
        GameObject.Find("CONTAINER").transform.position = new Vector3(-4.75f, 0, 3.34f);

    }

}
