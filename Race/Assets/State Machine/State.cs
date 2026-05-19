using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State
{
    public List<ITransitionState> Transition { get; private set; } = new List<ITransitionState>();

    public virtual void Tick()
    {
    }

    public virtual void FixedTick()
    {
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }

    public void AddTransition(ITransitionState transition)
    {
        Transition.Add(transition);
    }

    public void RemoveTransition(ITransitionState transition)
    {
        if (Transition.Contains(transition))
        {
            Transition.Remove(transition);
        }
    }

    public void InitializeTransitions()
    {
        foreach (ITransitionState transition in Transition)
        {
            transition.Initialize();
        }
    }

    public void DeInitializeTransitions()
    {
        foreach (ITransitionState transition in Transition)
        {
            transition.DeInitialize();
            if (Transition.Contains(transition) == false)
            {
                DeInitializeTransitions();
                return;
            }
        }
    }
}

public class StateTransition : ITransitionState
{
    public State StateTo { get; private set; }
    public StateCondition Condition { get; }
    
    public event Action OnTransitionDeInitialized;

    public StateTransition(State stateTo, StateCondition condition)
    {
        StateTo = stateTo;
        Condition = condition;
    }
    
    public void Initialize()
    {
        Condition.Initialize();
    }
    public void DeInitialize()
    {
        Condition.DeInitialize();
        OnTransitionDeInitialized?.Invoke();
    }
}

public interface ITransitionState
{
    public State StateTo { get; }
    public StateCondition Condition { get; }
    public void Initialize();
    public void DeInitialize();


}

public abstract class StateCondition
{
    public abstract bool IsConditionSatisfied();

    public virtual void Tick()
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void DeInitialize()
    {
    }
}

public class StateMachine
{
    public State CurrentState { get; private set; }

    public StateMachine(State state)
    {
        SetState(state);
    }

    public void Tick()
    {
        int currentTransition = IsTransitionsCondition();
        if (currentTransition == -1)
        {
            CurrentState.Tick();
        }
        else
        {
            SetState(CurrentState.Transition[currentTransition].StateTo);
        }
    }

    public void FixedTick()
    {
        CurrentState.FixedTick();
    }

    public void SetState(State NextState)
    {
        CurrentState?.DeInitializeTransitions();
        CurrentState?.OnStateExit();

        CurrentState = NextState;
        CurrentState.OnStateEnter();
        CurrentState.InitializeTransitions();
    }

    private int IsTransitionsCondition()
    {
        List<ITransitionState> currentList = CurrentState.Transition;
        for (int i = 0; i < currentList.Count; i++)
        {
            StateCondition currentCondition = currentList[i].Condition;
            currentCondition.Tick();
            if (currentCondition.IsConditionSatisfied())
            {
                return i;
            }
        }
        
        return -1;
    }
}