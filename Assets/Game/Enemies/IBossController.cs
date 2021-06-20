using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossController : IEnemyController
{
    void SpecialAttack();
    void MinionTroops(); // Boss'un sahip olduğu veya çağırdığı minionları listeler
    void PhasePath();

    // TODO: Any boss specific generic function can be added below.
}
