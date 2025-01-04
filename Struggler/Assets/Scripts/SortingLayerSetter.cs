using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SortingLayerSetter : MonoBehaviour
{
    public string sortingLayerName = "Foreground"; 
    public int sortingOrder = 0;           

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.sortingLayerName = sortingLayerName;
            meshRenderer.sortingOrder = sortingOrder;
        }
        else
        {
            Debug.LogWarning("No MeshRenderer found on this GameObject!");
        }
    }
}
