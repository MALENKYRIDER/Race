namespace Buff.SlowSpeedBuff
{
    public class SlowSpeedBuff : IBuff
    {
        private readonly ISpeedForBuff _multiplierSpeedForBuff;
        
        public int Duration => 5;
        
        private const float _speedMultiplier = 15f;

        public SlowSpeedBuff(ISpeedForBuff multiplierSpeedForBuff)
        {
            _multiplierSpeedForBuff = multiplierSpeedForBuff;
        }

        public void StartBuff()
        {
            _multiplierSpeedForBuff.RemoveSpeed(_speedMultiplier);
        }

        public void EndBuff()
        {
            _multiplierSpeedForBuff.AddSpeed(_speedMultiplier);
        }

        public void Tick()
        {
            
        }
    }
}