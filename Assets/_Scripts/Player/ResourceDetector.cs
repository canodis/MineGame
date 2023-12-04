using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ObjectPool HitEffectPool;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private ResourceInfoController resourceInfoController;
    [SerializeField] private GameObject playerObject;

    public Inventory inventory;
    public Transform pickaxeHitPoint;
    public GameObject Axe;
    public bool debugDraw;
    [HideInInspector] public bool mining = false;

    private SoundManager soundManager;
    private MineSpawner mineSpawner;
    private GameObject resource;
    private equipmentState state;
    private Material resourceMaterial;
    private string resourceTag;


    void Start()
    {
        state = equipmentState.pickaxe;
        mineSpawner = GameObject.FindGameObjectWithTag("MineSpawner").GetComponent<MineSpawner>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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
        StartCoroutine(RotateTowardsMineCoroutine(gameObject.transform.position, 500));
        resource = gameObject;
        resourceMaterial = resource.GetComponentInChildren<MeshRenderer>().material;
        mining = true;
        resourceInfoController.ShowResourceInfoPanel(resource.GetComponent<Resource>());
    }

    public void damageToResource()
    {
        soundManager.PlayPicaxeHitSound();
        if (resource.GetComponent<Resource>().takeDamage(inventory.EquippedTool.GetComponent<EquippableTool>().damage))
        {
            soundManager.PlayMineFractionSound();
            inventory.addItem(resource.GetComponent<Resource>().itemData, 1);
            mineSpawner.RemoveMineFromList(resource);
            mineSpawner.decreaseMineCount();
            resource.GetComponent<FractureObject>().Fracture();
            resource.SetActive(false);
            Destroy(resource, 1);
            resource = null;
            mining = false;
            anim.SetBool("mining", false);
            resourceInfoController.HideResourceInfoPanel();
        }
        else
            resourceInfoController.UpdateResourceInfo(resource.GetComponent<Resource>());
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

    IEnumerator RotateTowardsMineCoroutine(Vector3 targetPosition, float rotationSpeed)
    {
        while (true)
        {
            if (anim.GetBool("mining") == false)
                break;
            Vector3 direction = targetPosition - playerObject.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion rotationY = Quaternion.LookRotation(Vector3.RotateTowards(playerObject.transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f));

            rotationY.x = 0;
            rotationY.z = 0;

            float angle = Quaternion.Angle(playerObject.transform.rotation, targetRotation);
            playerObject.transform.rotation = Quaternion.RotateTowards(playerObject.transform.rotation, rotationY, rotationSpeed * Time.deltaTime);
            if (angle < 10.0f)
                break;
            yield return null;
        }
    }
}
