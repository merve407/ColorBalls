using System.Collections;   
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform Yollar;
    public Camera anaKamera;
    
    //Top Referansları
    public Transform TopTransform;
    public Rigidbody TopRigidbody;
    public Collider TopCollider;

    public Renderer TopMeshRenderer;

    public ParticleSystem ToplarıToplamaEfekti;
    public ParticleSystem havadakiCizgiEfekti;
    public ParticleSystem sacilma;

    public Material[] TopMateryalleri = new Material[2];
    
    
    [Header("Oynanis Ayarlari")]
    public float SwipeSpeed;
    public float KameraHizi = 0.5f;
    public float YolDegistirmeHizi = 30f;
    public float TopunDönmeHizi = 650f;
    
    
    private float camVelocity_x,camVelocity_y;
    private float startTouchPos, deltaPos;

    void Update()
    {
        if (GameManager.GameManagerInstance.oyunBasladi)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //İlk tıklanma noktası alındı
                startTouchPos = Input.mousePosition.x;
            }
            
            if (Input.GetMouseButton(0))
            {
                //hareket ettirme var mı diye kontrol edildi
                deltaPos = Input.mousePosition.x - startTouchPos;
                startTouchPos = Input.mousePosition.x;
            }
            if(Input.GetMouseButtonUp(0))
            {
                //hareket ettirme 0 landı
                deltaPos = 0;
            }

            if (Mathf.Abs(deltaPos) > 0.01f)
            {
                //sağ sol miktarı oranlandı
                var movePos = ( Time.deltaTime * SwipeSpeed * deltaPos  * Vector3.right);
                movePos += transform.position;
                
                //Sağ sol yapılabilicek sınırlar belirlendi
                movePos.x = Mathf.Clamp(movePos.x, -1.5f, 1.5f);
                
                //sağ sol uygulandı
                transform.position = movePos;
            }
            //Yukarıda inputlar alınarak sağ sol yapıldığındaki bilgiler alındı ve uygulatıldı
            
            //ileriye doğru hareket ettirme sağlandı
            var yollarPosition = Yollar.position;
            Yollar.position = Vector3.MoveTowards(yollarPosition,new Vector3(yollarPosition.x,yollarPosition.y,-1000f), Time.deltaTime * YolDegistirmeHizi);
            TopTransform.Rotate(Vector3.right * TopunDönmeHizi * Time.deltaTime);

        }
    }

    private void LateUpdate()
    {
        //Kamera takibi sağlandı
        var cameraNewPos = anaKamera.transform.position;
        
        if (TopRigidbody.isKinematic)
            anaKamera.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x,TopTransform.transform.position.x,ref camVelocity_x,KameraHizi )
                , Mathf.SmoothDamp(cameraNewPos.y,TopTransform.transform.position.y + 3f,ref camVelocity_y,KameraHizi ),cameraNewPos.z);
  
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem toplarıToplamaEfekti = new ParticleSystem();

        switch (other.gameObject.tag)
        {
            case "obstacle":
                gameObject.SetActive(false);
                GameManager.GameManagerInstance.OnRestartButton();
                break;
            case "red":
                //Değdiği objenin kapatıldığı yer
                other.gameObject.SetActive(false);
                
                //Topun Renginin Değişildiği yer
                TopMateryalleri[1] = other.GetComponent<Renderer>().material;
                TopMeshRenderer.materials = TopMateryalleri;
                
                //Toplanınca çıkan efektin oluştuğu ve rengini değiştiği yer
                toplarıToplamaEfekti = Instantiate(ToplarıToplamaEfekti, transform.position, Quaternion.identity);
                toplarıToplamaEfekti.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                
                //1 puan ekle
                GameManager.GameManagerInstance.SkorEkle(1);
                break;
            
            case "green":
                //Değdiği objenin kapatıldığı yer
                other.gameObject.SetActive(false);
                
                //Topun Renginin Değişildiği yer
                TopMateryalleri[1] = other.GetComponent<Renderer>().material;
                TopMeshRenderer.materials = TopMateryalleri;
                
                //Toplanınca çıkan efektin oluştuğu ve rengini değiştiği yer
                toplarıToplamaEfekti = Instantiate(ToplarıToplamaEfekti, transform.position, Quaternion.identity);
                toplarıToplamaEfekti.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                
                //1 puan ekle
                GameManager.GameManagerInstance.SkorEkle(1);
                break;
            
            case "yellow":
                //Değdiği objenin kapatıldığı yer
                other.gameObject.SetActive(false);
                
                //Topun Renginin Değişildiği yer
                TopMateryalleri[1] = other.GetComponent<Renderer>().material;
                TopMeshRenderer.materials = TopMateryalleri;
                
                //Toplanınca çıkan efektin oluştuğu ve rengini değiştiği yer
                toplarıToplamaEfekti = Instantiate(ToplarıToplamaEfekti, transform.position, Quaternion.identity);
                toplarıToplamaEfekti.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                
                //1 puan ekle
                GameManager.GameManagerInstance.SkorEkle(1);
                break;
            
            case "blue":
                //Değdiği objenin kapatıldığı yer
                other.gameObject.SetActive(false);
                
                //Topun Renginin Değişildiği yer
                TopMateryalleri[1] = other.GetComponent<Renderer>().material;
                TopMeshRenderer.materials = TopMateryalleri;
                
                //Toplanınca çıkan efektin oluştuğu ve rengini değiştiği yer
                toplarıToplamaEfekti = Instantiate(ToplarıToplamaEfekti, transform.position, Quaternion.identity);
                toplarıToplamaEfekti.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                
                //1 puan ekle
                GameManager.GameManagerInstance.SkorEkle(1);
                break;
            case "BitisCizgisi":
                StartCoroutine(DelayFinish());
                break;
        }
        

    }
    IEnumerator DelayFinish()
    {
        GameManager.GameManagerInstance.oyunBasladi = false;
        yield return new WaitForSeconds(3f);
        GameManager.GameManagerInstance.OnRestartButton();
    }

   //Oncollisionenter objelerin bir birbirinde değdiğinde ilk değdikleri anda 1 kere çalışır collision  , unitynin hazır methodlarından biri
   //Yere indğinde çalışıyor
   private void OnCollisionEnter(Collision collider)
    {
        //Yoldan yola atlarken birinden çıkıp diğerine girdiğinde çalışan method
        if (collider.collider.tag == "Yol")
        {
            
            //topun (isKinematic = true) yaparak fizik etkilerinden kurtulması sağlanır
            TopRigidbody.isKinematic = true;
            //topun (isTrigger = true) yaparak objelerin içinden geçmesi açılır
            TopCollider.isTrigger = true;
            YolDegistirmeHizi = 30f;
            
            //havadaki çizgi efektinin hızının azaltıldığı yer ,yerde iken hızı 10 du 4 de düşürüldü şuan
            var airEffectMain = havadakiCizgiEfekti.main;
            airEffectMain.simulationSpeed = 4f;

            //saçılma efektinin renk ve yerinin ayarlandığı yer
            sacilma.transform.position = collider.contacts[0].point + new Vector3(0f,0.3f,0f);
            sacilma.GetComponent<Renderer>().material = TopMeshRenderer.materials[1];
            
            //Saçılma efektinin oynatıldığı yer
            sacilma.Play();
            
            //Topun dönme hızının değiştirildiği yer
            TopunDönmeHizi = 500f;
        } 
    }
    
    //OnTriggerEnter objelerin bir birbirinde değdiğinde ilk değdikleri anda 1 kere çalışır , unitynin hazır methodlarından biri
    //Collision ile arasındaki fark collision obje içine girmesine izin vermez ike triggerda içine girip çıkabilir
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Yol")
        {
            //topun (isKinematic = false) yaparak fizikten etkilenmesi sağlandı
            TopRigidbody.isKinematic = false;
            //topun (isTrigger = true) yaparak objelerin içinden geçmesi kapatılır
            TopCollider.isTrigger = false;
            TopRigidbody.velocity = new Vector3(0f,8f,0f);
            
            //ileriye gitme hızı 2 katına çıkarılır
            YolDegistirmeHizi *= 2;

            //havadaki çizgi efektinin hızının arttırıldığı yer , havada iken hızı 4 dü yere inince 10 a çıkarıldı
            var airEffectMain = havadakiCizgiEfekti.main;
            airEffectMain.simulationSpeed = 10f;
            
            //Topun dönme hızının değiştirildiği yer
            TopunDönmeHizi = 1000f;
        }
    }

    
}
