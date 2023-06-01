using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // ����ȿ����ƼŬ�������Һ���
    public GameObject expEffect;
    // ���������������ؽ�ó�迭
    public Texture[] textures;
    // ���߹ݰ�
    public float radius = 10.0f;
    // �������ִ�Mesh Renderer ������Ʈ�������Һ���
    private new MeshRenderer renderer;
    // ������Ʈ�������Һ���
    private Transform tr;
    private Rigidbody rb;
    // �Ѿ˸���Ƚ����������ų����
    private int hitCount = 0;
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        // �������ִ�MeshRenderer ������Ʈ������
        renderer = GetComponentInChildren<MeshRenderer>();
        // �����߻�
        int idx = Random.Range(0, textures.Length);
        // �ؽ�ó����
        renderer.material.mainTexture = textures[idx];
    }
    // �浹�ù߻��ϴ��ݹ��Լ�
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            // �Ѿ˸���Ƚ����������Ű��3ȸ�̻��̸�����ó��
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }
    // �巳�������߽�ų�Լ�
    void ExpBarrel()
    {
        // ����ȿ����ƼŬ����
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        // ����ȿ����ƼŬ5���Ŀ�����
        Destroy(exp, 5.0f);
        // Rigidbody ������Ʈ��mass��1.0���μ����ع��Ը���������
        // rb.mass = 1.0f;
        // ���μڱ�ġ����������
        // rb.AddForce(Vector3.up * 1500.0f);
        // �������߷�����
        IndirectDamage(tr.position);
        // 3���Ŀ��巳������
        Destroy(gameObject, 3.0f);
    }
    // ���߷����ֺ��������ϴ��Լ�
    void IndirectDamage(Vector3 pos)
    {
        // �ֺ����ִµ巳�����������
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);
        foreach (var coll in colls)
        {
            // ���߹��������Եȵ巳����Rigidbody ������Ʈ����
            rb = coll.GetComponent<Rigidbody>();
            // �巳���ǹ��Ը���������
            rb.mass = 1.0f;
            // freezeRotation ���Ѱ�������
            rb.constraints = RigidbodyConstraints.None;
            // ���߷�������
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }
}
