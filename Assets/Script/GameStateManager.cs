using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        Tutorial,
        CountdownToStart,
        Playing,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;

    private void Awake()
    {
        Instance = this;
        state = State.Tutorial;
    }


    void Update()
    {
        switch(state)
        {
            case State.Tutorial:
                if(Input.touchCount > 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(countdownToStartTimer <= 0)
                {
                    state = State.Playing;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.Playing:
                if(Controller.Instance.GetHealth() <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool isTutorial()
    {
        return state == State.Tutorial;
    }

    public bool isCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public bool isPlaying()
    {
        return state == State.Playing;
    }

}
