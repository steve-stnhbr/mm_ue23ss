using System.Linq;
using System.Reflection;
using UnityEngine;
using TypeReferences;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class WizardSkillBehaviour : MonoBehaviour
{
    private VisualElement root;
    public VisualTreeAsset skillTemplate;

    int selectedSkill;

    WizardSkill[] availableWizardSkillInstances;


    // Start is called before the first frame update
    void Start()
    {
        availableWizardSkillInstances = GetComponents<WizardSkill>();
        root = GetComponentInChildren<UIDocument>().rootVisualElement;
        BuildSkillElements();
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

    private void BuildSkillElements()
    {
        foreach (WizardSkill skill in availableWizardSkillInstances)
        {
            TemplateContainer template = skillTemplate.Instantiate();
            Label label = template.Q<Label>();
            label.text = skill.GetType().ToString();
            label.style.color = skill.UIColor;
            
            root.Add(template);
        }
    }
}
