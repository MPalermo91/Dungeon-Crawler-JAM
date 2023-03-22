 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 using System.Linq;


 
 public class MazoManager : MonoBehaviour
 {

    public static MazoManager Instance { get; private set; }

    public List<Sprite> mazoGuardado = new List<Sprite>();
    //private List<Sprite> mazoExportado = new List<Sprite>();


    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GuardarMazo(List<Sprite> imazo)
    {

        mazoGuardado = new List<Sprite>(imazo);
        mazoGuardado = Barajar();
    }

    public List<Sprite> Get_Mazo()
    {
        List<Sprite> mazoBarajado = new List<Sprite>(mazoGuardado);
        
        
        return new List<Sprite>(mazoBarajado);
    }

    private List<Sprite> Barajar()
    {  
        //var size : int = data.length;
        //mazoGuardado.Count


        for (int i = 0; i < mazoGuardado.Count; i++)
        {
            int indexToSwap = Random.Range(i, mazoGuardado.Count);
            var oldValue = mazoGuardado[i];
            mazoGuardado[i] = mazoGuardado[indexToSwap];
            mazoGuardado[indexToSwap] = oldValue;
        }

        return mazoGuardado;
        
    }


/*function ShuffleT$$anonymous$$s(data : Array) : Array
 {  
     var size : int = data.length;
  
     for (var i : int = 0; i < size; i++)
     {
         var indexToSwap : int = Random.Range(i, size);
         var oldValue = data[i];
         data[i] = data[indexToSwap];
         data[indexToSwap] = oldValue;
     }
     return data;
 }*/

}