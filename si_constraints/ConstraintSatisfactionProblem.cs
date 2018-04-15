using System;
using System.Diagnostics;

namespace si_constraints
{
    public abstract class ConstraintSatisfactionProblem<T>
    {
        public ConstraintSatisfactionProblem(int problemSize)
        {
            ProblemSize = problemSize;
            stopwatch = Stopwatch.StartNew();
        }

        //public T[,] Variables { get; set; }
        public T[] AvailableValues { get; set; }
        public abstract bool IsLegal(T[,] vars);
        public int MetaNodesVisited = 0;
        public int MetaSolutionsFound = 0;
        public double MetaGetExecutionTime() { return (stopwatch.Elapsed - MetaTimeStarted).TotalMilliseconds; }
        protected TimeSpan MetaTimeStarted;
        public abstract void StartBT();
        public abstract void StartFC();



        protected abstract T[,] ForwardCheck(T[,] vars, T[] availableValues, int startFrom);
        protected abstract T[,] BackTrack(T[,] vars, int startFrom);
        protected Stopwatch stopwatch;


        public int ProblemSize { get; protected set; }

        public void SolutionFound(T[,] solution)
        {
            MetaSolutionsFound++;
            if(MetaSolutionsFound == 1)
            {
                Console.WriteLine("First solution found in " + MetaGetExecutionTime() + " ms");
                foreach(T t in solution)
                {
                    Console.Write(t + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}