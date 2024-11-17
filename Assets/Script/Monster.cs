using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HitCount = 0;
    public GameObject mergedObjectPrefab;
    private static bool hasMerged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HitCount >= 3)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if (other.collider.tag == "bullet")
        // {
        //     HitCount++;
        // }

        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            // 충돌한 두 몬스터 삭제
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // 충돌 지점의 중간 위치에 새로운 오브젝트 1개 생성
            Vector3 spawnPosition = (transform.position + collision.transform.position) / 2;
            Instantiate(mergedObjectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
