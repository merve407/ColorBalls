using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public TextMeshProUGUI skorText;
    public GameObject RestartButton;
    public GameObject StartButton;
    
    private int skor;
    public bool oyunBasladi;

    void Start()
    {
        GameManagerInstance = this;
        oyunBasladi = false;
        
    }
    
    //Oyunu baslat butonuna basınca çalışıyor
    public void OyunuBaslat()
    {
        skor = 0;
        oyunBasladi = true;
        
        StartButton.SetActive(false);
    }

    //Yeniden baslat butonuna basınca çalışıyor
    public void YenidenBaslat()
    {
        oyunBasladi = false;
        RestartButton.SetActive(false);
        
        SahneyiYenidenYukle();
        
    }
    
    //Yeniden baslat butonunun top çarpınca veya bölüm sonuna gelince aktif edildiği yer
    public void OnRestartButton()
    {
        RestartButton.SetActive(true);
    }
    
    //Skorun eklenerek ekrana yazdırıldığı yer
    public void SkorEkle(int deger)
    {
        skor += deger;
        skorText.text = skor.ToString();
    }
    
    //Oyunun Tekrardan başlatıldığı yer
    private void SahneyiYenidenYukle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
