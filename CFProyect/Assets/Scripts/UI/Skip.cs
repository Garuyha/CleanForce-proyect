using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class Skip : MonoBehaviour
{
    
    public Button playButton;
    public Button exitButton;

    void Start()
    {
        
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    
    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    

}