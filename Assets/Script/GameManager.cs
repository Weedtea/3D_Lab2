using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform[] SpwanPoint;
    public Material[] Materials;
    public GameObject MonsterPrfabs;
    void Awake()
    {
        if(Instance == null){
            Destroy(Instance);
            Instance = this;
        }
        else{
            Instance = this;
        }
    }

    private void Start() {
        for(int i = 0; i <SpwanPoint.Length; i++){
            GameObject monster = Instantiate (MonsterPrfabs, SpwanPoint[i].position, Quaternion.identity);
            int materialIndex = Random.Range(0, Materials.Length);

            monster.GetComponent<MeshRenderer>().material = Materials[materialIndex];

            switch(materialIndex){
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
