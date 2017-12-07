using System;

namespace SudokuValidator
{
    class MainClass
    {
        static string successMessage = "The input validated, this is a good sudoku";
        static string FailureMessage = "The input should validate, this is a bad sudoku";
		public static void Main(string[] args)
		{

			// Sudoku Puzzles test Inputs , From the file
			int[][] goodSudoku1 = {
				new int[] {7,8,4,  1,5,9,  3,2,6} ,
				new int[] {5,3,9,  6,7,2,  8,4,1} ,
				new int[] {6,1,2,  4,3,8,  7,5,9} ,

				new int[] {9,2,8,  7,1,5,  4,6,3} ,
				new int[] {3,5,7,  8,4,6,  1,9,2} ,
				new int[] {4,6,1,  9,2,3,  5,8,7} ,

				new int[] {8,7,6,  3,9,4,  2,1,5} ,
				new int[] {2,4,3,  5,6,1,  9,7,8} ,
				new int[] {1,9,5,  2,8,7,  6,3,4}
			};


			int[][] goodSudoku2 = {
				new int[] {1,4, 2,3} ,
				new int[] {3,2, 4,1} ,

				new int[] {4,1, 3,2} ,
				new int[] {2,3, 1,4}
			};

			int[][] badSudoku1 =  {
				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9} ,

				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9} ,

				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9} ,
				new int[] {1,2,3, 4,5,6, 7,8,9}
			};

			int[][] badSudoku2 = {
				new int[] {1,2,3,4,5} ,
				new int[] {1,2,3,4} ,
				new int[] {1,2,3,4} ,
				new int[] {1}
			};
			// End of Test inputs //


			
			Console.WriteLine(ValidateSudoku(goodSudoku1) + System.Environment.NewLine);
			
			Console.WriteLine(ValidateSudoku(goodSudoku2) + System.Environment.NewLine);
			
			Console.WriteLine(ValidateSudoku(badSudoku1) + System.Environment.NewLine);
			
			Console.WriteLine(ValidateSudoku(badSudoku2) + System.Environment.NewLine);


			Console.ReadLine();

		}
		// End of Main Function //

		// Utility Functions //

		// Validate Sudoku Method to check that whether the solution is filled correctly or not //
		public static string ValidateSudoku(int[][] grid)
		{

			// Check if grid is square;
			if (!validateGridSize(grid))
			{
                return FailureMessage;
			}

			// Get small square size
			int size = grid.Length;
			int smallSquareSize = (int)Math.Sqrt((double)size);

			// Make sure all the values are in the right range
			if (!validateInputs(grid))
			{
				return FailureMessage;
			}

			// Check that the rows contain no duplicate values
			for (int i = 0; i < size; ++i)
			{
				if (!CheckRow(grid, i))
				{
					return FailureMessage;
				}
			}

			// Check that the columns contain no duplicate values
            for (int j = 0; j < size; ++j)
			{
				if (!CheckColumn(grid, j))
				{
					return FailureMessage;
				}
			}

			// Check that the subsquares contain no duplicate values
			for (int baseRow = 0; baseRow < size; baseRow += smallSquareSize)
			{
				for (int baseCol = 0; baseCol < size; baseCol += smallSquareSize)
				{
					if (!CheckSquare(grid, baseRow, baseCol, smallSquareSize))
					{
						return FailureMessage;
					}
				}
			}

			// If all steps succeed, grid is valid
			return successMessage;
		}
		// End of Validate Sudoku Puzzle //


		// Check Whether the grid have NXN dimension and is a N is a perfect square //
		private static bool validateGridSize(int[][] puzzle)
		{
			//checking whether the grid is a perfect square or not
			if (Math.Sqrt((double)puzzle.Length) % 1 != 0)
			{
				return false;
			}

			// check that the each, row has exact same size as array size
			for (int i = 0; i < puzzle.Length; i++)
			{
				if (puzzle[i].Length != puzzle.Length)
				{
					return false;
				}
			}
			return true;
		}
		// End of validateGrid Method //


		// Validating that if the numbers are in the range that is from 1...N
		private static bool validateInputs(int[][] grid)
		{
			for (int i = 0; i < grid.Length; ++i)
			{
				for (int j = 0; j < grid.Length; ++j)
				{
					if (grid[i][j] > 1 && grid[i][j] < grid.Length)
					{
						return true;
					}
				}
			}
			return false;
		}
		// End of Validate Inputs //

		// Find that if any duplicate value in a row //
		private static bool CheckRow(int[][] grid, int curRow)
		{

			int[] arr = grid[curRow];

            if (!checkDuplicateVal((arr)))
            {

                return false;
            }
			// did not find duplicates, row is valid
			return true;
		}
		// End of Check Row//


		// Validate if any duplicate value in column //
		private static bool CheckColumn(int[][] grid, int curCol)
		{
            // coverting the two D array into column
            int width = grid.Length;
            int[] arr = new int[width];

            for (int i = 0; i < width;i++)
            {

                arr[i] = grid[i][curCol];
            }

            if (!checkDuplicateVal((arr)))
            {
                return false;
            }

			// did not find duplicates, column is valid
			return true;
		}
		// end of Check column


        // Check the duplicate value exists in an array or not
        private static bool checkDuplicateVal(int[] arr)
        {

			for (int i = 0; i < arr.Length; ++i)
			{
				for (int j = i + 1; j < arr.Length; ++j)
				{
					if (arr[i] == arr[j])
					{
						// duplicate value found
						return false;
					}
				}
			}
            // No Duplicate Value Founds
            return true;
        }

		// validate if any subsquare contain any duplicate value //
		private static bool CheckSquare(int[][] grid, int baseRow, int baseCol, int smallSquareSize)
		{
            
			bool[] found = new bool[grid.Length];

			for (int row = baseRow; row < (baseRow + smallSquareSize); ++row)
			{
				for (int col = baseCol; col < (baseCol + smallSquareSize); ++col)
				{
					int index = grid[row][col] - 1;

					if (!found[index])
					{
						found[index] = true;
					}
					else
					{
						return false;
					}
				}
			}
            // did not find any duplicates, small square is valid
            return true;
		}
		// End of check square //

	}
}
