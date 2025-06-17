using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance;

    public TargetArea[] targetAreas;
    private bool[] areaCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        areaCompleted = new bool[targetAreas.Length];
    }

    public void NotifyTargetFilled(TargetArea area)
    {
        int index = System.Array.IndexOf(targetAreas, area);
        if (index < 0 || areaCompleted[index]) return;

        areaCompleted[index] = true;
        Debug.Log($"Target {index} filled!");

        if (AllTargetsCompleted())
            EndLevel();
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
        Debug.Log("✅ All containers placed. Level Complete!");
        // Add event or load next level here
    }
}
