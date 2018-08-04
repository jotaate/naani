using UnityEngine;
using System.Collections;

public class GameStates : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] States state;
    Fade[] uiElements;

    public enum States { NONE, PAUSE, IN_GAME}
    public static GameStates Instance;

    void Awake() { Instance = this; }

    void Start()
    {
        state = States.PAUSE;
        uiElements = FindObjectsOfType<Fade>();

        // init all ui elements as visible
        foreach (Fade f in uiElements)
        {
            f.In_ = true;
            f.Out_ = false;
        }
    }

    void Update()
    {
        foreach (Button key in InputControl.Instance.Actions)
            if (key.buttonWasPress)
                InputToAction(key.value);
    }

    void StartGame()
    {
        // Play sound
        foreach (Fade f in uiElements)
            f.Trigger(false, 0.4f);
        StartCoroutine(ChangeState(States.IN_GAME));
    }

    /// <summary>
    /// Map input to a action that depends of game state
    /// </summary>
    /// <param name="input"></param>
    public void InputToAction(int input)
    {
        switch (input)
        {
            case 0: Vertical(); break;
            case 1: Horizontal(); break;
            default: break;
        }
    }

    /// <summary>
    /// Acction to excecute when X button is pressed
    /// </summary>
    void Vertical()
    {
        switch (state)
        {
            case States.PAUSE:
                StartGame();
                break;
        }
    }

    /// <summary>
    /// Acction to excecute when C button is pressed
    /// </summary>
    void Horizontal()
    {

    }

    IEnumerator ChangeState(States s)
    {
        state = States.NONE;
        yield return new WaitForSeconds(2);
        state = s;
        yield break;
    }
}