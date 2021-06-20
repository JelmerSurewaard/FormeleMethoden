﻿using System;
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
                if (state == ndfa.startStates.ElementAt(0))
                {
                    newStates.Add(state);
                }
               
                foreach (var symbol in ndfa.alphabet)
                {
                    
                    //var specificTrans = ndfa.getTransitions(state, symbol);
                    var toStates = ndfa.getNextStatesEpsilon(state, symbol, false);
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
                        newStates.Add("Fuik");
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
                    List<string> toStatesList = new List<string>();

                    //var specificTrans = ndfa.getTransitions(state, symbol);

                    for (int i = 1; i < fromStates.Length; i++)
                    {
                        toStatesList.AddRange(ndfa.getNextStatesEpsilon("q" + fromStates[i], symbol, false));
                    }

                    toStatesList = toStatesList.Distinct().ToList();

                    foreach (var item in toStatesList)
                    {
                        tempToState += item;
                    }
                    if (tempToState == "")
                    {
                        tempToState = "Fuik";
                    }
                    tempDFA.transitions.Add(new Transition<string>(state, symbol, tempToState));
                }
            }

            tempDFA.states = newStates;
            tempDFA.startStates = ndfa.startStates;
            foreach (var state in newStates)
            {
                if (state.Contains(ndfa.finalStates.ElementAt(0).ToString()))
                {
                    tempDFA.finalStates.Add(state);
                }
            }

            //Check if endless state exists. If not, delete endless state from transitions.
            int fuikExists = 0;
            foreach (var trans in tempDFA.transitions)
            {
                if (trans.toState.Equals("Fuik"))
                {
                    fuikExists++;
                }
            }

            if (fuikExists == tempDFA.alphabet.Count)
            {
                var temp2DFA = new List<Transition<string>>();

                foreach (var trans in tempDFA.transitions)
                {
                    if (!trans.toState.ToString().Equals("Fuik"))
                    {
                        temp2DFA.Add(trans);
                    }
                }

                tempDFA.transitions.Clear();


                for (int i = 0; i < temp2DFA.Count(); i++)
                {
                    tempDFA.transitions.Add(temp2DFA.ElementAt(i));
                }
                
            }

            return tempDFA;
        }
    }
}
