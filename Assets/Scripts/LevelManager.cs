using System.Collections;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public string NextScene;

    public TargetArea[] targetAreas;
    private bool[] areaCompleted;

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultsText;
    private int placedContainer = 0;
    private int containerAmount;

    private float elapsedTime = 0f;
    private bool levelEnded = false;

    [Header("Star Time Limits")]
    public float twoStar;
    public float threeStar;
    public float fourStar;
    public float fiveStar;

    [Header("Star UI")]
    public GameObject[] stars;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        areaCompleted = new bool[targetAreas.Length];

        containerAmount = targetAreas.Length;
        counterText.text = $"Placed: {placedContainer}/{containerAmount}";
    }

    private void Update()
    {
        if (levelEnded) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();

        
    }

    public void NotifyTargetFilled(TargetArea area)
    {
        int index = System.Array.IndexOf(targetAreas, area);
        if (index < 0 || areaCompleted[index]) return;

        areaCompleted[index] = true;

        Debug.Log($"Target {index} filled!");

        UpdateContainerCounterUI();

        if (AllTargetsCompleted())
            EndLevel();
    }

    public void UpdateContainerCounterUI()
    {
        placedContainer++;
        counterText.text = $"Placed: {placedContainer}/{containerAmount}";
        //Play sound
    }

    private int minutes;
    private int seconds;

    private void UpdateTimerUI()
    {
        minutes = Mathf.FloorToInt(elapsedTime / 60f);
        seconds = Mathf.FloorToInt(elapsedTime % 60f);

        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private bool AllTargetsCompleted()
    {
        foreach (bool completed in areaCompleted)
        {
            if (!completed) return false;
        }
        return true;
    }

    
    private void EndLevel()
    {
        if (levelEnded) return;

        levelEnded = true;

        int starCount = CalculateStars(elapsedTime);
        ShowStars(starCount);

        Debug.Log($"Level complete in {elapsedTime:F1} seconds. Stars: {stars}");

        resultsText.gameObject.transform.parent.gameObject.SetActive(true);

        if (resultsText != null)
            resultsText.text = "Level Completed!\n" + $"{minutes:00}:{seconds:00}";

        StartCoroutine(LoadNextSceneAfterDelay(5f));

        //SceneManager.LoadScene(NextScene);
    }

    private void ShowStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }


    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(NextScene);
    }

    private int CalculateStars(float time)
    {
        if (time <= fiveStar)
            return 5;
        else if (time <= fourStar)
            return 4;
        else if (time <= threeStar)
            return 3;
        else if (time <= twoStar)
            return 2;
        else
            return 1;
    }
}
