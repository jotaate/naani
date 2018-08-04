using UnityEngine;

[System.Serializable]
public struct Button
{
    public string name;
    public int value;
    public KeyCode[] keys;
    [HideInInspector] public bool buttonWasPress;
    [HideInInspector] public bool buttonIsPress;
    [HideInInspector] public bool butttonIsRelease;
}

public class InputControl : MonoBehaviour
{
    [SerializeField] Button[] actions;
    public static InputControl Instance;
    public Button[] Actions { get { return actions; } }

    void Awake() { Instance = this; }

    bool ActionWasPress(Button action)
    {
        bool actionWasPress = false;
        foreach (KeyCode key in action.keys)
            actionWasPress = actionWasPress || Input.GetKeyDown(key);

        return actionWasPress;
    }

    bool ActionIsPress(Button action)
    {
        bool actionWasPress = false;
        foreach (KeyCode key in action.keys)
            actionWasPress = actionWasPress || Input.GetKey(key);

        return actionWasPress;
    }

    bool ActionIsRelease(Button action)
    {
        bool actionWasPress = false;
        foreach (KeyCode key in action.keys)
            actionWasPress = actionWasPress || Input.GetKeyUp(key);

        return actionWasPress;
    }

    void CheckInput()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].buttonWasPress = ActionWasPress(Actions[i]);
            Actions[i].buttonIsPress = ActionIsPress(Actions[i]);
            Actions[i].butttonIsRelease = ActionIsRelease(Actions[i]);
        }
    }

    void Update() { CheckInput(); }
}
