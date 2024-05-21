using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestGoal : MonoBehaviour
{
    [SerializeField] private GameObject questLineGameObject;
    [SerializeField] private GameObject questLineGameObject1;
    [SerializeField] private GameObject claim;
    private int requiredAmount = 4;
    

    public void Update()
    {
        IsReached();      
    }
    public bool IsReached()
    {
        return (Currency.Instance.currentAmount >= requiredAmount);
    }

   public void quest1()
    {
        if (IsReached() == true)
        {
            Currency.Instance.gold += 200;
            questLineGameObject.SetActive(false);
            questLineGameObject.SetActive(true);
            claim.SetActive(false);
        }
    }
    

}
