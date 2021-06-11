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
            this.alphabet = new SortedSet<char>();
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
        private new List<T> getNextStates(List<T> states, char c)
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
    }
}
