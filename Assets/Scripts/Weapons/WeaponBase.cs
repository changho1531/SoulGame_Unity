using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponHitType
{
    HitScan,
    Projectile,
    Melee
}
   
public enum WeaponShotType
{
    Single,
    Automatic,
    Interupted,
    Charge,
}

public enum WeaponEffectType
{
    Normal,
    KnockBack,
    Explosion,
    Blooding
}

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected GameObject           muzzleFire;
    [SerializeField] protected GameObject           bulletEffect;
    [SerializeField] protected GameObject           muzzleObject;
    [SerializeField] protected WeaponHitType        hitType;
    [SerializeField] protected WeaponShotType       shotType;
    [SerializeField] protected WeaponEffectType     effectType;
    [SerializeField] protected float                rapid;
                     protected float                coolDown;
    [SerializeField] protected float                range;
                     protected float                recoilCur;
    [SerializeField] protected float                recoilEachShot;
    [SerializeField] protected float                recoilRecovery;
    [SerializeField] protected float                recoilMinimum;
    [SerializeField] protected int                  damage;
                     protected int                  ammoCur;
    [SerializeField] protected int                  ammoMax;
    [SerializeField] protected int                  bulletPerShot = 1;
    [SerializeField] protected int                  pierce;
    [SerializeField] protected int                  stack;

    public bool Shot(Vector3 targetPosition)
    {
        //쿨타임을 먼저 확인해보죠!
        //쿨다운시간이 현재시간보다 큰 경우!
        //   3.2초      3.01초    안발사!
        //   3.2초      360초     발사!
        if(coolDown <= Time.time)
        {
            if(muzzleFire && muzzleObject)
            {
                GameObject created = PoolManager.Instantiate(muzzleFire);
                //부모 설정!
                created.transform.SetParent(muzzleObject.transform);
                //위치도 총구 위치 그대로!
                created.transform.localPosition = Vector3.zero;
                //회전도 총구 회전 그대로!
                created.transform.localRotation = Quaternion.identity;
                //크기도 총구 크기 그대로!
                created.transform.localScale    = Vector3.one;

                //바로 없애는 게 아니라 0.5초 기다렸다가 없앨 수 있으니까!
                PoolManager.Destroy(created, 0.5f);
            };
            //발사했으니까 쿨다운시간은 현재시간 + 발사간격
            //                          3초        0.2초  = 3.2초

            //선을 발사해서 맞은 친구를 돌려주는 거예요!
            RaycastHit hit;

            //선을 미리 정의합시다!
            Ray shotRay = new Ray();
            //    시작위치    총구가 있으면?           총구위치로!             없으면 그냥 여기서!
            shotRay.origin = muzzleObject ? muzzleObject.transform.position : transform.position;
            //       방향        목적지 - 출발지
            shotRay.direction = targetPosition - shotRay.origin;

            //레이캐스트는 어떤 요건인가 생각해보는 거예요!
            //선을 발사하는 건 "물리"요소중에서 확인하는 거예요!
            Physics.Raycast(shotRay, out hit);

            //부딪힌 대상이 있어야겠죠!
            if(hit.collider)
            {
                GameObject created = PoolManager.Instantiate(bulletEffect);
                //맞은 곳으로 이펙트를 이동시키기!
                created.transform.position = hit.point;
                //look at : 바라보다!                 노말 : 평면의 방향
                created.transform.LookAt(hit.point + hit.normal);

                PoolManager.Destroy(created, 0.5f);

                CharacterBase asCharacter = hit.transform.GetComponentInParent<CharacterBase>();
                if(asCharacter)
                {
                    asCharacter.TakeDamage(null, damage);
                };
            };

            coolDown = Time.time + rapid;
            return true;
        }
        else
        {
            return false;
        };
    }
    public bool Reload()
    {
        return true;
    }
}
