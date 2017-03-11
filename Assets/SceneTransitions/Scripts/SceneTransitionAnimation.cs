using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionAnimation : MonoBehaviour {

    [SerializeField]
    private AnimationCurve m_CutoffCurve;

    [SerializeField]
    private AnimationCurve m_FadeCurve;

    [SerializeField]
    private Camera m_Camera;

    [Tooltip("Transition Material")]
    public Material m_Material;

    [SerializeField]
    [Header("Length of Animation in seconds")]
    private float m_Length;
    [SerializeField]
    private bool m_Active = false;
    private float m_CurrentTime = 0;

    private CameraTransition m_CameraTransition;

    private float m_StartCutoff;
    private float m_StartFade;

    [SerializeField]
    [Header("Reverse Animation?")]
    private bool m_isReverse = false;

    [Header("Load Scene after transitions")]
    [SerializeField]
    private string m_SceneName;

    public delegate void OnAnimationFinished();
    public OnAnimationFinished onTransitionFinished;

    public void Awake()
    {
        if (m_Camera == null)
            m_Camera = Camera.main;

        m_CameraTransition = m_Camera.GetComponent<CameraTransition>();

        if (m_CameraTransition == null)
            Debug.LogError("Attach CameraTransition to the (main) Camera");

        if (m_isReverse) m_CurrentTime = m_Length;
    }

    private void OnEnable()
    {
        if (m_Active)
            m_CameraTransition.setMaterial(m_Material);

        m_StartCutoff = m_Material.GetFloat("_Cutoff");
        m_StartFade = m_Material.GetFloat("_Fade");
    }

    private void Update()
    {
        if (!m_Active) return;
        if (m_CurrentTime > m_Length || m_CurrentTime < 0)
        {
            m_Active = false;
            m_isReverse = false;
            m_CurrentTime = 0;

            if (onTransitionFinished != null){
                onTransitionFinished();
            }
            else if(m_SceneName != null && m_SceneName.Length != 0) {
                SceneManager.LoadScene(m_SceneName);
            }

            return;
        }

        if (!m_CameraTransition.m_isActive)
            m_CameraTransition.m_isActive = true;

        if (m_isReverse) m_CurrentTime -= Time.deltaTime;
        else m_CurrentTime += Time.deltaTime;

        float t = m_CurrentTime / m_Length;

        float cuttOff = Mathf.Clamp01(m_CutoffCurve.Evaluate(t));
        float fade = Mathf.Clamp01(m_FadeCurve.Evaluate(t));

        m_Material.SetFloat("_Cutoff", cuttOff);
        m_Material.SetFloat("_Fade", fade);
    }

    public void reverseAnimation()
    {
        m_CameraTransition.setMaterial(m_Material);
        m_CurrentTime = m_Length;
        m_Active = true;
        m_isReverse = true;
    }

    public void startAnimation()
    {
        m_CameraTransition.setMaterial(m_Material);
        m_CurrentTime = 0;
        m_Active = true;
        m_isReverse = false;
    }

    public void Resume()
    {
        m_Active = true;
    }

    public void Pause()
    {
        m_Active = false;
    }


    private void OnDisable()
    {
        m_Material.SetFloat("_Cutoff", m_StartCutoff);
        m_Material.SetFloat("_Fade", m_StartFade);
    }

}
