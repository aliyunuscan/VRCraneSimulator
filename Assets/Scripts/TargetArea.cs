using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TargetArea : MonoBehaviour
{
    [Tooltip("Yük alanına girip kancadan ayrıldıktan sonra başlayacak süre (s)")]
    public float countdownTime = 5f;

    [Tooltip("Yük başarılı yerleştirildiğinde tetiklenecek event")]
    public UnityEvent OnLevelComplete;

    private Coroutine countdownRoutine;
    private Load currentLoad;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Load")) return;

        var load = other.GetComponent<Load>();
        if (load == null) return;

        if (!load.IsHooked)
        {
            currentLoad = load;
            countdownRoutine = StartCoroutine(CountdownAndComplete());
        }

        load.OnHooked += HandleLoadHooked;
        load.OnUnhooked += HandleLoadUnhooked;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Load")) return;

        if (other.GetComponent<Load>() == currentLoad)
            CancelCountdown();

        var load = other.GetComponent<Load>();
        if (load != null)
        {
            load.OnHooked -= HandleLoadHooked;
            load.OnUnhooked -= HandleLoadUnhooked;
        }
    }

    private IEnumerator CountdownAndComplete()
    {
        float t = countdownTime;
        while (t > 0f)
        {
            if (currentLoad == null || currentLoad.IsHooked)
                yield break;

            Debug.Log($"[TargetPointTrigger] Countdown: {t:F2} seconds remaining");

            t -= Time.deltaTime;
            yield return null;
        }

        OnLevelComplete?.Invoke();
    }

    private void HandleLoadHooked()
    {
        CancelCountdown();
    }

    private void HandleLoadUnhooked()
    {
        if (currentLoad != null)
            countdownRoutine = StartCoroutine(CountdownAndComplete());
    }

    private void CancelCountdown()
    {
        if (countdownRoutine != null)
        {
            StopCoroutine(countdownRoutine);
            countdownRoutine = null;
        }
        currentLoad = null;
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete");
    }
}
