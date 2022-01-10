using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterAction : MonoBehaviour
{
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction()
    {
        switch(gameObject.name)
        {
            case "Saber":
                gameObject.GetComponent<Saber>().HolsterAction();
                break;
            case "MadsonD9":
                //gameObject.GetComponent<MadsonD9>().HolsterAction();
                break;
        }
        
    }
}
