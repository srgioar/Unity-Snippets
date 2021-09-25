    void SimpleMove(){
     // lookRot = Quaternion.LookRotation();
        Vector3 move = new Vector3(inputX, 0, inputZ);
        Quaternion rotation = Quaternion.LookRotation(move, Vector3.up);
        playerBody.rotation = rotation;
        cc.Move(move * Time.deltaTime * playerSpeed);

        if (inputX != 0 || inputZ != 0) {
            anim.SetBool("Running", true);
        }
        
        else {
            anim.SetBool("Running", false);
        }

    }
