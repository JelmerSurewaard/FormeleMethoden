using AutomatentheorieEindopdracht.Logic;
using System;
using System.Collections.Generic;

namespace AutomatentheorieEindopdracht
{
    class Lesson1
    {
        public Lesson1()
        {

        }

        public void lesson1a()
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

            automaton.generateGraph("../Graphs/DFATest.dot");
            
            Console.WriteLine("End Lesson 1a");
        }

        public void lesson1b()
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

            Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("../Graphs/NDFATest.dot");

            DFA<string> automaton2 = automaton.toDFA();

            automaton2.generateGraph("../Graphs/DFATest.dot");

            Console.WriteLine("End Lesson 1b");
        }

        public void lesson2a()
        {
            var a = new RegEx("a");
            var b = new RegEx("b");

            // expr1: "baa"
            var expr1 = new RegEx("baa");
            // expr2: "bb"
            var expr2 = new RegEx("bb");
            // expr3: "baa | baa"
            var expr3 = expr1.or(expr2);

            // all: "(a|b)*"
            var all = (a.or(b)).star();

            // expr4: "(baa | baa)+"
            var expr4 = expr3.plus();
            // expr5: "(baa | baa)+ (a|b)*"
            var expr5 = expr4.dot(all);

            //Console.WriteLine("taal van (baa):\n" + expr1.languageToString(expr1.getLanguage(5)));
            //Console.WriteLine("taal van (bb):\n" + expr2.languageToString(expr2.getLanguage(5)));
            //Console.WriteLine("taal van (baa | bb):\n" + expr3.languageToString(expr3.getLanguage(5)));

            Console.WriteLine("Language? van (a|b)*:");
            all.printLanguage(all.getLanguage(4));
            Console.WriteLine(all.ToString());

            NDFA<string> automaton = RegExConverter.CreateNDFA(all);

            automaton.printTransitions();

            automaton.generateGraph("../Graphs/NDFAFromRegExTest.dot");

            //Console.WriteLine("taal van (baa | bb)+:\n" + expr4.languageToString(expr4.getLanguage(5)));
            //Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n" + expr5.languageToString(expr5.getLanguage(6)));
        }


    }
}
