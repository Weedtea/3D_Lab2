using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HitCount = 0; // 몬스터가 받은 히트 수
    public int mergedCount = 1; // 현재 몬스터의 합체 횟수
    private bool isMerging = false; // 현재 합체 중인지 확인하는 플래그

    void Update()
    {
        // HitCount가 일정 이상이면 파괴
        if (HitCount >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 합체 중이거나 충돌한 객체의 태그가 일치하지 않으면 무시
        if (isMerging || !collision.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        // 충돌한 오브젝트가 Monster 컴포넌트를 가지고 있는지 확인
        Monster otherMonster = collision.gameObject.GetComponent<Monster>();
        if (otherMonster == null || otherMonster.isMerging)
        {
            return;
        }

        // 합체 시작
        isMerging = true;
        otherMonster.isMerging = true;

        // 합체된 몬스터의 합체 횟수 계산
        int totalMergedCount = this.mergedCount + otherMonster.mergedCount;

        // 현재 몬스터의 크기와 합체 횟수 업데이트
        mergedCount = totalMergedCount;
        float sizeMultiplier = 1f + (mergedCount - 1) * 0.2f; // 크기 증가
        transform.localScale = Vector3.one * sizeMultiplier;

        // 충돌한 다른 몬스터 삭제
        Destroy(otherMonster.gameObject);

        // 합체 중 상태 초기화
        isMerging = false;
    }
}
