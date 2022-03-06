using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownIconImage : MonoBehaviour
{


    public float brightestIntensity;
    public Color startingColor;
    public Color flashColor;
    public float coolDownTime;


    [SerializeField]
    private Material emissiveMat;

    [SerializeField]
    private Material emissiveMatCopy;
                     

    private float elapsedTime = 0;

    private Image iconImage;
    
    

    private bool flashed;
    

    // Start is called before the first frame update
    void Start()
    {
        iconImage = GetComponent<Image>();
        //emissiveMat = iconImage.material;

        emissiveMatCopy = new Material(iconImage.material);
        iconImage.material = emissiveMatCopy;
        

        emissiveMatCopy.SetColor("_Color", startingColor);
        emissiveMatCopy.SetColor("_EmissionColorNew", startingColor * 1);        
    }

    public void Update()
    {        
        if(flashed)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / coolDownTime;
            emissiveMatCopy.SetColor("_Color", Color.Lerp(flashColor, startingColor, percentageComplete));                        
            emissiveMatCopy.SetColor("_EmissionColorNew", Color.Lerp(startingColor * Mathf.Pow(2, brightestIntensity), startingColor * Mathf.Pow(2, 0), percentageComplete));
            if (elapsedTime > coolDownTime)
            {
                flashed = false;
                elapsedTime = 0;
            }
        }
        
    }

    public void FlashIcon()
    {
        Debug.Log("Flashing icon");
        emissiveMatCopy.SetColor("_Color", flashColor);
        emissiveMatCopy.SetColor("_EmissionColorNew", startingColor * Mathf.Pow(2, brightestIntensity));
        flashed = true;        
    }

    
}
