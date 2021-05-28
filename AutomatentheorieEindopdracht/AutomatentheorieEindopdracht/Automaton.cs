using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht
{
    abstract class Automaton <T> : IComparable<Transition<T>> where T : IComparable
    {
        private HashSet<Transition<T>> transitions;
        
        private List<T> states;
        private List<T> startStates;
        private List<T> finalStates;
        private List<char> alphabet;

        //Constructor Automaton
        public Automaton()
        {

        }
        public Automaton(List<char> alphabet)
        {
            this.transitions = new HashSet<Transition<T>>();
            this.states = new List<T>();
            this.startStates = new List<T>();
            this.finalStates = new List<T>();
            this.alphabet = alphabet;
        }
        #region Automaton methods
        
        public virtual void addTransition(Transition<T> t)
        {
            transitions.Add(t);
            states.Add(t.fromState);
            states.Add(t.toState);
        }

        public virtual void defineAsStartState(T t)
        {
            // If already in states no problem because a HashSet will remove duplicates.
            states.Add(t);
            startStates.Add(t);
        }

        public virtual void defineAsFinalState(T t)
        {
            // If already in states no problem because a HashSet will remove duplicates.
            states.Add(t);
            finalStates.Add(t);
        }
        #endregion

        public int CompareTo(Transition<T> other)
        {
            throw new NotImplementedException();
        }
    }
}
