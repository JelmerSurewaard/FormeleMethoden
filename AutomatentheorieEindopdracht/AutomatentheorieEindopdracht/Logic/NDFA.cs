using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht
{
    class NDFA<T> : Automaton<T> where T : IComparable
    {
        private HashSet<Transition<T>> transitions;

        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        private SortedSet<char> alphabet;

        public NDFA(int n) : base(n)
        {
            this.transitions = base.transitions;
            this.states = base.states;
            this.startStates = base.startStates;
            this.finalStates = base.finalStates;
            this.alphabet = base.alphabet;
            fillAlphabet(n);
        }
         private void fillAlphabet(int n)
        {
            base.fillAlphabet(n);
        }

        public override void addTransition(Transition<T> t)
        {
            base.addTransition(t);
        }

        public override void defineAsStartState(T t)
        {
            base.defineAsStartState(t);
        }

        public override void defineAsFinalState(T t)
        {
            base.defineAsFinalState(t);
        }

        //Checks if input string is accepted in DFA
        public bool accept(String s)
        {
            Console.WriteLine("Next string going though the accept() method for NDFA: " + s);

            //checks if input string contains values from alphabet
            foreach (char c in s)
            {
                if (!alphabet.Contains(c)) return false;
            }

            // Creates a list of states starting with the startState
            List<T> iterationList = new List<T>();

            for (int i = 0; i < startStates.Count; i++)
            {
                iterationList.Add(startStates.ElementAt(i));

                for (int j = 0; j < s.Length; j++)
                {
                    iterationList = getNextStates(iterationList, s[j]);
                }

                if (finalStates.Contains(iterationList.Last()))
                {
                    return true;
                }
            }
            
            return false;
        }
        public new List<T> getNextStates(List<T> states, char c)
        {
            return base.getNextStates(states, c);
        }

        public new void printTransitions()
        {
            base.printTransitions();
        }

        public void generateGraph(string output)
        {
            base.generateGraph(output);
        }

        public DFA<string> toDFA()
        {
            DFA<string> dfa = new DFA<string>(this.alphabet.Count);
            List<T> toStates = new List<T>();

            foreach (Transition<T> transition in this.transitions)
            {
                List<T> iterationList = new List<T>();
                iterationList.Add(transition.fromState);
                toStates = this.getNextStates(iterationList, transition.symbol);

                if (toStates.Count > 1)
                {
                    string newState = "";
                    int i = 0;
                    toStates.ToList().ForEach(state =>
                    {
                        if (i == 0)
                        {
                            newState = state.ToString();
                        }
                        else
                        {
                            newState = newState +  state;
                        }
                        i++;
                    });

                    dfa.addTransition(new Transition<string>(transition.fromState.ToString(), transition.symbol, newState));

                    foreach (char symbol in this.alphabet)
                    {
                        if (symbol == transition.symbol)
                        {
                            dfa.addTransition(new Transition<string>(newState.ToString(), transition.symbol, newState.ToString()));
                        }
                        else
                        {
                            List<T> states = this.getNextStates(iterationList, symbol);
                            if (states.Count < 2)
                            {
                                dfa.addTransition(new Transition<string>(newState.ToString(), symbol, states.ToList().ElementAt(0).ToString()));
                            }
                        }
                    }
                }
                else
                {
                    dfa.addTransition(transition);
                }
            }

            return dfa;
        }
    }
}
