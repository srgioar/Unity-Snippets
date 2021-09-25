    void CheckInput(){

        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");

        inputJump = Input.GetButtonDown("Jump");

        mouseX = Input.GetAxis("Mouse X") * mouseAccel * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseAccel * Time.deltaTime;

        RMB = Input.GetMouseButton(1);
        LMB = Input.GetMouseButton(0);

        QKey = Input.GetKey(KeyCode.Q);
        EKey = Input.GetKey(KeyCode.E);

    }
