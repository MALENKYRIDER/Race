using Unity.VisualScripting;
using UnityEngine;

namespace Buff.SpeedBuff
{
    public class SpeedCarTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(gameObject);
                FindObjectOfType<BuffSystem>().AddSpeedBuff();
            }
        }
    }
}