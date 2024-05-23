using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float YPos;
    public float strength = 15f;
    public float gravity = -9.8f;

    private Animator animator;
    private Vector3 jumpDir;

    public bool isGround = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isGround && !GameManager.Instance.IsPlayerDie)
        {
            Jump();
            isGround = false;
        }

        Gravity();

        transform.position += jumpDir * Time.deltaTime;

        if (transform.localPosition.y <= YPos + 0.1f)
        {
            isGround = true;
        }
    }

    private void Gravity()
    {
        // 중력 적용
        if (!isGround) // 수정된 부분
            jumpDir.y += gravity * Time.deltaTime;
        else
        {
            jumpDir.y = 0;
            animator.SetBool("Jump", false);
        }
    }

    private void Jump()
    {
        SoundManager.PlaySound("Jump");
        jumpDir = Vector3.up * strength;
        animator.SetBool("Jump", true);
    }

    private void Die()
    {
        animator.SetBool("Die", true);
        GameManager.Instance.IsPlayerDie = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerObstacle"))
            Die();
    }
}
