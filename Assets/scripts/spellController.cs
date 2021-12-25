//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class spellController : MonoBehaviour
{
    public Valve.VR.SteamVR_Action_Boolean triggerAction;

    public Hand hand;

    public GameObject prefabToPlant;

    private bool spellOngoing;
    private List<spell> spells;
    [SerializeField]
    private Transform hitbox;
    [SerializeField]
    private Transform hitboxBack;

    void Start()
    {
        spells = new List<spell>(GetComponentsInChildren<spell>());
        foreach (spell s in spells)
        {
            s.hitboxBack = hitboxBack;
            s.hitbox = hitbox;
        }
    }

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();
        if (triggerAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No trigger action assigned", this);
            return;
        }
        triggerAction.AddOnChangeListener(OnTriggerActionChange, hand.handType);
    }

    private void OnDisable()
    {
        if (triggerAction != null)
            triggerAction.RemoveOnChangeListener(OnTriggerActionChange, hand.handType);
    }

    private void OnTriggerActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool triggerHold)
    {
        if (triggerHold)
        {
            print("start spell");
            foreach (spell s in spells)
            {
                s.startSpellChecking();
            }
            startSpell();
        }
    }

    private void startSpell()
    {
        spellOngoing = true;
        spellHelper[] spellHelpers = GetComponentsInChildren<spellHelper>();
        foreach (var spell in spells)
        {
            spell.getTargetHelp().GetComponent<spellHelper>().startHelp();
        }
    }

    private void finishSpell()
    {
        spellOngoing = false;
        print("fire");
        foreach (var spell in spells)
        {
            spell.getTargetHelp().GetComponent<spellHelper>().endHelp();
        }
    }
    public void Update()
    {
        if (!spellOngoing) return;

        foreach (var spellinstance in spells)
        {
            var sucess = spellinstance.checkCheckPoints();
            if (sucess)
            {
                finishSpell();
            }
        }
    }


    public void Plant()
    {
        StartCoroutine(DoPlant());
    }

    private IEnumerator DoPlant()
    {
        Vector3 plantPosition;

        RaycastHit hitInfo;
        bool hit = Physics.Raycast(hand.transform.position, Vector3.down, out hitInfo);
        if (hit)
        {
            plantPosition = hitInfo.point + (Vector3.up * 0.05f);
        }
        else
        {
            plantPosition = hand.transform.position;
            plantPosition.y = Player.instance.transform.position.y;
        }

        GameObject planting = GameObject.Instantiate<GameObject>(prefabToPlant);
        planting.transform.position = plantPosition;
        planting.transform.rotation = Quaternion.Euler(0, Random.value * 360f, 0);

        planting.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

        Rigidbody rigidbody = planting.GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.isKinematic = true;



        Vector3 initialScale = Vector3.one * 0.01f;
        Vector3 targetScale = Vector3.one * (1 + (Random.value * 0.25f));

        float startTime = Time.time;
        float overTime = 0.5f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            planting.transform.localScale = Vector3.Slerp(initialScale, targetScale, (Time.time - startTime) / overTime);
            yield return null;
        }


        if (rigidbody != null)
            rigidbody.isKinematic = false;
    }
}