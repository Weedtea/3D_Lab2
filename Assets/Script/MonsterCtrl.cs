using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public NavMeshAgent MonsterAgent;

    public void Start(){
        MonsterAgent = GetComponent<NavMeshAgent>();
    }



    public void Update(){

        float mindistance = float.MaxValue;
        GameObject target = null;

        GameObject[] sameColorMonsters = GameObject.FindGameObjectsWithTag(this.tag);

        foreach(GameObject monster in sameColorMonsters){

            if(monster.GetInstanceID() == gameObject.GetInstanceID()){
                continue;
            }
            
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if(distance < mindistance)
            {
                mindistance = distance;
                target = monster;
            }
        }

        if(target == null){
            return;
        }
        MonsterAgent.SetDestination(target.transform.position);
    }
}
