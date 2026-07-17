namespace Buff.ShieldBuff
{
    using UnityEngine;

    public class ShieldBuff : IBuff
    {
        private readonly IShieldForBuff _shield;
        private float _elapsedTime;
        private float _blinkTimer;

        public int Duration => 5;

        public ShieldBuff(IShieldForBuff shield)
        {
            _shield = shield;
        }

        public void StartBuff()
        {
            _elapsedTime = 0f;
            _blinkTimer = 0f;
            _shield.SetShieldActive(true);
            _shield.ResetShieldVisual();
        }

        public void EndBuff()
        {
            _shield.SetShieldActive(false);
        }

        public void Tick()
        {
            _elapsedTime += Time.deltaTime;
            _blinkTimer += Time.deltaTime;

            float remainingTime = Mathf.Max(0f, Duration - _elapsedTime);
            float blinkInterval = GetBlinkInterval(remainingTime);

            if (_blinkTimer < blinkInterval)
                return;

            _blinkTimer = 0f;
            _shield.FlashShield();
        }

        private float GetBlinkInterval(float remainingTime)
        {
            if (remainingTime > 3f)
                return 0.8f;

            if (remainingTime > 1f)
                return 0.5f;

            return 0.2f;
        }
    }
}
