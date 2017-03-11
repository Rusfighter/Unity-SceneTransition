using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {
    public SceneTransitionAnimation sta1;
    public SceneTransitionAnimation sta2;
    public SceneTransitionAnimation sta3;
    public SceneTransitionAnimation sta4;


    public void Fade()
    {
        sta1.startAnimation();
        sta1.onTransitionFinished += loadNextScene;
    }

    public void Transition1()
    {
        sta2.startAnimation();
        sta2.onTransitionFinished += loadNextScene;
    }

    public void Transition2()
    {
        sta3.startAnimation();
        sta3.onTransitionFinished += loadNextScene;
    }

    public void Transition3()
    {
        sta4.startAnimation();
        sta4.onTransitionFinished += loadNextScene;
    }

    private void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
