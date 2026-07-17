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

public interface IShieldForBuff
{
    void SetShieldActive(bool active);
    void FlashShield();
    void ResetShieldVisual();
}
