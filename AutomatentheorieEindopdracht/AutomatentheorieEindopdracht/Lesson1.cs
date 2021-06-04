using System;
using System.Collections.Generic;

namespace AutomatentheorieEindopdracht
{
    class Lesson1
    {
        public Lesson1()
        {

        }

        public void Lesson1a()
        {
            SortedSet<char> list = new SortedSet<char>();
            list.Add('a');
            list.Add('b');
            DFA<string> automaton = new DFA<string>(list);

            automaton.addTransition(new Transition<string>("q0", 'a', "q1"));
            automaton.addTransition(new Transition<string>("q0", 'b'));

            automaton.addTransition(new Transition<string>("q1", 'a'));
            automaton.addTransition(new Transition<string>("q1", 'b', "q2"));

            automaton.addTransition(new Transition<string>("q2", 'a', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a'));
            automaton.addTransition(new Transition<string>("q3", 'b'));

            automaton.defineAsStartState("q0");
            automaton.defineAsFinalState("q3");

            automaton.printTransitions();

            Console.WriteLine(automaton.accept("baab"));
            

            Console.WriteLine("End Lesson 1a");
        }

    }
}
