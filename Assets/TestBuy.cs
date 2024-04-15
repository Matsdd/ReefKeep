using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuy : MonoBehaviour
{

   public void increaseMoney()
    {
        GameManager.instance.ChangeMoney(10);
    }
}
