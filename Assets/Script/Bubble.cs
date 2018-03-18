using UnityEngine;

public class Bubble : MonoBehaviour {

    public float Duration = 12.0f;
    float shootDuration;

    Direction currentDirection;
    
    private void Update() {
        switch (currentDirection) {
            case Direction.up:
                Duration -= Time.deltaTime;
                if (Duration <= 0)
                    GameObject.Destroy(gameObject);
                break;
            case Direction.left:
            case Direction.right:
                shootDuration -= Time.deltaTime;
                if(currentDirection == Direction.right)
                    transform.position += new Vector3(0.02f, 0,0);
                else
                    transform.position -= new Vector3(0.02f, 0, 0);
                if (shootDuration <= 0)
                    currentDirection = Direction.up;
                break;
        }
    }

    public void Shoot(ShootInfo info) {
        currentDirection = info.ShootDirection;
        shootDuration = info.ShootDuration;
    }

    #region 

    public enum Direction {
        up,
        left,
        right,
    }

    public struct ShootInfo {
        public Direction ShootDirection;
        public float ShootDuration;

        public ShootInfo(Direction shootDirection, float duration) {
            ShootDirection = shootDirection;
            ShootDuration = duration;
        }
    }

    #endregion
}
