using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBarUI : MonoBehaviour
{
    private RectTransform progressBar;
    [SerializeField] private RectTransform playerUI = null;
    [SerializeField] private Slider plasmaUI = null;

    void Start()
    {
        #region Warnings
        if(playerUI == null)
            Debug.LogWarning("Missing PlayerUI object in inspector of LevelProgressBarUI.cs");
        if(plasmaUI == null)
            Debug.LogWarning("Missing PlasmaUI object in inspector of LevelProgressBarUI.cs");
        #endregion Warnings

        progressBar = transform.GetComponent<RectTransform>();      
    }

    public void UpdatePlayerPosition(Transform playerPos, float levelHeight)
    {
        float progressBarHeight = progressBar.sizeDelta.y;
        float newPlayerPosUI = Mathf.Clamp((playerPos.position.y / levelHeight) * progressBarHeight, -progressBarHeight + playerUI.sizeDelta.y / 2f, 0f);  
        playerUI.anchoredPosition = new Vector2(0f, newPlayerPosUI);
    }

    public void UpdatePlasmaPosition(Transform plasmaPos, float levelHeight)
    {
        plasmaUI.value = -plasmaPos.position.y / levelHeight;
    }
}
