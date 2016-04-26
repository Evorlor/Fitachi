using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AdventureCoin : MonoBehaviour
{

    private const string InvisiblizeMethodName = "Invisiblize";
    private const float InvisiblizeRate = 0.01f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        AdventureStats.Gold++;
        InvokeRepeating(InvisiblizeMethodName, InvisiblizeRate, InvisiblizeRate);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Invisiblize()
    {
        float newAlpha = spriteRenderer.color.a - 0.01f;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
    }
}
