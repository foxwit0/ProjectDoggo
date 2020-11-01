using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textMesh = null;

    void Start() 
    {
        if(textMesh == null)
            Debug.LogWarning("Missing TMP_Text object in inspector of TimerUI.cs");
        textMesh.text = CounterToTextTimer(0f);
    }
    
    public void UpdateTimer(float counter) //Compteur en secondes
    {
        textMesh.text = CounterToTextTimer(counter);
    }

    private string CounterToTextTimer(float counter)
    {
        float seconds = counter % 60;
        int minutes = (int) (counter / 60);

        return (minutes*10000 + seconds*100).ToString("00:00:00");
    }

}
