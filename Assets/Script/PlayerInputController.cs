using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

    #region properties

    public float MovementSpeed = 0.1f;
    public float JumpForce = 0.1f;
    public float JumpDurationTime = 1.0f;

    float jumpCounter = 0;
    Animator animator;
    BoxCollider2D boxCollider;

    private JumpState _currentJumpState;

    public JumpState CurrentJumpState {
        get { return _currentJumpState; }
        set {
            if(_currentJumpState != value) {
                _currentJumpState = value;
                OnJumpStateChanged();
            }
        }
    }



    private State _currentState;
    /// <summary>
    /// 
    /// </summary>
    public State CurrentState {
        get { return _currentState; }
        set {
            if(_currentState != value) { 
                _currentState = value;
                OnStateChanged();
            }
        }
    }
    
    public UnityEngine.UI.Text debugText;

    #endregion

    #region lifecycle

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        checkJumpState();
        if (Input.GetKey(KeyCode.A)) {
            // move left
            transform.position -= new Vector3(MovementSpeed, 0);
            CurrentState = State.walk_l;
        } else if (Input.GetKey(KeyCode.D)) {
            // move right
            transform.position += new Vector3(MovementSpeed, 0);
            CurrentState = State.walk_r;
        } else {
            CurrentState = State.idle;
        }
    }

    void checkJumpState() {

        switch (CurrentJumpState) {
            case JumpState.no_jump:
                // posso iniziare a saltare
                if (Input.GetKey(KeyCode.O)) {
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

        if(debugText)
            debugText.text = jumpCounter.ToString();
    }

    #endregion

    #region Events

    private void OnStateChanged() {
        Debug.Log("State Changed: " + CurrentState);
        animator.SetInteger("CurrentState", (int)CurrentState);
    }

    private void OnJumpStateChanged() {
        switch (CurrentJumpState) {
            case JumpState.jump_up:
                boxCollider.enabled = false;
                break;
            case JumpState.jump_down:
            case JumpState.no_jump:
                boxCollider.enabled = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }

    #endregion

    #region State Machine

    public enum JumpState {
        no_jump,
        jump_up,
        jump_down,
    }

    public enum State {
        idle,
        walk_l,
        walk_r,
    }

    #endregion

}
