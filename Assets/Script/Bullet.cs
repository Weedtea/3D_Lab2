using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3.0f; // 총알 지속 시간

    private void Start()
    {
        Destroy(this.gameObject, _lifetime); // 일정 시간이 지나면 제거
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 경우에도 제거
        if(collision.collider.CompareTag("Blue")||collision.collider.CompareTag("Green")||collision.collider.CompareTag("Yellow")){
        Destroy(this.gameObject);
        Debug.Log($"Bullet hit: {collision.gameObject.name}");
    }
    }
}