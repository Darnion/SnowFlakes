namespace SnowFlakes
{
    internal class SnowFlake
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Size { get; set; }

        public SnowFlake(int left, int top, int size)
        {
            Left = left;
            Top = top;
            Size = size;
        }
    }
}
