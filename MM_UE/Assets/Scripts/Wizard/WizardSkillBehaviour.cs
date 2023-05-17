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

    public string selectedClass;

    int selectedSkill;

    WizardSkill[] availableWizardSkillInstances;
    TemplateContainer[] skillContainers;


    // Start is called before the first frame update
    void Start()
    {
        availableWizardSkillInstances = GetComponents<WizardSkill>();
        root = GetComponentInChildren<UIDocument>().rootVisualElement;
        BuildSkillElements();
        setSelectedSkill(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("WizardInteract"))
        {
            availableWizardSkillInstances[selectedSkill].OnExecute(gameObject);
        }

        SwitchSkill((int)Input.mouseScrollDelta.y);
    }

    public void SwitchSkill(int delta)
    {
        if (delta == 0) return;
        int newSelectedSkill = (selectedSkill + delta) % availableWizardSkillInstances.Length;
        if (newSelectedSkill < 0)
        {
            newSelectedSkill = availableWizardSkillInstances.Length + newSelectedSkill;
        }
        
        setSelectedSkill(newSelectedSkill);
    }

    public void setSelectedSkill(int index)
    {
        UnmarkSelected(skillContainers[selectedSkill]);
        selectedSkill = index;
        MarkSelected(skillContainers[selectedSkill]);
    }

    public WizardSkill[] GetWizardSkills()
    {
        return availableWizardSkillInstances;
    }

    private void BuildSkillElements()
    {
        skillContainers = new TemplateContainer[availableWizardSkillInstances.Length];
        for (int i = 0 ; i < availableWizardSkillInstances.Length; i++)
        {
            WizardSkill skill = availableWizardSkillInstances[i];
            TemplateContainer template = skillTemplate.Instantiate();
            Label label = template.Q<Label>();
            label.text = skill.GetType().ToString();
            label.style.color = skill.UIColor;

            skillContainers[i] = template;
            root.Add(template);
        }
    }

    private void UnmarkSelected(TemplateContainer template)
    {
        template.RemoveFromClassList(selectedClass);
    }

    private void MarkSelected(TemplateContainer template)
    {
        template.AddToClassList(selectedClass);
    }
}
