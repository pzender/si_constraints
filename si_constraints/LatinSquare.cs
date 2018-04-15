using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_constraints
{
    class LatinSquare : ConstraintSatisfactionProblem<int>
    {
        public LatinSquare(int problemSize) : base(problemSize)
        {
            AvailableValues = Enumerable.Range(1, problemSize).ToArray();
        }

        public override bool IsLegal(int[,] vars)
        {
            for(int i = 0; i<ProblemSize; i++)
            {
                for(int j = 0; j<ProblemSize; j++)
                {
                    for(int k = 0; k <ProblemSize; k++)
                    {
                        if (vars[i, j] != 0)
                        {
                            if (i != k && vars[k, j] == vars[i, j])
                                return false;
                            if (j != k && vars[i, k] == vars[i, j])
                                return false;
                        }
                    }
                }
            }
            return true;
            throw new NotImplementedException();
        }

        public override void StartBT()
        {
            int[,] initial = new int[ProblemSize, ProblemSize];
            for (int i = 0; i < ProblemSize; i++)
                for(int j = 0; j < ProblemSize; j++)
                    initial[i, j] = 0;


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
            int[,] initial = new int[ProblemSize, ProblemSize];
            for (int i = 0; i < ProblemSize; i++)
                for (int j = 0; j < ProblemSize; j++)
                    initial[i, j] = 0;


            MetaNodesVisited = 0;
            MetaSolutionsFound = 0;
            MetaTimeStarted = stopwatch.Elapsed;

            ForwardCheck(initial, AvailableValues, 0);

            Console.WriteLine("Solutions found: " + MetaSolutionsFound);
            Console.WriteLine("Nodes visited: " + MetaNodesVisited);
            Console.WriteLine("Exec time: " + MetaGetExecutionTime());


        }

        protected override int[,] BackTrack(int[,] vars, int startFrom)
        {
            MetaNodesVisited++;
            if (IsLegal(vars))
            {
                if (startFrom < ProblemSize * ProblemSize)
                {
                    foreach (int v in AvailableValues)
                    {
                        int[,] new_variables = new int[ProblemSize, ProblemSize];
                        for (int i = 0; i < startFrom; i++)
                        {
                            new_variables[i / ProblemSize, i % ProblemSize] = vars[i / ProblemSize, i % ProblemSize];
                        }
                        new_variables[startFrom / ProblemSize, startFrom % ProblemSize] = v;
                        vars = BackTrack(new_variables, startFrom + 1);
                    }

                }
                else SolutionFound(vars);

            }
            return vars;
            throw new NotImplementedException();
        }

        protected override int[,] ForwardCheck(int[,] vars, int[] availableValues, int startFrom)
        {
            MetaNodesVisited++;
            if (availableValues.Length == 0)
                availableValues = AvailableValues;
            if (startFrom < ProblemSize*ProblemSize)
            {
                foreach (int v in availableValues)
                {
                    int[,] new_variables = new int[ProblemSize,ProblemSize];
                    for (int i = 0; i < startFrom; i++)
                    {
                        new_variables[i / ProblemSize, i % ProblemSize] = vars[i / ProblemSize, i % ProblemSize];
                    }
                    new_variables[startFrom / ProblemSize, startFrom % ProblemSize] = v;
                    int[] new_available = availableValues.Except(new int[] { v }).ToArray();
                    if (IsLegal(new_variables))
                        ForwardCheck(new_variables, new_available, startFrom + 1);
                }
            }
            else
            {
                SolutionFound(vars);
            }
            return vars;
        }
    }
}
