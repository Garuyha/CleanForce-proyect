using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CambiarEscena : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);
    }
}
