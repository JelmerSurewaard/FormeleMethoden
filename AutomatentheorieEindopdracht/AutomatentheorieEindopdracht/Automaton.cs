using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht
{
    class Automaton <T> : IComparable<Transition<T>> where T : IComparable
    {
        public HashSet<Transition<T>> transitions { get; set; }
        
        public SortedSet<T> states { get; set; }
        public SortedSet<T> startStates { get; set; }
        public SortedSet<T> finalStates { get; set; }
        public SortedSet<char> alphabet { get; set; }

        //Constructor Automaton

        public Automaton(SortedSet<char> alphabet)
        {
            this.transitions = new HashSet<Transition<T>>();
            this.states = new SortedSet<T>();
            this.startStates = new SortedSet<T>();
            this.finalStates = new SortedSet<T>();
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
