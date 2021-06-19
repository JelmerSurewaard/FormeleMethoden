using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht.Logic
{
    class NDFAConverter
    {
        public static DFA<string> createDFA(NDFA<string> ndfa)
        {
            DFA<string> tempDFA = new DFA<string>(ndfa.alphabet.Count);

            SortedSet<string> newStates = new SortedSet<string>();

            foreach (var state in ndfa.states)
            {
                foreach (var symbol in ndfa.alphabet)
                {
                    var toStates = ndfa.getNextStatesEpsilon(state, symbol);
                    string tempString = "";
                    foreach (string s in toStates)
                    {
                        tempString += s; 
                    }
                    if (!tempString.Equals(""))
                    {
                        newStates.Add(tempString);
                    } else
                    {
                        newStates.Add("fuik");
                    }
                    
                }
            }

            tempDFA.states = newStates;

            foreach (var state in tempDFA.states)
            {
                foreach (var symbol in tempDFA.alphabet)
                {
                    string tempToState = "";
                    string[] fromStates = state.Split('q');
                    List<string> fromStatesList = new List<string>();
                    List<string> toStatesList = new List<string>();
                    for (int i = 1; i < fromStates.Length; i++)
                    {
                        fromStatesList.Add("q" + fromStates[i]);
                        toStatesList.AddRange(ndfa.getNextStates(fromStatesList, symbol));
                        fromStatesList.Clear();
                    }

                    foreach (var item in toStatesList)
                    {
                        tempToState += item;
                    }
                    
                    tempDFA.transitions.Add(new Transition<string>(state, symbol, tempToState));
                }
            }

            tempDFA.states = newStates;
            tempDFA.startStates = ndfa.startStates;
            foreach (var state in newStates)
            {
                if (state.Contains(ndfa.finalStates.ToString()))
                {
                    tempDFA.finalStates.Add(state);
                }
            }
            return tempDFA;
        }
    }
}
