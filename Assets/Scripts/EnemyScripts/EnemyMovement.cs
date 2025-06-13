using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]    
    [SerializeField] private float moveSpeed = 1f;

    [Header("Animation")]
    [SerializeField] private Sprite[] walkSprites;         // Sprites da animação de caminhada
    [SerializeField] private float animationSpeed = 0.01f;   // Tempo entre cada troca de sprite

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;
    private float animationTimer;

    private Transform target;
    private int pathIndex = 0;
    private float originalXScale;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
        originalXScale = transform.localScale.x;

        spriteRenderer = GetComponent<SpriteRenderer>();
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

        rb.linearVelocity = direction * moveSpeed;

        // Espelhar sprite com base na direção
        if (direction.x > 0.01f)
        {
            transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < -0.01f)
        {
            transform.localScale = new Vector3(-originalXScale, transform.localScale.y, transform.localScale.z);
        }
    }

    private void AnimateWalk()
    {
        if (walkSprites.Length == 0 || spriteRenderer == null) return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % walkSprites.Length;
            spriteRenderer.sprite = walkSprites[currentSpriteIndex];
            animationTimer = 0f;
        }
    }
}
