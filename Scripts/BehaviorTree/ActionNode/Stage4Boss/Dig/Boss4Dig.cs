using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Dig : Boss4ActionNodeFunction
{
    public Boss4Dig(Boss4_Battle _enemy, Boss4AIController _con) : base(_enemy, _con)
    {
    }

    public override INode.ENodeState CheckNode()
    {
        throw new System.NotImplementedException();
    }
}
