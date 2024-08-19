Scriptable Objective에 사용하는 분야가 여러가지 있는데
<p>
 <ol>
  <li>아이템 관리</li>
  <li>NPC 관리</li>
  <li>스킬 데이터</li>
 </ol>
</p>

<hr/>

<h3>1. 아이템 관리</h3>
<p>
아이템 SO에서 조직으로 표현하자면<br>

![SO_Item](https://github.com/user-attachments/assets/59c89ab4-4450-4644-949b-dfdc86795cc8)
<br>RecoverItem과 Weapon형식으로 나뉘어져 있다.
</p>

<h4>1.1. 소비 아이템</h4>
 소비 아이템의 구조는 이런식으로 되어 있다.
 
![RecoverItem](https://github.com/user-attachments/assets/9550b57b-78b0-454f-9383-384f026a75db)
<p>
보다 자세한 속성들은<br>
Health Amount : 체력 회복량<br>
Mana Amount : 마나 회복량<br>
Price : 판매 가격
</p>

<h4>1.2. 무기 아이템</h4>
무기 아이템도 소비처럼 똑같은 구조로 되어 있다.

![imageSwordSO](https://github.com/user-attachments/assets/4402c02c-a1cc-446c-8e7c-1ee9c2f434c3)

<hr/>

<h3>2.NPC 관리</h3>
<p>
 NPC에는 적에게 많이 사용하는데 적이 아닌 NPC도 있는 경우가 있다.<br>
 적 NPC의 경우에는 아이디, 스탯이 있다.

 <h4>2.1. 적 ID</h4>

 ![imageNPCSO](https://github.com/user-attachments/assets/bc4fefd9-8bea-4ee4-978e-aa6b0e700ca5)
<br>이거는 플레이어가 전투를 했으면 다시 재전투하지 못하게하는 역할이다.
<br>여기서 ID를 받아서 NPCDataManager에서 관리해서 싸웠는지 체크한다.

![imageNPCData](https://github.com/user-attachments/assets/894b959a-f87d-4936-836d-b2b6758ca65c)
<br>딕셔너리에 데이터가 많은 이유는 NPCSO파일을 여러개를 생성했기 때문이다.

![imageNPCFiles](https://github.com/user-attachments/assets/cf33f073-658b-4582-83a9-86207394fd84)



<h4>2.2. 적 스탯</h4>

![imageNPCStat](https://github.com/user-attachments/assets/04963928-3de4-4b80-9346-eb6fbadddb28)
<br>이건 적 종류마다 공통적인 정보가 담겨있다.
<br>파일을 보면 적은 수량이 있는데, ID를 어떻게 관리하냐?
<br>배틀씬으로 들어갈 때 배틀적 프리팹의 statSO의 ID를 NPCSO의 ID를 덮어쓰는 방식이 있다.

![imageStatFiles](https://github.com/user-attachments/assets/ca62f91a-056b-44a7-8afc-c5ee48ab5e3a)

```EnemyStatHandler.cs
public void EnterBattleScene(int _npcID)
{
    eField = EField.Battle;
    _id = _npcID;
    SaveSceneName();
    
    FadeScreen(Define.SCENE_BATTLE);
}
```
</p>
<hr />

<h3>스킬 데이터</h3>
<p>
 스킬에도 데미지, 공속, 지속시간 등이 있을 것이다.

 ![imageSkill](https://github.com/user-attachments/assets/1db596e7-430c-41d1-8f5e-de3e8b0e077d)
<br>
액티브 스킬정보에 따라 적에게 입히는 Damage, 플레이어의 체력을 회복하는 Heal, 데미지의 추가 공격인 Attack 그리고 공격하는 속도인 Speed 등이 있다.

하지만 스킬에서는 기간 내에 완성되지 못했다.... ㅠㅠ
</p>
