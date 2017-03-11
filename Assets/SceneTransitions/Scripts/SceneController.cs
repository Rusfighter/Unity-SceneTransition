using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
    public SceneTransitionAnimation st;

    private void Start()
    {
        if (st != null)
        {
            st.reverseAnimation();
        }
    }
}
