namespace Buff.SpeedBuff
{
    public class SpeedBuff : IBuff
    {
        private readonly IMultiplierSpeedForBuff _multiplierSpeedForBuff;

        public int Duration => 3;
        
        private const float _speedBuff = 1.1f;

        public SpeedBuff(IMultiplierSpeedForBuff multiplierSpeedForBuff)
        {
            _multiplierSpeedForBuff = multiplierSpeedForBuff;
        }

        public void StartBuff()
        {
            _multiplierSpeedForBuff.AddSpeedMultiplier(_speedBuff);
        }

        public void EndBuff()
        {
            _multiplierSpeedForBuff.RemoveSpeedMultiplier(_speedBuff);
        }

        public void Tick()
        {
            
        }
    }
}