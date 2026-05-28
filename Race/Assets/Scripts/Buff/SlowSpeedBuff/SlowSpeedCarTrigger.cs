using UnityEngine;

namespace Buff.SlowSpeedBuff
{
    public class SlowSpeedCarTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(gameObject);
                FindObjectOfType<BuffSystem>().AddSlowSpeedBuff();
            }
        }
    }
}