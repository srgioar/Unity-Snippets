    void Fisica(){

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if (inputJump && grounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
