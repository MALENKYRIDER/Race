using System;
using System.Runtime.InteropServices;

public interface ISpeedForBuff
{
    void AddSpeed(float speed);
    void RemoveSpeed(float speed);
}

public interface IMultiplierSpeedForBuff
{
    void AddSpeedMultiplier(float speedMultiplier);
    void RemoveSpeedMultiplier(float speedMultiplier);
}

public interface IInvisibleForBuff
{
    void SetInvisible(bool invisible);
}