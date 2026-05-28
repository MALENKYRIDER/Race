using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform[] _wheels;
    public Transform[] _turnWheels;
    public ChunkManager _chunkManager;
    public ChunkSpeed _chunkSpeed;
    public ChunkController _chunkController;

    private CarRotateModule _carRotateModule;
    private StateMachine _stateMachine;
    private WheelCarModule _wheelModule;
    private WheelRotateModule _wheelRotateModule;
    private CarCrushState _crushState;
    private State _runState;

    private void Start()
    {
        InitializationStateMachine();
    }

    public void Crush()
    {
        _stateMachine.SetState(_crushState);
    }

    private void InitializationStateMachine()
    {
        
        _wheelModule = new WheelCarModule(_chunkManager, _wheels, _chunkSpeed);
        _carRotateModule = new CarRotateModule(_chunkController, transform);
        _wheelRotateModule = new WheelRotateModule(_chunkController, _turnWheels);
        State idleState = new IdleStateForCar(transform);
        State runState = new RunStateForCar(_wheelModule,_carRotateModule, transform);
        _crushState = new CarCrushState(_wheelModule, _carRotateModule, transform);
        
        _runState = runState;
        
        _crushState.AddTransition(new StateTransition(_runState, new FuncCondition(() => _crushState.IsFinish)));

        idleState.AddTransition(new StateTransition(runState,
            new FuncCondition(() => _chunkSpeed.CurrentSpeed != 0)));
        _stateMachine = new StateMachine(idleState);
    }

    private void Update()
    {
        _stateMachine.Tick();
        _wheelModule.Tick();
        _wheelRotateModule.Tick();
    }
}
