using UnityEngine;

public class Shooter : MonoBehaviour {

    public Bubble BulletPrefab;
    public KeyCode ShootKey;
    PlayerPhysic Player;

    float shootDuration = 0.4f;
    Bubble.Direction ShootDirection;
    Vector3 offSetPosition = Vector3.zero;

	void Start () {
        Player = FindObjectOfType<PlayerPhysic>();	
	}
	
	void Update () {
        switch (Player.CurrentState) {
            case PlayerPhysic.State.walk_l:
                ShootDirection = Bubble.Direction.left;
                offSetPosition += Vector3.left * 0.2f;
                break;
            case PlayerPhysic.State.walk_r:
                ShootDirection = Bubble.Direction.right;
                offSetPosition += Vector3.right * 0.2f;
                break;
        }
        if (Input.GetKeyDown(ShootKey)) {
            Shoot();
        }
	}

    public void Shoot() {
        Bubble newBubble = Instantiate<Bubble>(BulletPrefab, transform.position + offSetPosition, transform.rotation);
        newBubble.Shoot(new Bubble.ShootInfo(ShootDirection, shootDuration));
    }
}
