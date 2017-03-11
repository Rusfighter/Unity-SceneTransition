using UnityEngine;

[ExecuteInEditMode]
public class CameraTransitionFast : CameraTransition {

    private RenderTexture rTex;

    private enum Downscale
    {
        NONE = 1,
        TWICE = 2,
        THIRD = 3,
        FOUR = 4
    };

    [SerializeField]
    private Downscale downScaleFactor = Downscale.NONE;

    private enum DepthSize
    {
        ZERO = 0,
        SIXTEEN = 16,
        TWENTYFOUR = 24
    }

    [SerializeField]
    private DepthSize depthSize = DepthSize.SIXTEEN;
 

    protected override void Awake()
    {
        base.Awake();
        rTex = new RenderTexture((int)(Screen.width / (int)downScaleFactor), (int)(Screen.height / (int)downScaleFactor),
            (int)depthSize, RenderTextureFormat.Default);
    }

    protected void OnPostRender()
    {
        m_Camera.targetTexture = null;
        if (m_isActive && m_TransitionMaterial != null)
            Graphics.Blit(rTex, null as RenderTexture, m_TransitionMaterial);
        else
            Graphics.Blit(rTex, null as RenderTexture);

        rTex.DiscardContents();
    }

    protected void OnPreRender()
    {
        m_Camera.targetTexture = rTex;
    }
}