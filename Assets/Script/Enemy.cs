using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {

    public float MovementSpeed = 1;
    public List<PatrolNode> patrolNodes = new List<PatrolNode>();
    PatrolNode currentNode;
    int currentNodeIndex = 0;

    private State _currentState;

    public State CurrentState {
        get { return _currentState; }
        set { _currentState = value; }
    }


    // Use this for initialization
    void Awake () {
        currentNode = patrolNodes[currentNodeIndex];
        transform.DOMove(currentNode.Transf.position, MovementSpeed).OnComplete(NodeReached).SetEase(Ease.Linear).SetDelay(currentNode.Amount).SetSpeedBased(true);
    }
	
    void NodeReached() {
        currentNodeIndex++;
        if (currentNodeIndex > patrolNodes.Count-1)
            currentNodeIndex = 0;
        currentNode = patrolNodes[currentNodeIndex];
        transform.DOMove(currentNode.Transf.localPosition, MovementSpeed).OnComplete(NodeReached).SetEase(Ease.Linear).SetDelay(currentNode.Amount).SetSpeedBased(true);
    }

    public enum State {

    }

    [System.Serializable]
    public class PatrolNode {
        public Transform Transf;
        public PatrolAction Action;
        public float Amount;
    }

    public enum PatrolAction {
        wait,
        shoot,
        jump,
    }
}
