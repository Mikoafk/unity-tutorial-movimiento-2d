using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float jumpForce = 100f;

    [SerializeField] private Vector2 raycastGroundOffset;
    [SerializeField] private Vector2 raycastGroundSize;
    [SerializeField] private LayerMask layerGround;

    private Rigidbody2D rigidbody;
    private float horizontalMove;
    private bool isLookRight = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButton("Jump") && IsGrounded())
        {
            JumpPlayer();
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rigidbody.velocity.y);

        if (isLookRight && horizontalMove < 0)
        {
            FlipPlayer();
        }
        else if (!isLookRight && horizontalMove > 0)
        {
            FlipPlayer();
        }
    }

    private void JumpPlayer()
    {
        rigidbody.AddForce(Vector2.up * jumpForce);
    }

    private bool IsGrounded() => Physics2D.OverlapBox((Vector2)transform.position + raycastGroundOffset, raycastGroundSize, 0f, layerGround);

    private void FlipPlayer()
    {
        isLookRight = !isLookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + raycastGroundOffset, raycastGroundSize);
    }
}
