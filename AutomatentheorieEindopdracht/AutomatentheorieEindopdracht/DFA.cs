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

        private List<T> states;
        private List<T> startStates;
        private List<T> finalStates;
        private List<char> alphabet;

        public DFA(List<char> alphabet) : base(alphabet)
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
            Console.WriteLine(this.transitions.Count);
            return true;
        }

    }
}
