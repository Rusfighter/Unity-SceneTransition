using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTransition : MonoBehaviour
{
    [SerializeField]
    protected Material m_TransitionMaterial;

    public bool m_isActive = false;

    protected Camera m_Camera;
    private RenderTexture m_Rtex;

    protected virtual void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    public void setMaterial(Material mat)
    {
        m_TransitionMaterial = mat;
    }
}
