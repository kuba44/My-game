using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] Image imageToFade;

    [SerializeField] Image weaponsImage;
    [SerializeField] Text weaponsName;

    public Slider healthSlider;
    public Text healthText;

    [SerializeField] GameObject deathScreen;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFade");
    }

    public void WeaponChangeUI( Sprite gunImage, string gunName)
    {
        weaponsImage.sprite = gunImage;
        weaponsName.text = gunName;
    }

    public void DeathScreenOn()
    {
        deathScreen.SetActive(true);
    }

}
