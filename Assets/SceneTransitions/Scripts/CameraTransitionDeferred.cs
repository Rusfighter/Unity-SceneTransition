using UnityEngine;

public class CameraTransitionDeferred : CameraTransition
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_isActive && m_TransitionMaterial != null)
            Graphics.Blit(source, destination, m_TransitionMaterial);
    }
}
