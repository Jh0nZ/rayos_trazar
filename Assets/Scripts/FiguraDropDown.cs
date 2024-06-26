using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiguraDropDown : MonoBehaviour
{
    public Material defaultWhiteMaterial;
    public Dropdown addDropdown;
    public Dropdown selectedFiguresDropdown;
    public GameObject foco;
    public MovementManager movementManager;
    private List<GameObject> selectedFigures = new List<GameObject>(); // Lista para almacenar las figuras  seleccionadas
    public Button deleteButton;
    public Slider rotateXInput, rotateYInput, rotateZInput;
    public Slider scaleXInput, scaleYInput, scaleZInput;

    public Toggle rayTracingToggle; // Toggle para activar/desactivar el ray tracing
    public RayTracingManager rayTracingManager; // Referencia al script de RayTracingManager
    public Button captureScreenButton; // Botón para capturar pantalla
    public ScreenCaptureManager screenCaptureManager; // Referencia al script ScreenCaptureManager


    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
    public GameObject capsulePrefab;
    public GameObject planePrefab;
    public GameObject quadPrefab;
    void Start()
    {
        selectedFigures.Add(foco);
        movementManager.CambiarCurrentObject(foco);
        InitAddDropDown();
        UpdateSelectedFiguresDropdown();
        deleteButton.onClick.AddListener(DeleteSelectedFigure);
        deleteButton.interactable = false;

        rotateXInput.onValueChanged.AddListener(delegate { RotateObject(); });
        rotateYInput.onValueChanged.AddListener(delegate { RotateObject(); });
        rotateZInput.onValueChanged.AddListener(delegate { RotateObject(); });

        scaleXInput.onValueChanged.AddListener(delegate { ScaleObject(); });
        scaleYInput.onValueChanged.AddListener(delegate { ScaleObject(); });
        scaleZInput.onValueChanged.AddListener(delegate { ScaleObject(); });
        SetInitialSliderValues();
        rayTracingToggle.onValueChanged.AddListener(delegate { ToggleRayTracing(rayTracingToggle.isOn); });
        captureScreenButton.onClick.AddListener(delegate { screenCaptureManager.CaptureScreen(); });
    }

    void ToggleRayTracing(bool isOn)
    {
        if (rayTracingManager != null)
        {
            rayTracingManager.SetRayTracingEnabled(isOn);
        }
    }
    void SetInitialSliderValues()
    {
        // Set initial rotation values
        Vector3 initialRotation = foco.transform.rotation.eulerAngles;
        rotateXInput.value = initialRotation.x;
        rotateYInput.value = initialRotation.y;
        rotateZInput.value = initialRotation.z;

        // Set initial scale values
        Vector3 initialScale = foco.transform.localScale;
        scaleXInput.value = initialScale.x;
        scaleYInput.value = initialScale.y;
        scaleZInput.value = initialScale.z;
    }
    private void DeleteSelectedFigure()
    {
        int selectedIndex = selectedFiguresDropdown.value;

        // No permitir eliminar el foco (primera figura)
        if (selectedIndex == 0)
        {
            Debug.Log("No se puede eliminar el foco.");
            return;
        }

        if (selectedIndex > 0 && selectedIndex < selectedFigures.Count)
        {
            GameObject selectedObject = selectedFigures[selectedIndex];
            selectedFigures.Remove(selectedObject);
            Destroy(selectedObject);
            UpdateSelectedFiguresDropdown();
        }
    }
    void InitAddDropDown()
    {
        addDropdown.options.Clear();
        List<string> options = new List<string> { "Seleccionar figura", "Cube", "Sphere", "Cylinder", "Capsule", "Plane", "Quad" };
        foreach (string option in options)
        {
            addDropdown.options.Add(new Dropdown.OptionData(option));
        }

        addDropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(addDropdown); });
    }
    void DropdownValueChanged(Dropdown change)
    {
        string selectedOption = change.options[change.value].text;
        GameObject prefabToInstantiate = null;

        switch (selectedOption)
        {
            case "Cube":
                prefabToInstantiate = cubePrefab;
                break;
            case "Sphere":
                prefabToInstantiate = spherePrefab;
                break;
            case "Cylinder":
                prefabToInstantiate = cylinderPrefab;
                break;
            case "Capsule":
                prefabToInstantiate = capsulePrefab;
                break;
            case "Plane":
                prefabToInstantiate = planePrefab;
                break;
            case "Quad":
                prefabToInstantiate = quadPrefab;
                break;
            case "Seleccionar figura":
            default:
                break;
        }

        if (prefabToInstantiate != null)
        {
            CreatePrimitive(prefabToInstantiate);
        }

        addDropdown.value = 0;
    }
    void SelectedFiguresDropdownValueChanged(Dropdown change)
    {
        int selectedIndex = change.value;
        if (selectedIndex >= 0 && selectedIndex < selectedFigures.Count)
        {
            GameObject selectedObject = selectedFigures[selectedIndex];
            movementManager.CambiarCurrentObject(selectedObject);
            // Mostrar datos del objeto
            Vector3 rotation = selectedObject.transform.rotation.eulerAngles;
            rotateXInput.value = rotation.x;
            rotateYInput.value = rotation.y;
            rotateZInput.value = rotation.z;

            Vector3 scale = selectedObject.transform.localScale;
            scaleXInput.value = scale.x;
            scaleYInput.value = scale.y;
            scaleZInput.value = scale.z;
        }
        deleteButton.interactable = !(selectedFiguresDropdown.value == 0);
    }
    private void UpdateSelectedFiguresDropdown()
    {
        selectedFiguresDropdown.ClearOptions();
        foreach (var figure in selectedFigures)
        {
            selectedFiguresDropdown.options.Add(new Dropdown.OptionData(figure.name));
        }
        selectedFiguresDropdown.value = selectedFigures.Count;

        // Asignar el listener para el cambio en el dropdown de figuras seleccionadas
        selectedFiguresDropdown.onValueChanged.RemoveAllListeners(); // Limpiar listeners anteriores
        selectedFiguresDropdown.onValueChanged.AddListener(delegate { SelectedFiguresDropdownValueChanged(selectedFiguresDropdown); });
        deleteButton.interactable = !(selectedFiguresDropdown.value == 0);
    }
    private void CreatePrimitive(GameObject prefab)
    {
        GameObject newPrimitive = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // Asignar el material blanco por defecto
        Renderer renderer = newPrimitive.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = defaultWhiteMaterial;
        }

        selectedFigures.Add(newPrimitive);
        UpdateSelectedFiguresDropdown();
    }
    private void RotateObject()
    {
        int selectedIndex = selectedFiguresDropdown.value;
        if (selectedIndex >= 0 && selectedIndex < selectedFigures.Count)
        {
            GameObject selectedObject = selectedFigures[selectedIndex];

            // Obtener los valores de rotación desde los InputField
            float rotX = rotateXInput.value;
            float rotY = rotateYInput.value;
            float rotZ = rotateZInput.value;

            // Aplicar la rotación al objeto
            selectedObject.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }

    private void ScaleObject()
    {
        int selectedIndex = selectedFiguresDropdown.value;
        if (selectedIndex >= 0 && selectedIndex < selectedFigures.Count)
        {
            GameObject selectedObject = selectedFigures[selectedIndex];

            // Obtener los valores de escala desde los InputField
            float scaleX = scaleXInput.value;
            float scaleY = scaleYInput.value;
            float scaleZ = scaleZInput.value; // float.Parse(scaleZInput.text);

            // Aplicar la escala al objeto
            selectedObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}
