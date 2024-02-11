using UnityEngine;
using Logging;

public class SimpleMonster : Monster
{
    public override bool InRange(GameObject Other)
    {
        return Vector3.Distance(model_.transform.position, Other.transform.position) <= base.range_;
    }

    public override bool Attack(GameObject Other)
    {
        if (InRange(Other))
        {
            XLogger.Log(Category.Damage, "Successfully dealt damage");
            return true;
        }
        else
        {
            XLogger.Log(Category.Damage, "Did not deal damage");
            return false;
        }
    }
}
