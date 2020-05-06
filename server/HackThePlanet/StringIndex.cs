namespace HackThePlanet
{
    using System.Collections.Generic;


    public class StringIndex
    {
        private List<string> index = new List<string>();

        
        
        public string this[int index]
        {
            get
            {
                return this.index.Count > index 
                           ? this.index[index] 
                           : null;
            }
            set { this.index[index] = value; }
        }
        
        public int Add(string value)
        {
            if (this.index.Contains(value))
                return this.index.IndexOf(value);
            
            this.index.Add(value);
            return this.index.Count - 1;
        }
    }
}