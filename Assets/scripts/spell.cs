using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class spell : MonoBehaviour
{
    public float distanceThreshold;

    private List<Vector3> checkPointPositions = new List<Vector3>();
    List<Transform> movingCheckPoints = new List<Transform>();
    int currentCP;
    private GameObject targetHelp;
    [Header("ignore")]
    public Transform hitbox;
    public Transform hitboxBack;

    void Start()
    {
        targetHelp = GetComponentInChildren<spellHelper>().gameObject;
        setupCheckpoints();
    }

    private void setupCheckpoints()
    {
        spellCheckPoint[] spellCheckPoints = GetComponentsInChildren<spellCheckPoint>();
        for (int i = 0; i < spellCheckPoints.Length; i++)
        {
            spellCheckPoint cp = spellCheckPoints[i];
            movingCheckPoints.Add(cp.transform);
        }
    }

    public void startSpellChecking()
    {
        checkPointPositions.Clear();
        currentCP = 0;
        foreach (Transform t in movingCheckPoints)
        {
            var pos = t.position;
            checkPointPositions.Add(new Vector3(pos.x, pos.y, pos.z));
        }

    }
    public bool checkCheckPoints()
    {
        targetHelp.GetComponent<spellHelper>().setDestination(checkPointPositions[currentCP], currentCP == 0);
        float distanceToCP = Vector3.Distance(hitbox.position, checkPointPositions[currentCP]);
        if (distanceToCP < distanceThreshold)
        {
            currentCP += 1;
            GetComponent<spellEffect>().CPsRemainingEvent(checkPointPositions.Count-currentCP);
            print("currentCP: " + currentCP);

        }
        if (currentCP == checkPointPositions.Count)
        {
            launchSpell();
            return true;
        }
        return false;
    }

    private void launchSpell()
    {
        Vector3 dir = (hitbox.transform.position - hitboxBack.transform.position).normalized;
        GetComponent<spellEffect>().activateSpell(dir);
    }
    void OnDrawGizmosSelected()
    {
        foreach (Transform pos in movingCheckPoints)
        {
            Gizmos.color = Color.cyan;
            // Gizmos.DrawSphere(pos.position, distanceThreshold);
        }
        foreach (Vector3 pos in checkPointPositions)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pos, distanceThreshold);
        }
    }

    internal GameObject getTargetHelp()
    {
        return targetHelp;
    }
}
