using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public NavMeshAgent MonsterAgent;
    public string PlayerTag = "Player"; // 플레이어를 찾기 위한 태그 설정

    public void Start()
    {
        MonsterAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        float mindistance = float.MaxValue;
        GameObject target = null;

        // 태그가 같은 몬스터 찾기
        GameObject[] sameColorMonsters = GameObject.FindGameObjectsWithTag(this.tag);

        foreach (GameObject monster in sameColorMonsters)
        {
            if (monster.GetInstanceID() == gameObject.GetInstanceID())
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position,
             monster.transform.position);

            if (distance < mindistance)
            {
                mindistance = distance;
                target = monster;
            }
        }

        // 플레이어와의 거리 계산
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        foreach (GameObject player in players)
        {
            float playerDistance = Vector3.Distance(transform.position, 
            player.transform.position);

            if (playerDistance < mindistance)
            {
                mindistance = playerDistance;
                target = player;
            }
        }

        // 목표가 없으면 이동하지 않음
        if (target == null)
        {
            return;
        }

        // NavMeshAgent를 사용해 목표로 이동
        MonsterAgent.SetDestination(target.transform.position);
    }
}
