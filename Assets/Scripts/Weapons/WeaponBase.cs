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
        //��Ÿ���� ���� Ȯ���غ���!
        //��ٿ�ð��� ����ð����� ū ���!
        //   3.2��      3.01��    �ȹ߻�!
        //   3.2��      360��     �߻�!
        if(coolDown <= Time.time)
        {
            if(muzzleFire && muzzleObject)
            {
                GameObject created = Instantiate(muzzleFire, muzzleObject.transform);
                //�ٷ� ���ִ� �� �ƴ϶� 0.5�� ��ٷȴٰ� ���� �� �����ϱ�!
                Destroy(created, 0.5f);
            };
            //�߻������ϱ� ��ٿ�ð��� ����ð� + �߻簣��
            //                          3��        0.2��  = 3.2��
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
