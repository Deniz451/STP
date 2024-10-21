using System.Collections;
using UnityEngine;

public class MosquitoEnemyAttack : EnemyAttack
{

    private MosquitoEnemyLogic mosquitoEnemyLogic;


    public override IEnumerator RangeAttack()
    {
        if (hasRangeAttack)
        {
            yield return new WaitForSeconds(attackDelay);
        }
    }

    public override IEnumerator MeleeAttack()
    {
        if (hasMeleeAttack)
        {
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
