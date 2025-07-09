using System;
using System.Threading.Tasks;

namespace MySortLib
{
    public static class OddEvenMergeSort
    {
        public static float[][] NSort(float[][] unsortedMatrix)
        {
            //Get the size of the matrix
            int matrixSize = unsortedMatrix.Length;
            //Base Case
            if (matrixSize == 1)
            {
                return unsortedMatrix;
            }

            float[][] leftMatrix = new float[matrixSize / 2][];
            float[][] rightMatrix = new float[matrixSize / 2][];
            for (int i = 0; i < matrixSize / 2; i++)
            {
                leftMatrix[i] = unsortedMatrix[i];
                rightMatrix[i] = unsortedMatrix[matrixSize / 2 + i];
            }
            //Sort the left and right halves
            leftMatrix = NSort(leftMatrix);
            rightMatrix = NSort(rightMatrix);

            for (int i = 0; i < matrixSize / 2; i++)
            {
                unsortedMatrix[i] = leftMatrix[i];
                unsortedMatrix[matrixSize / 2 + i] = rightMatrix[i];
            }
            //Sort the matrix using O-E Merge Sort
            float[][] sortedMatrix = OEMergeSort(unsortedMatrix);
            return sortedMatrix;
        }

        private static float[][] OEMergeSort(float[][] unsortedMatrix)
        {
            //Get the size of the matrix
            int matrixSize = unsortedMatrix.Length;

            //Base Case
            if (matrixSize == 1)
            {
                return unsortedMatrix;
            }

            //Divide the matrix into two halves
            float[][] leftMatrix = new float[matrixSize / 2][];
            float[][] rightMatrix = new float[matrixSize / 2][];

            for (int i = 0; i < matrixSize / 2; i++)
            {
                leftMatrix[i] = unsortedMatrix[2 * i];
                rightMatrix[i] = unsortedMatrix[2 * i + 1];
            }

            //(N/2) O-E Merge Sort Sort
            leftMatrix = OEMergeSort(leftMatrix);
            rightMatrix = OEMergeSort(rightMatrix);

            //N O-E Merge Sort
            float[][] sortedMatrix = new float[matrixSize][];
            sortedMatrix = ComparatorStep(leftMatrix, rightMatrix);
            return sortedMatrix;
        }

        private static float[][] ComparatorStep(float[][] leftMatrix, float[][] rightMatrix)
        {
            int halfMatrixSize = leftMatrix.Length;
            float[][] sortedMatrix = new float[halfMatrixSize * 2][];

            //Base Case
            if (halfMatrixSize == 1)
            {

                if (leftMatrix[0][0] < rightMatrix[0][0])
                {
                    sortedMatrix[0] = leftMatrix[0];
                    sortedMatrix[1] = rightMatrix[0];
                }
                else if (leftMatrix[0][0] > rightMatrix[0][0])
                {
                    sortedMatrix[0] = rightMatrix[0];
                    sortedMatrix[1] = leftMatrix[0];
                }
                else
                {
                    if (leftMatrix[0][1] > rightMatrix[0][1])
                    {
                        sortedMatrix[0] = leftMatrix[0];
                        sortedMatrix[1] = rightMatrix[0];
                    }
                    else
                    {
                        sortedMatrix[0] = rightMatrix[0];
                        sortedMatrix[1] = leftMatrix[0];
                    }
                }
                return sortedMatrix;
            }

            //Exeption
            sortedMatrix[0] = leftMatrix[0];
            sortedMatrix[halfMatrixSize * 2 - 1] = rightMatrix[halfMatrixSize - 1];

            //Merge the two halves
            for (int i = 0; i < halfMatrixSize - 1; i++)
            {
                if (leftMatrix[i + 1][0] < rightMatrix[i][0])
                {
                    sortedMatrix[i * 2 + 1] = leftMatrix[i + 1];
                    sortedMatrix[i * 2 + 2] = rightMatrix[i];
                }
                else if (leftMatrix[i + 1][0] > rightMatrix[i][0])
                {
                    sortedMatrix[i * 2 + 1] = rightMatrix[i];
                    sortedMatrix[i * 2 + 2] = leftMatrix[i + 1];
                }
                else
                {
                    if (leftMatrix[i + 1][1] > rightMatrix[i][1])
                    {
                        sortedMatrix[i * 2 + 1] = leftMatrix[i + 1];
                        sortedMatrix[i * 2 + 2] = rightMatrix[i];
                    }
                    else
                    {
                        sortedMatrix[i * 2 + 1] = rightMatrix[i];
                        sortedMatrix[i * 2 + 2] = leftMatrix[i + 1];
                    }
                }
            }
            return sortedMatrix;
        }
    }
}