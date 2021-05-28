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
            this.transitions = new HashSet<Transition<T>>();
            this.states = new List<T>();
            this.startStates = new List<T>();
            this.finalStates = new List<T>();
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

    }
}
