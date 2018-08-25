using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrozzleApplication
{
    class CrozzleFileItem
    {
        #region constants - symbols
        const String NoCrozzleItem = "NO_CROZZLE_ITEM";

        // Replace 1
        const String ConfigurationFileSymbol = "CONFIG-DATA";
        const String WordListFileSymbol = "SEQUENCE-DATA";

        // Replace 2
        const String SizeSymbol = "SIZE";

        const String SequenceSymbol = "SEQUENCE";
        const String ColonSymbol = ":";
        const String AtoZ = @"^[A-Z]$";
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region properties
        private String OriginalItem { get; set; }
        public Boolean Valid { get; set; } = false;
        public String Name { get; set; }
        public KeyValue KeyValue { get; set; }
        #endregion

        #region properties - testing the type of the crozzle file item
        public Boolean IsConfigurationFile
        {
            get { return (Regex.IsMatch(Name, @"^" + ConfigurationFileSymbol + @"$")); }
        }

        public Boolean IsWordListFile
        {
            get { return (Regex.IsMatch(Name, @"^" + WordListFileSymbol + @"$")); }
        }

        public Boolean IsSize
        {
            get { return (Regex.IsMatch(Name, @"^" + SizeSymbol + @"$")); }
        }

        public Boolean IsSequence
        {
            get { return (Regex.IsMatch(Name, @"^" + SequenceSymbol + @"$")); }
        }
        
        #endregion

        #region constructors
        public CrozzleFileItem(String originalItemData)
        {
            OriginalItem = originalItemData;
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String crozzleFileItem, out CrozzleFileItem aCrozzleFileItem)
        {
            Errors = new List<String>();
            aCrozzleFileItem = new CrozzleFileItem(crozzleFileItem);

            // Discard comment.
            if (crozzleFileItem.Contains("//"))
            {
                int index = crozzleFileItem.IndexOf("//");
                crozzleFileItem = crozzleFileItem.Remove(index);
                crozzleFileItem = crozzleFileItem.Trim();
            }

            // Discard empty line
            if (Regex.IsMatch(crozzleFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aCrozzleFileItem.Name = NoCrozzleItem;
            }
            
            // Trim non-empty, non-comment line
            else
            {
                crozzleFileItem = crozzleFileItem.Trim();
            }
            
            if (Regex.IsMatch(crozzleFileItem, @"^" + ConfigurationFileSymbol + @".*"))
            {
                // Get the CONFIGURATION_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ConfigurationFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ConfigurationFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + WordListFileSymbol + @".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, WordListFileSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = WordListFileSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }

            // Size
            else if (Regex.IsMatch(crozzleFileItem, @"^" + SizeSymbol + @".*"))
            {
                // Get the number of rows for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, SizeSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = SizeSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }

            // Row and column
            else if (Regex.IsMatch(crozzleFileItem, @"^" + SequenceSymbol + @".*"))
            {
                // Get data for a horizontal word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, SequenceSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = SequenceSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            // Error
            else
                Errors.Add(String.Format(CrozzleFileItemErrors.SymbolError, crozzleFileItem));

            aCrozzleFileItem.Valid = Errors.Count == 0;
            return (aCrozzleFileItem.Valid);
        }
        #endregion
    }
}