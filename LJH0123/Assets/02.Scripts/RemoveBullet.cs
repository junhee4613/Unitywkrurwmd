using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // ����ũ��ƼŬ�������������Һ���
    public GameObject sparkEffect;
    void OnCollisionEnter(Collision coll)
    {
        // �浹�Ѱ��ӿ�����Ʈ���±װ���
        if (coll.collider.CompareTag("BULLET"))
        {
            // ù��°�浹��������������
            ContactPoint cp = coll.GetContact(0);
            // �浹���Ѿ��ǹ������͸����ʹϾ�Ÿ�����κ�ȯ
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            // ����ũ��ƼŬ���������λ���
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            // �����ð��������Ľ���ũ��ƼŬ������
            Destroy(spark, 0.5f);
            // �浹�Ѱ��ӿ�����Ʈ����
            Destroy(coll.gameObject);
        }
    }
}
