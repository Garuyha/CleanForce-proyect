using UnityEngine;

public class ResetScreenController : MonoBehaviour
{    

    public GameObject resetScreen;
    public BestTimesManager bestTimesManager;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetScreen.SetActive(false);
    }

    public void ConfirmScoreReset()
    {
        bestTimesManager.ResetBestTimes();
        ToggleResetScreen();
    }
    
    public void ToggleResetScreen()
    {
        resetScreen.SetActive(!resetScreen.activeSelf);
    }
}
