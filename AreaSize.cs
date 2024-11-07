namespace TjuvPolis
{
    public class AreaSize
    {
        /// <summary>
        /// det är peropety som beskriver get( readonly) x vilket är  längst till vänster för min är 0 från vinster.
        /// </summary>
        public int MinWidthX { get; }
        /// <summary>
        /// det är property som inehåller get ( readonly) Y vilket är längst uppe eftersom ligger till 00 från längst upp.
        /// </summary>
        public int MinHeightY { get; }
        /// <summary>
        /// det är propety som har get (readonly) X vilket är max till längst höger eftersom ligger max sista siffran i höger.
        /// </summary>
        public int MaxWidthX { get; }
        /// <summary>
        /// propety som har get (read only ) Y vilket är max till längst nere eftersom ligger max sista sifraan i ner.
        /// </summary>
        public int MaxHeightY { get; }

        public AreaSize(int MinWidthX, int MinHeightY, int MaxWidthX, int MaxHeightY)
     
        {
            //vi nere har kallt propety alltså själva storlekerna till Area size som vi
            // skapat där övan för de ska  inehhålla informerna . 
            this.MinWidthX = MinWidthX;
            this.MinHeightY = MinHeightY;
            this.MaxWidthX = MaxWidthX;
            this.MaxHeightY = MaxHeightY;
            
        }
    }
}