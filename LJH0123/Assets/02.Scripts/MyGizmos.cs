using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos: MonoBehaviour
 {
    public Color color = Color.yellow;
    public float tradius = 0.1f;
    void OnDrawGizmos()
    {
        // 기즈모색상설정
        Gizmos.color = color;
        // 구체모양의기즈모생성. 인자는(생성위치, 반지름)
        Gizmos.DrawSphere(transform.position, tradius);
    }
}

