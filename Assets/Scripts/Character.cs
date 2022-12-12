using UnityEngine;

public class Character : MonoBehaviour
{
    // Movement
    public float directionalSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public bool isOnGround;

    // Items
    public int nbrCandies = 0;

    void Start()
    {
        directionalSpeed = 5f;
        rotateSpeed = directionalSpeed / 2 * directionalSpeed * directionalSpeed;
        jumpForce = 20f;
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            isOnGround = true;
    }
}
