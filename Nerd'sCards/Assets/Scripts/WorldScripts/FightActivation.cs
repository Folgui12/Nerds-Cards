using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightActivation : MonoBehaviour
{
    [SerializeField] private List<Behaviour> Enemys = new List<Behaviour>();
    public int amountOfEnemysInRoom = 0;

    [SerializeField] private UnityEvent startFight;
    [SerializeField] private UnityEvent stopFight; 

    private bool endBattle = false;

    private void Start()
    {

    }

    void ActivateBattle()
    {
        if(!endBattle)
        {
            foreach(Behaviour thisobject in Enemys)
            {
                amountOfEnemysInRoom++;
                thisobject.enabled = true;
            }
        }
        
    }
    
    public void CountEnemys()
    {
        amountOfEnemysInRoom--;
        Check();
    }

    private void Check()
    {
        if (amountOfEnemysInRoom <= 0)
        {
            endBattle = true;
            stopFight.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemys.Add(other.GetComponent<EnemyAI>());
        }

        if (other.tag == "Player")
        {
            ActivateBattle();
            startFight.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Check();
        }
    }
}
