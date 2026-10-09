using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ComboSystem : MonoBehaviour
{
    [SerializeField] private List<ComboData> combos;
    [SerializeField] private float inputWindow = 0.6f;

    private readonly List<ComboTracker> trackers = new List<ComboTracker>();
    private HashSet<KeyCode> watchedKeys;
    private AudioSource audioSource;
    private float lastInputTime;
    private ComboData pending;
    private float pendingTime;

    public event Action<ComboData> ComboPerformed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (ComboData combo in combos)
        {
            trackers.Add(new ComboTracker(combo));
        }
        watchedKeys = new HashSet<KeyCode>(combos.SelectMany(c => c.Sequence));
    }

    private void Update()
    {
        if (Time.time - lastInputTime > inputWindow)
        {
            ResetAll();
        }

        foreach (KeyCode key in watchedKeys)
        {
            if (Input.GetKeyDown(key))
            {
                HandleKey(key);
            }
        }

        if (pending != null && Time.time - pendingTime > inputWindow)
        {
            Play(pending);
        }
    }

    private void HandleKey(KeyCode key)
{
    lastInputTime = Time.time;

    if (pending != null)
    {
        pendingTime = Time.time;
    }

    foreach (ComboTracker tracker in trackers)
    {
        if (!tracker.Feed(key)) continue;

        if (HasLongerComboInProgress(tracker))
        {
            pending = tracker.Data;
            pendingTime = Time.time;
        }
        else
        {
            Play(tracker.Data);
        }
    }
}

    private bool HasLongerComboInProgress(ComboTracker completed)
    {
        return trackers.Any(t => t != completed && t.Data.Length > completed.Data.Length && t.Progress >= completed.Data.Length);
    }

    private void Play(ComboData combo)
    {
        pending = null;
        ResetAll();
        audioSource.PlayOneShot(combo.Clip);
        ComboPerformed?.Invoke(combo);
        Debug.Log(combo.ComboName);
    }

    private void ResetAll()
    {
        foreach (ComboTracker tracker in trackers)
        {
            tracker.Reset();
        }
    }
}