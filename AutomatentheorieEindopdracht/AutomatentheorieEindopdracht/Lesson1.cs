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

        public void dfa()
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
        }

        public void ndfa()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));
            

            automaton.defineAsStartState("q1");
            automaton.defineAsFinalState("q5");

            automaton.printTransitions();

            Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("../Graphs/NDFATest.dot");
        }

        public void ndfaToDfa()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));


            automaton.defineAsStartState("q1");
            automaton.defineAsFinalState("q5");

            automaton.printTransitions();

            Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("../Graphs/NDFAPreTest.dot");

            DFA<string> automaton2 = NDFAConverter.createDFA(automaton);

            automaton2.generateGraph("../Graphs/DFAPostTest.dot");
        }

        public void regEx()
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

            Console.WriteLine("Language? van " + all.ToString() + ":");
            all.printLanguage(all.getLanguage(4));

            NDFA<string> automaton = RegExConverter.CreateNDFA(all);

            automaton.printTransitions();

            automaton.generateGraph("../Graphs/NDFAFromRegExTest.dot");

            //Console.WriteLine("taal van (baa | bb)+:\n" + expr4.languageToString(expr4.getLanguage(5)));
            //Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n" + expr5.languageToString(expr5.getLanguage(6)));
        }

        public void reGexToDFA()
        {
            var a = new RegEx("a");
            var b = new RegEx("b");

            // expr1: "baa"
            var expr1 = new RegEx("baa");
            // expr2: "bb"
            var expr2 = new RegEx("bb");
            // expr3: "baa | bb"
            var expr3 = expr1.or(expr2);

            // all: "(a|b)*"
            var all = (a.or(b)).star();

            // expr4: "(baa | bb)+"
            var expr4 = expr3.plus();
            // expr5: "(baa | bb)+ (a|b)*"
            var expr5 = expr4.dot(all);

            

            Console.WriteLine("Language? van " + expr4.ToString() + ":");
            expr5.printLanguage(expr4.getLanguage(4));

            NDFA<string> automaton = RegExConverter.CreateNDFA(expr4);

            automaton.generateGraph("../Graphs/NDFAFromRegExTest.dot");

            DFA<string> automaton2 = NDFAConverter.createDFA(automaton);

            automaton2.generateGraph("../Graphs/DFAfromNDFAtTest.dot");
        }


        public void minimalize()
        {
            DFA<string> automaton = new DFA<string>(2);

            Minimalization minimalization = new Minimalization();

            automaton.addTransition(new Transition<string>("q0", 'a'));
            automaton.addTransition(new Transition<string>("q0", 'b', "q1"));

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'b'));

            automaton.addTransition(new Transition<string>("q2", 'a', "q0"));
            automaton.addTransition(new Transition<string>("q2", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q4"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q1"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));
            automaton.addTransition(new Transition<string>("q4", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q0"));
            automaton.addTransition(new Transition<string>("q5", 'b', "q3"));


            automaton.defineAsStartState("q0");
            automaton.defineAsFinalState("q2");
            automaton.defineAsFinalState("q4");


            automaton.printTransitions();

            Console.WriteLine("minimalization...");

            automaton = minimalization.minimalize(automaton);

            automaton.generateGraph("../Graphs/NormalizeTest.dot");
        }


    }
}
