using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class MenuController : MonoBehaviour
{
    


    void Start()
    {
        Time.timeScale = 1f;

    }

    
    public void LoadFirstLevel()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

    }

    
    public void ExitGame()
    {
        
        Application.Quit();
    }
}
