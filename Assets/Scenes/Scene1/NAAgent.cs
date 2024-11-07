using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class NAAgent : MonoBehaviour
{
    private NavMeshAgent _agnet;
    private GameObject target;
    private Transform _moveTarget;

    private int iters = 0;
    public float detectionRange = 10f; // 플레이어 감지 범위
    private bool isPlayerLockedOn = false;

    void Start()
    {
        // Tag로 타겟 설정
        _agnet = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        DetectPlayer(); // 레이캐스트로 플레이어 감지

        // 캐릭터 지속적으로 따라옴
        if (isPlayerLockedOn && iters % 10 == 0)
        {
            _agnet.destination = target.transform.position;
        }
        iters++;

        var sameColorEnemy = GameObject.FindGameObjectsWithTag(tag);
        foreach(var enemy in sameColorEnemy)
        {
            if(enemy.GetInstanceID() == gameObject.GetInstanceID())
            {
                continue;
            }
        }

    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        // 레이캐스트가 플레이어와 충돌하는지 확인
        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.transform == target.transform)
            {
                isPlayerLockedOn = true;
            }else
            {
                isPlayerLockedOn = false;
            }
        }else
        {
            isPlayerLockedOn = false;
        }
    }
}
