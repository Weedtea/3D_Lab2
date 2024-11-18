using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f; // 플레이어 이동 속도
    [SerializeField] private GameObject _bulletPrefab; // 총알 프리팹
    [SerializeField] private Transform _bulletSpawnPoint; // 총알 생성 위치
    [SerializeField] private float _bulletSpeed = 1f; // 총알 속도

    private void Update()
    {
        // 이동 처리
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v).normalized;

        // 플레이어의 이동
        this.transform.Translate(dir * _playerSpeed * Time.deltaTime, Space.World);

        // 이동 방향으로 회전
        if (dir != Vector3.zero)
        {
            transform.LookAt(dir + transform.position);
        }

        // 공격 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (_bulletPrefab != null && _bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * _bulletSpeed;
            }
        }
    }
}
