using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; 
    private bool isPaused = false;
    private bool canPause = false; 
    private bool isVictoryScreenActive = false; 
    [SerializeField] private AudioClip pauseSFX;

    void Start()
    {
       
        pauseMenuPanel.SetActive(false);

        
        StartCoroutine(EnablePauseAfterDelay(0.1f));
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && canPause && !isVictoryScreenActive)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; 
        SoundFXManager.instance.PlaySoundFXClip(pauseSFX, transform, 1f);
        pauseMenuPanel.SetActive(true); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; 
        pauseMenuPanel.SetActive(false); 
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }

    private System.Collections.IEnumerator EnablePauseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        canPause = true; 
    }

    
    public void DisablePauseAfterVictory()
    {
        isVictoryScreenActive = true; 
    }
}
