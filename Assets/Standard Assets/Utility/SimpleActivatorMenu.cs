using UnityEngine;
using UnityEngine.UI; // Add this to use the new Text component

public class SimpleActivatorMenu : MonoBehaviour
{
    public Text camSwitchButton;  // Change GUIText to Text
    public GameObject[] objects;
    private int m_CurrentActiveObject;

    void OnEnable()
    {
        m_CurrentActiveObject = 0;
        camSwitchButton.text = objects[m_CurrentActiveObject].name;
    }

    public void NextCamera()
    {
        int nextActiveObject = (m_CurrentActiveObject + 1) % objects.Length;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == nextActiveObject);
        }

        m_CurrentActiveObject = nextActiveObject;
        camSwitchButton.text = objects[m_CurrentActiveObject].name;
    }
}
