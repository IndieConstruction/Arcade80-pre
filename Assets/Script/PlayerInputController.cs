using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    public float MovementSpeed = 0.1f;
    public float JumpForce = 0.1f;
    public float JumpDurationTime = 1.0f;
    float jumpCounter = 0;
    JumpState CurrentJumpState = JumpState.jump_down;

    public UnityEngine.UI.Text text;
    // Update is called once per frame
    void Update() {
        checkJumpState();
        if (Input.GetKey(KeyCode.A)) {
            // move left
            transform.position -= new Vector3(MovementSpeed, 0);
        } else if (Input.GetKey(KeyCode.D)) {
            // move right
            transform.position += new Vector3(MovementSpeed, 0);
        }
    }

    void checkJumpState() {

        switch (CurrentJumpState) {
            case JumpState.no_jump:
                // posso iniziare a saltare
                if (Input.GetKeyDown(KeyCode.O)) {
                    jumpCounter = JumpDurationTime;
                    CurrentJumpState = JumpState.jump_up;
                }
                break;
            case JumpState.jump_up:
                if (jumpCounter > 0) {
                    // continuo a saltare
                    jumpCounter -= Time.deltaTime;
                    transform.position += new Vector3(0, JumpForce);
                } else {
                    CurrentJumpState = JumpState.jump_down;
                }
                break;
            case JumpState.jump_down:
                transform.position -= new Vector3(0, JumpForce);
                break;
            default:
                break;
        }

        text.text = jumpCounter.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        CurrentJumpState = JumpState.no_jump;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        CurrentJumpState = JumpState.no_jump;
    }



    //    Debug.Log("On Collisione ")
    //    CurrentJumpState = JumpState.no_jump;
    //}

    #region State Machine

    public enum JumpState {
        no_jump,
        jump_up,
        jump_down,
    }

    public enum State {
        idle,
    }

    #endregion

}
