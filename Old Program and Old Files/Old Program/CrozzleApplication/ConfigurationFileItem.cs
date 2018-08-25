using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrozzleApplication
{
    class ConfigurationFileItem
    {
        #region constants - symbols
        const String NoConfigItem = "NO_CONFIG_ITEM";
        const String LogfileName = "DEFAULT";

        const String MinimumEquals = "MINIMUM=";
        const String Minimum = "MINIMUM";

        const String MaximumEquals = "MAXIMUM=";
        const String Maximum = "MAXIMUM";
        

        const String MinimumNumberOfUniqueWords = "MINIMUM_NUMBER_OF_UNIQUE_WORDS";
        const String MaximumNumberOfUniqueWords = "MAXIMUM_NUMBER_OF_UNIQUE_WORDS";

        const String InvalidCrozzleScore = "INVALID-CROZZLE-SCORE";
        const String Uppercase = "UPPERCASE";
        const String Style = "STYLE";
        const String BGcolourEmptyTD = "BGCOLOUR-EMPTY-TD";
        const String BGcolourNonEmptyTD = "BGCOLOUR-NON-EMPTY-TD";

        const String MinimumNumberOfRows = "MINIMUM-ROWS";
        const String MaximumNumberOfRows = "MAXIMUM-ROWS";
        const String MinimumNumberOfColumns = "MINIMUM-COLUMNS";
        const String MaximumNumberOfColumns = "MAXIMUM-COLUMNS";

        // New min max vertical horozontal start
        const String MinimumHorizontal = "MINIMUM-HORIZONTAL";
        const String MinimumVertical = "MINIMUM-VERTICAL";
        const String MaximumHorizontal = "MAXIMUM-HORIZONTAL";
        const String MaximumVertical = "MAXIMUM-VERTICAL";
        // New min max vertical horozontal end

        // Old min max vertical horozontal start
        const String MinimumHorizontalWords = "MINIMUM_HORIZONTAL_WORDS";
        const String MinimumVerticalWords = "MINIMUM_VERTICAL_WORDS";
        const String MaximumHorizontalWords = "MAXIMUM_HORIZONTAL_WORDS";
        const String MaximumVerticalWords = "MAXIMUM_VERTICAL_WORDS";

        const String MinimumIntersectionsInHorizontalWord = "MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORD";
        const String MaximumIntersectionsInHorizontalWord = "MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORD";
        const String MinimumIntersectionsInVerticalWord = "MINIMUM_INTERSECTIONS_IN_VERTICAL_WORD";
        const String MaximumIntersectionsInVerticalWord = "MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORD";
        // Old min max vertical horozontal end

        const String MinimumNumberOfTheSameWord = "MINIMUM_NUMBER_OF_THE_SAME_WORD";
        const String MaximumNumberOfTheSameWord = "MAXIMUM_NUMBER_OF_THE_SAME_WORD";

        const String MinimumNumberOfGroups = "MINIMUM_NUMBER_OF_GROUPS";
        const String MaximumNumberOfGroups = "MAXIMUM_NUMBER_OF_GROUPS";

        const String PointsPerWord = "POINTS_PER_WORD";

        const String IntersectingPointsPerLetter = "INTERSECTING_POINTS_PER_LETTER";
        const String NonIntersectingPointsPerLetter = "NON_INTERSECTING_POINTS_PER_LETTER";

        const String ColonSymbol = ":";
        const String AtoZ = @"^[A-Z]$"; 
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region properties
        public String OriginalItem { get; set; }
        public Boolean Valid { get; set; }
        public String Name { get; set; }
        public KeyValue KeyValue { get; set; }
        #endregion

        #region properties - testing the type of the configuration item
        public Boolean IsLogFile
        {
            get { return (Regex.IsMatch(Name, @"^" + LogfileName + @"$")); }
        }

        public Boolean IsMinimum
        {
            get { return OriginalItem.StartsWith(MinimumEquals); }
        }

        public Boolean IsMaximum
        {
            get { return OriginalItem.StartsWith(MaximumEquals); }
        }

        public Boolean IsInvalidCrozzleScore
        {
            get { return (Regex.IsMatch(Name, @"^" + InvalidCrozzleScore + @"$")); }
        }

        public Boolean IsUppercase
        {
            get { return (Regex.IsMatch(Name, @"^" + Uppercase + @"$")); }
        }

        public Boolean IsStyle
        {
            get { return (Regex.IsMatch(Name, @"^" + Style + @"$")); }
        }

        public Boolean IsBGcolourEmptyTD
        {
            get { return (Regex.IsMatch(Name, @"^" + BGcolourEmptyTD + @"$")); }
        }

        public Boolean IsBGcolourNonEmptyTD
        {
            get { return (Regex.IsMatch(Name, @"^" + BGcolourNonEmptyTD + @"$")); }
        }

        public Boolean IsMinimumNumberOfRows
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfRows + @"$")); }
        }

        public Boolean IsMaximumNumberOfRows
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfRows + @"$")); }
        }

        public Boolean IsMinimumNumberOfColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfColumns + @"$")); }
        }

        public Boolean IsMaximumNumberOfColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfColumns + @"$")); }
        }

        // New min max horizontal vertical start
        public Boolean IsMinimumHorizontal
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumHorizontal + @"$")); }
        }

        public Boolean IsMaximumHorizontal
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumHorizontal + @"$")); }
        }

        public Boolean IsMinimumVertical
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumVertical + @"$")); }
        }

        public Boolean IsMaximumVertical
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumVertical + @"$")); }
        }
        // New min max horizontal vertical end

        // Old min max horizontal vertical start
        public Boolean IsMinimumHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumHorizontalWords + @"$")); }
        }

        public Boolean IsMaximumHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumHorizontalWords + @"$")); }
        }

        public Boolean IsMinimumVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumVerticalWords + @"$")); }
        }

        public Boolean IsMaximumVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumVerticalWords + @"$")); }
        }

        public Boolean IsMinimumIntersectionsInHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumIntersectionsInHorizontalWord + @"$")); }
        }

        public Boolean IsMaximumIntersectionsInHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumIntersectionsInHorizontalWord + @"$")); }
        }

        public Boolean IsMinimumIntersectionsInVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumIntersectionsInVerticalWord + @"$")); }
        }

        public Boolean IsMaximumIntersectionsInVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumIntersectionsInVerticalWord + @"$")); }
        }
        // Old min max horizontal vertical end

        public Boolean IsMinimumNumberOfTheSameWord
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfTheSameWord + @"$")); }
        }

        public Boolean IsMaximumNumberOfTheSameWord
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfTheSameWord + @"$")); }
        }

        public Boolean IsMinimumNumberOfGroups
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfGroups + @"$")); }
        }

        public Boolean IsMaximumNumberOfGroups
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfGroups + @"$")); }
        }

        public Boolean IsPointsPerWord
        {
            get { return (Regex.IsMatch(Name, @"^" + PointsPerWord + @"$")); }
        }

        public Boolean IsIntersecting
        {
            get { return (Regex.IsMatch(Name, @"^" + IntersectingPointsPerLetter + @"$")); }
        }

        public Boolean IsNonIntersecting
        {
            get { return (Regex.IsMatch(Name, @"^" + NonIntersectingPointsPerLetter + @"$")); }
        }
        #endregion      

        #region constructors
        public ConfigurationFileItem(String originalItemData)
        {
            OriginalItem = originalItemData;
            Valid = false;
            Name = null;
            KeyValue = null;
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String configurationFileItem, out ConfigurationFileItem aConfigurationFileItem)
        {
            configurationFileItem = configurationFileItem.Trim();

            Errors = new List<String>();
            configurationFileItem = configurationFileItem.Trim();
            aConfigurationFileItem = new ConfigurationFileItem(configurationFileItem);

            // Discard comment.
            if (configurationFileItem.Contains("//"))
            {
                int index = configurationFileItem.IndexOf("//");
                configurationFileItem = configurationFileItem.Remove(index);
                configurationFileItem = configurationFileItem.Trim();
            }

            if (Regex.IsMatch(configurationFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aConfigurationFileItem.Name = NoConfigItem;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + LogfileName + @".*"))
            {
                // Get the LogFile key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, LogfileName, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = LogfileName;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + InvalidCrozzleScore + @".*"))
            {
                // Get the CrozzleInvalidScoreValue key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, InvalidCrozzleScore, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = InvalidCrozzleScore;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + Uppercase + @".*"))
            {
                // Get the CrozzleUppercase key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Uppercase, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Uppercase;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + Style + @".*"))
            {
                // Get the CrozzleHtmlStyle key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Style, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Style;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + BGcolourEmptyTD + @".*"))
            {
                // Get the CrozzleHtmlTdBgcolourSpace key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, BGcolourEmptyTD, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = BGcolourEmptyTD;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + BGcolourNonEmptyTD + @".*"))
            {
                // Get the CrozzleHtmlTdBgcolourNoSpace key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, BGcolourNonEmptyTD, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = BGcolourNonEmptyTD;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfRows + @".*"))
            {
                // Get the MinimumNumberOfRows key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfRows, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfRows;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfRows + @".*"))
            {
                // Get the MaximumNumberOfRows key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfRows, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfRows;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfColumns + @".*"))
            {
                // Get the MinimumNumberOfColumns key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfColumns, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfColumns;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfColumns + @".*"))
            {
                // Get the MaximumNumberOfColumns key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfColumns, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfColumns;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumHorizontal + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumHorizontal, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumHorizontal;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumVertical + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumVertical, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumVertical;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumHorizontal + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumHorizontal, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumHorizontal;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumVertical + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumVertical, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumVertical;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumHorizontalWords + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumHorizontalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumHorizontalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumVerticalWords + @".*"))
            {
                // Get the MinimumVerticalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumVerticalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumVerticalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumHorizontalWords + @".*"))
            {
                // Get the MaximumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumHorizontalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumHorizontalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumVerticalWords + @".*"))
            {
                // Get the MaximumVerticalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumVerticalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumVerticalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumIntersectionsInHorizontalWord + @".*"))
            {
                // Get the MinimumIntersectionsInHorizontalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumIntersectionsInHorizontalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumIntersectionsInHorizontalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumIntersectionsInHorizontalWord + @".*"))
            {
                // Get the MaximumIntersectionsInHorizontalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumIntersectionsInHorizontalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumIntersectionsInHorizontalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumIntersectionsInVerticalWord + @".*"))
            {
                // Get the MinimumIntersectionsInVerticalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumIntersectionsInVerticalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumIntersectionsInVerticalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumIntersectionsInVerticalWord + @".*"))
            {
                // Get the MaximumIntersectionsInVerticalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumIntersectionsInVerticalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumIntersectionsInVerticalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfTheSameWord + @".*"))
            {
                // Get the MinimumNumberOfTheSameWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfTheSameWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfTheSameWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfTheSameWord + @".*"))
            {
                // Get the MaximumNumberOfTheSameWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfTheSameWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfTheSameWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfGroups + @".*"))
            {
                // Get the MinimumNumberOfGroups key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfGroups, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfGroups;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfGroups + @".*"))
            {
                // Get the MaximumNumberOfGroups key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfGroups, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfGroups;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + PointsPerWord + @".*"))
            {
                // Get the PointsPerWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, PointsPerWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = PointsPerWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + IntersectingPointsPerLetter + @".*"))
            {
                // Get the key-value pair for the intersecting points.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, IntersectingPointsPerLetter, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = IntersectingPointsPerLetter;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + NonIntersectingPointsPerLetter + @".*"))
            {
                // Get the key-value pair for the non-intersecting points.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, NonIntersectingPointsPerLetter, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = NonIntersectingPointsPerLetter;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + Minimum + @".*"))
            {
                // Get the MinimumNumberOfUniqueWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Minimum, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Minimum;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else if (Regex.IsMatch(configurationFileItem, @"^" + Maximum + @".*"))
            {
                // Get the MinimumNumberOfUniqueWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Maximum, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Maximum;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }

            else
            {
                Errors.Add(String.Format(ConfigurationFileItemErrors.SymbolError, configurationFileItem));
            }

            aConfigurationFileItem.Valid = Errors.Count == 0;
            return (aConfigurationFileItem.Valid);
        }
        #endregion
    }
}