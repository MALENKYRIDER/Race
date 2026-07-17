using System;
using System.Collections.Generic;
using Buff.SlowSpeedBuff;
using Buff.SpeedBuff;
using Buff.ShieldBuff;
using DG.Tweening;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public CarTrigger carTrigger;
    public ChunkSpeed chunkSpeed;
    
    private List<IBuff> _buffs =  new List<IBuff>();
    
    private void Update()
    {
        foreach (var buff in _buffs)
        {
            buff.Tick();
        }
    }

    public void AddBuff(IBuff buff)
    {
        buff.StartBuff();
        _buffs.Add(buff);
        DOVirtual.DelayedCall(buff.Duration, () => { RemoveBuff(buff); });
    }

    public void RemoveBuff(IBuff buff)
    {
        buff.EndBuff();
        _buffs.Remove(buff);
    }

    public void AddSpeedBuff()
    {
        AddBuff(new SpeedBuff(chunkSpeed));
    }
    
    public void AddSlowSpeedBuff()
    {
        AddBuff(new SlowSpeedBuff(chunkSpeed));
    }

    public void AddShieldBuff()
    {
        AddBuff(new ShieldBuff(carTrigger));
    }
}