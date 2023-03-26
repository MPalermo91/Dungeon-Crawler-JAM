 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 using System.Linq;


 
 public class MazoEnemigoManager : MonoBehaviour
 {

    public static MazoEnemigoManager Instance { get; private set; }

    public List<Sprite> mazoGuardado = new List<Sprite>();


    public List<Sprite> mazoEne1 = new List<Sprite>();
    public List<Sprite> mazoEne2 = new List<Sprite>();
    public List<Sprite> mazoEne3 = new List<Sprite>();
    public List<Sprite> mazoEne4 = new List<Sprite>();
    public List<Sprite> mazoEne5 = new List<Sprite>();
    public List<Sprite> mazoBoss1 = new List<Sprite>();
    public List<Sprite> mazoBoss2 = new List<Sprite>();
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



    //Setteo de enemigo
    
    [SerializeField] int tipoEnemigo; 

    public int Get_TipoEnemigo()
    {
        return tipoEnemigo;
    }

    public void Set_TipoEnemigo(int nuevoEnemigo)
    {
        tipoEnemigo = nuevoEnemigo;
    }


    private void MazosEnemigos()
    {
        if (tipoEnemigo == 0)
        {
            mazoGuardado = new List<Sprite>(mazoEne1);
        }
        if (tipoEnemigo == 1)
        {
            mazoGuardado = new List<Sprite>(mazoEne2);
        }
        if (tipoEnemigo == 2)
        {
            mazoGuardado = new List<Sprite>(mazoEne3);
        }
        if (tipoEnemigo == 3)
        {
            mazoGuardado = new List<Sprite>(mazoEne4);
        }
        if (tipoEnemigo == 4)
        {
            mazoGuardado = new List<Sprite>(mazoEne5);
        }
        if (tipoEnemigo == 5)
        {
            mazoGuardado = new List<Sprite>(mazoBoss1);
        }
        if (tipoEnemigo == 6)
        {
            mazoGuardado = new List<Sprite>(mazoBoss2);
        }
    }

    private void OnEnable() 
    {
        MazosEnemigos();
    }






    /*public void GuardarMazo(List<Sprite> imazo)
    {

        mazoGuardado = new List<Sprite>(imazo);
        //mazoGuardado = Barajar();
    }*/

    /*public List<Sprite> Get_Mazo()
    {
        List<Sprite> mazoBarajado = new List<Sprite>(mazoGuardado);
        
        
        return new List<Sprite>(mazoBarajado);
    }*/


    //funcion barajar un mazo
    private List<Sprite> Barajar(List<Sprite> mazoNuevo)
    {  
        //var size : int = data.length;
        //mazoGuardado.Count


        for (int i = 0; i < mazoNuevo.Count; i++)
        {
            int indexToSwap = Random.Range(i, mazoNuevo.Count);
            var oldValue = mazoNuevo[i];
            mazoNuevo[i] = mazoNuevo[indexToSwap];
            mazoNuevo[indexToSwap] = oldValue;
        }

        return mazoNuevo;
        
    }


    //Control de cartas, para las listas
    [SerializeField] int numeroDeCarta = 0;
    [SerializeField] Sprite spriteActual;

     public void ContadorDeCartas()
     {
        if(numeroDeCarta == 0)
        {
            mazoGuardado = Barajar(mazoGuardado);

        }
        else if (numeroDeCarta >= mazoGuardado.Count)
        {
            numeroDeCarta = 0;
            mazoGuardado = Barajar(mazoGuardado);
        }
        spriteActual = mazoGuardado[numeroDeCarta];

        numeroDeCarta++;
     }

     public Sprite Get_SpriteActual()
     {
        return spriteActual;
     }

    //Recordatorio: me quede en la parte de los mazos y el generador de cartas (tambien falta el daño y demas)



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