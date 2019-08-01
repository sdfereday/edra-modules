namespace RedPanda.Toolbox.Helpers
{
    public static class Arrays
    {
        public static int[,] RotateArrayClockwise(int[,] src)
        {
            int width;
            int height;
            int[,] dst;

            width = src.GetUpperBound(0) + 1;
            height = src.GetUpperBound(1) + 1;
            dst = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = width - (col + 1);
                    newCol = row;

                    dst[newCol, newRow] = src[col, row];
                }
            }

            return dst;
        }
    }
}