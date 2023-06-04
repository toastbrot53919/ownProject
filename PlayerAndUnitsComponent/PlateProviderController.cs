using UnityEngine;
using UnityEngine.UI;
public class PlateProviderController : MonoBehaviour
{
    public GameObject namePlateTemplate;
    public float OffsetY = 4.0f;
    public float FadingDinstanceMin = 15f;
    public float FadingDinstanceMax = 45;
    public float ScaleStart = 0.2f;
    public float ScaleEnd = 0.05f;
    public float ScaleDistanceMin = 5f;
    public float ScaleDistanceMax = 20f;
    private Canvas ScreenSpaceCanvas;
    private Transform PlateTransform;
    private RectTransform PlateRectTransform;
    public Slider healthSlider;
    public Slider manaSlider;
    public Image panelImage;
    private HealthController healthController;
    private ManaController manaController;
    private CharacterStats characterStats;
    private AIController aIController;


    private GameObject namePlate;

    public void Start()
    {
        healthController = GetComponent<HealthController>();
        manaController = GetComponent<ManaController>();
        characterStats = GetComponent<CharacterStats>();
        aIController = GetComponent<AIController>();

    }

    bool shouldBeVisible = true;
    bool newShouldBeVisible = false;
    public void Update()
    {
        if (namePlate == null)
        {
            CreateNamePlate();
        }

        if (healthController.currentHealth <= 0)
        {
            DestroyNamePlate();
        }


        newShouldBeVisible = distanceIsVisible() && raycastIsVisible(GameObject.Find("Player"));
        if (newShouldBeVisible != shouldBeVisible)
        {
            shouldBeVisible = newShouldBeVisible;
            namePlate.gameObject.SetActive(shouldBeVisible);
        }

        if (namePlate.gameObject.activeSelf == false && shouldBeVisible)
        {
            namePlate.gameObject.SetActive(true);
        }

        if (namePlate != null)
        {
            if (healthController != null)
            {
                healthSlider.value = healthController.currentHealth / healthController.maxHealth;
            }
            else
            {
                healthSlider.value = 1;
            }
            if (manaController != null)
            {
                manaSlider.value = manaController.currentMana / manaController.maxMana;

            }
            else
            {
                manaSlider.value = 1;
            }
        }
        PlateRectTransform.localPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, OffsetY, 0)); //make it scale with the camera
        //Size change depending on disntance to camera
        //Size change depending on disntance to camera
        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        float scale = ScaleStart;
        if (distance > ScaleDistanceMin)
        {
            scale = Mathf.Lerp(ScaleStart, ScaleEnd, (distance - ScaleDistanceMin) / (ScaleDistanceMax - ScaleDistanceMin));
        }
        PlateRectTransform.localScale = new Vector3(scale, scale, scale);
        //Alpha change depending on disntance to camera
        float alpha = 1;
        if (distance > FadingDinstanceMin)
        {
            alpha = Mathf.Lerp(1, 0, (distance - FadingDinstanceMin) / (FadingDinstanceMax - FadingDinstanceMin));
        }
        namePlate.GetComponentInChildren<CanvasGroup>().alpha = alpha;
        //Color change depending on the AggroTag of CharacterCombatController
        if(aIController!=null){
            if (aIController.aggroTag == "Player")
            {
                panelImage.color = Color.red;
            }
            else if (aIController.aggroTag == "NONE")
            {
                panelImage.color = Color.gray;
            }
            else if(aIController.aggroTag == "Enemy"){
                panelImage.color = Color.green;
            }
        }



    }
    bool distanceIsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        //debending of the distance to the camera, the object is visible if it is in the screen or near the screen
        bool onScreen = screenPoint.z > 0 && screenPoint.x > -0.1 && screenPoint.x < 1.1 && screenPoint.y > -0.1 && screenPoint.y < 1.1;

        //Check if the unit is within the center threshold of the screen
        float centerX = 0.5f;
        float centerY = 0.5f;
        float threshold = 0.3f;
        bool inCenter = Mathf.Abs(screenPoint.x - centerX) < threshold && Mathf.Abs(screenPoint.y - centerY) < threshold;

        return onScreen && inCenter;
    }

    public bool raycastIsVisible(GameObject player)
    {
        // Get player's position and rotation
        Vector3 playerPosition = player.transform.position;
        Quaternion playerRotation = player.transform.rotation;

        // Get object's position
        Vector3 objectPosition = transform.position;

        // Check if object is within player's view frustum
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main),
            new Bounds(objectPosition, GetComponent<Collider>().bounds.size)))
        {
            // Object is visible, now check if it's obscured by any obstacles
            RaycastHit hit;
            if (Physics.Linecast(playerPosition, objectPosition, out hit))
            {
                // Check if hit collider is the object itself
                if (hit.collider.gameObject == this.gameObject)
                {
                    // Object is visible
                    return true;
                }
                else
                {
                    // Object is obscured by an obstacle
                    return false;
                }
            }
            else
            {
                // Object is visible
                return true;
            }
        }
        else
        {
            // Object is outside of player's view frustum
            return false;
        }
    }

    public GameObject CreateNamePlate()
    {

        ScreenSpaceCanvas = GameObject.Find("ScreenSpaceCanvas").GetComponent<Canvas>();
        if (ScreenSpaceCanvas == null)
        {
            Debug.Log("ScreenSpaceCanvas not found");
        }
        namePlate = Instantiate(namePlateTemplate, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        namePlate.SetActive(true);
        namePlate.transform.SetParent(ScreenSpaceCanvas.transform);

        PlateTransform = namePlate.transform;
        PlateRectTransform = namePlate.GetComponentInChildren<RectTransform>();

        PlateRectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (healthSlider == null)
        {
            Debug.Log("healthSlider not found");
        }
        if (manaSlider == null)
        {
            Debug.Log("manaSlider not found");
        }
        return namePlate;
    }


    public void DestroyNamePlate()
    {
        Destroy(namePlate);
    }
}
