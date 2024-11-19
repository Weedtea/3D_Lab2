using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HitCount = 0; // 몬스터가 받은 히트 수
    public int mergedCount = 1; // 합체 횟수
    private bool isMerging = false; // 합체 플래그
    private bool isAttacking = false; // 공격 중 상태 플래그

    [SerializeField] private int _damage = 1; // 몬스터가 가하는 데미지
    [SerializeField] private float _attackRange = 2.0f; // 공격 범위
    [SerializeField] private float _attackCooldown = 1.0f; // 공격 간격
    private float _lastAttackTime;

    private GameObject _player;
    private Animator _animator;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator가 할당되지 않았습니다!");
        }
    }

    void Update()
    {
        // 몬스터 제거 조건
        if (HitCount >= 3)
        {
            Destroy(this.gameObject);
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
    }

    // 공격 애니메이션 이벤트로 호출
    private void AttackPlayer()
    {
        if (isAttacking && _player != null)
        {
            PlayerCtrl player = _player.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                Debug.Log("Player attacked by Monster!");
                player.PlayerHp -= _damage;
            }
        }
    }

    private void EndAttack()
    {
        isAttacking = false; // 공격 상태 종료
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttacking && collision.gameObject.CompareTag("Player"))
        {
            PlayerCtrl player = collision.gameObject.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                Debug.Log("Player attacked on collision!");
                player.PlayerHp -= _damage;
            }
        }

        // 총알 충돌 처리
        if (collision.gameObject.CompareTag("bullet"))
        {
            HitCount++;
            Debug.Log($"Monster Hit Count: {HitCount}");
            Destroy(collision.gameObject); // 충돌한 총알 제거
        }

        // 몬스터 간 합체 처리
        if (isMerging || !collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        Monster otherMonster = collision.gameObject.GetComponent<Monster>();
        if (otherMonster == null || otherMonster.isMerging)
        {
            return;
        }

        // 합체 시작
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
}
