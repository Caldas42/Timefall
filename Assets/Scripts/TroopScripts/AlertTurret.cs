using UnityEngine;

public class AlertTurret : Turret
{
    [Header("Alert Sprites")]
    [SerializeField] private SpriteRenderer turretSpriteRenderer;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite alertSprite;

    private bool isAlert = false;

    protected override void Update()
    {
        base.Update();

        if (target != null && CheckTargetIsInRange())
        {
            if (!isAlert)
            {
                turretSpriteRenderer.sprite = alertSprite;
                isAlert = true;
            }
        }
        else
        {
            if (isAlert)
            {
                turretSpriteRenderer.sprite = normalSprite;
                isAlert = false;
            }
        }
    }
}