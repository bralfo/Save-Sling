using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Estilingue : MonoBehaviour
{
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float maxPullDistance = 5f;

    [SerializeField] private Transform launchPoint;
    [SerializeField] private SalvageItem Item;

    private SalvageItem currentItem;
    private Rigidbody2D currentRigidbody;
    private NPC currentNPC;

    private Camera mainCamera;
    private bool isAiming;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (currentItem == null) // verifica se tem item no estilingue
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartAiming();
        }

        if (isAiming)
        {
            UpdateAim();

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Launch();
            }
        }
    }

    public void SetItem(SalvageItem item, NPC npc)
    {
        if (currentItem != null)
            return;

        currentItem = item;
        currentNPC = npc;

        currentRigidbody = item.GetComponent<Rigidbody2D>();

        currentRigidbody.bodyType = RigidbodyType2D.Kinematic;
        currentRigidbody.linearVelocity = Vector2.zero;
        currentRigidbody.angularVelocity = 0f;

        item.transform.position = launchPoint.position;
    }

    private void StartAiming()
    {
        isAiming = true;
    }

    private void UpdateAim()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseScreenPosition.z = -mainCamera.transform.position.z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = (Vector2)(mouseWorldPosition - launchPoint.position);
        direction = Vector2.ClampMagnitude(direction, maxPullDistance);

        currentItem.transform.position = launchPoint.position + (Vector3)direction;
    }

    private void Launch()
    {
        isAiming = false;

        Vector2 launchDirection = (Vector2)(launchPoint.position - currentItem.transform.position);

        // Voltar o corpo para Dynamic permite que a gravidade e forças atuem no item durante o voo
        currentRigidbody.bodyType = RigidbodyType2D.Dynamic;

        currentRigidbody.AddForce(
            launchDirection.normalized * launchForce,
            ForceMode2D.Impulse
        );

        // 1. AVISAR A CÂMERA PARA SEGUIR O ITEM AGORA NO DISPARO
        CameraController cam = FindAnyObjectByType<CameraController>();
        if (cam != null)
        {
            cam.FollowItem(currentItem.transform);
            Debug.Log("Item Lançado - Câmera informada com sucesso!");
        }
        else
        {
            Debug.LogError("CameraController não foi encontrado na cena!");
        }

        // 2. Notifica o NPC para sair
        if (currentNPC != null)
        {
            currentNPC.ItemFoiLancado();
            currentNPC = null;
        }

        // 3. Limpa a referência do item atual no estilingue
        currentItem = null;
        currentRigidbody = null;
    }
}