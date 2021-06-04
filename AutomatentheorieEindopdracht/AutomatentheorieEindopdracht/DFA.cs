using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht
{
    class DFA<T> : Automaton<T> where T : IComparable
    {
        private HashSet<Transition<T>> transitions;

        private SortedSet<T> states;
        private SortedSet<T> startStates;
        private SortedSet<T> finalStates;
        private SortedSet<char> alphabet;

        public DFA(SortedSet<char> alphabet) : base(alphabet)
        {
            this.transitions = base.transitions;
            this.states = base.states;
            this.startStates = base.startStates;
            this.finalStates = base.finalStates;
            this.alphabet = alphabet;
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
            foreach (Transition<T> transition in this.transitions)
            {
                Console.WriteLine(transition.toString());
            }

            Console.WriteLine("Next string going though the accept() method for DFA: " + s);

            //checks if input string contains values from alphabet
            foreach (char c in s)
            {
                if (!alphabet.Contains(c)) return false;
            }

            // Creates a list of states starting with the startState
            List<T> iterationList = new List<T>();
            iterationList.Add(startStates.First());

            for (int i = 0; i < s.Length; i++)
            {
                iterationList = getNextStates(iterationList, s[i]);
            }

            //Console.WriteLine(tempStates.Count);

            foreach (T state in iterationList)
            {
                Console.WriteLine("tempstate is: " + state.ToString());
            }

            //Console.WriteLine(tempStates.Last().ToString());

            if (finalStates.Contains(iterationList.Last()))
            {
                return true;
            }
            return false;

            
            
        }

        private List<T> getNextStates(List<T> states, char c)
        {
            List<T> nextStates = states;
            T lastState = nextStates.Last();

            foreach (Transition<T> transition in transitions)
            {
                if (transition.fromState.Equals(lastState) && transition.symbol.Equals(c))
                {
                    nextStates.Add(transition.toState);
                }
            }
            //Console.WriteLine(nextStates.Count);
            return nextStates;
        }

    }
}
