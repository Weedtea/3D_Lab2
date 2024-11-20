using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5.0f; // 걷기 속도
    [SerializeField] private GameObject _bulletPrefab; // 총알 프리팹
    [SerializeField] private Transform _bulletSpawnPoint; // 총알 생성 위치
    [SerializeField] private float _bulletSpeed = 1f; // 총알 속도

    public int PlayerHp; // 플레이어 체력
    public MeshRenderer muzzleFlash; // 총구 번쩍임

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>(); // Animator 컴포넌트 가져오기
        muzzleFlash.enabled = false; // 초기 상태에서 Muzzle Flash 비활성화
    }

    private void Update()
    {
        // 이동 처리
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(h, 0, v).normalized;

        Move(dir);

        // 공격 처리
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // 사망 처리
        if (PlayerHp <= 0)
        {
            Death();
        }
    }

    private void Move(Vector3 direction)
    {
        // 이동 처리
        transform.Translate(direction * _playerSpeed * Time.deltaTime, Space.World);

        // 이동 방향으로 회전
        if (direction != Vector3.zero)
        {
            transform.LookAt(direction + transform.position);
        }

        // Horizontal과 Vertical 값을 계산하여 Animator에 전달
        float horizontal = Vector3.Dot(direction, transform.right);  // 좌우 이동
        float vertical = Vector3.Dot(direction, transform.forward); // 전후 이동

        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
    }

    private void Fire()
    {
        if (_bulletPrefab != null && _bulletSpawnPoint != null)
        {
            // 총알 생성
            GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * _bulletSpeed; // 총알에 속도 적용
            }

            // 총구 번쩍임
            StartCoroutine(ShowEnable());

            // 공격 애니메이션 트리거
            _animator.SetTrigger("Fire");
        }
    }

    IEnumerator ShowEnable()
    {
        // Muzzle Flash 크기 및 회전 설정
        float scale = Random.Range(1.0f, 3.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        Quaternion rot = Quaternion.Euler(30.0f, 0f, Random.Range(0f, 90.0f));
        muzzleFlash.enabled = true;

        // 일정 시간 후 비활성화
        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        muzzleFlash.enabled = false;
    }

    private void Death()
    {
        // 사망 애니메이션 실행
        _animator.SetTrigger("Die");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Unity Editor에서 실행 종료
#endif
    }
}
