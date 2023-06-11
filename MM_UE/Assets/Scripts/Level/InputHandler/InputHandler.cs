using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    List<IDisableInputForMenu> interfacesMenu = new List<IDisableInputForMenu>();
    List<IDisableInputForInteraction> interfacesInteraction = new List<IDisableInputForInteraction>();

    private void Start()
    {
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            IDisableInputForMenu[] childrenInterfacesMenu = rootGameObject.GetComponentsInChildren<IDisableInputForMenu>();
            foreach (var childInterface in childrenInterfacesMenu)
            {
                interfacesMenu.Add(childInterface);
            }

            IDisableInputForInteraction[] childrenInterfacesInteraction = rootGameObject.GetComponentsInChildren<IDisableInputForInteraction>();
            foreach (var childInterface in childrenInterfacesInteraction)
            {
                interfacesInteraction.Add(childInterface);
            }
        }

    }

    public void DisableInputForMenu()
    {
        foreach (var interfaceMenu in interfacesMenu)
        {
            interfaceMenu.DisableInputForMenu();
        }
    }

    public void EnableInputForMenu()
    {
        foreach (var interfaceMenu in interfacesMenu)
        {
            interfaceMenu.EnableInputForMenu();
        }
    }

    public void DisableInputForInteraction()
    {
        foreach (var interfaceInteraction in interfacesInteraction)
        {
            interfaceInteraction.DisableInputForInteraction();
        }
    }

    public void EnableInputForInteraction()
    {
        foreach (var interfaceInteraction in interfacesInteraction)
        {
            interfaceInteraction.EnableInputForInteraction();
        }
    }
}
