
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class DialogueVariables 
{

    private Dictionary<string, Ink.Runtime.Object> variables;

    public DialogueVariables(TextAsset loadGlobalJSON)
    {
        //Membuat Story Baru
        Story globalVariablesStory = new Story(loadGlobalJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized Global Dialogue Variable " + name + " = " + value);
        }
    }

    public void Startlistening(Story story)
    {
        // ini penting harus mengisi VariableToStory sebelum listenernya
        VariableToStory(story);
        story.variablesState.variableChangedEvent += VariableChange;
    }

    public void Stoplistening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChange;
    }

    private void VariableChange(string name, Ink.Runtime.Object value)
    {
        // hanya menampung variables yang di initialisasi dari global ink file
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariableToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
