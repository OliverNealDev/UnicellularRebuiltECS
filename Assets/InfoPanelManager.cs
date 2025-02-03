using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    public GameObject InfoPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InfoPanel.SetActive(true);
    }

    public void OnCloseInfoPanel()
    {
        InfoPanel.SetActive(false);
    }

    public void OnToggleInfoPanel()
    {
        if (InfoPanel.activeSelf)
        {
            InfoPanel.SetActive(false);
        }
        else
        {
            InfoPanel.SetActive(true);
        }
    }
}
