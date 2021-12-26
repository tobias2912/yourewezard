using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expelliarmus : spell, spellEffect
{
    // [Header("Spell specific:")]
    
    public void activateSpell(Vector3 dir)
    {
        GetComponent<AudioSource>().Play();
    }

    public void CPsRemainingEvent(int remaining)
    {
    }
}
