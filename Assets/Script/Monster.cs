using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int HitCount = 0; // 몬스터가 받은 히트 수
    public int mergedCount = 1; // 합체 횟수
    private bool isMerging = false; // 합체 플래그

    [SerializeField] private int _damage = 1; // 몬스터가 가하는 데미지
    [SerializeField] private float _attackRange = 2.0f; // 공격 범위
    [SerializeField] private float _attackCooldown = 1.0f; // 공격 간격
    private float _lastAttackTime;

    private NavMeshAgent _agent;
    private GameObject _player;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // 몬스터 제거 조건
        if (HitCount >= 3)
        {
            Destroy(this.gameObject);
            return;
        }

        // // 플레이어 추적
        // if (_player != null)
        // {
        //     _agent.SetDestination(_player.transform.position);

        //     // 플레이어와의 거리 계산
        //     float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        //     if (distanceToPlayer <= _attackRange)
        //     {
        //         AttackPlayer();
        //     }
        // }
    }

    private void AttackPlayer()
    {
        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            // 플레이어 체력 감소 로직
            PlayerCtrl player = _player.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                Debug.Log("Player attacked by Monster!");
                player.PlayerHp -= _damage;
            }
            _lastAttackTime = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어 공격 처리
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
