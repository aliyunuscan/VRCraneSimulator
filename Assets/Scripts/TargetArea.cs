using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetArea : MonoBehaviour
{
    public string requiredColor; // e.g., "Red", "Blue", "Green"
    public float countdownTime = 2f;

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
        if (load == null || load.containerColor != requiredColor) return;

        currentLoad = load;
        countdownRoutine = StartCoroutine(CountdownAndNotify());
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentLoad != null && other.GetComponent<Load>() == currentLoad)
        {
            StopCoroutine(countdownRoutine);
            currentLoad = null;
        }
    }

    private System.Collections.IEnumerator CountdownAndNotify()
    {
        float t = countdownTime;

        while (t > 0f)
        {
            if (currentLoad == null)
                yield break;

            t -= Time.deltaTime;
            yield return null;
        }

        TargetManager.Instance.NotifyTargetFilled(this);
    }
}
