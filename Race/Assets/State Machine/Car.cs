using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private void Start()
    {
        InitializationStateMachine();
    }

    private void InitializationStateMachine()
    {
        _wheelModule = new WheelCarModule(_chunkManager, _wheels, _chunkSpeed);
        _carRotateModule = new CarRotateModule(_chunkController, transform);
        _wheelRotateModule = new WheelRotateModule(_chunkController, _turnWheels);
        State idleState = new IdleStateForCar(transform);
        State runState = new RunStateForCar(_wheelModule,_carRotateModule, transform);

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

public class IdleStateForCar : State
{
    private Transform _car;
    private Sequence _carSequence;
    private Vector3 _startScale;

    public IdleStateForCar(Transform car)
    {
        _car = car;
        _startScale = car.localScale;
    }

    public override void OnStateEnter()
    {
        _carSequence = DOTween.Sequence();
        _carSequence.Append(_car.DOShakeScale(0.2f, 0.01f));
        _carSequence.SetLoops(-1, LoopType.Yoyo);
    }

    public override void OnStateExit()
    {
        _carSequence.Kill();
        _car.transform.localScale = _startScale;
    }
}

public class WheelCarModule
{
    private readonly ChunkManager _chunkManager;
    private readonly Transform[] _wheels;
    public ChunkSpeed _chunkSpeed;


    private bool _isWheelRotate;
    private float _currentSpeedModification;

    public WheelCarModule(ChunkManager chunkManager, Transform[] wheels, ChunkSpeed chunkSpeed)
    {
        _chunkSpeed = chunkSpeed;
        _chunkManager = chunkManager;
        _wheels = wheels;
    }

    public void Tick()
    {
        if (_isWheelRotate)
        {
            _currentSpeedModification += Time.deltaTime;
        }
        else
        {
            _currentSpeedModification -= Time.deltaTime;
        }

        _currentSpeedModification = Mathf.Clamp(_currentSpeedModification, 0f, 1f);
        float currentSpeed = _chunkSpeed.CurrentSpeed * _currentSpeedModification;
        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(currentSpeed, 0, 0, Space.Self);
        }
    }

    public void StartWheel()
    {
        _isWheelRotate = true;
    }

    public void StopWheel()
    {
        _isWheelRotate = false;
    }
}

public class WheelRotateModule
{
    private readonly ChunkController _chunkController;
    private readonly Transform[] _turnWheels;

    private float SteerAngle = 25f;
    private float SteerSpeed = 10f;

    public WheelRotateModule(ChunkController chunkController, Transform[] wheels)
    {
        _chunkController = chunkController;
        _turnWheels = wheels;
    }

    public void Tick()
    {
        float direction = -_chunkController.Direction;
        float targetAngle = direction * SteerAngle;

        foreach (var wheel in _turnWheels)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            wheel.localRotation = Quaternion.Lerp(wheel.localRotation, targetRotation, SteerSpeed * Time.deltaTime);
        }
    }
}

public class CarRotateModule
{
    private readonly ChunkController _chunkController;
    private readonly Transform _car;

    private const float Angle = 15f;
    private const float Speed = 8f;

    public CarRotateModule(ChunkController chunkController, Transform car)
    {
        _chunkController = chunkController;
        _car = car;
    }

    public void Tick()
    {
        float direction = -_chunkController.Direction;
        Quaternion targetRotation = Quaternion.Euler(0f, direction * Angle, 0f);
        _car.transform.localRotation =
            Quaternion.Lerp(_car.transform.localRotation, targetRotation, Speed * Time.deltaTime);
    }
}

public class RunStateForCar : State
{
    private readonly WheelCarModule _wheelModule;
    private readonly CarRotateModule _carRotateModule;
    private readonly Transform _car;

    public RunStateForCar(WheelCarModule wheelModule, CarRotateModule carRotateModule, Transform car)
    {
        _wheelModule = wheelModule;
        _carRotateModule = carRotateModule;
        _car = car;
    }

    public override void OnStateEnter()
    {
        _car.transform.DOLocalRotate(new Vector3(-6, 0, 0), 0.4f).OnComplete(() =>
        {
            _car.transform.DOLocalRotate(Vector3.zero, 0.2f);
        });
        _wheelModule.StartWheel();
    }

    public override void OnStateExit()
    {
        _wheelModule.StopWheel();
    }

    public override void Tick()
    {
        _carRotateModule.Tick();
    }
}