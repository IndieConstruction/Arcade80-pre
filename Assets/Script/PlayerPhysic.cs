using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysic : MonoBehaviour {

    #region Properties

    public float MovementSpeed = 0.1f;
    public float JumpForce = 0.1f;

    Animator animator;

    private JumpState _currentJumpState;
    /// <summary>
    /// 
    /// </summary>
    public JumpState CurrentJumpState {
        get { return _currentJumpState; }
        set {
            if (_currentJumpState != value) {
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
            if (_currentState != value) {
                _currentState = value;
                OnStateChanged();
            }
        }
    }

    #endregion

    #region Lifecycle

    private void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Movement
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

        if (Input.GetKeyDown(KeyCode.O) && CurrentJumpState != JumpState.jump_up) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
            CurrentJumpState = JumpState.jump_up;
        }

    }

    #endregion

    #region Events

    private void OnStateChanged() {
        animator.SetInteger("CurrentState", (int)CurrentState);
    }

    private void OnJumpStateChanged() {

    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall" && GetComponent<Rigidbody2D>().velocity.y <= 0) {
            CurrentJumpState = JumpState.no_jump;
        }
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
