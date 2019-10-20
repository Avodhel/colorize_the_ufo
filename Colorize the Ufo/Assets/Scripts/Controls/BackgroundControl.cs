using UnityEngine;

public class BackgroundControl : MonoBehaviour
{ //!!önemli: arkaplan kaydirma için arayüzde arkaplani duplicate edip arkaplana ekledik. (parent-child hiyerarşisi)

    [SerializeField]
    [Range(0.1f, 1f)]
    private float splitSpeed = 0.3f; //arkaplanın y eksenindeki kayma hızı
    [SerializeField]
    private float tileSizeY; // y ekseninde ne kadar birimlik bir alanda kayma işlemi gerçekleşecek.

    private Vector2 startPos; //arkaplanımın başlangıç pozisyonu

    private void Start()
    {
        startPos = transform.position; //arkaplanımızın pozisyonu
    }

    private void Update()
    {
        float newPos = Mathf.Repeat(Time.time * splitSpeed, tileSizeY); //yeni pozisyonun belirlenmesi
        transform.position = startPos + Vector2.down * newPos; //arkaplanımızın yeni pozisyona göre güncellenmesi
    }
}
