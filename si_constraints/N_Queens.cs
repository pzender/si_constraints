using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_constraints
{
    class N_Queens : ConstraintSatisfactionProblem<int>
    {
        public N_Queens(int problemSize) : base(problemSize)
        {
            AvailableValues = Enumerable.Range(1, problemSize).ToArray();

        }
        public override bool IsLegal(int[,] vars)
        {

            for (int i = 1; i <= ProblemSize; i++)
            {
                for(int j = 1; j <= ProblemSize; j++)
                {
                    if (vars[i-1, 0] != 0 && vars[j - 1, 0] != 0 && i != j)
                    {
                        if (vars[j - 1, 0] == vars[i - 1, 0])
                            return false;
                        else if (vars[j - 1, 0] == -j + i + vars[i - 1, 0])
                            return false;
                        else if (vars[j - 1, 0] == j - i + vars[i - 1, 0])
                            return false;
                    }
                    
                }
            }
            return true;
        }

        public override void StartBT()
        {
            
            int[,] initial = new int[ProblemSize, 1];
            for (int i = 0; i < ProblemSize; i++)
                initial[i, 0] = 0;
            MetaNodesVisited = 0;
            MetaSolutionsFound = 0;
            MetaTimeStarted = stopwatch.Elapsed;

            BackTrack(initial, 0);

            Console.WriteLine("Solutions found: " + MetaSolutionsFound);
            Console.WriteLine("Nodes visited: " + MetaNodesVisited);
            Console.WriteLine("Exec time: " + MetaGetExecutionTime());

        }

        public override void StartFC()
        {
            int[,] initial = new int[ProblemSize, 1];
            for (int i = 0; i < ProblemSize; i++)
                initial[i, 0] = 0;
            MetaNodesVisited = 0;
            MetaSolutionsFound = 0;
            MetaTimeStarted = stopwatch.Elapsed;

            ForwardCheck(initial,AvailableValues, 0);

            Console.WriteLine("Solutions found: " + MetaSolutionsFound);
            Console.WriteLine("Nodes visited: " + MetaNodesVisited);
            Console.WriteLine("Exec time: " + MetaGetExecutionTime());

        }

        protected override int[,] BackTrack(int[,] variables, int startFrom)
        {
            MetaNodesVisited++;
            if (IsLegal(variables))
            {
                if(startFrom < ProblemSize)
                {
                    foreach (int v in AvailableValues) {
                        int[,] new_variables = new int[ProblemSize, 1];
                        for (int i = 0; i < startFrom; i++)
                        {
                            new_variables[i, 0] = variables[i, 0];
                        }
                        new_variables[startFrom, 0] = v;
                        variables = BackTrack(new_variables, startFrom + 1);
                        
                    }
                }
                else //mamy rozwiązanie!
                {
                    SolutionFound(variables);
                }
            }
            return variables;
            //throw new NotImplementedException();
        }

        protected override int[,] ForwardCheck(int[,] variables, int[] availableValues, int startFrom)
        {
            MetaNodesVisited++;
            if(startFrom < ProblemSize)
            {
                foreach (int v in availableValues)
                {
                    int[,] new_variables = new int[ProblemSize, 1];
                    for(int i = 0; i < startFrom; i++)
                    {
                        new_variables[i, 0] = variables[i, 0];
                    }
                    new_variables[startFrom, 0] = v;
                    int[] new_available = availableValues.Except(new int[]{v}).ToArray();
                    if(IsLegal(new_variables))
                        variables = ForwardCheck(new_variables, new_available, startFrom + 1);
                }
            }
            else
            {
                SolutionFound(variables);
            }
            return variables;
            throw new NotImplementedException();
        }
    }
}
