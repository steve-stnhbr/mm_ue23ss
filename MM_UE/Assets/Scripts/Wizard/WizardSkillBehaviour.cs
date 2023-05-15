using System.Linq;
using System.Reflection;
using UnityEngine;
using TypeReferences;

public class WizardSkillBehaviour : MonoBehaviour
{
    string[] skills = Assembly.GetAssembly(typeof(WizardSkill)).GetTypes()
        .Where(myType => myType.IsClass && !myType.IsAbstract && !myType.IsGenericType && myType.IsSubclassOf(typeof(WizardSkill)))
        .Select(type => type.ToString()).ToArray();
    [Inherits(typeof(WizardSkill))]
    public TypeReference[] availableWizardSkills;

    int selectedSkill;
    WizardSkill[] availableWizardSkillInstances;

    // Start is called before the first frame update
    void Start()
    {
        // Transforming all selected Skills at runtime to an array of instances improves performance and memory usage
        availableWizardSkillInstances =
            availableWizardSkills.Select(skillType => Assembly.GetAssembly(typeof(WizardSkillBehaviour)).CreateInstance(skillType.Type.Name) as WizardSkill).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("WizardInteract"))
        {
            availableWizardSkillInstances[selectedSkill].OnExecute(gameObject);
        }

        selectedSkill = (selectedSkill + (int)Input.mouseScrollDelta.y) % availableWizardSkillInstances.Length;
        if (selectedSkill < 0)
        {
            selectedSkill = availableWizardSkillInstances.Length + 1 - selectedSkill;
        }
    }

    public void setSelectedSkill(int index)
    {
        selectedSkill = index;
    }

    public WizardSkill[] GetWizardSkills()
    {
        return availableWizardSkillInstances;
    }
}
