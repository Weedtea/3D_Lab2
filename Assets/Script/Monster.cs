using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HitCount = 0; // 몬스터가 받은 히트 수
    public int mergedCount = 1; // 합체 횟수
    private bool isMerging = false; // 합체 플래그
    private bool isAttacking = false; // 공격 중 상태 플래그
    public bool check = true;

    [SerializeField] private int _damage = 1; // 몬스터가 가하는 데미지
    [SerializeField] private float _attackRange = 2.0f; // 공격 범위
    [SerializeField] private float _attackCooldown = 1.0f; // 공격 간격
    private float _lastAttackTime;

    private GameObject _player;
    private Animator _animator;
    [SerializeField] private GameObject weaponPrefab; // 무기 프리펩
    private Collider weaponCollider; // 무기 프리펩의 콜라이더

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

        if (weaponPrefab != null)
        {
            weaponCollider = weaponPrefab.GetComponent<Collider>();
            if (weaponCollider != null)
            {
                weaponCollider.isTrigger = true; // 무기 콜라이더를 트리거로 설정
                weaponCollider.enabled = false; // 처음에는 비활성화
            }
        }
    }

    void Update()
    {
        // 몬스터 제거 조건
        if (HitCount >= 3)
        {
            _animator.SetTrigger("isDead");
            StartCoroutine(WaitForIt());
            return;
        }

        // 플레이어와의 거리 계산
        if (_player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= _attackRange)
            {
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    StartAttack();
                }
            }
        }
    }

    private void StartAttack()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("isAttacking"); // 공격 애니메이션 실행
        }
        isAttacking = true; // 공격 상태 시작
        _lastAttackTime = Time.time;

        if (weaponCollider != null)
        {
            weaponCollider.enabled = true; // 공격 시 무기 콜라이더 활성화
        }
    }

    private void EndAttack()
    {
        isAttacking = false; // 공격 상태 종료

        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // 공격 종료 시 무기 콜라이더 비활성화
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (weaponCollider != null && weaponCollider.enabled && other.CompareTag("Player"))
        {
            PlayerCtrl player = other.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                player.PlayerHp -= _damage;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 기존 총알 충돌 및 합체 로직 유지
        if (collision.gameObject.CompareTag("bullet"))
        {
            HitCount++;
            Destroy(collision.gameObject); // 충돌한 총알 제거
        }

        if (isMerging || !collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        Monster otherMonster = collision.gameObject.GetComponent<Monster>();
        if (otherMonster == null || otherMonster.isMerging)
        {
            return;
        }

        // 합체 로직
        isMerging = true;
        otherMonster.isMerging = true;

        int totalMergedCount = this.mergedCount + otherMonster.mergedCount;

        // 크기 증가 및 합체 횟수 설정
        mergedCount = totalMergedCount;
        float sizeMultiplier = 1f + (mergedCount - 1) * 0.2f;
        transform.localScale = Vector3.one * sizeMultiplier;

        Destroy(otherMonster.gameObject);

        isMerging = false;
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(0.5f);
        check = true;
        Destroy(this.gameObject);
    }
    
}
