using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface spellEffect 
{
    void activateSpell(Vector3 dir);

    void CPsRemainingEvent(int remaining);
}
