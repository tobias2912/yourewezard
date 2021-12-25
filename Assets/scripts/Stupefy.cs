using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stupefy : spell, spellEffect
{
    [Header("Spell specific:")]
    public GameObject Ice;
    private GameObject iceSpell;
    [SerializeField]
    private float speed;
    private Vector3 dir;

    public void activateSpell(Vector3 wandDir)
    {
        dir = wandDir;
        if (iceSpell != null){
            Destroy(iceSpell);
        }
        iceSpell = Instantiate(Ice, GameObject.Find("Effects").transform);
        iceSpell.transform.position = transform.position;
        iceSpell.GetComponent<projectile>().setup(speed, dir);
    }

    void spellEffect.CPsRemainingEvent(int v)
    {
    }

    void Update()
    {
    }
}
