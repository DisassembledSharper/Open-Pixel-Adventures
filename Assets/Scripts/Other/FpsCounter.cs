using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public int Fps;
    private Text FpsText;
    private float Counter;
    private void Awake()
    {
        FpsText = GetComponent<Text>();
    }

    public void Update()
    {
        Counter += Time.deltaTime;

        if (Counter >= 1)
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            Fps = (int)current;
            FpsText.text = "FPS: " + Fps.ToString();
            Counter = 0;
        }
        
    }
}