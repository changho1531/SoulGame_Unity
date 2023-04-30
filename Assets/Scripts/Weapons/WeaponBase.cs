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
                GameObject created = Instantiate(muzzleFire, muzzleObject.transform);
                //바로 없애는 게 아니라 0.5초 기다렸다가 없앨 수 있으니까!
                Destroy(created, 0.5f);
            };
            //발사했으니까 쿨다운시간은 현재시간 + 발사간격
            //                          3초        0.2초  = 3.2초
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
