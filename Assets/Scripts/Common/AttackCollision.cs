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

        //Ÿ�ٿ� ������   (���� ���ȴ���, ����� ���ȴ���)
        target.TakeDamage(owner, damage);
        //Ÿ���� �о��ֱ�!
        target.AddVelocity(knockBack);
    }
    //                                                        ĳ�������� ����Ʈ   ��������� ������Ʈ�� ������Ʈ<CharacterBase>
    protected void OnCollisionEnter2D(Collision2D collision){ EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnTriggerEnter2D(Collider2D collision)   { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnCollisionEnter(Collision collision)    { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
    protected void OnTriggerEnter(Collider collision)       { EffectToCharacter(collision.gameObject.GetComponent<CharacterBase>()); }
}
