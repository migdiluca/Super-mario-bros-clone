using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    public int xDirection = 1;
    public float walkingSpeed = 130;

    public bool flippable = true;
    
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D.velocity = new Vector2(walkingSpeed * xDirection, rigidbody2D.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(walkingSpeed * xDirection, rigidbody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 hit = col.GetContact(0).normal;

        float dot = Vector2.Dot(hit, Vector2.right);

        if (Mathf.Approximately(dot, 0))
        {
            return;
        }
        
        if (dot > 0)
        {
            xDirection = 1;
        }
        else
        {
            xDirection = -1;
        }

        
        if(flippable)
            spriteRenderer.flipX = xDirection < 0;
        
    }
}