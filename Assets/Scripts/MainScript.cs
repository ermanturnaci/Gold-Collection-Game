﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public static Boolean dene = true;

    public static MainScript _instance;
    public InputField xEkseni; // giriş ekranından sonra oluşacak masamızın boyutlarını unityden tutmak için oluşturduğumuz değişken.
    public InputField yEkseni;// +++++++++++++++++++++++++++++
    public InputField altınMiktarı;
    public InputField gizliAltınMiktarı;
    public InputField adımSayısı;
    public InputField kullanıcıAltın;
    public InputField aHamle;
    public InputField aHedef;
    public InputField bHamle;
    public InputField bHedef;
    public InputField cHamle;
    public InputField cHedef;
    public InputField dHamle;
    public InputField dHedef;
    public static int xKenar;// bu değişkenleri kod içerisinde tutmak için oluşturduğumuz değişken.
    public static int yKenar;//+++++++++++++++++++++++++++++4
    public static int altınMiktar;
    public static int gizliAltınMiktar;
    public static int adımSayı;
    public static int kullanıcıAltınSayısı;
    public static int aHamleMaliyet;
    public static int aHedefMaliyet;
    public static int bHamleMaliyet;
    public static int bHedefMaliyet;
    public static int cHamleMaliyet;
    public static int cHedefMaliyet;
    public static int dHamleMaliyet;
    public static int dHedefMaliyet;
    public GameObject karePrefab; // 
    public GameObject altinPrefab;//  Masa oluşumu ve altınların yerleştirilmesi için oluşturulan nobjeler.
    public GameObject altinPrefab10;//
    public GameObject gizliAltinPrefab;//
    public GameObject oyuncuPrefab;
    public GameObject kirmiziOyuncuPrefab;
    public GameObject maviOyuncuPrefab;
    public GameObject yeşilOyuncuPrefab;
    public GameObject morOyuncuPrefab;
    public GameObject[,] gizliAltın;
    public static int kareSayisi;
    public static int altinKareSayisi;
    public static int gizliAltinKareSayisi;
    public static int altinKareSabit;
    public static int gizliAltinKareSabit;
    public static Vector3 kirmiziVektör;
    public static Vector3 maviVektör;
    public static Vector3 yeşilVektör;
    public static Vector3 morVektör;
    public static Vector3 hedef1;
    public static Vector3 hedef2;
    public static Boolean calistiMi = false;
    public static Boolean kırmızıHareketEt = false;
    public static Boolean maviHareketEt = false;
    public static Boolean morHareketEt = false;
    public static Boolean yeşilHareketEt = false;
    public static List<Vector3> altinVektör = new List<Vector3>();
    public static List<Vector3> gizliAltinVektör = new List<Vector3>();
    public static List<Vector3> altinVektör5 = new List<Vector3>();
    public static List<Vector3> altinVektör10 = new List<Vector3>();
    public static List<Vector3> açılanVektör = new List<Vector3>();
    public static List<AltinTile> altinlar = new List<AltinTile>();
    public static Vector3 hedef3;
    public static Vector3 hedef4;
    public static float kirmiziSonUzunluk;
    public static float maviSonUzunluk;
    public static float yesilSonUzunluk;
    public static string dosyaYolu;
    public static Vector3 hedef5;
    public static Vector3 hedef6;
    public static Vector3 hedef7;
    public static Vector3 hedefGizli;
    public static float kirmiziKasaAltin;
    public static float maviKasaAltin;
    public static float yesilKasaAltin;
    public static float morKasaAltin;
    public static float kirmiziAdim;
    public static float maviAdim;
    public static float yesilAdim;
    public static float morAdim;
    public static float kirmiziHarcanan;
    public static float maviHarcanan;
    public static float yesilHarcanan;
    public static float morHarcanan;
    public static float kirmiziToplanan;
    public static float maviToplanan;
    public static float yesilToplanan;
    public static float morToplanan;
    public static Vector3 hedef8;


    Tile[,] tiles;
    public AltinTile[,] altinTiles;
    public AltinTile[,] gizliAltinTiles;
    Kullanici kullaniciKirmizi;
    Kullanici kullaniciMavi;
    Kullanici kullaniciMor;
    Kullanici kullaniciYeşil;



    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        tiles = new Tile[xKenar, yKenar];
        altinTiles = new AltinTile[xKenar, yKenar];
        gizliAltinTiles = new AltinTile[xKenar, yKenar];

        kirmiziVektör = new Vector3(-1, -1, 0);
        maviVektör = new Vector3(xKenar, -1, 0);
        morVektör = new Vector3(-1, yKenar, 0);
        yeşilVektör = new Vector3(xKenar, yKenar, 0);

        kullaniciKirmizi = new Kullanici(kirmiziVektör);
        kullaniciMavi = new Kullanici(maviVektör);
        kullaniciMor = new Kullanici(morVektör);
        kullaniciYeşil = new Kullanici(yeşilVektör);

        kullaniciKirmizi.Adım = kirmiziVektör + "->";
        kullaniciMavi.Adım = maviVektör + "-> ";
        kullaniciMor.Adım = morVektör + "->";
        kullaniciYeşil.Adım = yeşilVektör + "->";


        StartCoroutine(SetupTiles(xKenar, yKenar));
        Thread.Sleep(3);


    }
    public void OyuncuOlustur(Vector3 kirmiziVektör, Vector3 maviVektör, Vector3 yeşilVektör, Vector3 morVektör) // masadaki oyuncuları oluşturan fonksiyon.
    {
        kirmiziOyuncuPrefab = Instantiate(kirmiziOyuncuPrefab, kirmiziVektör, Quaternion.identity) as GameObject;
        kirmiziOyuncuPrefab.name = "Kırmızı Oyuncu";
        kullaniciKirmizi = kirmiziOyuncuPrefab.GetComponent<Kullanici>();
        kirmiziOyuncuPrefab.transform.parent = transform;
        kullaniciKirmizi.KonumVektörü = kirmiziVektör;
        kullaniciKirmizi.AltinMiktari = kullanıcıAltınSayısı;

        maviOyuncuPrefab = Instantiate(maviOyuncuPrefab, maviVektör, Quaternion.identity) as GameObject;
        maviOyuncuPrefab.name = "Mavi Oyuncu";
        kullaniciMavi = maviOyuncuPrefab.GetComponent<Kullanici>();
        maviOyuncuPrefab.transform.parent = transform;
        kullaniciMavi.KonumVektörü = maviVektör;
        kullaniciMavi.AltinMiktari = kullanıcıAltınSayısı;

        yeşilOyuncuPrefab = Instantiate(yeşilOyuncuPrefab, yeşilVektör, Quaternion.identity) as GameObject;
        yeşilOyuncuPrefab.name = "Yeşil Oyuncu";
        kullaniciYeşil = yeşilOyuncuPrefab.GetComponent<Kullanici>();
        yeşilOyuncuPrefab.transform.parent = transform;
        kullaniciYeşil.KonumVektörü = yeşilVektör;
        kullaniciYeşil.AltinMiktari = kullanıcıAltınSayısı;

        morOyuncuPrefab = Instantiate(morOyuncuPrefab, morVektör, Quaternion.identity) as GameObject;
        morOyuncuPrefab.name = "Mor Oyuncu";
        kullaniciMor = morOyuncuPrefab.GetComponent<Kullanici>();
        morOyuncuPrefab.transform.parent = transform;
        kullaniciMor.KonumVektörü = morVektör;
        kullaniciMor.AltinMiktari = kullanıcıAltınSayısı;
        AltinUret5(xKenar, yKenar);

    }
    public IEnumerator SetupTiles(int xKenar, int yKenar) // Oyun Masasını oluşturan fonksiyon
    {
        kareSayisi = xKenar * yKenar;
        altinKareSayisi = kareSayisi * altınMiktar / 100;
        gizliAltinKareSayisi = altinKareSayisi * gizliAltınMiktar / 100;
        altinKareSabit = altinKareSayisi;

        if (gizliAltinKareSayisi < 1)
        {
            gizliAltinKareSayisi = 1;
            gizliAltinKareSabit = 1;
        }

        for (int i = 0; i < xKenar; i++) // kareleri oluşturan döngü.
        {
            for (int j = 0; j < yKenar; j++)
            {
                GameObject tile = Instantiate(karePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;

                tile.name = "Tile(" + i + "," + j + ") ";

                tiles[i, j] = tile.GetComponent<Tile>();

                tile.transform.parent = transform;

            }
        }
        OyuncuOlustur(kirmiziVektör, maviVektör, yeşilVektör, morVektör);



        yield return new WaitForSeconds(2);

    }
    public void AltinUret5(int xKenar, int yKenar) // masadaki altınları oluşturan fonksiyon.
    {
        for (int a = 0; a < altinKareSayisi / 2; a++)
        {
            int iRandom = UnityEngine.Random.Range(0, xKenar);
            int jRandom = UnityEngine.Random.Range(0, yKenar);

            GameObject tile2 = Instantiate(altinPrefab, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;

            tile2.name = "AltinTile(" + iRandom + "," + jRandom + ") ";

            altinTiles[iRandom, jRandom] = tile2.GetComponent<AltinTile>();

            tile2.transform.parent = transform;


            Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
            altinVektör.Add(yeniVektör);
            altinVektör5.Add(yeniVektör);
            altinTiles[iRandom, jRandom].AltinMiktari = 5;
            altinTiles[iRandom, jRandom].Konum = yeniVektör;
            altinTiles[iRandom, jRandom].GizliMi = false;



        }

        altinKareSayisi = kareSayisi * altınMiktar / 100;
        AltinUret10(xKenar, yKenar);
    }
    public void AltinUret10(int xKenar, int yKenar) // masadaki altınları oluşturan fonksiyon.
    {
        calistiMi = true;
        for (int a = 0; a < altinKareSayisi / 2; a++)
        {
            int iRandom = UnityEngine.Random.Range(0, xKenar);
            int jRandom = UnityEngine.Random.Range(0, yKenar);


            GameObject tile2 = Instantiate(altinPrefab10, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;

            tile2.name = "AltinTile(" + iRandom + "," + jRandom + ") ";

            altinTiles[iRandom, jRandom] = tile2.GetComponent<AltinTile>();

            tile2.transform.parent = transform;

            Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
            altinVektör.Add(yeniVektör);
            altinVektör10.Add(yeniVektör);
            altinTiles[iRandom, jRandom].AltinMiktari = 10;
            altinTiles[iRandom, jRandom].Konum = yeniVektör;
            altinTiles[iRandom, jRandom].GizliMi = false;

            calistiMi = false;


        }
        altinKareSayisi = kareSayisi * altınMiktar / 100;
        GizliAltinUret(xKenar, yKenar);
    }
    public void GizliAltinUret(int xKenar, int yKenar) // masadaki gizli altınları oluşturan fonksiyon.
    {
        for (int b = 0; b < gizliAltinKareSayisi; b++)
        {
            int iRandom = UnityEngine.Random.Range(0, xKenar);
            int jRandom = UnityEngine.Random.Range(0, yKenar);


            GameObject tile3 = Instantiate(gizliAltinPrefab, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;
            tile3.name = "AltinTile(" + iRandom + "," + jRandom + ") ";
            altinTiles[iRandom, jRandom] = tile3.GetComponent<AltinTile>();

            tile3.transform.parent = transform;

            tile3.GetComponent<SpriteRenderer>().enabled = false;

            Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
            gizliAltinVektör.Add(yeniVektör);
            altinTiles[iRandom, jRandom].AltinMiktari = 20;
            altinTiles[iRandom, jRandom].GizliMi = true;
            altinTiles[iRandom, jRandom].Konum = yeniVektör;



        }
        gizliAltinKareSayisi = altinKareSayisi * gizliAltınMiktar / 100;
        StartCoroutine(HareketKontrol());
    }
    public void KirmiziOyuncuHedefBelirle() // kırmızı oyuncunun hedef belirlediği fonksiyon. A oyuncusu
    {
        float hedefUzaklık = 123123123;
        calistiMi = true;
        foreach (Vector3 listedekiVektör in altinVektör)
        {
            float farkX = Math.Abs(kullaniciKirmizi.KonumVektörü.x - listedekiVektör.x);
            float farkY = Math.Abs(kullaniciKirmizi.KonumVektörü.y - listedekiVektör.y);
            float toplamFark = farkX + farkY;
            if (toplamFark < hedefUzaklık)
            {
                hedefUzaklık = toplamFark;
                kullaniciKirmizi.Hedef = listedekiVektör;
            }

        }
        kullaniciKirmizi.AltinMiktari -= aHedefMaliyet;
        kirmiziHarcanan += aHedefMaliyet;
        kirmiziSonUzunluk = hedefUzaklık;
    }
    public void MaviOyuncuHedefBelirle() // Mavi oyuncun hedef belirlediği fonk. B Oyuncusu
    {
        float hedefUzaklık5 = 123123123;
        float hedefUzaklık10 = 123123123;
        float hedefUzaklıkGizli = 12312312;
        float toplamFark5;
        float toplamFark10;
        float toplamFarkGizli;
        
        foreach (Vector3 listedekiVektör5 in altinVektör5) // 5 değerinde
        {
            float farkX5 = Math.Abs(kullaniciMavi.KonumVektörü.x - listedekiVektör5.x);
            float farkY5 = Math.Abs(kullaniciMavi.KonumVektörü.y - listedekiVektör5.y);
            toplamFark5 = farkX5 + farkY5;

            if (toplamFark5 < hedefUzaklık5)
            {
                hedefUzaklık5 = toplamFark5;
                hedef1 = listedekiVektör5;
            }

        }
        foreach (Vector3 listedekiVektör10 in altinVektör10) // 10 değerinde
        {
            float farkX10 = Math.Abs(kullaniciMavi.KonumVektörü.x - listedekiVektör10.x);
            float farkY10 = Math.Abs(kullaniciMavi.KonumVektörü.y - listedekiVektör10.y);
            toplamFark10 = farkX10 + farkY10;


            if (toplamFark10 < hedefUzaklık10)
            {
                hedefUzaklık10 = toplamFark10;
                hedef2 = listedekiVektör10;
            }

        }
        foreach (Vector3 listedekiVektörGizli in açılanVektör) // 20 değerinde
        {
            float farkXGizli = Math.Abs(kullaniciMavi.KonumVektörü.x - listedekiVektörGizli.x);
            float farkYGizli = Math.Abs(kullaniciMavi.KonumVektörü.y - listedekiVektörGizli.y);
            toplamFarkGizli = farkXGizli + farkYGizli;
            if(toplamFarkGizli < hedefUzaklıkGizli)
            {
                hedefUzaklıkGizli = toplamFarkGizli;
                hedef7 = listedekiVektörGizli;
            }
        }


        if (hedefUzaklık10 / adımSayı <= (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı < (hedefUzaklık10 / adımSayı) * 2)
            {
                kullaniciMavi.Hedef = hedef7;
                maviSonUzunluk = hedefUzaklıkGizli;
            }
            else
            {
                kullaniciMavi.Hedef = hedef2;
                maviSonUzunluk = hedefUzaklık10;
            }

        }
        else if (hedefUzaklık10 / adımSayı > (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı > (hedefUzaklık5 / adımSayı) * 4)
            {
                kullaniciMavi.Hedef = hedef1;
                maviSonUzunluk = hedefUzaklık5;

            }
            else
            {
                kullaniciMavi.Hedef = hedef7;
                maviSonUzunluk = hedefUzaklıkGizli;
            }


        }

        kullaniciMavi.AltinMiktari -= bHedefMaliyet;
        maviHarcanan += bHedefMaliyet;
        Debug.Log("mavi hedef" + kullaniciMavi.Hedef);
    }
    public void YeşilOyuncuHedefBelirle() // Yeşil oyuncun hedef belirlediği fonk. C oyuncusu
    {
        float hedefUzaklık5 = 123123123;
        float hedefUzaklık10 = 123123123;
        float hedefUzaklıkGizli = 123123123;
        float toplamFark5;
        float toplamFark10;
        float toplamFarkGizli;

        foreach (Vector3 listedekiVektör5 in altinVektör5)
        {
            float farkX5 = Math.Abs(kullaniciYeşil.KonumVektörü.x - listedekiVektör5.x);
            float farkY5 = Math.Abs(kullaniciYeşil.KonumVektörü.y - listedekiVektör5.y);
            toplamFark5 = farkX5 + farkY5;

            if (toplamFark5 < hedefUzaklık5)
            {
                hedefUzaklık5 = toplamFark5;
                hedef5 = listedekiVektör5;
            }

        }
        foreach (Vector3 listedekiVektör10 in altinVektör10)
        {
            float farkX10 = Math.Abs(kullaniciYeşil.KonumVektörü.x - listedekiVektör10.x);
            float farkY10 = Math.Abs(kullaniciYeşil.KonumVektörü.y - listedekiVektör10.y);
            toplamFark10 = farkX10 + farkY10;


            if (toplamFark10 < hedefUzaklık10)
            {
                hedefUzaklık10 = toplamFark10;
                hedef6 = listedekiVektör10;
            }

        }
        foreach (Vector3 listedekiVektörGizli in gizliAltinVektör)
        {
            float farkXGizli = Math.Abs(kullaniciYeşil.KonumVektörü.x - listedekiVektörGizli.x);
            float farkYGizli = Math.Abs(kullaniciYeşil.KonumVektörü.y - listedekiVektörGizli.y);
            toplamFarkGizli = farkXGizli + farkYGizli;


            if (toplamFarkGizli < hedefUzaklıkGizli)
            {
                hedefUzaklıkGizli = toplamFarkGizli;
                hedefGizli = listedekiVektörGizli;
            }

        }
        try
        {
            GameObject.Find("AltinTile(" + hedefGizli.x + "," + hedefGizli.y + ") ").GetComponent<SpriteRenderer>().enabled = true;
            Vector3 vektör = new Vector3(hedefGizli.x, hedefGizli.y, 0);
            açılanVektör.Add(vektör);
            altinVektör.Add(vektör);
            gizliAltinVektör.Remove(vektör);
            altinKareSayisi++;
        }
        catch
        {

        }
        

        if (hedefUzaklık10 / adımSayı <= (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı < (hedefUzaklık10 / adımSayı) * 2)
            {
                kullaniciYeşil.Hedef = hedefGizli;
                yesilSonUzunluk = hedefUzaklıkGizli;
            }
            else
            {
                kullaniciYeşil.Hedef = hedef6;
                yesilSonUzunluk = hedefUzaklık10;
            }

        }
        else if (hedefUzaklık10 / adımSayı > (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı > (hedefUzaklık5 / adımSayı) * 4)
            {
                kullaniciYeşil.Hedef = hedef5;
                yesilSonUzunluk = hedefUzaklık5;

            }
            else
            {
                kullaniciYeşil.Hedef = hedefGizli;
                yesilSonUzunluk = hedefUzaklıkGizli;
            }


        }

        kullaniciYeşil.AltinMiktari -= cHedefMaliyet;
        yesilHarcanan += cHedefMaliyet;
        Debug.Log("yesil son hedef" + kullaniciYeşil.Hedef);

    }
    public void MorOyuncuHedefBelirle() //Mor oyuncunun hedef belirlediği fonksiyon. D oyuncusu
    {
        float hedefUzaklık5 = 123123123;
        float hedefUzaklık10 = 123123123;
        float hedefUzaklıkGizli = 123123123;
        float toplamFark5;
        float toplamFark10;
        float toplamFarkGizli;
        Debug.Log("mor konum" + kullaniciMor.KonumVektörü);
        foreach (Vector3 listedekiVektör5 in altinVektör5)
        {
            float farkX5 = Math.Abs(kullaniciMor.KonumVektörü.x - listedekiVektör5.x);
            float farkY5 = Math.Abs(kullaniciMor.KonumVektörü.y - listedekiVektör5.y);
            toplamFark5 = farkX5 + farkY5;

            if (toplamFark5 < hedefUzaklık5)
            {
                if (listedekiVektör5 != kullaniciMavi.Hedef && listedekiVektör5 != kullaniciKirmizi.Hedef && listedekiVektör5 != kullaniciYeşil.Hedef)
                {
                    hedefUzaklık5 = toplamFark5;
                    hedef3 = listedekiVektör5;
                }
                if (listedekiVektör5 == kullaniciKirmizi.Hedef)
                {
                    if (toplamFark5 < kirmiziSonUzunluk)
                    {
                        hedefUzaklık5 = toplamFark5;
                        hedef3 = listedekiVektör5;
                    }

                }
                if (listedekiVektör5 == kullaniciMavi.Hedef)
                {
                    if (toplamFark5 < maviSonUzunluk)
                    {
                        hedefUzaklık5 = toplamFark5;
                        hedef3 = listedekiVektör5;
                    }

                }
                if (listedekiVektör5 == kullaniciYeşil.Hedef)
                {
                    if (toplamFark5 < yesilSonUzunluk)
                    {
                        hedefUzaklık5 = toplamFark5;
                        hedef3 = listedekiVektör5;
                    }

                }
            }

        }
        foreach (Vector3 listedekiVektör10 in altinVektör10)
        {
            float farkX10 = Math.Abs(kullaniciMor.KonumVektörü.x - listedekiVektör10.x);
            float farkY10 = Math.Abs(kullaniciMor.KonumVektörü.y - listedekiVektör10.y);
            toplamFark10 = farkX10 + farkY10;


            if (toplamFark10 < hedefUzaklık10)
            {
                if (listedekiVektör10 != kullaniciMavi.Hedef && listedekiVektör10 != kullaniciKirmizi.Hedef && listedekiVektör10 != kullaniciYeşil.Hedef)
                {
                    hedefUzaklık10 = toplamFark10;
                    hedef4 = listedekiVektör10;
                }
                if (listedekiVektör10 == kullaniciKirmizi.Hedef)
                {
                    if (toplamFark10 < kirmiziSonUzunluk)
                    {
                        hedefUzaklık10 = toplamFark10;
                        hedef4 = listedekiVektör10;
                    }

                }
                if (listedekiVektör10 == kullaniciMavi.Hedef)
                {
                    if (toplamFark10 < maviSonUzunluk)
                    {
                        hedefUzaklık10 = toplamFark10;
                        hedef4 = listedekiVektör10;
                    }

                }
                if (listedekiVektör10 == kullaniciYeşil.Hedef)
                {
                    if (toplamFark10 < yesilSonUzunluk)
                    {
                        hedefUzaklık10 = toplamFark10;
                        hedef4 = listedekiVektör10;
                    }

                }
            }

        }
        foreach(Vector3 açılanVektörGizli in açılanVektör)
        {
            float farkX10 = Math.Abs(kullaniciMor.KonumVektörü.x - açılanVektörGizli.x);
            float farkY10 = Math.Abs(kullaniciMor.KonumVektörü.y - açılanVektörGizli.y);
            toplamFarkGizli = farkX10 + farkY10;

            if (toplamFarkGizli < hedefUzaklıkGizli)
            {
                if (açılanVektörGizli != kullaniciMavi.Hedef && açılanVektörGizli != kullaniciKirmizi.Hedef && açılanVektörGizli != kullaniciYeşil.Hedef)
                {
                    hedefUzaklıkGizli = toplamFarkGizli;
                    hedef8 = açılanVektörGizli;
                }
                if (açılanVektörGizli == kullaniciKirmizi.Hedef)
                {
                    if (toplamFarkGizli < kirmiziSonUzunluk)
                    {
                        hedefUzaklıkGizli = toplamFarkGizli;
                        hedef8 = açılanVektörGizli;
                    }

                }
                if (açılanVektörGizli == kullaniciMavi.Hedef)
                {
                    if (toplamFarkGizli < maviSonUzunluk)
                    {
                        hedefUzaklıkGizli = toplamFarkGizli;
                        hedef8 = açılanVektörGizli;
                    }

                }
                if (açılanVektörGizli == kullaniciYeşil.Hedef)
                {
                    if (toplamFarkGizli < yesilSonUzunluk)
                    {
                        hedefUzaklıkGizli = toplamFarkGizli;
                        hedef8 = açılanVektörGizli;
                    }

                }
            }
        }

        if (hedefUzaklık10 / adımSayı <= (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı < (hedefUzaklık10 / adımSayı) * 2)
            {
                kullaniciMor.Hedef = hedef8;
 
            }
            else
            {
                kullaniciMor.Hedef = hedef4;
  
            }

        }
        else if (hedefUzaklık10 / adımSayı > (hedefUzaklık5 / adımSayı) * 2)
        {
            if (hedefUzaklıkGizli / adımSayı > (hedefUzaklık5 / adımSayı) * 4)
            {
                kullaniciMor.Hedef = hedef3;

            }
            else
            {
                kullaniciMor.Hedef = hedef8;
    
            }


        }
        kullaniciMor.AltinMiktari -= dHedefMaliyet;
        morHarcanan += dHedefMaliyet;
        Debug.Log("mor kullanıcı son hedef " + kullaniciMor.Hedef);
        calistiMi = false;
        Debug.Log("altin mikt" + kullaniciMor.GetAltinMiktari());

    }
    public void KirmiziOyuncuHareketEttir() // kırmızı oyuncunun hareket ettiği fonksiyon.
    {
        Vector3 kirmiziIlkKonum = kullaniciKirmizi.KonumVektörü;
        try
        {
            int hamleSayisi = adımSayı;
            if (kırmızıHareketEt)
            {
                if (kullaniciKirmizi.Hedef == altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].Konum)// hareket etmeden önce hedef yerinde mi kontrol et
                {

                    if (math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) + math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) <= hamleSayisi)
                    {
                        if (kullaniciKirmizi.Hedef.x == kullaniciKirmizi.KonumVektörü.x && kullaniciKirmizi.Hedef.y != kullaniciKirmizi.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciKirmizi.KonumVektörü.y - kullaniciKirmizi.Hedef.y) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, math.abs(kullaniciKirmizi.KonumVektörü.y - kullaniciKirmizi.Hedef.y), 0);

                                if (kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y >= 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniY;
                                    kullaniciKirmizi.KonumVektörü += yeniY;
                                    if (kullaniciKirmizi.KonumVektörü == kullaniciKirmizi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if(altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if(altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }

                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciKirmizi.KonumVektörü -= yeniY;
                                    if (kullaniciKirmizi.KonumVektörü == kullaniciKirmizi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciKirmizi.KonumVektörü.y - kullaniciKirmizi.Hedef.y) > hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniY;
                                    kullaniciKirmizi.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciKirmizi.KonumVektörü -= yeniY;
                                }
                            }


                        }
                        else if (kullaniciKirmizi.Hedef.x != kullaniciKirmizi.KonumVektörü.x && kullaniciKirmizi.Hedef.y == kullaniciKirmizi.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x), 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x >= 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniX;
                                    kullaniciKirmizi.KonumVektörü += yeniX;
                                    if (kullaniciKirmizi.KonumVektörü == kullaniciKirmizi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciKirmizi.KonumVektörü -= yeniX;
                                    if (kullaniciKirmizi.KonumVektörü == kullaniciKirmizi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniX;
                                    kullaniciKirmizi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciKirmizi.KonumVektörü -= yeniX;
                                }
                            }
                        }
                        else
                        {
                            if (math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x), 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                                {
                                    if (kullaniciKirmizi.Hedef == kullaniciKirmizi.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }
                                    kirmiziOyuncuPrefab.transform.position += yeniX;
                                    kullaniciKirmizi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    if (kullaniciKirmizi.Hedef == kullaniciKirmizi.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                        kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            kirmiziToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            kirmiziToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            kirmiziToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        KirmiziOyuncuHedefBelirle();
                                    }
                                    kirmiziOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciKirmizi.KonumVektörü -= yeniX;
                                }
                            }

                            else if (math.abs(kullaniciKirmizi.KonumVektörü.x - kullaniciKirmizi.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniX;
                                    kullaniciKirmizi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciKirmizi.KonumVektörü -= yeniX;
                                }
                            }
                            else
                            {
                                if (kullaniciKirmizi.KonumVektörü == kullaniciKirmizi.Hedef)
                                {
                                    Vector3 silinecekVektör = new Vector3(kullaniciKirmizi.KonumVektörü.x, kullaniciKirmizi.KonumVektörü.y, 0);
                                    kullaniciKirmizi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari;
                                    altinVektör.Remove(silinecekVektör);
                                    if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 5)
                                    {
                                        altinVektör5.Remove(silinecekVektör);
                                        kirmiziToplanan += 5;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 10)
                                    {
                                        altinVektör10.Remove(silinecekVektör);
                                        kirmiziToplanan += 10;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciKirmizi.Hedef.x), Convert.ToInt32(kullaniciKirmizi.Hedef.y)].AltinMiktari == 20)
                                    {
                                        altinVektör.Remove(silinecekVektör);
                                        açılanVektör.Remove(silinecekVektör);
                                        kirmiziToplanan += 20;
                                    }
                                    altinKareSayisi--;
                                    KirmiziOyuncuHedefBelirle();
                                }
                            }
                        }

                    }
                    else
                    {
                        // x ya da y hangisi hamle sayısından büyükse o yöne hamle sayısı kadar ilerle. ya da 
                        // diğer if ( x ve y değer toplamı hamle sayısından büyükse ve x< hamle && y< hamle ise (örnek x=2 y=2 önce x bitir sonra y hareket).

                        if (math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) > hamleSayisi || math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) > hamleSayisi)
                        {
                            if (math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) > hamleSayisi && math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) <= hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniX;
                                    kullaniciKirmizi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciKirmizi.KonumVektörü -= yeniX;
                                }
                            }
                            else if (math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) >= hamleSayisi && math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeniY;
                                    kullaniciKirmizi.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciKirmizi.KonumVektörü -= yeniY;
                                }

                            }
                            else if (math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) >= hamleSayisi && math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) >= hamleSayisi)
                            {
                                Vector3 yeni = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                                {
                                    kirmiziOyuncuPrefab.transform.position += yeni;
                                    kullaniciKirmizi.KonumVektörü += yeni;
                                }
                                else
                                {
                                    kirmiziOyuncuPrefab.transform.position -= yeni;
                                    kullaniciKirmizi.KonumVektörü -= yeni;
                                }


                            }
                        }
                        else if (math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) + math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) > hamleSayisi
                            && math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x) <= hamleSayisi && math.abs(kullaniciKirmizi.Hedef.y - kullaniciKirmizi.KonumVektörü.y) <= hamleSayisi)
                        {
                            Vector3 yeni = new Vector3(math.abs(kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x), 0, 0);
                            if (kullaniciKirmizi.Hedef.x - kullaniciKirmizi.KonumVektörü.x > 0)
                            {
                                kirmiziOyuncuPrefab.transform.position += yeni;
                                kullaniciKirmizi.KonumVektörü += yeni;
                            }
                            else
                            {
                                kirmiziOyuncuPrefab.transform.position -= yeni;
                                kullaniciKirmizi.KonumVektörü -= yeni;
                            }

                        }


                    }



                }
                else
                {
                    KirmiziOyuncuHedefBelirle();
                    KirmiziOyuncuHareketEttir();

                }

            }
        }
        catch
        {
            kullaniciKirmizi.AltinMiktari += aHedefMaliyet;
            KirmiziOyuncuHedefBelirle();
            KirmiziOyuncuHareketEttir();
        }

        float ToplamX; float ToplamY;
        kırmızıHareketEt = false;
        kullaniciKirmizi.AltinMiktari -= aHamleMaliyet;
        kirmiziHarcanan += aHamleMaliyet;
        kullaniciKirmizi.Adım = kullaniciKirmizi.Adım + "->" + kullaniciKirmizi.KonumVektörü;
        Vector3 kirmiziSonKonum = kullaniciKirmizi.KonumVektörü;
        ToplamX = Math.Abs(kirmiziSonKonum.x - kirmiziIlkKonum.x);
        ToplamY = Math.Abs(kirmiziSonKonum.y - kirmiziIlkKonum.y);
        kirmiziAdim += (ToplamX + ToplamY);

    }
    public void MaviOyuncuHareketEttir()
    {
        int hamleSayisi = adımSayı;
        Vector3 maviIlkKonum = kullaniciMavi.KonumVektörü;
        try
        {
            if (maviHareketEt)
            {
                if (kullaniciMavi.Hedef == altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].Konum)// hareket etmeden önce hedef yerinde mi kontrol et
                {

                    if (math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) + math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) <= hamleSayisi)
                    {
                        if (kullaniciMavi.Hedef.x == kullaniciMavi.KonumVektörü.x && kullaniciMavi.Hedef.y != kullaniciMavi.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciMavi.KonumVektörü.y - kullaniciMavi.Hedef.y) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, math.abs(kullaniciMavi.KonumVektörü.y - kullaniciMavi.Hedef.y), 0);
                                if (kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y >= 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMavi.KonumVektörü += yeniY;
                                    if (kullaniciMavi.KonumVektörü == kullaniciMavi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }

                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMavi.KonumVektörü -= yeniY;
                                    if (kullaniciMavi.KonumVektörü == kullaniciMavi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciMavi.KonumVektörü.y - kullaniciMavi.Hedef.y) > hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMavi.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMavi.KonumVektörü -= yeniY;
                                }
                            }


                        }
                        else if (kullaniciMavi.Hedef.x != kullaniciMavi.KonumVektörü.x && kullaniciMavi.Hedef.y == kullaniciMavi.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x), 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x >= 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMavi.KonumVektörü += yeniX;
                                    if (kullaniciMavi.KonumVektörü == kullaniciMavi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMavi.KonumVektörü -= yeniX;
                                    if (kullaniciMavi.KonumVektörü == kullaniciMavi.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMavi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMavi.KonumVektörü -= yeniX;
                                }
                            }
                        }
                        else
                        {
                            if (math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x), 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                                {
                                    if (kullaniciMavi.Hedef == kullaniciMavi.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }
                                    maviOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMavi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    if (kullaniciMavi.Hedef == kullaniciMavi.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                        kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            maviToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MaviOyuncuHedefBelirle();
                                    }
                                    maviOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMavi.KonumVektörü -= yeniX;
                                }
                            }

                            else if (math.abs(kullaniciMavi.KonumVektörü.x - kullaniciMavi.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMavi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMavi.KonumVektörü -= yeniX;
                                }
                            }
                            else
                            {
                                if (kullaniciMavi.KonumVektörü == kullaniciMavi.Hedef)
                                {
                                    Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                                    kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                                    altinVektör.Remove(silinecekVektör);
                                    if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                                    {
                                        altinVektör5.Remove(silinecekVektör);
                                        maviToplanan += 5;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                                    {
                                        altinVektör10.Remove(silinecekVektör);
                                        maviToplanan += 10;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                                    {
                                        açılanVektör.Remove(silinecekVektör);
                                        altinVektör.Remove(silinecekVektör);
                                        maviToplanan += 20;
                                    }
                                    altinKareSayisi--;
                                    MaviOyuncuHedefBelirle();
                                }
                            }
                        }

                    }
                    else
                    {
                        // x ya da y hangisi hamle sayısından büyükse o yöne hamle sayısı kadar ilerle. ya da 
                        // diğer if ( x ve y değer toplamı hamle sayısından büyükse ve x< hamle && y< hamle ise (örnek x=2 y=2 önce x bitir sonra y hareket).

                        if (math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) > hamleSayisi || math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) > hamleSayisi)
                        {
                            if (math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) > hamleSayisi && math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) <= hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMavi.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMavi.KonumVektörü -= yeniX;
                                }
                            }
                            else if (math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) >= hamleSayisi && math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMavi.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMavi.KonumVektörü -= yeniY;
                                }

                            }
                            else if (math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) >= hamleSayisi && math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) >= hamleSayisi)
                            {
                                Vector3 yeni = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                                {
                                    maviOyuncuPrefab.transform.position += yeni;
                                    kullaniciMavi.KonumVektörü += yeni;
                                }
                                else
                                {
                                    maviOyuncuPrefab.transform.position -= yeni;
                                    kullaniciMavi.KonumVektörü -= yeni;
                                }


                            }
                        }
                        else if (math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) + math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) > hamleSayisi &&
                        math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x) <= hamleSayisi && math.abs(kullaniciMavi.Hedef.y - kullaniciMavi.KonumVektörü.y) <= hamleSayisi)
                        {
                            Vector3 yeni = new Vector3(math.abs(kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x), 0, 0);
                            if (kullaniciMavi.Hedef.x - kullaniciMavi.KonumVektörü.x > 0)
                            {
                                maviOyuncuPrefab.transform.position += yeni;
                                kullaniciMavi.KonumVektörü += yeni;
                            }
                            else
                            {
                                maviOyuncuPrefab.transform.position -= yeni;
                                kullaniciMavi.KonumVektörü -= yeni;
                            }

                        }


                    }



                }

                else 
                {
                    MaviOyuncuHedefBelirle();
                    MaviOyuncuHareketEttir();
                }
                if(kullaniciMavi.Hedef == kullaniciMavi.KonumVektörü)
                {
                    Vector3 silinecekVektör = new Vector3(kullaniciMavi.KonumVektörü.x, kullaniciMavi.KonumVektörü.y, 0);
                    kullaniciMavi.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari;
                    altinVektör.Remove(silinecekVektör);
                    if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 5)
                    {
                        altinVektör5.Remove(silinecekVektör);
                        maviToplanan += 5;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 10)
                    {
                        altinVektör10.Remove(silinecekVektör);
                        maviToplanan += 10;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciMavi.Hedef.x), Convert.ToInt32(kullaniciMavi.Hedef.y)].AltinMiktari == 20)
                    {
                        açılanVektör.Remove(silinecekVektör);
                        altinVektör.Remove(silinecekVektör);
                        maviToplanan += 20;
                    }
                }
            }
        }
        catch
        {
            kullaniciMavi.AltinMiktari += bHedefMaliyet;
            MaviOyuncuHedefBelirle();
            MaviOyuncuHareketEttir();
        }

        float ToplamX; float ToplamY;
        maviHareketEt = false;
        kullaniciMavi.AltinMiktari -= bHamleMaliyet;
        maviHarcanan += bHamleMaliyet;
        kullaniciMavi.Adım = kullaniciMavi.Adım + "->" + kullaniciMavi.KonumVektörü;
        Vector3 maviSonKonum = kullaniciMavi.KonumVektörü;
        ToplamX = Math.Abs(maviSonKonum.x - maviIlkKonum.x);
        ToplamY = Math.Abs(maviSonKonum.y - maviIlkKonum.y);
        maviAdim += (ToplamX + ToplamY);
    }
    public void MorOyuncuHareketEttir() // kırmızı oyuncunun hareket ettiği fonksiyon.
    {
        Vector3 morIlkKonum = kullaniciMor.KonumVektörü;

        int hamleSayisi = adımSayı;
        if (morHareketEt)
        {
            try
            {
                if (kullaniciMor.Hedef == altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].Konum)// hareket etmeden önce hedef yerinde mi kontrol et
                {

                    if (math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) + math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) <= hamleSayisi)
                    {
                        if (kullaniciMor.Hedef.x == kullaniciMor.KonumVektörü.x && kullaniciMor.Hedef.y != kullaniciMor.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciMor.KonumVektörü.y - kullaniciMor.Hedef.y) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, math.abs(kullaniciMor.KonumVektörü.y - kullaniciMor.Hedef.y), 0);
                                if (kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y >= 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMor.KonumVektörü += yeniY;
                                    if (kullaniciMor.KonumVektörü == kullaniciMor.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            morToplanan += 5;
                                        }
                                        else if(altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            morToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                    }

                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMor.KonumVektörü -= yeniY;
                                    if (kullaniciMor.KonumVektörü == kullaniciMor.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            morToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            morToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciMor.KonumVektörü.y - kullaniciMor.Hedef.y) > hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMor.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMor.KonumVektörü -= yeniY;
                                }
                            }


                        }
                        else if (kullaniciMor.Hedef.x != kullaniciMor.KonumVektörü.x && kullaniciMor.Hedef.y == kullaniciMor.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x), 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x >= 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMor.KonumVektörü += yeniX;
                                    if (kullaniciMor.KonumVektörü == kullaniciMor.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            morToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            morToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                    }
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMor.KonumVektörü -= yeniX;
                                    if (kullaniciMor.KonumVektörü == kullaniciMor.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            morToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            morToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMor.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMor.KonumVektörü -= yeniX;
                                }
                            }
                        }
                        else
                        {
                            if (math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x), 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                                {
                                    if (kullaniciMor.Hedef == kullaniciMor.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            morToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            morToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            altinVektör.Remove(silinecekVektör);
                                            açılanVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                    }
                                    morOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMor.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    if (kullaniciMor.Hedef == kullaniciMor.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                        kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        altinKareSayisi--;
                                        MorOyuncuHedefBelirle();
                                        if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            maviToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            maviToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            morToplanan += 20;
                                        }
                                    }
                                    morOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMor.KonumVektörü -= yeniX;
                                }
                            }

                            else if (math.abs(kullaniciMor.KonumVektörü.x - kullaniciMor.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMor.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMor.KonumVektörü -= yeniX;
                                }
                            }
                            else
                            {
                                if (kullaniciMor.KonumVektörü == kullaniciMor.Hedef)
                                {
                                    Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                                    kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                                    altinVektör.Remove(silinecekVektör);
                                    if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                                    {
                                        altinVektör5.Remove(silinecekVektör);
                                        morToplanan += 5;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                                    {
                                        altinVektör10.Remove(silinecekVektör);
                                        morToplanan += 10;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                                    {
                                        altinVektör.Remove(silinecekVektör);
                                        açılanVektör.Remove(silinecekVektör);
                                        morToplanan += 20;
                                    }
                                    altinKareSayisi--;
                                    MorOyuncuHedefBelirle();
                                }
                            }
                        }

                    }
                    else
                    {
                        // x ya da y hangisi hamle sayısından büyükse o yöne hamle sayısı kadar ilerle. ya da 
                        // diğer if ( x ve y değer toplamı hamle sayısından büyükse ve x< hamle && y< hamle ise (örnek x=2 y=2 önce x bitir sonra y hareket).

                        if (math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) > hamleSayisi || math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) > hamleSayisi)
                        {
                            if (math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) > hamleSayisi && math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) <= hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniX;
                                    kullaniciMor.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciMor.KonumVektörü -= yeniX;
                                }
                            }
                            else if (math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) >= hamleSayisi && math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeniY;
                                    kullaniciMor.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciMor.KonumVektörü -= yeniY;
                                }

                            }
                            else if (math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) >= hamleSayisi && math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) >= hamleSayisi)
                            {
                                Vector3 yeni = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                                {
                                    morOyuncuPrefab.transform.position += yeni;
                                    kullaniciMor.KonumVektörü += yeni;
                                }
                                else
                                {
                                    morOyuncuPrefab.transform.position -= yeni;
                                    kullaniciMor.KonumVektörü -= yeni;
                                }


                            }
                        }
                        else if (math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) + math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) > hamleSayisi
                            && math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x) <= hamleSayisi && math.abs(kullaniciMor.Hedef.y - kullaniciMor.KonumVektörü.y) <= hamleSayisi)
                        {
                            Vector3 yeni = new Vector3(math.abs(kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x), 0, 0);
                            if (kullaniciMor.Hedef.x - kullaniciMor.KonumVektörü.x > 0)
                            {
                                morOyuncuPrefab.transform.position += yeni;
                                kullaniciMor.KonumVektörü += yeni;
                            }
                            else
                            {
                                morOyuncuPrefab.transform.position -= yeni;
                                kullaniciMor.KonumVektörü -= yeni;
                            }

                        }


                    }



                }
                else
                {
                    MorOyuncuHedefBelirle();
                    MorOyuncuHareketEttir();
                }
                if(kullaniciMor.Hedef == kullaniciMor.KonumVektörü)
                {
                    Vector3 silinecekVektör = new Vector3(kullaniciMor.KonumVektörü.x, kullaniciMor.KonumVektörü.y, 0);
                    kullaniciMor.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari;
                    altinVektör.Remove(silinecekVektör);
                    if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 5)
                    {
                        altinVektör5.Remove(silinecekVektör);
                        morToplanan += 5;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 10)
                    {
                        altinVektör10.Remove(silinecekVektör);
                        morToplanan += 10;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciMor.Hedef.x), Convert.ToInt32(kullaniciMor.Hedef.y)].AltinMiktari == 20)
                    {
                        altinVektör.Remove(silinecekVektör);
                        açılanVektör.Remove(silinecekVektör);
                        morToplanan += 20;
                    }
                }

            }
            catch
            {
                kullaniciMor.AltinMiktari += dHedefMaliyet;
                MorOyuncuHedefBelirle();
                MorOyuncuHareketEttir();
            }


        }
        float ToplamX; float ToplamY;
        morHareketEt = false;
        kullaniciMor.AltinMiktari -= dHamleMaliyet;
        morHarcanan += dHamleMaliyet;
        kullaniciMor.Adım = kullaniciMor.Adım + "->" + kullaniciMor.KonumVektörü;
        Vector3 morSonKonum = kullaniciMor.KonumVektörü;
        ToplamX = Math.Abs(morSonKonum.x - morIlkKonum.x);
        ToplamY = Math.Abs(morSonKonum.y - morIlkKonum.y);
        morAdim += (ToplamX + ToplamY);

    }
    public void YeşilOyuncuHareketEttir()
    {
        int hamleSayisi = adımSayı;
        Vector3 yesilIlkKonum = kullaniciYeşil.KonumVektörü;

        try
        {
            if (yeşilHareketEt)
            {
                if (kullaniciYeşil.Hedef == altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].Konum)// hareket etmeden önce hedef yerinde mi kontrol et
                {

                    if (math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) + math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) <= hamleSayisi)
                    {
                        if (kullaniciYeşil.Hedef.x == kullaniciYeşil.KonumVektörü.x && kullaniciYeşil.Hedef.y != kullaniciYeşil.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciYeşil.KonumVektörü.y - kullaniciYeşil.Hedef.y) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, math.abs(kullaniciYeşil.KonumVektörü.y - kullaniciYeşil.Hedef.y), 0);
                                if (kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y >= 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniY;
                                    kullaniciYeşil.KonumVektörü += yeniY;
                                    if (kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if(altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }

                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciYeşil.KonumVektörü -= yeniY;
                                    if (kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciYeşil.KonumVektörü.y - kullaniciYeşil.Hedef.y) > hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniY;
                                    kullaniciYeşil.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciYeşil.KonumVektörü -= yeniY;
                                }
                            }


                        }
                        else if (kullaniciYeşil.Hedef.x != kullaniciYeşil.KonumVektörü.x && kullaniciYeşil.Hedef.y == kullaniciYeşil.KonumVektörü.y)
                        {
                            if (math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x), 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x >= 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniX;
                                    kullaniciYeşil.KonumVektörü += yeniX;
                                    if (kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciYeşil.KonumVektörü -= yeniX;
                                    if (kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }
                                }
                            }

                            else if (math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniX;
                                    kullaniciYeşil.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciYeşil.KonumVektörü -= yeniX;
                                }
                            }
                        }
                        else
                        {
                            if (math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x) < hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x), 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                                {
                                    if (kullaniciYeşil.Hedef == kullaniciYeşil.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }
                                    yeşilOyuncuPrefab.transform.position += yeniX;
                                    kullaniciYeşil.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    if (kullaniciYeşil.Hedef == kullaniciYeşil.KonumVektörü)
                                    {
                                        Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                        kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                        altinVektör.Remove(silinecekVektör);
                                        if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                        {
                                            altinVektör5.Remove(silinecekVektör);
                                            yesilToplanan += 5;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                        {
                                            altinVektör10.Remove(silinecekVektör);
                                            yesilToplanan += 10;
                                        }
                                        else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                        {
                                            açılanVektör.Remove(silinecekVektör);
                                            altinVektör.Remove(silinecekVektör);
                                            yesilToplanan += 20;
                                        }
                                        altinKareSayisi--;
                                        YeşilOyuncuHedefBelirle();
                                    }
                                    yeşilOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciYeşil.KonumVektörü -= yeniX;
                                }
                            }

                            else if (math.abs(kullaniciYeşil.KonumVektörü.x - kullaniciYeşil.Hedef.x) > hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniX;
                                    kullaniciYeşil.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciYeşil.KonumVektörü -= yeniX;
                                }
                            }
                            else
                            {
                                if (kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                                {
                                    Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                                    kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                                    altinVektör.Remove(silinecekVektör);
                                    if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                                    {
                                        altinVektör5.Remove(silinecekVektör);
                                        yesilToplanan += 5;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                                    {
                                        altinVektör10.Remove(silinecekVektör);
                                        yesilToplanan += 10;
                                    }
                                    else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                                    {
                                        açılanVektör.Remove(silinecekVektör);
                                        altinVektör.Remove(silinecekVektör);
                                        yesilToplanan += 20;
                                    }
                                    altinKareSayisi--;
                                    YeşilOyuncuHedefBelirle();
                                }
                            }
                        }

                    }
                    else
                    {
                        // x ya da y hangisi hamle sayısından büyükse o yöne hamle sayısı kadar ilerle. ya da 
                        // diğer if ( x ve y değer toplamı hamle sayısından büyükse ve x< hamle && y< hamle ise (örnek x=2 y=2 önce x bitir sonra y hareket).

                        if (math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) > hamleSayisi || math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) > hamleSayisi)
                        {
                            if (math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) > hamleSayisi && math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) <= hamleSayisi)
                            {
                                Vector3 yeniX = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniX;
                                    kullaniciYeşil.KonumVektörü += yeniX;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniX;
                                    kullaniciYeşil.KonumVektörü -= yeniX;
                                }
                            }
                            else if (math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) >= hamleSayisi && math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) <= hamleSayisi)
                            {
                                Vector3 yeniY = new Vector3(0, hamleSayisi, 0);
                                if (kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeniY;
                                    kullaniciYeşil.KonumVektörü += yeniY;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeniY;
                                    kullaniciYeşil.KonumVektörü -= yeniY;
                                }

                            }
                            else if (math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) >= hamleSayisi && math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) >= hamleSayisi)
                            {
                                Vector3 yeni = new Vector3(hamleSayisi, 0, 0);
                                if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                                {
                                    yeşilOyuncuPrefab.transform.position += yeni;
                                    kullaniciYeşil.KonumVektörü += yeni;
                                }
                                else
                                {
                                    yeşilOyuncuPrefab.transform.position -= yeni;
                                    kullaniciYeşil.KonumVektörü -= yeni;
                                }


                            }
                        }
                        else if (math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) + math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) > hamleSayisi &&
                        math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x) <= hamleSayisi && math.abs(kullaniciYeşil.Hedef.y - kullaniciYeşil.KonumVektörü.y) <= hamleSayisi)
                        {
                            Vector3 yeni = new Vector3(math.abs(kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x), 0, 0);
                            if (kullaniciYeşil.Hedef.x - kullaniciYeşil.KonumVektörü.x > 0)
                            {
                                yeşilOyuncuPrefab.transform.position += yeni;
                                kullaniciYeşil.KonumVektörü += yeni;
                            }
                            else
                            {
                                yeşilOyuncuPrefab.transform.position -= yeni;
                                kullaniciYeşil.KonumVektörü -= yeni;
                            }

                        }


                    }



                }

                else
                {
                    YeşilOyuncuHedefBelirle();
                    YeşilOyuncuHareketEttir();
                }
                if(kullaniciYeşil.KonumVektörü == kullaniciYeşil.Hedef)
                {
                    Vector3 silinecekVektör = new Vector3(kullaniciYeşil.KonumVektörü.x, kullaniciYeşil.KonumVektörü.y, 0);
                    kullaniciYeşil.AltinMiktari += altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari;
                    altinVektör.Remove(silinecekVektör);
                    if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 5)
                    {
                        altinVektör5.Remove(silinecekVektör);
                        yesilToplanan += 5;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 10)
                    {
                        altinVektör10.Remove(silinecekVektör);
                        yesilToplanan += 10;
                    }
                    else if (altinTiles[Convert.ToInt32(kullaniciYeşil.Hedef.x), Convert.ToInt32(kullaniciYeşil.Hedef.y)].AltinMiktari == 20)
                    {
                        açılanVektör.Remove(silinecekVektör);
                        altinVektör.Remove(silinecekVektör);
                        yesilToplanan += 20;
                    }
                }
            }
        }
        catch
        {
            kullaniciYeşil.AltinMiktari += cHedefMaliyet;
            YeşilOyuncuHedefBelirle();
            YeşilOyuncuHareketEttir();
        }

        float ToplamX; float ToplamY;
        yeşilHareketEt = false;
        kullaniciYeşil.AltinMiktari -= cHamleMaliyet;
        yesilHarcanan += cHamleMaliyet;
        kullaniciYeşil.Adım = kullaniciYeşil.Adım + "->" + kullaniciYeşil.KonumVektörü;
        Vector3 yesilSonKonum = kullaniciYeşil.KonumVektörü;
        ToplamX = Math.Abs(yesilSonKonum.x - yesilIlkKonum.x);
        ToplamY = Math.Abs(yesilSonKonum.y - yesilIlkKonum.y);
        yesilAdim += (ToplamX + ToplamY);

    }
    public void setget() // oynanacak masanın kenar ölçülerini tutan fonksiyon.
    {
        xKenar = Int32.Parse(xEkseni.text);
        yKenar = Int32.Parse(yEkseni.text);
        altınMiktar = Int32.Parse(altınMiktarı.text);
        gizliAltınMiktar = Int32.Parse(gizliAltınMiktarı.text);
        adımSayı = Int32.Parse(adımSayısı.text);
        kullanıcıAltınSayısı = Int32.Parse(kullanıcıAltın.text);
        aHamleMaliyet = Int32.Parse(aHamle.text);
        aHedefMaliyet = Int32.Parse(aHedef.text);
        bHamleMaliyet = Int32.Parse(bHamle.text);
        bHedefMaliyet = Int32.Parse(bHedef.text);
        cHamleMaliyet = Int32.Parse(cHamle.text);
        cHedefMaliyet = Int32.Parse(cHedef.text);
        dHamleMaliyet = Int32.Parse(dHamle.text);
        dHedefMaliyet = Int32.Parse(dHedef.text);

        Debug.Log(xKenar);
        Debug.Log(yKenar);
        Debug.Log("kullanıcı altın sayısı" + kullanıcıAltınSayısı);

    }
    public void OyunSahnesiGec(string SahneGec) // başla butonuna tıklandığı zaman oyunun oynandığı sahneye geçen fonksiyon.
    {
        SceneManager.LoadScene(SahneGec);
        Debug.Log(SahneGec + "yüklendi");
    }
    public void TekrarOlustur5()
    {
        int iRandom = UnityEngine.Random.Range(0, xKenar);
        int jRandom = UnityEngine.Random.Range(0, yKenar);

        GameObject tile2 = Instantiate(altinPrefab, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;

        tile2.name = "AltinTile(" + iRandom + "," + jRandom + ") ";

        altinTiles[iRandom, jRandom] = tile2.GetComponent<AltinTile>();

        tile2.transform.parent = transform;


        Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
        altinVektör.Add(yeniVektör);
        altinVektör5.Add(yeniVektör);
        altinTiles[iRandom, jRandom].AltinMiktari = 5;
        altinTiles[iRandom, jRandom].Konum = yeniVektör;
        altinTiles[iRandom, jRandom].GizliMi = false;
    }
    public void TekrarOlustur10()
    {
        int iRandom = UnityEngine.Random.Range(0, xKenar);
        int jRandom = UnityEngine.Random.Range(0, yKenar);

        GameObject tile2 = Instantiate(altinPrefab10, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;

        tile2.name = "AltinTile(" + iRandom + "," + jRandom + ") ";

        altinTiles[iRandom, jRandom] = tile2.GetComponent<AltinTile>();

        tile2.transform.parent = transform;


        Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
        altinVektör.Add(yeniVektör);
        altinVektör10.Add(yeniVektör);
        altinTiles[iRandom, jRandom].AltinMiktari = 10;
        altinTiles[iRandom, jRandom].Konum = yeniVektör;
        altinTiles[iRandom, jRandom].GizliMi = false;
    }
    public void TekrarOlusturGizli()
    {
        int iRandom = UnityEngine.Random.Range(0, xKenar);
        int jRandom = UnityEngine.Random.Range(0, yKenar);


        GameObject tile3 = Instantiate(gizliAltinPrefab, new Vector3(iRandom, jRandom, 0), Quaternion.identity) as GameObject;
        tile3.name = "AltinTile(" + iRandom + "," + jRandom + ") ";
        altinTiles[iRandom, jRandom] = tile3.GetComponent<AltinTile>();

        tile3.transform.parent = transform;

        tile3.GetComponent<SpriteRenderer>().enabled = false;

        Vector3 yeniVektör = new Vector3(iRandom, jRandom, 0);
        gizliAltinVektör.Add(yeniVektör);
        altinTiles[iRandom, jRandom].AltinMiktari = 20;
        altinTiles[iRandom, jRandom].GizliMi = true;
        altinTiles[iRandom, jRandom].Konum = yeniVektör;
    }
    IEnumerator HareketKontrol()
    {
        yield return new WaitForSeconds(2);
        KirmiziOyuncuHedefBelirle();
        MaviOyuncuHedefBelirle();
        YeşilOyuncuHedefBelirle();
        MorOyuncuHedefBelirle();
        while (altinKareSayisi != 0)
        {

            if (kullaniciKirmizi.AltinMiktari > 0)
            {
                kırmızıHareketEt = true;
                KirmiziOyuncuHareketEttir();
                Debug.Log("kirmizi altin" + kullaniciKirmizi.AltinMiktari);
                kirmiziKasaAltin = kullaniciKirmizi.AltinMiktari;
                if (altinKareSayisi == 0)
                {
                    SceneManager.LoadScene("SonucSahnesi");
                }
                TabloYaz();
    
                yield return new WaitForSeconds(2);

            }
            else
            {
                Debug.Log("kırmızı oyuncunun altını bitti");
                kırmızıHareketEt = false;
            }
            if (kullaniciMavi.AltinMiktari > 0)
            {
                maviHareketEt = true;
                MaviOyuncuHareketEttir();
                Debug.Log("kalan altın mavi " + kullaniciMavi.AltinMiktari);
                maviKasaAltin = kullaniciMavi.AltinMiktari;
                if (altinKareSayisi == 0)
                {
                    SceneManager.LoadScene("SonucSahnesi");
                }
                TabloYaz();

                yield return new WaitForSeconds(2);
            }
            else
            {
                Debug.Log("mavi oyuncunun altını bitti");
                maviHareketEt = false;
            }
            if (kullaniciYeşil.AltinMiktari > 0)
            {
                yeşilHareketEt = true;
                YeşilOyuncuHareketEttir();
                Debug.Log("kalan yeşil altın " + kullaniciYeşil.AltinMiktari);
                yesilKasaAltin = kullaniciYeşil.AltinMiktari;
                if (altinKareSayisi == 0)
                {
                    SceneManager.LoadScene("SonucSahnesi");
                }
                TabloYaz();

                yield return new WaitForSeconds(2);
            }
            else
            {
                Debug.Log("yeşil oyuncunun altını bitti");
                yeşilHareketEt = false;
            }
            if (kullaniciMor.AltinMiktari > 0)
            {
                morHareketEt = true;
                MorOyuncuHareketEttir();
                Debug.Log("kalan mor altın " + kullaniciMor.AltinMiktari);
                morKasaAltin = kullaniciMor.AltinMiktari;
                if (altinKareSayisi == 0)
                {
                    SceneManager.LoadScene("SonucSahnesi");
                }
                TabloYaz();

                yield return new WaitForSeconds(2);
            }
            else
            {
                Debug.Log("mor oyuncunun altını bitti");
                morHareketEt = false;
            }



           
        }

    }
    public void Yazdır()
    {
        DosyaYaz.TextiSil();
        DosyaYaz.texteYaz($"Kırmızı kullanıcı(A Oyuncusu)={ kullaniciKirmizi.Adım}\t\n\n");
        DosyaYaz.texteYaz($"Mavi kullanıcı(A Oyuncusu)={ kullaniciMavi.Adım}\t\n\n");
        DosyaYaz.texteYaz($"Yeşil kullanıcı(A Oyuncusu)={ kullaniciYeşil.Adım}\t\n\n");
        DosyaYaz.texteYaz($"Mor kullanıcı(A Oyuncusu)={ kullaniciMor.Adım}\t\n\n");


    }
    public void TabloYaz()
    {
        if (kullaniciKirmizi.AltinMiktari <= 0 && kullaniciMavi.AltinMiktari <= 0 && kullaniciMor.AltinMiktari <= 0 && kullaniciYeşil.AltinMiktari <= 0)
        {
            SceneManager.LoadScene("SonucSahnesi");
        }
        else if (altinKareSayisi <= 0)
        {
            SceneManager.LoadScene("SonucSahnesi");
        }

    }
    // Update is called once per frame
    void Update()
    {
        Yazdır();
       
    }
}
