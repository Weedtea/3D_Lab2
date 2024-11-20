using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage = 1; // 무기가 가하는 데미지

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCtrl player = other.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                player.PlayerHp -= damage;
            }
        }
    }
}