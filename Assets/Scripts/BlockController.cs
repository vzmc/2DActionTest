using UnityEngine;

public class BlockController : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public GameObject brokenBlock;

    public bool canBreak;

    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 pos = transform.position;
            Vector2 groundCheck = new Vector2(pos.x, pos.y - transform.lossyScale.y);
            Vector2 groundArea = new Vector2(boxCollider2D.size.x * transform.lossyScale.y * 0.45f, 0.05f);
            var col2D = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsPlayer);

            if (col2D)
            {
                if (canBreak)
                {
                    GameObject broken = Instantiate(brokenBlock, transform.position, transform.rotation);
                    broken.transform.localScale = transform.lossyScale;
                    Destroy(gameObject);
                }
            }
        }
    }
}