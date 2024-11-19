using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform[] SpwanPoint; // 스폰 위치 배열
    public Material[] Materials; // 몬스터 색상을 설정할 머티리얼 배열
    public GameObject MonsterPrfabs; // 몬스터 프리팹

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복된 GameManager 오브젝트가 있을 경우 파괴
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < SpwanPoint.Length; i++)
        {
            GameObject monster = Instantiate(MonsterPrfabs, SpwanPoint[i].position, Quaternion.identity);
            int materialIndex = Random.Range(0, Materials.Length);

            // 몬스터의 머티리얼 설정
            MeshRenderer renderer = monster.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = Materials[materialIndex];
            }

            // 머티리얼 인덱스에 따라 태그 설정
            switch (materialIndex)
            {
                case 0:
                    monster.tag = "Blue";
                    break;
                case 1:
                    monster.tag = "Green";
                    break;
                case 2:
                    monster.tag = "Yellow";
                    break;
            }
        }
    }
}
