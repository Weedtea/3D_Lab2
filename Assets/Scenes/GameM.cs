using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    // public float Distance = 10f;          // 캐릭터와 카메라 사이 거리
    // public float Height = 5f;             // 카메라의 높이
    public Transform Target;              // 따라갈 캐릭터
    public Vector3 offset;

    void LateUpdate()
    {
        // if (Target == null) return;

        // // 1. 카메라 위치 계산 (캐릭터 뒤쪽 고정)
        // Vector3 desiredPosition = Target.position + Vector3.up * Height - Target.forward * Distance;

        // // 2. 위치 이동 (즉시 이동)
        // transform.position = desiredPosition;

        // // 3. 카메라는 캐릭터를 바라보는 방향 유지
        // transform.LookAt(Target.position); // 시야를 조금 위로 보정
        transform.position = Target.position + offset;
        
    }
}
