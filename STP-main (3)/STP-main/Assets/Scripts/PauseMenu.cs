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
                EnableText(_index);
            }
        }
    }

    private void Start() {
        Index = 0;
        EnableText(Index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (Index-- < 0) 
                Index = texts.Length - 1;
            else
                Index--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (Index++ > texts.Length - 1)
                Index = 0;
            else
                Index++;
        }
    }

    private void EnableText(int index) {
        foreach (var text in texts) 
            text.Blink = false;

        texts[index].Blink = true;
        Debug.Log("Enabled text with index of: " + index);
    }
}
