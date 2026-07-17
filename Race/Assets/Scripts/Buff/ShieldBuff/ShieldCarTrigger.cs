using UnityEngine;

namespace Buff.ShieldBuff
{
    public class ShieldCarTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(gameObject);
                FindObjectOfType<BuffSystem>().AddShieldBuff();
            }
        }
    }
}