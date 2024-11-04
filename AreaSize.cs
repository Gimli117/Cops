namespace TjuvPolis
{
    public class AreaSize
    {
        public int MinWidthX { get; }
        public int MinHeightY { get; }
        public int MaxWidthX { get; }
        public int MaxHeightY { get; }

        public AreaSize(int MinWidthX, int MinHeightY, int MaxWidthX, int MaxHeightY)
        {
            this.MinWidthX = MinWidthX;
            this.MinHeightY = MinHeightY;
            this.MaxWidthX = MaxWidthX;
            this.MaxHeightY = MaxHeightY;
        }
    }
}