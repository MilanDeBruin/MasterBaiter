using UnityEngine;

[ExecuteInEditMode]
public class HookCamera : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Transform m_CameraTarget;

    void Update()
    {
        m_Camera.transform.position = new Vector3(0, m_CameraTarget.position.y);
    }
}
