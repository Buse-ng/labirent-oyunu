using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopControl : MonoBehaviour
{
    public UnityEngine.UI.Button btn;

    public UnityEngine.UI.Text zaman, can,sonuc;
    float zamanSayac = 15;
    int canSayac = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;

    private Rigidbody rb;
    private float hiz=2.5f;
    void Start()
    {
        can.text = canSayac + "";
        rb = GetComponent<Rigidbody> ();
    }
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayac -= Time.deltaTime; // zamani saniyede 1 tane dusurecek.
            zaman.text = (int)zamanSayac + ""; // zaman sayaci float oldugu icin stringe cevirmek icin "" eklemeyi unutma. kusuratlý degilde tam sayi olarak zamani saysin diye int e cevirdik.

        }
        else if(!oyunTamam)
        {
            sonuc.text = "OYUN TAMAMLANAMADI.";
            btn.gameObject.SetActive(true);

        }

        //oyunun bitip bitmedigini kontrol edecegiz.
        if (zamanSayac < 0)
        {
            oyunDevam = false;
        }
    
    }
    void FixedUpdate() // fiziksel hesaplama gerektirenleri genelede fixed in icine yazýyoruz.
    {
        if (oyunDevam && !oyunTamam) // eger oyun devam etmiyorsa bu islemleri yaomayacak.
        {
            float dikey = Input.GetAxis("Horizontal");  // edit-> project settings-> Ýnput  týkladýðýmýzda axix ekraný geliyor. horizantal ve vertical ý ordan istedigimiz gibi degisebiliriz.
            float yatay = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(dikey, 0, yatay);
            rb.AddForce(kuvvet * hiz);
        }
       
        else
        {
            rb.velocity = Vector3.zero; // hareket hizi bunu yaparak hareket etmesini engelleriz.
            rb.angularVelocity = Vector3.zero; // dongusel hizi
        }
        
    }
    private void OnCollisionEnter(Collision cls)
    {
        //Debug.Log(cls.gameObject.name); // carpisma gerceklestiginde karsi taraftaki objenin ismini verecek.
        string objName = cls.gameObject.name;
        if (objName.Equals("bitis"))
        {
            // print("Oyun tamamlandi!");
            oyunTamam = true;
            sonuc.text = "Oyun tamamlandi.Tebrikler!";
            btn.gameObject.SetActive(true);
        }
        else if(!objName.Equals("zemin")&& !objName.Equals("labirentZemini"))
        {
            canSayac -= 1;     //canSayac i her carpmada 1 deger dusurecegiz.
            can.text = canSayac + "";
            if (canSayac <= 0)
            {
                oyunDevam = false;
            }
        }
    }
}
