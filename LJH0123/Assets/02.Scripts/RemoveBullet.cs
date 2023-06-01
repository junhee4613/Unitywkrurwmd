using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 스파크파티클프리팹을연결할변수
    public GameObject sparkEffect;
    void OnCollisionEnter(Collision coll)
    {
        // 충돌한게임오브젝트의태그값비교
        if (coll.collider.CompareTag("BULLET"))
        {
            // 첫번째충돌지점의정보추출
            ContactPoint cp = coll.GetContact(0);
            // 충돌한총알의법선벡터를쿼터니언타입으로변환
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            // 스파크파티클을동적으로생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            // 일정시간이지난후스파크파티클을삭제
            Destroy(spark, 0.5f);
            // 충돌한게임오브젝트삭제
            Destroy(coll.gameObject);
        }
    }
}
