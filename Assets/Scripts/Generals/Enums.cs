//enumerate
//�����Ѵ�
//���ڷ� ���� ���߿� �򰥸���. ���ڷ� �ᵵ ���ڷ� ����
//enum�� ������! ������ ����� ��ﳯ ���ΰ�
//256���� �����?
//-2147483648 ~ 2147483647 : 4Bye
//byte : 1byte 256
//short : 2byte 65536
//int : 4byte 43�ﰳ (�ڵ�)
//long : 8byte 2��? 2��?
public enum Ally : byte
{
    Human,
    Orc,
    Zombie,
    Wolf,
}
public enum Grade : byte
{
    Common,
    Rare,
    Epic,
    Legend
}
public enum Job : byte
{
    Knight,
    Monk,
    Farmer,
    Wizzard,
    Developer
}
public enum CrowdControl : byte
{
    None,
    Frozen,
    Poison,
    Slow,
    Stun,
    Sleep,
    KnockBack,
    Airborne
}
public enum KeyType : byte
{
    Front, Back, Left, Right, Run, Jump, Attack, Skill, 
    Lenght //�� �������� ���� �� ������ ��ü�� ������ �� �� �ִ� ģ���� �˴ϴ�.
}