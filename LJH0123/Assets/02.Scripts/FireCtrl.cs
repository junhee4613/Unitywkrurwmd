using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl: MonoBehaviour
 {
    // 총알프리팹
    public GameObject bullet;
    // 총알발사좌표
    public Transform firePos;
    // 총소리에사용할오디오음원
    public AudioClip fireSfx;
    // AudioSource 컴포넌트를저장할변수
    private new AudioSource audio;
    // Muzzle Flash의MeshRenderer 컴포넌트
    private MeshRenderer muzzleFlash;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        // FirePos 하위에있는MuzzleFlash의Material 컴포넌트를추출
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        // 처음시작할때비활성화
        muzzleFlash.enabled = false;
    }
    void Update()
    {
        // 마우스왼쪽버튼을클릭했을때Fire 함수호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void Fire()
    {
        // Bullet 프리팹을동적으로생성(생성할객체, 위치, 회전)
        Instantiate(bullet, firePos.position, firePos.rotation);
        // 총소리발생
        audio.PlayOneShot(fireSfx, 1.0f);
        // 총구화염효과코루틴함수호출
        StartCoroutine(ShowMuzzleFlash());
    }
    IEnumerator ShowMuzzleFlash()
    {
        // 오프셋좌푯값을랜덤함수로생성
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        // 텍스처의오프셋값설정
        muzzleFlash.material.mainTextureOffset = offset;
        // MuzzleFlash의회전변경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
        // MuzzleFlash의크기조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        // MuzzleFlash 활성화
        muzzleFlash.enabled = true;
        // 0.2초동안대기(정지)하는동안메시지루프로제어권을양보
        yield return new WaitForSeconds(0.2f);
        // MuzzleFlash 비활성화
        muzzleFlash.enabled = false;
    }
}
