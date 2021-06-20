using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion1 : IMinionController
{  
    public string name = "Yol İzleyici";   // Türkçe karakter sıkıntı yaratır mı?
    private string type = "Flying";
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Follow()
    {
        throw new System.NotImplementedException();
    }

    public void Movement()
    {
        throw new System.NotImplementedException();
    }

    public void Path()
    {
        throw new System.NotImplementedException();
    }

    public void Phase()
    {
        throw new System.NotImplementedException();
    }

    public string Type()
    {
        return type;
    }
}
