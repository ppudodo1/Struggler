using UnityEngine;

public class ParallaxMaterial : MonoBehaviour
{
    [SerializeField] private Transform player; 
    private Renderer materialRenderer;
    public float parallaxEffectMultiplier = 0.1f;
    private Vector3 previousPlayerPosition;

    private void Start(){
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned to the ParallaxMaterial script.");
        }

        materialRenderer = GetComponent<Renderer>();
        previousPlayerPosition = player.position;
    }

    private void Update()
    {
        if (player == null || materialRenderer == null) return;

        // Calculate the player's movement since the last frame
        float deltaX = player.position.x - previousPlayerPosition.x;

        Vector2 currentOffset = materialRenderer.material.mainTextureOffset;
        currentOffset.x += deltaX * parallaxEffectMultiplier;
        materialRenderer.material.mainTextureOffset = currentOffset;

        previousPlayerPosition = player.position;
    }
}
