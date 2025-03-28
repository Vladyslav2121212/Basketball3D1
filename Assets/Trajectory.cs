using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int linePoints = 30;
    public float timeStep = 0.05f;
    public Transform hoop; 
    public Transform startPosition; 

    private Camera mainCamera;
    private Vector3 throwDirection; 
    private bool isVisible = true; 

    private void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        mainCamera = Camera.main;

        
        if (hoop != null)
        {
            throwDirection = (hoop.position - startPosition.position).normalized;
        }

        DrawTrajectory(startPosition.position);
    }

    public void MoveTrajectory()
    {
        if (mainCamera == null || !isVisible) return;

       
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        
        throwDirection = (mouseWorldPosition - startPosition.position).normalized;

        
        DrawTrajectory(startPosition.position);
    }

    public void HideTrajectory()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
        isVisible = false;
    }

    public void ShowTrajectory()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
        isVisible = true;
    }

    public void DrawTrajectory(Vector3 startPos)
    {
        if (lineRenderer == null || !isVisible) return;

        Vector3[] points = new Vector3[linePoints];

       
        Vector3 velocity = throwDirection * 10f + Vector3.up * 5f;

        for (int i = 0; i < linePoints; i++)
        {
            float time = i * timeStep;
            points[i] = startPos + velocity * time + 0.5f * Physics.gravity * time * time;
        }

        lineRenderer.positionCount = linePoints;
        lineRenderer.SetPositions(points);
    }

    public Vector3 GetThrowDirection()
    {
        return throwDirection;
    }
}






