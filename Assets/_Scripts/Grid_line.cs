using UnityEngine;

public class Grid_line : MonoBehaviour
{
    [Header("Grid Line")]
    [SerializeField] private Material gridLineMaterial;
    [SerializeField] private Color gridLineColor = new Color(1f, 1f, 1f, 0.15f);
    [SerializeField] private Vector3 offset;
    [SerializeField] private float gridSize = 10f / 16f;
    private GameObject parent;
    void Start()
    {
        CreateGridLines();
    }
    void CreateGridLines()
    {
        parent = new GameObject("GridLines");
        parent.transform.SetParent(transform);
        for (float x = 0; x <=10; x+=gridSize)
        {
            CreateLine(new Vector3(x, 0, -10) + offset, new Vector3(x, 0, 0) + offset);
        }
        for (float z = 0; z >= -10; z-=gridSize)
        {
            CreateLine(new Vector3(10, 0, z) + offset, new Vector3(0, 0, z) + offset);
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GridLine");
        lineObj.transform.SetParent(parent.transform);
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = gridLineMaterial;
        lineRenderer.startColor = gridLineColor;
        lineRenderer.endColor = gridLineColor;
        lineRenderer.useWorldSpace = true;
    }
}
