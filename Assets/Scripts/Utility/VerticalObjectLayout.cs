using UnityEngine;

public class VerticalObjectLayout : MonoBehaviour
{
    [Range(0,1)]
    public float _spacing = 0.02f;

#if UNITY_EDITOR
    private void OnValidate()
    {
        LayoutChildren();
    }
#endif

    public void LayoutChildren()
    {
        var localSpacing = _spacing / 10f;

        float scale = 1f / transform.childCount;

        float totalHeight = -0.5f + scale / 2f;

        scale -= localSpacing;

        foreach (Transform child in transform)
        {
            SetPosition(totalHeight, child);

            child.localScale = new Vector3(1, scale, child.localScale.z);

            totalHeight += scale + localSpacing;
        }
    }

    private static void SetPosition(float totalHeight, Transform child)
    {
        Vector3 childPosition = new Vector3(0f, -totalHeight, 0f);
        child.localPosition = childPosition;
    }
}
