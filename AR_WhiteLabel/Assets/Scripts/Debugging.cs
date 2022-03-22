
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
    
    bool isDebug;
    static Debugging instance;


    public Text fpsDisplay;
    public Text averageFPSDisplay;
    int framesPassed = 0;
    float fpsTotal = 0f;

    public Text minFPSDisplay, maxFPSDisplay;
    float minFPS = Mathf.Infinity;
    float maxFPS = 0f;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        //Application.targetFrameRate = 30;
    }

    void Update()
    {
        if(isDebug)
        {
            float fps = 1 / Time.unscaledDeltaTime;
            fpsDisplay.text = "FPS: " + fps;

            fpsTotal += fps;
            framesPassed++;
            averageFPSDisplay.text = "AvFPS: " + (fpsTotal / framesPassed);

            if (fps > maxFPS && framesPassed > 10)
            {
                maxFPS = fps;
                maxFPSDisplay.text = "Max: " + maxFPS;
            }

            if (fps < minFPS && framesPassed > 10)
            {
                minFPS = fps;
                minFPSDisplay.text = "Min: " + minFPS;
            }
        }
    }

    // Update is called once per frame
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Debug(GameObject gameObject)
    {
        isDebug = !isDebug;
        gameObject.SetActive(isDebug);
    }
}
