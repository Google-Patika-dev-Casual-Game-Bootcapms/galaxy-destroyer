using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GalaxyLogoAnimation()
    {
        Color alpha = GetComponent<Image>().color;
        for (float a = 0; a <= 10; a += 1)
        {
            alpha.a = a/10f;
            GetComponent<Image>().color = alpha;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int a = 10; a >= 0; a -= 1)
        {
            alpha.a = a/10f;
            GetComponent<Image>().color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
