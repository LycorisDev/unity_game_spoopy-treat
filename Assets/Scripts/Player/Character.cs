using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    // Movement
    public float DirectionalSpeed { get; private set; }
    public float RotationalSpeed { get; private set; }
    public float JumpForce { get; private set; }
    public bool IsOnGround { get; private set; }
    private Rigidbody _rb;

    // Items
    public int CandyAmount { get; private set; }
    public int MaxCandyAmount { get; private set; }

    private void Awake()
    {
        DirectionalSpeed = 5f;
        RotationalSpeed = DirectionalSpeed / 2 * DirectionalSpeed * DirectionalSpeed;
        JumpForce = 20f;
        IsOnGround = false;
        _rb = GetComponent<Rigidbody>();

        CandyAmount = 0;
        MaxCandyAmount = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("InvisibleWall"))
            IsOnGround = true;
    }

    public void IncreasePhysicalStats()
    {
        DirectionalSpeed += 1f;
        RotationalSpeed = DirectionalSpeed / 2 * DirectionalSpeed * DirectionalSpeed;
        JumpForce *= 1.5f;
        _rb.mass += 2;
    }

    public void ModifyCandyAmount(int amountToAdd)
    {
        CandyAmount = Mathf.Clamp(CandyAmount + amountToAdd, 0, MaxCandyAmount);
    }

    public void Jump()
    {
        if (IsOnGround)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }
}
