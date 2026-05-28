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