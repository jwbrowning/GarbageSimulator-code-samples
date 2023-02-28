using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// reusable, event-driven class to manage a cutscene with Dialogue and animation in Unity
// contains a list of "Cutscene Events" which will get triggered in order, and call the NextEvent method when they are done
public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    private class CutsceneEvent
    {
        public string name;
        public UnityEvent start;
        public UnityEvent end;
    }

    [SerializeField] private List<CutsceneEvent> events;
    private int current = 0;

    void Start()
    {
        GameObject.Find("SoundManager").GetComponent<MusicManager>().PlayIntro();
        if (events.Count > 0) events[0].start.Invoke();
    }

    public void NextEvent()
    {
        events[current].end.Invoke();
        current++;
        if (current >= events.Count) return;
        events[current].start.Invoke();
    }
}
