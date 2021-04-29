using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyElfeZoneRouge : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private SphereCollider _sphereCollider;
    private Rigidbody _rigidbody;

    private MeshCollider _mesh;
    private bool isgrounded = false;

    public  bool isActive = false;
    public float size = 0;
    private int maxSize;

    private Vector3 taille = new Vector3(0, 0,0); //0
    
    private AudioManager audioManager;

    void Start()
    {
        _mesh = GetComponent<MeshCollider>();
        
        maxSize = Random.Range(25, 40);
        
        audioManager = FindObjectOfType<AudioManager>();
        
        PlayAudio("ZoneRouge");
    }

    // Update is called once per frame
    void Update()
    {
        if (size < maxSize && isActive)
        {
            taille += new Vector3( 0.1f, 0.1f, 0.1f);
            transform.localScale = taille;

            size += 0.1f;
        }
    }

    void OnTriggerEnter(Collider col){

        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Un elfe a ete dezingué par la zone rouge haha");
            Destroy(col.gameObject);
        }
        
        if(col.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("pret a atterir");
            isgrounded = true;
        }
    }
    
    public void PlayAudio(string name)
    {
        if (audioManager)
            audioManager.Play(name);
        
    }
}
