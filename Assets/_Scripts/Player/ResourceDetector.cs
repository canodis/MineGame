using System.Collections.Generic;
using UnityEngine;
using static Data;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField] private EquippableTool pickaxe;
    [SerializeField] private Animator anim;
    [SerializeField] private ObjectPool HitEffectPool;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float detectionRadius = 5f;

    public Inventory inventory;
    public Transform pickaxeHitPoint;
    public GameObject Axe;
    public GameObject Pickaxe;
    public Material woodMaterial;
    public bool debugDraw;
    [HideInInspector]
    public bool mining = false;

    private GameObject resource;
    private equipmentState state;
    private string resourceTag;
    private Material resourceMaterial;


    void Start()
    {
        state = equipmentState.pickaxe;
    }

    public void detectResource()
    {
        if (mining == true)
            return;
        if (state == equipmentState.pickaxe)
            resourceTag = "Mine";
        else if (state == equipmentState.axe)
            resourceTag = "Tree";
        else
            return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(resourceTag))
            {
                if (state == equipmentState.pickaxe)
                    collectMine(collider.gameObject);
                else if (state == equipmentState.axe)
                    collectTree(collider.gameObject);
            }
        }
    }

    private void collectTree(GameObject gameObject)
    {
        anim.SetBool("felling", true);
        resource = gameObject;
        mining = true;
    }

    private void collectMine(GameObject gameObject)
    {
        anim.SetBool("mining", true);
        resource = gameObject;
        resourceMaterial = resource.GetComponentInChildren<MeshRenderer>().material;
        mining = true;
    }

    public void damageToResource()
    {
        if (resource.GetComponent<Resource>().takeDamage(inventory.EquippedTool.GetComponent<EquippableTool>().damage))
        {
            inventory.addItem(resource.GetComponent<Resource>().itemData, 1);
            Destroy(resource);
            resource = null;
            mining = false;
            anim.SetBool("mining", false);
        }
        GetHitEffect();
    }

    public void damageToTree()
    {
        if (resource.GetComponent<Resource>().takeDamage(Axe.GetComponent<EquippableTool>().damage))
        {
            inventory.addItem(resource.GetComponent<Resource>().itemData, 1);
            Destroy(resource);
            resource = null;
            mining = false;
            anim.SetBool("felling", false);
        }
        GetHitEffect();
    }
    void OnDrawGizmos()
    {
        if (!debugDraw) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void GetHitEffect()
    {
        GameObject obj = HitEffectPool.GetObject();
        obj.transform.position = pickaxeHitPoint.position;
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>().material = resourceMaterial;
        StartCoroutine(WaitAndDeactivate(obj, 1f));
    }

    private IEnumerator<WaitForSeconds> WaitAndDeactivate(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
