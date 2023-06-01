using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    // 폭발효과파티클을연결할변수
    public GameObject expEffect;
    // 무작위로적용할텍스처배열
    public Texture[] textures;
    // 폭발반경
    public float radius = 10.0f;
    // 하위에있는Mesh Renderer 컴포넌트를저장할변수
    private new MeshRenderer renderer;
    // 컴포넌트를저장할변수
    private Transform tr;
    private Rigidbody rb;
    // 총알맞은횟수를누적시킬변수
    private int hitCount = 0;
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        // 하위에있는MeshRenderer 컴포넌트를추출
        renderer = GetComponentInChildren<MeshRenderer>();
        // 난수발생
        int idx = Random.Range(0, textures.Length);
        // 텍스처지정
        renderer.material.mainTexture = textures[idx];
    }
    // 충돌시발생하는콜백함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            // 총알맞은횟수를증가시키고3회이상이면폭발처리
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }
    // 드럼통을폭발시킬함수
    void ExpBarrel()
    {
        // 폭발효과파티클생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        // 폭발효과파티클5초후에제거
        Destroy(exp, 5.0f);
        // Rigidbody 컴포넌트의mass를1.0으로수정해무게를가볍게함
        // rb.mass = 1.0f;
        // 위로솟구치는힘을가함
        // rb.AddForce(Vector3.up * 1500.0f);
        // 간접폭발력전달
        IndirectDamage(tr.position);
        // 3초후에드럼통제거
        Destroy(gameObject, 3.0f);
    }
    // 폭발력을주변에전달하는함수
    void IndirectDamage(Vector3 pos)
    {
        // 주변에있는드럼통을모두추출
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);
        foreach (var coll in colls)
        {
            // 폭발범위에포함된드럼통의Rigidbody 컴포넌트추출
            rb = coll.GetComponent<Rigidbody>();
            // 드럼통의무게를가볍게함
            rb.mass = 1.0f;
            // freezeRotation 제한값을해제
            rb.constraints = RigidbodyConstraints.None;
            // 폭발력을전달
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }
}
