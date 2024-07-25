using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDirection : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private void Update()
    {
        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        DrawLine(worldPoint.Value);
    }
private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = {
transform.position,worldPoint};
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
        Input.mousePosition.x,
        Input.mousePosition.y,
        Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
       Input.mousePosition.x,
       Input.mousePosition.y,
       Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
}
