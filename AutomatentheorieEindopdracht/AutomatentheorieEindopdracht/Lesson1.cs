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
            DFA<string> automaton = new DFA<string>(2);

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

            Console.WriteLine(automaton.accept("bbbb"));
            
            Console.WriteLine("End Lesson 1a");
        }

        public void Lesson1b()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q0", 'a', "q1"));
            automaton.addTransition(new Transition<string>("q0", 'a'));
            automaton.addTransition(new Transition<string>("q0", 'b'));
            automaton.addTransition(new Transition<string>("q0", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q1", 'b', "q2"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a'));
            automaton.addTransition(new Transition<string>("q4", 'b'));

            automaton.defineAsStartState("q0");
            automaton.defineAsFinalState("q4");

            automaton.printTransitions();

            Console.WriteLine(automaton.accept("bbbb"));

            Console.WriteLine("End Lesson 1b");
        }

    }
}
