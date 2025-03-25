using UnityEngine;

[ExecuteInEditMode]
public class HookLineRender : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform RodPosition;
    [SerializeField] private Transform HookPosition;

    void Update()
    {
        lineRenderer.SetPosition(0, RodPosition.transform.position);
        lineRenderer.SetPosition(1, HookPosition.transform.position);
    }

}
