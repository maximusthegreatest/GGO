using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEffectTransition : MonoBehaviour
{
    public Material shatterMaterialTransition;
    private Material shatterMaterialCurrent;
    public Material shatterMaterialInitial;

    public Color newColor;
    public Color endColor;
    public float elapsed;
    

    // Start is called before the first frame update
    void Start()
    {
        shatterMaterialCurrent = gameObject.GetComponent<MeshRenderer>().material;
        endColor = shatterMaterialTransition.GetColor("_EmissionColor");
        //shatterMaterialInitial = gameObject.

        //StartCoroutine(LerpColor());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LerpColor());
        /*
        float duration = .35f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            //Debug.Log("init color: " + ColorUtility.ToHtmlStringRGBA(shatterMaterialInitial.color));
            //Debug.Log("shatter color: " + ColorUtility.ToHtmlStringRGBA(shatterMaterial.color));
            //shatterMaterialInitial.Lerp(shatterMaterialInitial, shatterMaterial, t / duration);
            //shatterMaterialInitial.color = Color.Lerp(shatterMaterialInitial.color, shatterMaterial.color, t / duration);
            //shatterMaterialInitial.SetColor("_EmissionColor", Color.Lerp(shatterMaterialInitial.color, shatterMaterial.color, t / duration));
            //Time.timeScale = 0f;
            
        }
        */
        //Destroy(gameObject);

        
    }

    IEnumerator LerpColor()
    {
        shatterMaterialCurrent = gameObject.GetComponent<MeshRenderer>().material; //this will be the purple mat
        newColor = shatterMaterialCurrent.GetColor("_EmissionColor"); //purple emission
        shatterMaterialCurrent.SetColor("_EmissionColor", newColor); // set color to be that same color
        Color startColor = newColor; //set startcolor to be that same color

        float duration = .75f;
        elapsed += Time.deltaTime / duration;

        while(elapsed < duration)
        {
            shatterMaterialCurrent.color = Color.Lerp(shatterMaterialInitial.color, shatterMaterialTransition.color, elapsed);
            shatterMaterialCurrent.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, elapsed));
            yield return null;
        }

        newColor = endColor;
        //elapsed = 0;
    }
}
