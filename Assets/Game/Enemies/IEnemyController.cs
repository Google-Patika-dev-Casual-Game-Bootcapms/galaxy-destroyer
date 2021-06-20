using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController
{
    string Type();    // Başka bir data tipine de döndürebiliriz.
    void Movement();
    void Attack();
    void Follow();  // Her düşmanın follow yeteneği olacak mı? Olmayacak ise direk olarak classlara da eklenebilir
    void Phase();
    void Death();

}
