using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public BlinkingText[] texts;
    private int _index;
    private int Index 
    {
        get => _index;
        set
        {
            if (_index != value)
            {
                _index = value;
                _index = Mathf.Clamp(_index, 0, texts.Length - 1);
                EnableText(_index);
            }
        }
    }

    private void Start() {
        EnableText(Index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Index = (Index - 1 + texts.Length) % texts.Length;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Index = (Index + 1) % texts.Length;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (Index == 0) EventManager.Instance.Publish(GameEvents.EventType.ResumeBtnClick);
            else if (Index == 1) EventManager.Instance.Publish(GameEvents.EventType.ExitBtnClick);
        }
    }

    private void EnableText(int index) {
        foreach (var text in texts) 
            text.Blink = false;

        texts[index].Blink = true;
    }
}