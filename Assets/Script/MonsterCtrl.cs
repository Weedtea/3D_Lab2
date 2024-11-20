using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public NavMeshAgent MonsterAgent;
    public string PlayerTag = "Player"; // 플레이어를 찾기 위한 태그 설정
    private Animator _animator;

    public void Start()
    {
        MonsterAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }

    public void Update()
    {
        if (MonsterAgent == null) return;

        float minDistance = float.MaxValue;
        GameObject target = null;

        // 같은 태그를 가진 몬스터 찾기
        GameObject[] sameColorMonsters = GameObject.FindGameObjectsWithTag(this.tag);
        foreach (GameObject monster in sameColorMonsters)
        {
            if (monster == null || monster.GetInstanceID() == gameObject.GetInstanceID())
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = monster;
            }
        }

        // 플레이어와의 거리 계산
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        foreach (GameObject player in players)
        {
            if (player == null)
            {
                continue;
            }

            float playerDistance = Vector3.Distance(transform.position, player.transform.position) * 2f;

            if (playerDistance < minDistance)
            {
                minDistance = playerDistance;
                target = player;
            }
        }

        // 목표 설정 및 이동
        if (target != null)
        {
            MonsterAgent.SetDestination(target.transform.position);
            _animator.SetBool("isWalking", true); // 걷기 애니메이션 활성화
        }
        else
        {
            _animator.SetBool("isWalking", false); // 걷기 애니메이션 중지
        }
    }
}
