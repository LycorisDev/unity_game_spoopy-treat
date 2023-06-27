using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    // Movement
    [HideInInspector] public Vector2 Movements = Vector2.zero;
    [HideInInspector] public float SideStep = 0f;
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

    private void Update()
    {
        // Move the character forward or backward
        if (Movements.y > 0f)
            transform.Translate(Vector3.forward * Time.deltaTime * DirectionalSpeed);
        if (Movements.y < 0f)
            transform.Translate(Vector3.back * Time.deltaTime * DirectionalSpeed);

        // Rotate the character to the left or the right
        if (Movements.x < 0f)
            transform.Rotate(Vector3.down * Time.deltaTime * RotationalSpeed);
        if (Movements.x > 0f)
            transform.Rotate(Vector3.up * Time.deltaTime * RotationalSpeed);

        // Move the character to the side
        if (SideStep < 0f)
            transform.Translate(Vector3.left * Time.deltaTime * DirectionalSpeed);
        if (SideStep > 0f)
            transform.Translate(Vector3.right * Time.deltaTime * DirectionalSpeed);
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
