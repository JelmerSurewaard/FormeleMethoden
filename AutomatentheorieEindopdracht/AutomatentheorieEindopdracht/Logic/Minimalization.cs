using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatentheorieEindopdracht.Logic
{

    class Minimalization
    {
        private List<char> partitionName;
        private DFA<string> dfa;
        private bool lastItiration = true;

        public Minimalization()
        {
        }

        public DFA<string> minimalize(DFA<string> dfa)
        {
            initStates();
            this.dfa = dfa;

            List<Partition> AB = initialPartition();
            List<Partition> FinalTable = AB;

            while ( AB != null )
            {
                FinalTable = AB;
                AB = partitionize(AB);

            }
            
            return convertToDFA(FinalTable);
        }

        private DFA<string> convertToDFA(List<Partition> final)
        {
            DFA<string> automaton = new DFA<string>(2);

            foreach(Partition p in final)
            {
                automaton.addTransition(new Transition<string>(p.PartitionName.ToString(),'a', p.States.ElementAt(0)));
                automaton.addTransition(new Transition<string>(p.PartitionName.ToString(),'b', p.States.ElementAt(1)));

                if (this.dfa.finalStates.Contains(p.States.ElementAt(0))) { automaton.defineAsFinalState(p.States.ElementAt(0)); }
                if (this.dfa.finalStates.Contains(p.States.ElementAt(1))) { automaton.defineAsFinalState(p.States.ElementAt(1)); }

                if (this.dfa.startStates.Contains(p.States.ElementAt(0))) { automaton.defineAsStartState(p.States.ElementAt(0)); }
                if (this.dfa.startStates.Contains(p.States.ElementAt(1))) { automaton.defineAsStartState(p.States.ElementAt(1)); }

            }

            return automaton;
        }

        private List<Partition> partitionize(List<Partition> allPartitions)
        {
            List<Partition> newPartitions = new List<Partition>();
            
            if ( this.lastItiration ) 
            {
                foreach (Partition p in allPartitions)
                {
                    Dictionary<string, char[]> rows = new Dictionary<string, char[]>();

                    //create table rows
                    foreach (string state in p.States)
                    {
                        char[] tuple = new char[2];
                        var toStateA = this.dfa.GetToStates(state, 'a')[0];
                        var toStateB = this.dfa.GetToStates(state, 'b')[0];

                        tuple[0] = p.States.Contains(toStateA) ? p.PartitionName : getToPartition(toStateA, allPartitions);
                        tuple[1] = p.States.Contains(toStateB) ? p.PartitionName : getToPartition(toStateB, allPartitions);

                        rows.Add(state, tuple);
                    }

                    //check if partision is already most simplified
                    var test_val = rows.ElementAt(0).Value;
                    bool temp_bool = true;
                    foreach (char[] value in rows.Values)
                    {
                        if ( ! (test_val == value) )
                        {
                            temp_bool = false;
                            this.lastItiration = false; 
                        }
                    }
                    if (temp_bool) 
                    {
                        newPartitions.Add(p);
                    }
                    else
                    {
                       while(rows.Count != 0)
                        {
                            Partition tempP = new Partition();
                            
                                char[] transition = rows.ElementAt(0).Value; //A, A
                                tempP.ab = transition;

                                foreach(var row in rows)
                                {
                                    if (compareArrays(row.Value, transition))
                                    {
                                        tempP.States.Add(row.Key);
                                    }
                                }

                                foreach (var state in tempP.States)
                                {
                                    rows.Remove(state);
                                }

                            tempP.PartitionName = this.partitionName[0];
                            popStateNames();
                            newPartitions.Add(tempP);
                        }                               
                    }
                }
                return newPartitions;
            }

            return null;

        }

        private bool compareArrays(char[] a, char[] b)
        {
            if ( a[0] == b[0] && a[1] == b[1]) { return true; } else { return false; }
        }

        private SortedSet<string> convertListToSS(List<string> list)
        {
            SortedSet<string> temp = new SortedSet<string>();

            foreach(string s in list)
            {
                temp.Add(s);
            }

            return temp;
        }

        private char getToPartition(string state, List<Partition> partitions)
        {
            var ToPartition = '0';

            foreach(Partition p in partitions)
            {
                ToPartition = p.States.Contains(state) ? p.PartitionName : '0';
            }

            return ToPartition;
        }

        private List<Partition> initialPartition()
        {
            List<Partition> partitions = new List<Partition>();

            partitions.Add(new Partition(this.partitionName[0], mergeSortedSets(this.dfa.getBetweenStates(), this.dfa.startStates)));
            popStateNames();
            partitions.Add(new Partition(this.partitionName[0], this.dfa.finalStates));
            popStateNames();

            return partitions;
        }

        private SortedSet<string> mergeSortedSets(SortedSet<string> a, SortedSet<string> b)
        {
            SortedSet<string> c = a;
            foreach( string s in b)
            {
                c.Add(s);
            }

            return c;
        }

        private void initStates()
        {
            partitionName = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        }

        private void popStateNames()
        {
            if (this.partitionName.Count > 0)
            {
                this.partitionName.RemoveAt(0);
            } else
            {
                List<char> temp = new List<char> { '1', '2', '3', '4', '5', '6', '7' };
                this.partitionName.AddRange(temp);
            }
        }
    }

    class Partition
        {
            public char PartitionName { get; set; }
            public SortedSet<string> States { get; set; }

            public char[] ab { get; set; }

            public Partition() {
            this.States = new SortedSet<string>();
        }
            public Partition(char PartitionName, SortedSet<string> states)
                {
                    this.PartitionName = PartitionName;
                    this.States = states;
                 }
        }
}
