namespace HackThePlanet
{
    using System.Collections.Generic;
    

    /// <summary>
    /// Encapsulates a string value as a handful of bytes, using
    /// dictionaries to find and/or store values rather than storing literals.
    /// </summary>
    public struct StringReference
    {
        private static readonly Dictionary<byte, List<string>> _stringTables = 
            new Dictionary<byte, List<string>>()
                {
                    { 0, null }, // string.Empty or null
                    { 1, MiscWords },
                    { 2, MaleNames },
                    { 3, FemaleNames },
                    { 4, Surnames}
                };
        
        public static List<string> MiscWords = new List<string>();
        public static List<string> MaleNames = new List<string>();
        public static List<string> FemaleNames = new List<string>();
        public static List<string> Surnames = new List<string>();

        private byte stringTable; 
        private int[] stringIndexes;


        /// <summary>
        /// Create a new StringReference.
        /// References one or more indexes into a cached string table.
        /// </summary>
        /// <param name="stringTable">String table that indexes refer to.</param>
        /// <param name="stringIndexes">Indexes of string values that will be referenced.</param>
        public StringReference(byte stringTable, params int[] stringIndexes)
        {
            this.stringTable = stringTable;
            this.stringIndexes = stringIndexes;
        }

        
        /// <summary>
        /// Automatically convert a StringReference to a string value.
        /// Null/empty is returned for references to string table 0.
        /// </summary>
        /// <param name="stringReference">StringReference to convert.</param>
        /// <returns>A string value retrieved from cached string tables.</returns>
        public static implicit operator string(StringReference stringReference)
        {
            if (stringReference.stringTable == 0)
                return null;
            
            List<string> stringTable = _stringTables[stringReference.stringTable];

            string[] values = new string[stringReference.stringIndexes.Length];
            for (int index = 0; index < values.Length; index++)
            {
                values[index] = stringTable[index];
            }

            return string.Join(' ', values);
        }


        /// <summary>
        /// Automatically convert a string value to a StringReference.
        /// StringReferences made in this way always use the MiscWords table.
        /// </summary>
        /// <param name="value">String value to convert to a StringReference.</param>
        /// <returns>New StringReference to MiscWords string table.</returns>
        public static implicit operator StringReference(string value)
        {
            string[] words = value.Split(' ');
            
            // Determine string indexes for MiscWords table.
            // Each word in the string is a new index into the table.
            int[] indexes = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                int index = MiscWords.IndexOf(word);
                
                // Word doesn't exist in MiscWords table, make a new entry.
                if (index < 0)
                {
                    MiscWords.Add(word);
                    index = MiscWords.Count - 1;
                }

                indexes[i] = index;
            }
            
            return new StringReference(1, indexes);
        }
    }
}