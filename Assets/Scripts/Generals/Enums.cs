//enumerate
//열거한다!
//숫자로 쓰면 나중에 헷갈리니까! 글자로 써도 숫자로 쳐줌!
//enum의 사이즈! 종족은 몇개까지 기억날 것인가.. 65536개...?
//256개는 어떤가요?
//-2147483648 ~ 2147483647 : 4Byte
//byte  : 1byte 256
//short : 2byte 65536
//int   : 4byte 43억개 (자동)
//long  : 8byte
public enum Ally : byte
{
    Human,
    Orc,
    Zombie,
    Wolf,
}
public enum Grade : byte
{
    Common, Rare, Epic, Legend
}
public enum Job : byte
{
    Knight, Monk, Farmer, Wizzard, Developer, 
}
public enum CrowdControl : byte
{
    None, Frozen, Poison, Slow, Stun, Sleep, KnockBack, Airborne
}
public enum KeyType : byte
{
    Front, Back, Left, Right, Run, Jump, Attack, Skill, Roll,
    Length //맨 마지막에 놓는 것 만으로 전체의 개수를 잴 수 있는 친구가 됩니다!
}