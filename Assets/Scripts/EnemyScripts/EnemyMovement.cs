using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]    
    [SerializeField] private float moveSpeed = 1f;

    [Header("Animation")]
    [SerializeField] private Sprite[] walkFrontSprites;
    [SerializeField] private Sprite[] walkBackSprites;
    [SerializeField] private Sprite[] walkSideSprites;
    [SerializeField] private float animationSpeed = 0.2f;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private float animationTimer;
    private Sprite[] currentAnimation;

    private Transform target;
    private int pathIndex = 0;
    private float originalXScale;

    private Vector2 lastDirection;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
        originalXScale = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentAnimation = walkFrontSprites;
    }

    private void Update()
    {
        AnimateWalk();

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                LevelManager.main.DamagePlayer(1);
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        lastDirection = direction;

        rb.linearVelocity = direction * moveSpeed;

        UpdateDirectionAnimation(direction);
    }

    private void AnimateWalk()
    {
        if (currentAnimation.Length == 0 || spriteRenderer == null) return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % currentAnimation.Length;
            spriteRenderer.sprite = currentAnimation[currentSpriteIndex];
            animationTimer = 0f;
        }
    }

    private void UpdateDirectionAnimation(Vector2 direction)
    {
        // Lado
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            currentAnimation = walkSideSprites;

            if (direction.x > 0.01f)
                transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-originalXScale, transform.localScale.y, transform.localScale.z);
        }
        // Frente ou costas
        else
        {
            transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);

            if (direction.y > 0.01f)
                currentAnimation = walkBackSprites;
            else
                currentAnimation = walkFrontSprites;
        }
    }
}
