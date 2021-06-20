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
            automaton.defineAsStartState("q2");
            automaton.defineAsFinalState("q5");

            automaton.printTransitions();

            Console.WriteLine(automaton.accept("bab"));

            automaton.generateGraph("../Graphs/NDFAPreTest.dot");

            NDFAConverter nDFAConverter = new NDFAConverter();

            DFA<string> automaton2 = nDFAConverter.createDFA(automaton);

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

            NDFAConverter nDFAConverter = new NDFAConverter();

            Console.WriteLine("Language? van " + expr4.ToString() + ":");
            expr4.printLanguage(expr4.getLanguage(4));

            NDFA<string> automaton = RegExConverter.CreateNDFA(expr4);

            automaton.generateGraph("../Graphs/NDFAFromRegExTest.dot");

            DFA<string> automaton2 = nDFAConverter.createDFA(automaton);

            automaton2.generateGraph("../Graphs/DFAfromNDFAtTest.dot");
        }


        public void minimalize()
        {
            NDFA<string> automaton = new NDFA<string>(2);

            automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            automaton.addTransition(new Transition<string>("q1", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q1", 'b', "q4"));
            Minimalization minimalization = new Minimalization();

            //automaton.addTransition(new Transition<string>("q0", 'a'));
            //automaton.addTransition(new Transition<string>("q0", 'b', "q1"));

            //automaton.addTransition(new Transition<string>("q1", 'a', "q2"));
            //automaton.addTransition(new Transition<string>("q1", 'b'));

            automaton.addTransition(new Transition<string>("q2", 'b', "q1"));
            automaton.addTransition(new Transition<string>("q2", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q2", "q3"));
            //automaton.addTransition(new Transition<string>("q2", 'a', "q0"));
            //automaton.addTransition(new Transition<string>("q2", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q3", 'a', "q3"));
            automaton.addTransition(new Transition<string>("q3", 'b', "q5"));
            automaton.addTransition(new Transition<string>("q3", "q4"));
            //automaton.addTransition(new Transition<string>("q3", 'a', "q4"));
            //automaton.addTransition(new Transition<string>("q3", 'b', "q1"));

            automaton.addTransition(new Transition<string>("q4", 'a', "q5"));
            //automaton.addTransition(new Transition<string>("q4", 'a', "q5"));
            //automaton.addTransition(new Transition<string>("q4", 'b', "q3"));

            automaton.addTransition(new Transition<string>("q5", 'a', "q4"));
            //automaton.addTransition(new Transition<string>("q5", 'a', "q0"));
            //automaton.addTransition(new Transition<string>("q5", 'b', "q3"));


            automaton.defineAsStartState("q1");
            automaton.defineAsStartState("q2");
            automaton.defineAsFinalState("q5");
            //automaton.defineAsStartState("q0");
            //automaton.defineAsFinalState("q2");
            //automaton.defineAsFinalState("q4");

            Minimalization minimalization = new Minimalization();
            NDFAConverter nDFAConverter = new NDFAConverter();
            automaton.addTransition(new Transition<string>("1", 'a', "2"));
            automaton.addTransition(new Transition<string>("1", 'b', "3"));


            automaton.addTransition(new Transition<string>("2", 'a', "6"));
            automaton.addTransition(new Transition<string>("2", 'b', "2"));

            automaton.addTransition(new Transition<string>("3", 'a', "4"));
            automaton.addTransition(new Transition<string>("3", 'b', "5"));

            automaton.addTransition(new Transition<string>("4", 'a', "2"));
            automaton.addTransition(new Transition<string>("4", 'b', "3"));

            automaton.addTransition(new Transition<string>("5", 'a', "8"));
            automaton.addTransition(new Transition<string>("5", 'b', "7"));

            automaton.addTransition(new Transition<string>("6", 'a', "7"));
            automaton.addTransition(new Transition<string>("6", 'b', "5"));

            automaton.addTransition(new Transition<string>("7", 'a', "5"));
            automaton.addTransition(new Transition<string>("7", 'b', "8"));

            automaton.addTransition(new Transition<string>("8", 'a', "9"));
            automaton.addTransition(new Transition<string>("8", 'b', "10"));

            automaton.addTransition(new Transition<string>("9", 'a', "9"));
            automaton.addTransition(new Transition<string>("9", 'b', "10"));

            automaton.addTransition(new Transition<string>("10", 'a', "10"));
            automaton.addTransition(new Transition<string>("10", 'b', "10"));

            automaton.defineAsStartState("1");

            automaton.defineAsFinalState("6");
            automaton.defineAsFinalState("8");
            automaton.defineAsFinalState("9");

            automaton.printTransitions();
            automaton.generateGraph("../Graphs/NDFApreReverseTest.dot");
            Console.WriteLine("minimalization...");

            var automaton2 = minimalization.reverseAutomaton(automaton);
            automaton2.printTransitions();
            Console.WriteLine("minimalization complete... Converting to DFA...");
            automaton2.generateGraph("../Graphs/ReverseTest.dot");

            var automaton3 = nDFAConverter.createDFA(automaton2);
            automaton3.printTransitions();
            Console.WriteLine("DFA complete");

            automaton3.generateGraph("../Graphs/NormalizeTest.dot");
        }


    }
}
