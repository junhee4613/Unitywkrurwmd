using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos: MonoBehaviour
 {
    public Color color = Color.yellow;
    public float tradius = 0.1f;
    void OnDrawGizmos()
    {
        // ����������
        Gizmos.color = color;
        // ��ü����Ǳ�������. ���ڴ�(������ġ, ������)
        Gizmos.DrawSphere(transform.position, tradius);
    }
}

