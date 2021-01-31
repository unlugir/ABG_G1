using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InputPatterns
{
    Left,
    Right,
    Up,
    Down
}

public class CombatReader : MonoBehaviour
{
    [SerializeField] Transform combatPanel;
    [SerializeField] float maxInputDelay;
    [SerializeField] UnityEvent<Combination> onPlayerInputSuccess;
    [SerializeField] UnityEvent onPlayerInputFail;
    float lastInputTime;
    bool inputInProgress;
    Vector2 keyDownPosition;
    Vector2 keyUpPosition;
    List<InputPatterns> lastInputs;
    void Start()
    {
        lastInputs = new List<InputPatterns>();
        combatPanel.gameObject.SetActive(false);
    }

    void Update()
    {

        if (lastInputTime >= maxInputDelay && !inputInProgress && lastInputs.Count != 0)
        {
            FinishInput();
            inputInProgress = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            inputInProgress = true;
            combatPanel.gameObject.SetActive(true);
            keyDownPosition = Input.mousePosition;
            Time.timeScale = 0.3f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            keyUpPosition = Input.mousePosition;
            var pattern = FindPattern(keyDownPosition, keyUpPosition);
            Debug.Log(pattern.ToString());
            lastInputs.Add(pattern);
            lastInputTime = 0;
            inputInProgress = false;
        }
        lastInputTime += Time.deltaTime;
      
    }

    InputPatterns FindPattern(Vector2 p1, Vector2 p2)
    {
        float horizontalDiff = p1.x - p2.x;
        float verticalDiff = p1.y - p2.y;
        // horizontal line
        if (Mathf.Abs(horizontalDiff) > Mathf.Abs(verticalDiff))
        {
            if (horizontalDiff > 0)
                return InputPatterns.Left;
            return InputPatterns.Right;
        }
        // vertical line
        else
        {
            if (verticalDiff > 0)
                return InputPatterns.Down;
            return InputPatterns.Up;
        }
    }
    void FinishInput()
    {
        Time.timeScale = 1f;
        combatPanel.gameObject.SetActive(false);
        Combination combination = InGameManager.Instance.combinationDatabase.FindCombination(lastInputs);
        if (combination != null)
        {
            onPlayerInputSuccess.Invoke(combination);
            Debug.Log("Combination Found");
        }
        else
        {
            onPlayerInputFail.Invoke();
            Debug.Log("Combination Not Found");
        }
        lastInputs.Clear();
    } 
}
