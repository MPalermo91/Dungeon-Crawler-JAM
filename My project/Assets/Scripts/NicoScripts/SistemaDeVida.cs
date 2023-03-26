using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeVida : MonoBehaviour
{

    public static SistemaDeVida Instance { get; private set; }

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


    //public Text healthText;
    public Image p_healthBar;
    float p_health, p_maxHealth = 10;
    float lerpSpeed;


    public Image e_healthBar;
    float e_health, e_maxHealth = 10;


    private void Start()
    {
        p_health = p_maxHealth;
        e_health = e_maxHealth;
    }

    private void Update()
    {
        //healthText.text = "Health: " + health + "%";
        
        if (p_health > p_maxHealth)
            p_health = p_maxHealth;
        
        lerpSpeed = 3f * Time.deltaTime;
        
        HealthBarFiller(p_health, p_maxHealth, p_healthBar);
        HealthBarFiller(e_health, e_maxHealth, e_healthBar);
        ColorChanger(p_health, p_maxHealth, p_healthBar);
        ColorChanger(e_health, e_maxHealth, e_healthBar);
    }
    
    void HealthBarFiller(float health, float maxHealth, Image healthBar)
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }
    
      void ColorChanger(float health, float maxHealth, Image healthBar)
  {
    Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

    healthBar.color = healthColor;
  }

  public void HacerDañoPlayer(int daño)
  {
    p_health = p_health - daño;

    if (p_health <= 0)
    {
        Debug.Log("Has muerto");
    }
  }


  public void HacerDañoEnemigo(int daño)
  {
    e_health = e_health - daño;

    if (e_health <= 0)
    {
        Debug.Log("Has ganado");
    }
  }

}
