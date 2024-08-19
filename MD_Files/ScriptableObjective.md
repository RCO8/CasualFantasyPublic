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
