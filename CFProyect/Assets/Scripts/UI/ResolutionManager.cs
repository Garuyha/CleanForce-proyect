using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resolutionText;
    [SerializeField] private Button applyButton;
    [SerializeField] private int[] widths = { 1280, 1366, 1600, 1920, 2560 };
    [SerializeField] private int[] heights = { 720, 768, 900, 1080, 1440 };
    
    private int currentResolutionIndex = 0;
    private int appliedResolutionIndex = 0;

    private void Start()
    {
        LoadResolution();
        UpdateResolutionText();
    }

    public void IncreaseResolution()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % widths.Length;
        UpdateResolutionText();
    }

    public void DecreaseResolution()
    {
        currentResolutionIndex = (currentResolutionIndex - 1 + widths.Length) % widths.Length;
        UpdateResolutionText();
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(widths[currentResolutionIndex], heights[currentResolutionIndex], Screen.fullScreen);
        appliedResolutionIndex = currentResolutionIndex;
        PlayerPrefs.SetInt("ResolutionIndex", appliedResolutionIndex);
        PlayerPrefs.Save();
        UpdateApplyButton();
    }

    private void LoadResolution()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            appliedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        }
        else
        {
            appliedResolutionIndex = GetCurrentResolutionIndex();
        }
        currentResolutionIndex = appliedResolutionIndex;
    }

    private int GetCurrentResolutionIndex()
    {
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;
        
        for (int i = 0; i < widths.Length; i++)
        {
            if (widths[i] == width && heights[i] == height)
            {
                return i;
            }
        }
        return 0; // Si la resolución actual no está en la lista, seleccionar la primera por defecto
    }

    private void UpdateResolutionText()
    {
        resolutionText.text = $"{widths[currentResolutionIndex]} x {heights[currentResolutionIndex]}";
        UpdateApplyButton();
    }

    private void UpdateApplyButton()
    {
        applyButton.gameObject.SetActive(currentResolutionIndex != appliedResolutionIndex);
    }
}
