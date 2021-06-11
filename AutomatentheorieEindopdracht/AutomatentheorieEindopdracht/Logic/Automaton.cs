using csdot;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Automaton(int n)
        {
            this.transitions = new HashSet<Transition<T>>();
            this.states = new SortedSet<T>();
            this.startStates = new SortedSet<T>();
            this.finalStates = new SortedSet<T>();
            this.alphabet = new SortedSet<char>();
            fillAlphabet(n);
        }

        protected void fillAlphabet(int n)
        {
            string stockAlphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < n; i++)
            {
                this.alphabet.Add(stockAlphabet.ElementAt(i));
            }
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
            // If already in states no problem because a SortedSet will remove duplicates.
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

        protected List<T> getNextStates(List<T> states, char c)
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

            return nextStates;
        }
        public void printTransitions()
        {
            foreach (Transition<T> transition in this.transitions)
            {
                Console.WriteLine(transition.toString());
            }
        }

        public void generateGraph(string output)
        {
            Graph graph = new Graph("id");

            graph.strict = false;
            graph.type = "digraph";

            foreach (var transition in transitions)
            {
                Edge tempEdge = new Edge();

                List<Transition> ts = new List<Transition>()
                    {
                        new Transition(transition.fromState.ToString(), "-> " + transition.toState.ToString() + " [\"label\"=\"" + transition.symbol + "\"];"),
                    };

                tempEdge.Transition = ts;
                graph.AddElement(tempEdge);
            }

            DotDocument doc = new DotDocument();
            doc.SaveToFile(graph, output);

            // load the DOT file
            var converter = new GroupDocs.Conversion.Converter(output);
            // set the convert options for PNG format
            var convertOptions = converter.GetPossibleConversions()["png"].ConvertOptions;
            // convert to PNG format
            string tempString = output.Substring(0, output.IndexOf(".dot"));
            converter.Convert(tempString + ".png", convertOptions);
        }
    }
}
