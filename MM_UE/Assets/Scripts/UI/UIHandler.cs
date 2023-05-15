using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    private VisualElement root;
    public VisualTreeAsset skillTemplate;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        buildSkillElements();
    }

    private void buildSkillElements()
    {
        foreach (WizardSkill skill in LevelManager.getCurrentLevel().GetWizardSkillBehaviour().GetWizardSkills())
        {
            TemplateContainer template = skillTemplate.Instantiate();
            Debug.Log(template);
            template.Q<Label>("#text").text = skill.GetType().ToString();
            root.Add(template);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
