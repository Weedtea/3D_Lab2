using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 1; // 무기가 가하는 데미지

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와의 충돌 확인
        if (other.CompareTag("Player"))
        {
            PlayerCtrl player = other.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                Debug.Log("Player hit by weapon!");
                player.PlayerHp -= _damage; // 플레이어 체력 감소
            }
        }
    }
}
