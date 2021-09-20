using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour {

    public RectTransform OptionsPanel;
    private Button button;
    private bool isActive;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        isActive = false;
    }

    void Update()
    {
        if (isActive)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void TaskOnClick()
    {
        isActive = !isActive;
        OptionsPanel.gameObject.SetActive(isActive);
    }
}
