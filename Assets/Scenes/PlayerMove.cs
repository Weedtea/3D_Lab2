using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f;

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v).normalized;

        // 플레이어의 이동
        this.transform.Translate(dir * _playerSpeed * Time.deltaTime, Space.World);

        // // 입력값이 있을 때만 회전
        // if (dir != Vector3.zero)
        // {
        //     this.transform.rotation = Quaternion.LookRotation(dir);
        // }
        transform.LookAt(dir + transform.position);
    }
}