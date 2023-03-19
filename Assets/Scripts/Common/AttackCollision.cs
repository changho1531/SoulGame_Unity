using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public float damage;
    public Vector3 knockBack;
    public CharacterBase owner;

    public void EffectToCharacter(CharacterBase target)
    {
        if (target == null || target == owner) return;

        //타겟에 데미지   (누가 때렸는지, 몇뎀으로 때렸는지)
        target.TakeDamage(owner, damage);
        //타겟을 밀어주기!
        target.AddVelocity(knockBack);
    }
    //                                                        캐릭터한테 이펙트   맞은대상의 오브젝트의 컴포넌트<CharacterBase>
    protected void OnCollisionEnter2D(Collision2D collision){ EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnTriggerEnter2D(Collider2D collision)   { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnCollisionEnter(Collision collision)    { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnTriggerEnter(Collider collision)       { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
}
