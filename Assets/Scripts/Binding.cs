using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Binding : MonoBehaviour {

    public PlayerController player;
    public RectTransform BindPrompt;

    private Button button;
    private KeyCode keyCode;
    private bool isBinding;

    void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        isBinding = false;
    }
	
	void Update () {
        BindPrompt.gameObject.SetActive(isBinding);

        if (isBinding)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {

                if (Input.GetKeyDown(kcode)) {

                    if (kcode == KeyCode.Escape)
                    {
                        isBinding = false;
                        break;
                    }

                    player.Jump = kcode;
                    PlayerPrefs.SetString("Jump", kcode.ToString());
                    PlayerPrefs.Save();
                    isBinding = false;
                }

            }

        }

        GetComponentInChildren<Text>().text = player.Jump.ToString();

    }

    void TaskOnClick()
    {
        isBinding = !isBinding;
    }
}
