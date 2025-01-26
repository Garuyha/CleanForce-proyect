using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class MenuController : MonoBehaviour
{
    
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        
        playButton.onClick.AddListener(OnPlayButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    
    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    
    void OnExitButtonClicked()
    {
        
        Application.Quit();
    }
}
